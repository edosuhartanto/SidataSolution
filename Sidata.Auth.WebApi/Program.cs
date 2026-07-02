using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.Abstractions.Services;
using Sidata.Abstractions.WebApi.Services;
using Sidata.Auth.Data.Context;

internal class Program
{
    private static void Main(string[] args)
    {
        //---------------------------------------------------------------
        //-- Setup Builder
        //---------------------------------------------------------------
        var builder = WebApplication.CreateBuilder(args);

        // dataContext
        builder.Services.AddDbContextFactory<AuthDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        // Like must be handled with special custom handle
        builder.Services.AddQueryableLikeOperatorForSqlServer();
        // setup CRUD Definition to be consumed by webapi crud controller base
        builder.Services.AddCrudDefinitions();

        // set controller 
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo   // <-- tanpa prefix Microsoft.OpenApi.Models
            {
                Title = AssemblyProperties.GetProductName(),
                Description = AssemblyProperties.GetProductDescription(),
                Version = AssemblyProperties.GetProductVersion()
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", AssemblyProperties.GetProductName());
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}