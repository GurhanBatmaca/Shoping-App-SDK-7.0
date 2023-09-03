using Microsoft.EntityFrameworkCore;
using shopapp.business.Abstract;
using shopapp.business.Concrete;
using shopapp.data.Abstract;
using shopapp.data.Concrete.EfCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("MsSqlConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();   

builder.Services.AddScoped<IProductService,ProductManager>();   
builder.Services.AddScoped<ICategoryService,CategoryManager>();   


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "search",
    pattern: "arama",
    defaults: new { controller = "Shop", action = "Search"}
);

app.MapControllerRoute(
    name: "productdetails",
    pattern: "urun/{url}",
    defaults: new { controller = "Shop", action = "Details"}
);

app.MapControllerRoute(
    name: "popularproducts",
    pattern: "populer",
    defaults: new { controller = "Shop", action = "Popular"}
);

app.MapControllerRoute(
    name: "products",
    pattern: "kategoriler/{kategori?}",
    defaults: new { controller = "Shop", action = "List"}
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
