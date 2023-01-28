using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebTokenWork
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = "http://localhost",//�reten
                    ValidAudience = "http://localhost",//kulanan
                    ValidateAudience = true,//�stekkilerin ge�erli olabilmesi i�in
                    ValidateIssuer = true,//�sttekilerin ge�erli olabilmesi i�in
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Hakan123.")), //�ifreleme i�lemi yap�ld�. simetrik olarak. �ifre=Hakan123.
                    ValidateIssuerSigningKey=true,    //�ifremi do�rula demi� olduk.
                    ValidateLifetime= true,//token� kontrol etsin,zaman� ge�mi� mi ge�memi� mi
                    ClockSkew=TimeSpan.Zero,//client ile server aras�nda gecikme s�resi. zero diyerek gecikme olmas�n demi� olduk


                };
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JsonWebTokenWork", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JsonWebTokenWork v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
