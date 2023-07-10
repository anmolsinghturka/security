using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PicSecurityChecksWin.api.Interface;
using PicSecurityChecksWin.api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISecurityCheckNamesRepository, SecurityCheckNamesRepository>();
builder.Services.AddScoped<IFlagedNamesRepository, FlaggedNamesRepository>();
builder.Services.AddScoped<ISearchFlaggedNamesRepository, SearchFlaggedNamesRepository>();
builder.Services.AddScoped<ICurrentPersonRepository, CurrentPersonRepository>();
builder.Services.AddScoped<IGetNicheDataRepository, GetNicheDataRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(opion =>
{
    opion.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "PicSecurityChecksws");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

//app.UseAuthentication();

app.MapControllers();

app.Run();
