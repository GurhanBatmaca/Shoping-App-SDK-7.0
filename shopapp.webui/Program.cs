using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using shopapp.business.Abstract;
using shopapp.business.Concrete;
using shopapp.data.Abstract;
using shopapp.data.Concrete.EfCore;
using shopapp.webui.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("MsSqlConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddDbContext<ApplicationContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("MsSqlConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
        // password
    options.Password.RequireDigit = true; // şifrede numara olmak zorunda
    options.Password.RequireLowercase = true; // küçük harf 
    options.Password.RequireUppercase = true; // büyük harf 
    options.Password.RequiredLength = 6; // 6 karakter minimum
    options.Password.RequireNonAlphanumeric = true; // alphanumeric 

    // lockout
    options.Lockout.MaxFailedAccessAttempts = 5; // 5 hatadan sonra hesap kilitlenir
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // kilitli hesap kaç dk sonra açılsın
    options.Lockout.AllowedForNewUsers = true; // lockout aktif olması için

    // options.User.AllowedUserNameCharacters = ""; // isimde olmasını istediğin ekler
    options.User.RequireUniqueEmail = true; // 1 mail adresi ile tek kullanıcı
    options.SignIn.RequireConfirmedEmail = true; // mail adresi onaylamadan giriş yapamaz
    options.SignIn.RequireConfirmedPhoneNumber = false; // telefonla onaylamadan giriş yapamaz
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/account/login"; // cookie tanınmaz ise vs gideceği sayfa
    options.LogoutPath = "/account/logout"; // çıkış yapınca gidilecek yer
    options.AccessDeniedPath = "/account/accessdenied"; // yetkisiz yere girmek istenince gidlecek sayfa
    options.SlidingExpiration = true; // giriş yaptıktan sonra istek yaptığında var sayılan süreyi sıfırlar(alttaki süre tekrar başlar)
    options.ExpireTimeSpan = TimeSpan.FromDays(7); // giriş yaptıktan sonra ne kadar süre site kullanıcıyı tanısın(istek yapmadan, eğer istek yaparsa üstteki ayar sayesinde sıfırlanır)

    // FromMinutes(60)

    options.Cookie = new CookieBuilder
    {
        HttpOnly = true, // cookieler sadece http isteği ile alınır
        Name = ".MyShopapp.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
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

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "accontregister",
    pattern: "uyelik/uyeol",
    defaults: new { controller = "Accont", action = "Register"}
);

app.MapControllerRoute(
    name: "accontlogin",
    pattern: "uyelik/girisyap",
    defaults: new { controller = "Accont", action = "Login"}
);

app.MapControllerRoute(
    name: "categorylist",
    pattern: "admin/kategorilistesi",
    defaults: new { controller = "Admin", action = "CategoryList"}
);

app.MapControllerRoute(
    name: "createcategory",
    pattern: "admin/kategoriekle",
    defaults: new { controller = "Admin", action = "CreateCategory"}
);

app.MapControllerRoute(
    name: "editcategory",
    pattern: "admin/kategoridüzenle/{id}",
    defaults: new { controller = "Admin", action = "EditCategory"}
);

app.MapControllerRoute(
    name: "deletecategory",
    pattern: "admin/kategorisil/{id}",
    defaults: new { controller = "Admin", action = "DeleteCategory"}
);

app.MapControllerRoute(
    name: "productlist",
    pattern: "admin/urunlistesi",
    defaults: new { controller = "Admin", action = "ProductList"}
);

app.MapControllerRoute(
    name: "createproduct",
    pattern: "admin/urunekle",
    defaults: new { controller = "Admin", action = "CreateProduct"}
);

app.MapControllerRoute(
    name: "editproduct",
    pattern: "admin/urunduzenle/{id}",
    defaults: new { controller = "Admin", action = "EditProduct"}
);

app.MapControllerRoute(
    name: "deleteproduct",
    pattern: "admin/urunsil/{id}",
    defaults: new { controller = "Admin", action = "DeleteProduct"}
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
    name: "search",
    pattern: "arama",
    defaults: new { controller = "Shop", action = "Search"}
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
