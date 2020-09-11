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
        public DB2Connection Connection { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(opt => opt.AddPolicy("AllowSpecificOrigin",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            /*if (Environment.IsDevelopment())
                services.AddDbContext<DatabaseContext>(
                    opt => opt.UseSqlite("Data source=tourofheroes.db"));

            if (Environment.IsProduction())
                services.AddDbContext<DatabaseContext>(
                    opt => opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection"))); */

            if (Environment.IsProduction())
                services.AddDbContext<DatabaseContext>(
                    opt => opt.UseDb2());

            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();

            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IHeroService, HeroService>();
            services.AddControllers();

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var service = app.ApplicationServices.CreateScope().ServiceProvider;
                var ctx = service.GetService<DatabaseContext>();
                ctx.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction())
            {
                var service = app.ApplicationServices.CreateScope().ServiceProvider;
                var ctx = service.GetService<DatabaseContext>();
                ctx.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }

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
