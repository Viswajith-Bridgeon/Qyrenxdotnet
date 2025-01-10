

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Qyrenx.Business.Services.CloudinaryService;
using Qyrenx.Business.Services.DeliveryServices;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Business.Services.JwtServices;
using Qyrenx.Business.Services.UserServices;
using Qyrenx.Business.Services.VendorServices;
using System.Text;
using Qyrenx.Business.Mapper;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Business.Services.CategoryServices;
using Qyrenx.CustomMidlleware;
using Qyrenx.Business.Services.AddressServices;
using Qyrenx.Business.Services.UserSecurityPay;
using Qyrenx.Business.Services.GadgetServices;
using Qyrenx.Dataccess.DbAccess;
using Qyrenx.Dataccess.DbAccess.AddressRepo;
using Qyrenx.Dataccess.DbAccess.DeliveryRepo;
using Qyrenx.Dataccess.DbAccess.VendorRepo;
using Qyrenx.Dataccess.DbAccess.CategoryRepo;
using Qyrenx.Dataccess.DbAccess.UserRepo;
using Qyrenx.Dataccess.DbAccess.UserSecurityPay;
using Qyrenx.Dataccess.DbAccess.GadgetRepo;
using Qyrenx.Business.Services.PickupServices;
using Qyrenx.Dataccess.DbAccess.Pickuprep;
using Qyrenx.Dataccess.DbAccess.StatusRepo;
using Qyrenx.Dataccess.DbAccess.VendorCostRepo;

namespace Qyrenx
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(AutoMapping));
            builder.Services.AddScoped<IEmailServices, EmailServices>();
            builder.Services.AddScoped<IUserServices,UserServices>();
            builder.Services.AddScoped<IDeliveryService, DeliveryService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IVendorServices, VendorService>();
            builder.Services.AddScoped<ICloudinaryService, CloudinaryServices>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAddressServices, AddressServices>();
            builder.Services.AddScoped<IUserSecurityPaymentService, UserSecurityPaymentService>();
            builder.Services.AddScoped<IGadgetSerives, GadgetServices>();
            builder.Services.AddScoped<IAddress, AddressService>();
            builder.Services.AddScoped<ICategory,CategoryServiceRepo>();
            builder.Services.AddScoped<IdeliveryRepo, DeliveryServiceRepo>();
            builder.Services.AddScoped<IgadgetRepo, GadgetRepo>();  
            builder.Services.AddScoped<IVendorRepo, VendorServiceRepo>();
            builder.Services.AddScoped<ICategory, CategoryServiceRepo>();
            builder.Services.AddScoped<IuserRepo,UserRepoo>();
            builder.Services.AddScoped<IuserSecurityRepo, UserSecurityRepo>(); 
            builder .Services.AddScoped<IPickupServices, PickupServices>();
            builder.Services.AddScoped<IpickupsRepo, PickupsRepo>();
            builder .Services.AddScoped<IstatusRepo,StatusRepo>(); 
            builder .Services.AddScoped<IVendorCostRepo,VendorCostServicRepo>();


            builder.Services.AddDbContext<QyrenxContext>(options =>
                        options.UseMySql(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        new MySqlServerVersion(new Version(8, 0, 32)),
                        mysqlOptions => mysqlOptions.EnableRetryOnFailure()));            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Ecommerce API", Version = "v1" });

                // Add JWT Bearer token definition
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Please enter token in the format **Bearer {your token}**",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // Apply JWT security requirement to all endpoints
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            // Configure JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<IdAcessMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}
