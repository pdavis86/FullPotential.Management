using FullPotential.Management;
using FullPotential.Management.Features.Security;
using FullPotential.Management.Utilities;
using FullPotential.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FullPotential");
builder.Services.AddDbContext<GeneralDbContext>(options => options.UseSqlServer(connectionString));

ServicesConfig.ConfigureServices(builder.Services);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerHeaderParameter>();
});

builder.Services.AddRateLimiter(options => new SlidingWindowRateLimiter().Configure(options));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<TokenValidationMiddleware>();

app.UseRateLimiter();

app.Run();
