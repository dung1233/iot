using MongoDB.Driver;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ vào container
builder.Services.AddControllers();

// Khai báo MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    return new MongoClient("mongodb://localhost:27017");
});

// Khai báo Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "TestApp";
});

// Khai báo Swagger
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();           

// Khai báo các dịch vụ khác
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Cấu hình Middleware Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();     // kích hoạt middleware Swagger
    app.UseSwaggerUI();   // kích hoạt giao diện Swagger UI
}

app.UseRouting();
app.MapControllers();

app.Run();
