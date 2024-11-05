
using CarshoppingCartApi.Const;
using CarshoppingCartApi.Models;
using CarshoppingCartApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarshoppingCartApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", policy => 
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
         
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(option =>
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
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });
            builder.Services.AddDbContext<CarShoppingDb>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<CarShoppingDb>();
            builder.Services.AddTransient<ICarRepo, CarRepo>();
            builder.Services.AddTransient<IGenreRepo, GenreRepo>();
            builder.Services.AddTransient<IHomeRepo, HomeRepo>();
            builder.Services.AddTransient<IGenreRepo, GenreRepo>();
            builder.Services.AddTransient<IStockRepo, StockRepo>();
            builder.Services.AddTransient<IUserOrderRepo, UserOrderRepo>();
            builder.Services.AddTransient<ICartRepo, CartRepo>();
            builder.Services.AddAuthorization(option=>
            {
                option.AddPolicy("AdminPolicy", policy => policy.RequireRole(Role.Admin.ToString()));
                option.AddPolicy("UserPolicy", policy => policy.RequireRole(Role.User.ToString()));



            });
            var app = builder.Build();
           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
