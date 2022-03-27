using DomainEntities.Dto;
using FluentValidation.AspNetCore;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.Helpers;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using WebApp.ConfigureServices;

namespace WebApp
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
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectTemplate(.Net5)", Version = "v1" });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true; // consent required
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHttpContextAccessor();
            services.Dependenies();
            services.AddDistributedMemoryCache();
            services.AddSession(opts =>
            {
                opts.Cookie.IsEssential = true; // make the session cookie Essential
            });
            services.AddMemoryCache();
            services.AddCors(option => option.AddPolicy("CORSPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.FluentValidators();
            services.AddMvc().AddFluentValidation();

            // In production, the Vue files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task management system"));
            }
            else
            {
                app.UseHsts();
            }

            ContextAccessor.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.Use(async (context, next) =>
            {
                await next.Invoke();
            });

            app.UseCors("CORSPolicy");

            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>();
                    if (exception != null)
                    {
                        string message = exception.Error.InnerException != null ? exception.Error.InnerException.Message :
                            !string.IsNullOrEmpty(exception.Error.Message) ? exception.Error.Message : "Internal Server Error";
                        var error = new ReturnOption(message, false);

                        var errObj = JsonSerializer.Serialize(error);
                        await context.Response.WriteAsync(errObj).ConfigureAwait(false);
                    }
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp/";
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });

            if (env.IsDevelopment())
            {
                /*
                 * comment this if you're not using typescript
                 * Warning: if you using .js file and this enable you will loose those file located at the currentPath variable.
                 * Note: if vue app changes structure from current ts to js, please create a new vue using vue cli.
                 */
                string currentPath = $"{Directory.GetCurrentDirectory()}\\ClientApp\\src";
                string[] allfiles = Directory.GetFiles(currentPath, "*.*", SearchOption.AllDirectories);
                string[] fileExtToRemove = new string[] { ".js", ".map" };

                foreach (var file in allfiles)
                {
                    FileInfo fileInfo = new(file);
                    if (fileExtToRemove.Contains(fileInfo.Extension))
                    {
                        fileInfo.Delete();
                    }
                }
            }
        }
    }
}
