namespace ShoppingCartExam.Domain;

public class ShoppingCart(List<CartItem> items)
{
    public IReadOnlyCollection<CartItem> Items => items;
    public decimal Subtotal => items.Sum(item => item.TotalPrice);
}
