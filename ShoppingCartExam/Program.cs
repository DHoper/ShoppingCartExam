using ShoppingCartExam.Interfaces;
using ShoppingCartExam.Services;
using ShoppingCartExam.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 註冊服務
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddSingleton<IProductCategoryLookup, ProductCategoryLookup>();

// 加入 Controller 與 Swagger
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, new TextPlainInputFormatter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 開發環境啟用 Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
