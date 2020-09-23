using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heroes.Core.ApplicationService;
using Heroes.Core.ApplicationService.impl;
using Heroes.Core.DomainService;
using IBM.Data.DB2.Core;
using infrastructure.SQL;
using infrastructure.SQL.repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IBM.EntityFrameworkCore;
using IBM.EntityFrameworkCore.Infrastructure;
using Microsoft.OpenApi.Models;

namespace IBM.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(opt => opt.AddPolicy("AllowSpecificOrigin",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            if (Environment.IsDevelopment())
                services.AddDbContext<DatabaseContext>(
                    opt => opt.UseSqlite("Data source=tourofheroes.db"));

            /*if (Environment.IsProduction())
                services.AddDbContext<DatabaseContext>(
                    opt => opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection"))); */
            var db2Builder = new DB2ConnectionStringBuilder
            {
                Server = "dashdb-txn-sbox-yp-lon02-04.services.eu-gb.bluemix.net:50000",
                UserID = "ftd42305",
                Password = "9pgxwwkcw9g75c^s",
                Database = "BLUDB"
            };
            DB2Connection conn = new DB2Connection(db2Builder.ConnectionString);
            
            if (Environment.IsProduction())
                services.AddDbContext<DatabaseContext>(
                    opt => opt.UseDb2(conn, builder => builder.SetServerInfo(IBMDBServerType.AS400)));

            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();

            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IHeroService, HeroService>();
            services.AddControllers();

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Heroes API",
                    Version = "v1",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var service = app.ApplicationServices.CreateScope().ServiceProvider;
                var ctx = service.GetService<DatabaseContext>();
                ctx.Database.EnsureCreatedAsync();
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction())
            {
                var service = app.ApplicationServices.CreateScope().ServiceProvider;
                var ctx = service.GetService<DatabaseContext>();
                ctx.Database.EnsureCreatedAsync();
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
