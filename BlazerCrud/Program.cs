using BlazerCrud.Dtos;
using BlazerCrud.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlazorCrudConStr"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/products", async(AppDbContext db, ProductDto product) =>
{
    product.Id = 0;
    var newProduct = new Product
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
    };
    db.Products.Add(newProduct);
    await db.SaveChangesAsync();
    return Results.Ok(newProduct.Id);
});

app.MapGet("/api/products", async (AppDbContext db) =>
{
    var products = await db.Products.AsNoTracking().ToListAsync();
    return Results.Ok(products);
});

app.MapGet("/api/products/{productid}", async (AppDbContext db, int productId ) =>
{
    var product = await db.Products.Where(x => x.Id == productId)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync();
    return Results.Ok(product);
});

app.MapDelete("/api/products/{productid}", async(AppDbContext db, int productId ) =>
{
    await db.Products.Where(x => x.Id == productId).ExecuteDeleteAsync();
                                   
    return Results.Ok();
});

app.MapPut("/api/products", async(AppDbContext db, ProductDto product ) =>
{
    var currentProduct = new Product
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
    };
    await db.Products.Where(x =>x.Id == currentProduct.Id)
                     .ExecuteUpdateAsync(
                        x => x.SetProperty(z => z.Name, currentProduct.Name)
                              .SetProperty(z => z.Price, currentProduct.Price)
                     );
                                   
    return Results.Ok(currentProduct);
});



app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
