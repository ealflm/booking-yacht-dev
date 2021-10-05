using BookingYacht.API.Utilities.Response;
using BookingYacht.API.Utilities.Slugify;
using BookingYacht.API.Utilities.Swagger;
using BookingYacht.Business.Implement.Admin;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.Implement.Business;
using BookingYacht.Business.Interfaces.Business;
using BookingYacht.Data.Context;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Repositories;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookingYacht.API
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

            services.AddDbContext<BookingYachtContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("BookingYacht")));

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            services.AddControllers();

            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookingYacht.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });

                c.DocumentFilter<KebabCaseDocumentFilter>();
            });

            services.AddSingleton(FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(Configuration["Firebase:Admin"]),
                })
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IManageBusinessAccountService, ManageBusinessAccountService>();
            services.AddTransient<IPlaceTypeService, PlaceTypeService>();
            services.AddTransient<Business.Interfaces.Admin.ITicketTypeService, Business.Implement.Admin.TicketTypeService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IDestinationTourService, DestinationTourService>();
            services.AddTransient<ITourService, TourService>();
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<IVehicleTypeService, VehicleTypeService>();
            services.AddTransient<IDestinationService, DestinationService>();
            services.AddTransient<IAgencyService, AgencyService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<ITicketService, TickService>();
            services.AddTransient<Business.Interfaces.Business.ITicketTypeService, Business.Implement.Business.TicketTypeService>();
            services.AddTransient<ITripService, TripService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRewriter(new RewriteOptions().Add(new PascalRule()));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookingYacht v1");
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            // Handle Exceptions
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new ErrorModel()
                {
                    Error = exception.Message
                };
                await context.Response.WriteAsJsonAsync(response);
            }));

            // Handle Unauthorized
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    var response = new ErrorModel()
                    {
                        Error = "Token Validation Has Failed. Request Access Denied"
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            });

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                if (env.IsDevelopment())
                    endpoints.MapControllers().WithMetadata(new AllowAnonymousAttribute());
                else
                    endpoints.MapControllers();
            });
        }
    }
}
