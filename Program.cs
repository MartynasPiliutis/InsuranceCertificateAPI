using InsuranceCertificates.Data;
using InsuranceCertificates.Domain;
using InsuranceCertificates.Interfaces;
using InsuranceCertificates.Repositories;
using InsuranceCertificates.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddResponseCaching();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("database"));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<IErrorMessage, ErrorMessage>();
builder.Services.AddScoped<ErrorModel, ErrorModel>();

var app = builder.Build();

FeedCertificates(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();


void FeedCertificates(IServiceProvider provider)
{
    using var scope = provider.CreateScope();
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    appDbContext.Certificates.Add(new Certificate()
    {
        Number = "00001",
        CreationDate = DateTime.UtcNow,
        ValidFrom = DateTime.UtcNow,
        ValidTo = DateTime.UtcNow.AddYears(1),
        CertificateSum = 15,
        InsuredItem = "Apple Iphone 14 PRO",
        InsuredSum = 99,
        Customer = new Customer()
        {
            Name = "Customer 1",
            DateOfBirth = new DateTime(2000, 1, 1)
        }
    });

    appDbContext.SaveChanges();
}