using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PicSecurityChecks.api.Interface;
using PicSecurityChecks.api.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISecurityCheckNamesRepository, SecurityCheckNamesRepository>();
builder.Services.AddScoped<IFlagedNamesRepository, FlaggedNamesRepository>();
builder.Services.AddScoped<ISearchFlaggedNamesRepository, SearchFlaggedNamesRepository>();
builder.Services.AddScoped<IInetRepository, InetRepository>();
builder.Services.AddScoped<ICurrentPersonRepository, CurrentPersonRepository>();
builder.Services.AddScoped<IGetNicheDataRepository, GetNicheDataRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.UseAuthentication();


app.MapControllers();

app.Run();
