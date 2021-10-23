using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Security.Requirements;
using App.Services;
using hrmProject.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace hrmProject
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

            //Đk dịch vụ email 
            services.AddOptions();
            var mailSettings = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailSettings);

            services.AddSingleton<IEmailSender, SendMailService>();

            services.AddRazorPages();
            services.AddDbContext<AppDbContext>(options =>
            {
                // Đọc chuỗi kết nối
                string connectstring = Configuration.GetConnectionString("AppDbContext");
                // Sử dụng MS SQL Server
                options.UseSqlServer(connectstring);
            });

            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập. 
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true;

            });

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/login";
                option.LogoutPath = "/logout";
                option.AccessDeniedPath = "/accessdenied.html";
            });

            services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
            // Trên 30 giây truy cập lại sẽ nạp lại thông tin User (Role)
            // SecurityStamp trong bảng Use r đổi -> nạp lại thông tinn Security
            options.ValidationInterval = TimeSpan.FromSeconds(30);});
            // services.AddAuthorization(option =>{
            //     option.AddPolicy("AllowEditRole",policyBuilder=>
            //     {
            //         //Điều kiện của policyBuilder
            //         policyBuilder.RequireAuthenticatedUser();
            //         policyBuilder.RequireRole("Admin"); 
            //         // policyBuilder.RequireRole("Editor");

            //         policyBuilder.RequireClaim("allowEdit","role");

            //         // policyBuilder.RequireClaim("TenClaim",new string[]{"giatri1","giatri2"})
            //         // IdentityRoleClaim<string> claim1; 
            //         // IdentityUserClaim<string> claim2;
            //         // Claim claim;  
            //     });
            //      option.AddPolicy("Genz",policyBuilder=>
            //     {
            //        policyBuilder.RequireAuthenticatedUser(); 
            //        policyBuilder.Requirements.Add(new GenZRequirement());
            //        // new GenzRequirement() -> Authorization handler
            //        // Reqirement chỉ khai báo các thành phần có . Nên cần phải kiểm tra thông qua auth handler
            //     });
            //     option.AddPolicy("ShowAdminMenu",policyBuilder=>
            //     {
            //         policyBuilder.RequireRole("Admin");
            //     });
                
            // }); 
            services.AddTransient<IAuthorizationHandler,AppAuthorizationHandler>(); 



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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();   

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

//dotnet aspnet-codegenerator razorpage -m App.Models.Post -dc App.Models.AppDbContext -outDir Pages/Post -udl --referenceScriptLibraries