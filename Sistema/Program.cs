using Sistema.Handler;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Sistema.Class;

var builder = WebApplication.CreateBuilder(args);
TimeSpan session = TimeSpan.FromMinutes(ClassUtilidad.parseMultiple(Environment.GetEnvironmentVariable("Session"), ClassUtilidad.TipoDato.Integer).numero);
string bucket = Environment.GetEnvironmentVariable("Bucket");
string objectName = Environment.GetEnvironmentVariable("Object");
string kmsKeyName = Environment.GetEnvironmentVariable("KmsKeyName");
bool isGoogleCloud = bool.Parse(Environment.GetEnvironmentVariable("IsGoogleCloud"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

if (isGoogleCloud)
{
    builder.Services.AddDataProtection()
        // Store keys in Cloud Storage so that multiple instances
        // of the web application see the same keys.
        .PersistKeysToGoogleCloudStorage(
            bucket, objectName)
        // Protect the keys with Google KMS for encryption and fine-
        // grained access control.
        .ProtectKeysWithGoogleKms(kmsKeyName);
}
else
{
    builder.Services.AddDataProtection()
    .SetApplicationName("Sistema")
    .PersistKeysToFileSystem(new DirectoryInfo(@"root"))
    .AddKeyManagementOptions(options =>
    {
        options.NewKeyLifetime = new TimeSpan(365, 0, 0, 0);
        options.AutoGenerateKeys = true;
    }).UseCryptographicAlgorithms(
    new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    })
    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
}

builder.Services.AddSession(options => {
    options.IdleTimeout = session;
    options.IOTimeout = session;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.HttpOnly = true;
    // Make the session cookie essential if you wish
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationCookie(options => {
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
});

builder.Services.Configure<FormOptions>(x =>
{
    x.BufferBody = false;
    x.KeyLengthLimit = 2048; // 2 KiB
    x.ValueLengthLimit = 4194304; // 32 MiB
    x.ValueCountLimit = int.MaxValue;// 1024
    x.MultipartHeadersCountLimit = 32; // 16
    x.MultipartHeadersLengthLimit = 32768; // 16384
    x.MultipartBoundaryLengthLimit = 256; // 128
    x.MultipartBodyLengthLimit = 134217728; // 128 MiB
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
});

app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<HandlerMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    // Add endpoints for Razor pages
    endpoints.MapRazorPages();
});

app.Run();
