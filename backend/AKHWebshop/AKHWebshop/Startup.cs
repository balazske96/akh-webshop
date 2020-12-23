using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AKHWebshop.Models.Auth;
using AKHWebshop.Models.Mail;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace AKHWebshop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAkhMailClient>(_ =>
            {
                string bandAddress = Configuration["Mail:BandMail"];
                string username = Configuration["Mail:Credentials:User"];
                string password = Configuration["Mail:Credentials:Password"];
                string host = Configuration["Mail:Host"];
                int port = Int32.Parse(Configuration["Mail:Port"]);
                bool ssl = Boolean.Parse(Configuration["Mail:Ssl"]);

                SmtpClient emailClient = new SmtpClient();
                emailClient.Host = host;
                emailClient.Port = port;
                emailClient.Credentials = new NetworkCredential(username, password);
                emailClient.EnableSsl = ssl;
                emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                emailClient.UseDefaultCredentials = false;

                ILogger<AkhMailClient> logger = new Logger<AkhMailClient>(new LoggerFactory());

                return new AkhMailClient(emailClient, logger, bandAddress);
            });

            services.AddScoped(options =>
            {
                JwtTokenHelperOptions jwtOptions = new JwtTokenHelperOptions()
                {
                    SecretKey = Configuration["Jwt:Secret"],
                    Audience = Configuration["Jwt:Audience"],
                    Issuer = Configuration["Jwt:Issuer"],
                };
                return new JwtTokenHelper(jwtOptions);
            });

            services.AddControllers()
                .AddJsonOptions(
                    options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); }
                );

            services.AddLogging();
            services.AddDbContextPool<ShopDataContext>(options =>
            {
                string connectionString = Configuration["Database:ConnectionString"];
                options.UseMySql(connectionString);
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ShopDataContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // ValidateIssuer = true,
                        // ValidateAudience = true,
                        // ValidAudience = Configuration["Jwt:ValidAudience"],
                        // ValidIssuer = Configuration["Jwt:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]))
                    };

                    // We specify where to look for the token 
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            var cookieToken = context.Request.Cookies["_uc"];
                            if (cookieToken == null)
                            {
                                /* if the token is not found in the cookie,
                                 we stick to the basic method */
                                return Task.CompletedTask;
                            }

                            context.Token = cookieToken;
                            return Task.CompletedTask;
                        },
                    };
                }
            );

            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("Allow credentials", builder => { builder.AllowCredentials(); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API"); });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}