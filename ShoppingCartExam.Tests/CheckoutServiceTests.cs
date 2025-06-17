using ShoppingCartExam.Domain;
using ShoppingCartExam.Interfaces;
using ShoppingCartExam.Services;
using System.Globalization;
using Xunit;

namespace ShoppingCartExam.Tests;

public class CheckoutServiceTests
{
    [Fact]
    public void CalculateTotal_ShouldReturnExpectedAmount_ForCaseA()
    {
        // Arrange
        var cart = new ShoppingCart(new List<CartItem>
        {
            new("ipad", 2399.00m, 1),
            new("螢幕", 1799.00m, 1),
            new("啤酒", 25.00m, 12),
            new("麵包", 9.00m, 5)
        });

        var checkoutDate = DateOnly.ParseExact("2015.11.11", "yyyy.M.d", CultureInfo.InvariantCulture);

        var promotions = new List<PromotionRule>
        {
            new(DateOnly.ParseExact("2015.11.11", "yyyy.M.d", CultureInfo.InvariantCulture), 0.7m, "電子")
        };

        var coupon = new Coupon(
            startDate: DateOnly.MinValue, // 表示不限開始日
            endDate: DateOnly.ParseExact("2016.3.2", "yyyy.M.d", CultureInfo.InvariantCulture), // 到 2016.3.2 為止有效
            thresholdAmount: 1000,
            discountAmount: 200
        );

        var categoryLookup = new StubProductCategoryLookup(new Dictionary<string, string>
        {
            ["ipad"] = "電子",
            ["螢幕"] = "電子",
            ["啤酒"] = "飲料",
            ["麵包"] = "食品"
        });

        var service = new CheckoutService(categoryLookup);

        // Act
        var total = service.CalculateTotal(cart, checkoutDate, promotions, coupon);

        // Assert
        Assert.Equal(3083.60m, total);
    }

    [Fact]
    public void CalculateTotal_ShouldReturnExpectedAmount_ForCaseB()
    {
        // Arrange
        var cart = new ShoppingCart(new List<CartItem>
        {
            new("蔬菜", 5.98m, 3),
            new("餐巾紙", 3.20m, 8)
        });

        var checkoutDate = DateOnly.ParseExact("2015.01.01", "yyyy.M.d", CultureInfo.InvariantCulture);
        var promotions = new List<PromotionRule>(); // 無促銷
        Coupon? coupon = null; // 無優惠券

        var categoryLookup = new StubProductCategoryLookup(new Dictionary<string, string>
        {
            ["蔬菜"] = "食品",
            ["餐巾紙"] = "生活用品"
        });

        var service = new CheckoutService(categoryLookup);

        // Act
        var total = service.CalculateTotal(cart, checkoutDate, promotions, coupon);

        // Assert
        Assert.Equal(43.54m, total);
    }

    /// <summary>
    /// 提供指定商品對應的分類，測試用 Stub 實作
    /// </summary>
    private class StubProductCategoryLookup(Dictionary<string, string> map) : IProductCategoryLookup
    {
        private readonly Dictionary<string, string> _map = map;

        public string GetCategory(string productName)
        {
            return _map.TryGetValue(productName, out var category)
                ? category
                : throw new KeyNotFoundException($"找不到商品「{productName}」的分類");
        }
    }
} 