
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Sidata.SLIP2.Data.Context;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;

//---------------------------------------------------------------
//-- Setup Builder
//---------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);

// dataContext
builder.Services.AddDbContextFactory<LoyaltyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddQueryableLikeOperatorForSqlServer();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//---------------------------------------------------------------
//-- Build the App
//---------------------------------------------------------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//---------------------------------------------------------------
//-- Run it
//---------------------------------------------------------------
app.Run();
