
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.Abstractions.Services;
using Sidata.Abstractions.WebApi.Services;
using Sidata.SLIP2.Data.Context;
using Swashbuckle.AspNetCore.SwaggerUI;
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
        builder.Services.AddCrudDefinitions();

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
                Title = AssemblyProperties.GetProductName(),
                Description = AssemblyProperties.GetProductDescription(),
                Version = AssemblyProperties.GetProductVersion()
            });

            // add grouping text name based on attribute in each controller
            // Urutkan berdasarkan tag (opsional tapi rapi)
            options.OrderActionsBy(
                (api) => api.ActionDescriptor.EndpointMetadata
                            .OfType<TagsAttribute>()
                            .SelectMany(t => t.Tags)
                            .FirstOrDefault()
                         ?? api.ActionDescriptor.RouteValues["controller"]!
            );

            // Kelompokkan endpoint dengan tag custom (bukan nama controller)
            options.TagActionsBy(api =>
            {
                // Ambil tag dari attribute [Tags] jika ada
                var tags = api.ActionDescriptor.EndpointMetadata
                    .OfType<TagsAttribute>()
                    .SelectMany(t => t.Tags)
                    .ToList();

                if (tags.Any()) return tags;

                // Fallback ke nama controller
                return new[] { api.ActionDescriptor.RouteValues["controller"]! };
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
                options.SwaggerEndpoint("/swagger/v2/swagger.json", AssemblyProperties.GetProductName());
                options.DocExpansion(DocExpansion.None);
                options.ConfigObject.AdditionalItems["syntaxHighlight"] = false;
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