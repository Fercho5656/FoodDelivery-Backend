using System;
using System.Text;
using FoodDelivery_Backend.Context;
using FoodDelivery_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FoodDelivery_Backend {
    public class Startup {

        private readonly string _MyCors = "";
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlite(Configuration.GetConnectionString("dev")));

            // Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
                opt.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]))
                };
            });

            // Services
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICategoryFoodService, CategoryFoodService>();
            services.AddScoped<IFoodService, FoodService>();

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodDelivery_Backend", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                    new string[]{}
                    }
                });
            });

            services.AddCors(opt => {
                opt.AddPolicy(name: _MyCors, builder => {
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                    .AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodDelivery_Backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_MyCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            /* app.Use(async (context, next) => {
                var token = context.Session.GetString("Token");
                if (token != null) {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            }); */

        }
    }
}
