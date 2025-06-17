using ShoppingCartExam.Interfaces;
using ShoppingCartExam.Services;
using ShoppingCartExam.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ���U�A��
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddSingleton<IProductCategoryLookup, ProductCategoryLookup>();

// �[�J Controller �P Swagger
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, new TextPlainInputFormatter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// �}�o���ұҥ� Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
