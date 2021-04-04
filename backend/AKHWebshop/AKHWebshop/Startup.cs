using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AKHWebshop.Models;
using AKHWebshop.Models.Auth;
using AKHWebshop.Models.Http.Request.DTO;
using AKHWebshop.Models.Http.Response;
using AKHWebshop.Models.Mail;
using AKHWebshop.Models.Shop.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            // Register simple services
            services.AddScoped<IAkhMailClient, AkhMailClient>();
            services.AddScoped<IRequestMapper, RequestMapper>();
            services.AddScoped<IModelMerger, ModelMerger>();
            services.AddScoped<IActionResultFactory<JsonResult>, JsonResultFactory>();

            // Register fluent validation
            services
                .AddMvc()
                .AddFluentValidation(
                    fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()
                );

            // Register loggin
            services.AddLogging();

            // Register database entrypoint
            services.AddDbContextPool<ShopDataContext>(options =>
            {
                string connectionString = Configuration["Database:ConnectionString"];
                options.UseMySql(connectionString);
            });

            // Register identity framework
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ShopDataContext>()
                .AddDefaultTokenProviders();


            // Auth and JWT Configuration
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

                    /* We specify where to look for the token */
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            var cookieToken = context.Request.Cookies[K.userAuthCookieName];
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

            // Swagger configuration 
            services.AddSwaggerGen();

            // CORS configration
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