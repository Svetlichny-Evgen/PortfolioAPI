﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;
using TeamPortfolio.Models;
using TeamPortfolio.Services;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация MongoDB
builder.Services.Configure<TeamPortfolioDatabaseSettings>(
    builder.Configuration.GetSection("TeamPortfolioDatabase"));

builder.Services.AddSingleton<ITeamPortfolioDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<TeamPortfolioDatabaseSettings>>().Value);

// Проверка подключения к MongoDB
try
{
    var mongoSettings = builder.Configuration.GetSection("TeamPortfolioDatabase");
    var mongoClient = new MongoClient(mongoSettings["ConnectionString"]);
    var pingDb = mongoClient.GetDatabase("admin");
    var command = new BsonDocument("ping", 1);
    await pingDb.RunCommandAsync<BsonDocument>(command);

    Console.WriteLine("✅ Successfully connected to MongoDB!");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ MongoDB connection failed: {ex}");
    Environment.Exit(1); // Не запускаем API, если БД не доступна
}


// Регистрация сервисов
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(sp.GetRequiredService<ITeamPortfolioDatabaseSettings>().ConnectionString));
builder.Services.AddSingleton<ITeamMemberService, TeamMemberService>();
builder.Services.AddSingleton<IAdminService, AdminService>();

// Настройка JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Secret"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();