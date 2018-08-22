using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlayTogether.Web.Infrastructure;

namespace PlayTogether.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var cacheSettings = Configuration.GetSection("CacheSettings").Get<CacheSettings>();
            //var cacheValues = Env.IsDevelopment() ? cacheSettings.Local : cacheSettings.Remote;

            //// Add framework services.
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(ApiExceptionFilter));
            //    options.CacheProfiles.Add("DynamicContent", new CacheProfile()
            //    {
            //        Duration = cacheValues.DynamicContent
            //    });
            //    options.CacheProfiles.Add("StaticContent", new CacheProfile()
            //    {
            //        Duration = cacheValues.StaticContent
            //    });
            //});

            //services.AddScoped<ApiExceptionFilter>();
            services.AddCodeSchool(Configuration, Env);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Authentication JWT Settings
            //var jwtSettings = optionsAccessor.Value;
            //var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey));

            //var tokenValidationParameters = GetTokenValidationParameters(signingKey, jwtSettings);

            //app.UseJwtBearerAuthentication(new JwtBearerOptions
            //{
            //    TokenValidationParameters = tokenValidationParameters
            //});

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AutomaticAuthenticate = false,
            //    AutomaticChallenge = false
            //});

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        //private TokenValidationParameters GetTokenValidationParameters(SymmetricSecurityKey signingKey,
        //    JWTSettings jwtSettings)
        //{
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = signingKey,

        //        // Validate the JWT Issuer (iss) claim
        //        ValidateIssuer = true,
        //        ValidIssuer = jwtSettings.Issuer,

        //        // Validate the JWT Audience (aud) claim
        //        ValidateAudience = true,
        //        ValidAudience = jwtSettings.Audience
        //    };
        //    return tokenValidationParameters;
        //}
    }
}
