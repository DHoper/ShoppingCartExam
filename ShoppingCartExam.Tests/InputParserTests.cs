using ShoppingCartExam.Domain;
using ShoppingCartExam.Utils;
using Xunit;

namespace ShoppingCartExam.Tests;

public class InputParserTests
{
    [Fact]
    public void CaseA_ShouldParseCorrectly()
    {
        // Arrange
        var input = string.Join('\n', new[]
        {
            "2015.11.11|0.7|電子",
            "",
            "1*ipad:2399.00",
            "1*螢幕:1799.00",
            "12*啤酒:25.00",
            "5*麵包:9.00",
            "",
            "2015.11.11",
            "2016.3.2 1000 200"
        });

        // Act
        var (promotions, cart, checkoutDate, coupon) = InputParser.ParseInput(input);

        // Assert
        Assert.Single(promotions);
        Assert.Equal("電子", promotions[0].Category);
        Assert.Equal(0.7m, promotions[0].DiscountRate);
        Assert.Equal(new DateOnly(2015, 11, 11), promotions[0].Date);

        Assert.Equal(4, cart.Items.Count);
        Assert.Contains(cart.Items, x => x.ProductName == "ipad" && x.UnitPrice == 2399.00m && x.Quantity == 1);
        Assert.Contains(cart.Items, x => x.ProductName == "螢幕" && x.UnitPrice == 1799.00m && x.Quantity == 1);
        Assert.Contains(cart.Items, x => x.ProductName == "啤酒" && x.UnitPrice == 25.00m && x.Quantity == 12);
        Assert.Contains(cart.Items, x => x.ProductName == "麵包" && x.UnitPrice == 9.00m && x.Quantity == 5);

        Assert.Equal(new DateOnly(2015, 11, 11), checkoutDate);
        Assert.NotNull(coupon);
        Assert.Equal(new DateOnly(2016, 3, 2), coupon!.EndDate);
        Assert.Equal(1000m, coupon.ThresholdAmount);
        Assert.Equal(200m, coupon.DiscountAmount);
    }

    [Fact]
    public void CaseB_ShouldParseWithoutPromotionOrCoupon()
    {
        // Arrange
        var input = string.Join('\n', new[]
        {
            "",
            "",
            "3*蔬菜:5.98",
            "8*餐巾紙:3.20",
            "",
            "2015.01.01",
            ""
        });

        // Act
        var (promotions, cart, checkoutDate, coupon) = InputParser.ParseInput(input);

        // Assert
        Assert.Empty(promotions);
        Assert.Null(coupon);
        Assert.Equal(new DateOnly(2015, 1, 1), checkoutDate);

        Assert.Equal(2, cart.Items.Count);
        Assert.Contains(cart.Items, x => x.ProductName == "蔬菜" && x.UnitPrice == 5.98m && x.Quantity == 3);
        Assert.Contains(cart.Items, x => x.ProductName == "餐巾紙" && x.UnitPrice == 3.20m && x.Quantity == 8);
    }
}
