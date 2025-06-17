namespace ShoppingCartExam.Interfaces;

/// <summary>
/// 查詢商品的分類
/// </summary>
public interface IProductCategoryLookup
{
    /// <summary>
    /// 傳回指定商品的分類名稱
    /// </summary>
    string? GetCategory(string productName);
}
