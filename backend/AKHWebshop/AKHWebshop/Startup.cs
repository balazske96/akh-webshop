using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AKHWebshop.Models.Mail;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


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
            services.AddScoped<IAkhMailClient>(options =>
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

                return new AkhMailClient(emailClient, bandAddress);
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddLogging();
            services.AddDbContextPool<ShopDataContext>(options =>
            {
                string connectionString = Configuration["Database:ConnectionString"];
                options.UseMySql(connectionString);
            });

            services.AddSwaggerGen();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}