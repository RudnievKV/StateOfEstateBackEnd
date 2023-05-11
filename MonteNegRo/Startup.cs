using Azure;
using Azure.Identity;
using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Cloud.Translation.V2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Services;
using MonteNegRo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MonteNegRo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment Environment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<MyDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:SqlServer"]));


            services.AddScoped<INeighborhoodService, NeighborhoodService>();
            services.AddScoped<IUserTypeService, UserTypeService>();
            services.AddScoped<ILocalService, LocalService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IBenefitService, BenefitService>();
            services.AddScoped<IAzureStorageService, AzureStorageService>();
            services.AddScoped<ITranslationService, TranslationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAdvertisementSettingService, AdvertisementSettingService>();
            services.AddScoped<ICounterpartyService, CounterpartyService>();
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IRealticAccountService, RealticAccountService>();



            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options =>
            {
                options.AddPolicy(CORSPolicies.StandartCORSPolicy, builder =>
                {
                    if (Environment.IsDevelopment())
                    {
                        builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("https://stateofestate.com", "http://localhost:4200")
                        .AllowCredentials(); ;
                    }
                    else
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("https://stateofestate.com")
                            .AllowCredentials(); ;
                    }
                });
            });

            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(), // HS256
                        ValidateIssuerSigningKey = true,
                    };
                });



            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MonteNegRo", Version = "v1" });
            });

            //services.AddResponseCompression(options =>
            //{
            //    options.Providers.Add<BrotliCompressionProvider>();
            //    options.Providers.Add<GzipCompressionProvider>();
            //    options.MimeTypes =
            //        ResponseCompressionDefaults.MimeTypes.Concat(
            //            new[] { "image/svg+xml" });
            //});
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MonteNegRo v1"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CORSPolicies.StandartCORSPolicy);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
