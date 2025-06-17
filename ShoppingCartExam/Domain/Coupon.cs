namespace ShoppingCartExam.Domain;

/// <summary>
/// 滿額折扣優惠券
/// </summary>
public class Coupon(DateOnly startDate, DateOnly endDate, decimal thresholdAmount, decimal discountAmount)
{
    public DateOnly StartDate { get; } = startDate;          // 有效起始日
    public DateOnly EndDate { get; } = endDate;              // 有效結束日
    public decimal ThresholdAmount { get; } = thresholdAmount; // 滿額門檻
    public decimal DiscountAmount { get; } = discountAmount;   // 折抵金額

    /// <summary>
    /// 根據結帳金額與結帳日，判斷是否符合優惠條件，並回傳折扣金額
    /// </summary>
    public decimal GetDiscount(decimal amount, DateOnly checkoutDate)
    {
        if (checkoutDate < StartDate || checkoutDate > EndDate)
            return 0;

        if (amount < ThresholdAmount)
            return 0;

        return DiscountAmount;
    }
}
