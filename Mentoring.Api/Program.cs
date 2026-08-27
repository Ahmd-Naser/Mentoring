using Mentoring.Api;
using Scalar.AspNetCore;

try
{

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();

app.UseCors("AllowAngular");

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

}
catch (Exception ex)
{
    // كتابة تفاصيل الخطأ الكاملة في ملف crash.txt في المجلد الرئيسي
    System.IO.File.WriteAllText("crash.txt", $"{DateTime.UtcNow}\n\n{ex.ToString()}");
    throw;
}