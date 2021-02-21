using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using news_api.Core;
using news_api.Core.Model;
using news_api.Extensions;
using news_api.Persistence;
using Reservation_API.Mapping;
using Reservation_API.Persistence;

namespace news_api
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

            services.AddControllers();
            services.AddDbContext<NewsDbContext>(dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));                   
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository, Repository>();

            var mapperConfiguration = new MapperConfiguration(options => { options.AddProfile(new MappingProfile()); });
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            var identityBuilder = services.AddIdentityCore<User>(identityOptions =>
            {
                identityOptions.Password = new PasswordOptions()
                {
                    RequireDigit = true,
                    RequireNonAlphanumeric = true,
                    RequiredLength = 8,
                    RequireUppercase = true
                };
            });
            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(Role), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<NewsDbContext>();
            identityBuilder.AddRoleValidator<RoleValidator<Role>>();
            identityBuilder.AddRoleManager<RoleManager<Role>>();
            identityBuilder.AddUserManager<UserManager<User>>();
            identityBuilder.AddSignInManager<SignInManager<User>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = 
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:SecurityKey").Value))
                    };
                });   
           
            services.AddMvc(mvcOptions =>
            {
                var authorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                mvcOptions.Filters.Add(new AuthorizeFilter(authorizationPolicy));
            });            

            //add policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.PolicyNameAdmin, policyBuilder => { policyBuilder.RequireRole(Constants.RoleNameAdmin); });
                options.AddPolicy(Constants.PolicyNameNormalAccess, policyBuilder => 
                    { policyBuilder.RequireRole(Constants.RoleNameAdmin, Constants.RoleNameNormalUser); });
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "news_api v1"));
            } 

            //app.UseHttpsRedirection();
            app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseRouting();  

            app.UseAuthentication();          
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
