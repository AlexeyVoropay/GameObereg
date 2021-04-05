namespace WebApiAuth
{
    using WebApiAuth.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

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

            // For Entity Framework  
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));

            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //            .AddJwtBearer(options =>
            //            {
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        // ��������, ����� �� �������������� �������� ��� ��������� ������
            //        ValidateIssuer = true,
            //        // ������, �������������� ��������
            //        ValidIssuer = AuthOptions.ISSUER,

            //        // ����� �� �������������� ����������� ������
            //        ValidateAudience = true,
            //        // ��������� ����������� ������
            //        ValidAudience = AuthOptions.AUDIENCE,
            //        // ����� �� �������������� ����� �������������
            //        ValidateLifetime = true,

            //        // ��������� ����� ������������
            //        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            //        // ��������� ����� ������������
            //        ValidateIssuerSigningKey = true,
            //    };
            //});

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    // ��������, ����� �� �������������� �������� ��� ��������� ������
                    ValidateIssuer = true,
                    // ����� �� �������������� ����������� ������
                    ValidateAudience = true,
                    // ��������� ����������� ������
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    // ����� �� �������������� ����� �������������
                    ValidateLifetime = true,
                    // ������, �������������� ��������
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    // ��������� ����� ������������
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                    // ��������� ����� ������������
                    ValidateIssuerSigningKey = true,
                };
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseDefaultFiles();
            //app.UseStaticFiles();

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
