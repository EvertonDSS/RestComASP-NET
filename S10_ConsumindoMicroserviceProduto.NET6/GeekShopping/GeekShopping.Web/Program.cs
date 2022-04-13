using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddHttpClient<IProductService, ProductService>(c => c.BaseAddress = new Uri(configuration["ServiceUrls:ProductAPI"])); //Se comunica com a API através da URL do Serviço e do nome do Serviço(ProductAPI)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
