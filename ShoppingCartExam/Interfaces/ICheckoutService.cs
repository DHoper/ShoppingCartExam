using ShoppingCartExam.Domain;

namespace ShoppingCartExam.Interfaces;

public interface ICheckoutService
{
    /// <summary>
    /// 計算結帳金額（含促銷折扣與優惠券）
    /// </summary>
    decimal CalculateTotal(
        ShoppingCart cart,
        DateOnly checkoutDate,
        IEnumerable<PromotionRule> promotions,
        Coupon? coupon
    );
}
