using ApiCertificateAuth;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<KestrelServerOptions>(opts =>
 {
     opts.ConfigureHttpsDefaults(options => options.ClientCertificateMode = ClientCertificateMode.RequireCertificate);
 });
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCertificateAuthetication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
