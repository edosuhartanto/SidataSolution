
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.SLIP2.Data.Context;
using Sidata.SLIP2.WebApi.Services;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        //---------------------------------------------------------------
        //-- Setup Builder
        //---------------------------------------------------------------
        var builder = WebApplication.CreateBuilder(args);

        // dataContext
        builder.Services.AddDbContextFactory<LoyaltyDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        // Like must be handled with special custom handle
        builder.Services.AddQueryableLikeOperatorForSqlServer();
        // setup CRUD Definition to be consumed by webapi crud controller base
        builder.Services.AddCrudDefinition();

        // Add json so endpoint can receive or response Enum in string name
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter());
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v2", new OpenApiInfo   // <-- tanpa prefix Microsoft.OpenApi.Models
            {
                Title = "Sistem Loyalti Pelanggan",
                Version = "v2",
                Description = "API utk melayani sistem loyalti pelanggan"
            });
        });

        //---------------------------------------------------------------
        //-- Build the App
        //---------------------------------------------------------------
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Sistem Loyalti Pelanggan v2");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        //---------------------------------------------------------------
        //-- Run it
        //---------------------------------------------------------------
        app.Run();
        }
}