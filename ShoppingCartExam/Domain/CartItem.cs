namespace ShoppingCartExam.Domain;

/// <summary>
/// 購物車中的單筆品項
/// </summary>
public class CartItem(string productName, decimal unitPrice, int quantity)
{
    public string ProductName { get; } = productName;  // 商品名稱
    public decimal UnitPrice { get; } = unitPrice;     // 單價
    public int Quantity { get; } = quantity;           // 數量

    public decimal TotalPrice => UnitPrice * Quantity; // 總價（單價 × 數量）
}
