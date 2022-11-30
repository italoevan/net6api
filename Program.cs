using api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

ProductRepository.TesteInjection(app.Configuration);

app.MapGet("/", () => "Hello World!");

app.MapGet("/user", () => new { Name = "ItaloEvan", Age = 22 });



app.MapGet("/altera", (HttpResponse response) =>
{

    response.Headers.Add("Teste", " O brabo");
    return new { Sucess = true };
});

app.MapPost("/products", (Product product, HttpRequest request) =>
{
    ProductRepository.Add(product);
    return Results.Created("/products" + product.Code, product.Code);
});

app.MapGet("/products", () => {
    return ProductRepository.GetAll();
});

app.MapGet("/products/{code}", ([FromRoute]string code) => {
    var product =  ProductRepository.GetBy(code);

    if(product != null)
    {
        return Results.Ok(product);
    }
    return Results.NotFound();

});



app.MapPut("/products", ([FromBody]Product product) => {
    var result = ProductRepository.GetBy(product.Code);
    result.Name = product.Name;
    return result;
});

app.MapDelete("/products/{code}", ([FromRoute]string code) => {
  
    ProductRepository.Remove(code);

});

app.MapGet("/query", ([FromQuery] string name) => {

    return name;
});



app.Run();

public static class ProductRepository
{
    public static List<Product> Products { get; set; }

    public static void Add(Product product)
    {
        if (Products == null)
        {
            Products = new List<Product>();
        }
        Products.Add(product);
    }

    public static void TesteInjection(IConfiguration configuration)
    {

    }

    public static Product? GetBy(string code)
    {   
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static List<Product> GetAll()
    {
        return Products;
    }

    public static string Remove(string code)
    {
      Product? product =   Products.FirstOrDefault(v => v.Code == code);

        if(product != null)
        {
            Products.Remove(product);
            return "Sucess";
        }
        return "Error";
        
    }
}
public class Product
{
    public string? Code { get; set; }
    public string? Name { get; set; }

}

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
}