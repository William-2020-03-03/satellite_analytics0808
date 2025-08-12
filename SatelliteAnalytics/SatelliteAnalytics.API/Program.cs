using Microsoft.EntityFrameworkCore;
using SatelliteAnalytics.Data;
using SatelliteAnalytics.Repository;
using SatelliteAnalytics.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularDev", policy =>
//    {
//        policy.WithOrigins("http://localhost:8888", "http://127.0.0.1:8888")
//                      .AllowAnyHeader()
//                      .AllowAnyMethod();

//    });
//});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext();
builder.Services.AddDbContext<SatelliteAnalyticsDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserOperationLogRepository, UserOperationLogRepository>();
builder.Services.AddScoped<IUserOperationLogService, UserOperationLogService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
//app.UseCors("AllowAngularDev");
app.UseAuthorization();
app.MapControllers();

app.Run();
