using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartExam.Controllers;
using ShoppingCartExam.Domain;
using ShoppingCartExam.Interfaces;
using Xunit;

namespace ShoppingCartExam.Tests;

public class CheckoutControllerTests
{
    [Fact]
    public void CalculateTotal_ShouldReturnExpectedAmount_ForCaseA()
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

        var controller = new CheckoutController(
            new FakeCheckoutService(3083.60m),
            new FakeProductCategoryLookup());

        // Act
        var result = controller.CalculateTotal(input) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(3083.60m, result.Value);
    }

    [Fact]
    public void CalculateTotal_ShouldReturnExpectedAmount_ForCaseB()
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

        var controller = new CheckoutController(
            new FakeCheckoutService(43.54m),
            new FakeProductCategoryLookup());

        // Act
        var result = controller.CalculateTotal(input) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(43.54m, result.Value);
    }

    private class FakeCheckoutService : ICheckoutService
    {
        private readonly decimal _fixedResult;

        public FakeCheckoutService(decimal fixedResult)
        {
            _fixedResult = fixedResult;
        }

        public decimal CalculateTotal(
            ShoppingCart cart,
            DateOnly checkoutDate,
            IEnumerable<PromotionRule> promotions,
            Coupon? coupon)
        {
            return _fixedResult;
        }
    }

    private class FakeProductCategoryLookup : IProductCategoryLookup
    {
        public string GetCategory(string productName)
        {
            return "電子"; 
        }
    }
}
