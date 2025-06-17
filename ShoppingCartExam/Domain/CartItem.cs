namespace ShoppingCartExam.Domain;

public class CartItem(string productName, decimal unitPrice, int quantity)
{
    public string ProductName { get; } = productName;
    public decimal UnitPrice { get; } = unitPrice;
    public int Quantity { get; } = quantity;

    public decimal TotalPrice => UnitPrice * Quantity;
}

