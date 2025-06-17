using ShoppingCartExam.Interfaces;

namespace ShoppingCartExam.Infrastructure;

/// <summary>
/// 商品分類查詢器(硬編碼)
/// </summary>
public class ProductCategoryLookup : IProductCategoryLookup
{
    private static readonly Dictionary<string, string> _productCategoryMap = new(StringComparer.OrdinalIgnoreCase)
    {
        // 電子
        ["ipad"] = "電子",
        ["iphone"] = "電子",
        ["螢幕"] = "電子",
        ["筆記型電腦"] = "電子",
        ["鍵盤"] = "電子",

        // 食品
        ["麵包"] = "食品",
        ["餅乾"] = "食品",
        ["蛋糕"] = "食品",
        ["牛肉"] = "食品",
        ["魚"] = "食品",
        ["蔬菜"] = "食品",

        // 日用品
        ["餐巾紙"] = "日用品",
        ["收納箱"] = "日用品",
        ["咖啡杯"] = "日用品",
        ["雨傘"] = "日用品",

        // 酒類
        ["啤酒"] = "酒類",
        ["白酒"] = "酒類",
        ["伏特加"] = "酒類",
    };

    /// <summary>
    /// 根據商品名稱取得對應分類，找不到則回傳 null
    /// </summary>
    public string? GetCategory(string productName)
    {
        return _productCategoryMap.TryGetValue(productName, out var category)
            ? category
            : null;
    }
}
