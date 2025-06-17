using ShoppingCartExam.Domain;
using ShoppingCartExam.Interfaces;

namespace ShoppingCartExam.Services;

/// <summary>
/// 結帳，計算購物車總金額（含折扣與優惠券）
/// </summary>
public class CheckoutService(IProductCategoryLookup categoryLookup) : ICheckoutService
{
    private readonly IProductCategoryLookup _categoryLookup = categoryLookup;

    public decimal CalculateTotal(
        ShoppingCart cart,
        DateOnly checkoutDate,
        IEnumerable<PromotionRule> promotions,
        Coupon? coupon)
    {
        decimal total = 0m;

        foreach (var item in cart.Items)
        {
            var category = _categoryLookup.GetCategory(item.ProductName);

            // 若無法對應分類，拋出例外
            if (category is not string validCategory)
                throw new InvalidOperationException($"無法判斷商品「{item.ProductName}」的分類。");

            // 套用所有符合的折扣規則
            var discounted = promotions.Aggregate(
                item.TotalPrice,
                (current, rule) => rule.ApplyDiscount(item, validCategory, checkoutDate));

            total += discounted;
        }

        // 套用優惠券折扣
        if (coupon != null)
        {
            var discount = coupon.GetDiscount(total, checkoutDate);
            total -= discount;
        }

        return Math.Round(total, 2, MidpointRounding.AwayFromZero);
    }
}
