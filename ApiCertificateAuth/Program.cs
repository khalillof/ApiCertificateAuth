using ApiCertificateAuth;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;


var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(c => {
//    c.ConfigureHttpsDefaults(o => o.ClientCertificateMode = ClientCertificateMode.RequireCertificate);
//});
builder.Services.Configure<KestrelServerOptions>(opts =>
 {
     opts.ConfigureHttpsDefaults(options => options.ClientCertificateMode = ClientCertificateMode.RequireCertificate);
 });

// add certificate services
builder.Services.AddCertificateAuthetication();

// Add services to the container.
builder.Services.AddControllers();



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
