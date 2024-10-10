﻿using BL.Interfaces;
using BL.Repositories;
using BL.Utilities;
using DL.Context;
using DL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(op =>
            {
                // op.SuppressModelStateInvalidFilter=true; // عشان يوقف (الموديل ستيت) عشان اعرف اهندل انا الاخطاء واغير رساله الايرور
            });                                                                                            // ModelState
                                                                                                           //----------------------------------------------------------------------------------------------
        builder.Services.AddDbContext<RealStateDbContext>(op =>
        {
            op.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
        });

        //----------------------------------------------------------------------------------------------
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
        {
            op.Password.RequireLowercase = false;
            op.Password.RequireUppercase = false;
            op.Password.RequiredLength = 6;
            op.Password.RequireNonAlphanumeric = false;
            op.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<RealStateDbContext>();
        //----------------------------------------------------------------------------------------------
        builder.Services.AddAuthentication(op =>
        {
            op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.SaveToken = true;
            opt.RequireHttpsMetadata = true; // Sure that Request HTTP
            opt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true, // Sure that i was the auther *(But in microservices and distributed systems false)
                ValidIssuer = builder.Configuration["JWT:IssuerIP"],

                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:AudienceIP"],

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"]))


            };

        });
        //----------------------------------------------------------------------------------------------
        // map the jwt in appsettings to JWT class
        builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
        //----------------------------------------------------------------------------------------------
        builder.Services.AddCors(op =>
        {
            op.AddPolicy("MyPolicy", CorsPolicyBuilder =>
            {
                CorsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            op.AddPolicy("MyPolicy2", CorsPolicyBuilder =>
            {
                // ???????????????????????
            });
        });

        //----------------------------------------------------------------------------------------------
       
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IAuthServices, AuthServices>();
       
        //----------------------------------------------------------------------------------------------
       
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("MyPolicy");

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}