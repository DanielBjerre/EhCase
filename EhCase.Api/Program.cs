using EhCase.Api;
using EhCase.Api.Context;
using EhCase.Api.ProductClientService;
using EhCase.Api.Services;
using EhCase.Api.Services.Baskets;
using EhCase.Api.Services.Products;
using EhCase.Api.Services.Products.Store;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<RefreshProductWorker>();

builder.Services.AddDbContext<BasketContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IProductStore, ProductStore>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddScoped<IBasketService, EfBasketService>();

builder.Services.AddSingleton<ProductClientDelegatingHandler>();

builder.Services.AddHttpClient<ProductClientDelegatingHandler>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ProductApi:BaseUrl"]!);
});

builder.Services.AddHttpClient<IProductClient, ProductClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ProductApi:BaseUrl"]!);
}).AddHttpMessageHandler<ProductClientDelegatingHandler>();

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

app.Run();
