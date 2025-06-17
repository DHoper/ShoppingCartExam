namespace ShoppingCartExam.Domain;

/// <summary>
/// 某一天特定類別商品的折扣規則
/// </summary>
public class PromotionRule(DateOnly date, decimal discountRate, string category)
{
    public DateOnly Date { get; } = date;           // 折扣日期
    public decimal DiscountRate { get; } = discountRate; // 折扣率
    public string Category { get; } = category;     // 適用的商品分類

    /// <summary>
    /// 檢查結帳日與商品分類是否符合折扣條件，若符合則回傳折扣後金額，否則回傳原價
    /// </summary>
    public decimal ApplyDiscount(CartItem item, string itemCategory, DateOnly checkoutDate)
    {
        if (checkoutDate != Date || itemCategory != Category)
            return item.TotalPrice;

        return item.TotalPrice * DiscountRate;
    }
}
