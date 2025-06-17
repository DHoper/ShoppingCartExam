namespace ShoppingCartExam.Domain;

/// <summary>
/// 購物車
/// </summary>
public class ShoppingCart(List<CartItem> items)
{
    public IReadOnlyCollection<CartItem> Items => items; // 商品清單

    public decimal Subtotal => items.Sum(item => item.TotalPrice); // 商品總額
}
