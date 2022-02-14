namespace DemoNStack;

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
        services.AddSession(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        services.AddControllersWithViews();
        services.AddMemoryCache();

            
        services.AddSingleton<NStackConfiguration>(r =>
        {
            var section = Configuration.GetSection("NStack");

            return new NStackConfiguration
            {
                ApiKey = section.GetValue<string>("ApiKey"),
                ApplicationId = section.GetValue<string>("ApplicationId")
            };
        });
        services.AddTransient<INStackRepository, NStackRepository>();
        services.AddTransient<INStackLocalizeService, NStackLocalizeService>();
        services.AddTransient<INStackTermsService, NStackTermsService>();
        services.AddTransient<INStackAppService, NStackAppService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
