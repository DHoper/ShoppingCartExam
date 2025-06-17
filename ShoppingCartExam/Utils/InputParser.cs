using ShoppingCartExam.Domain;
using System.Globalization;

namespace ShoppingCartExam.Utils;

/// <summary>將原始輸入文字解析為結帳所需模型</summary>
public static class InputParser
{
    public static (List<PromotionRule> Promotions, ShoppingCart Cart, DateOnly CheckoutDate, Coupon? Coupon)
        ParseInput(string input)
    {
        var lines = input
            .Split('\n', StringSplitOptions.None)
            .Select(line => line.Trim())
            .Where(line => line is not null)
            .ToList();

        // 移除尾端空白行
        while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[^1]))
            lines.RemoveAt(lines.Count - 1);

        if (lines.Count < 1)
            throw new FormatException("輸入內容不足，至少需包含結帳日期");

        // 處理優惠券（若存在）
        Coupon? coupon = null;
        var lastLine = lines[^1];
        if (IsCouponLine(lastLine))
        {
            coupon = ParseCoupon(lastLine);
            lines.RemoveAt(lines.Count - 1);
        }

        if (lines.Count < 1)
            throw new FormatException("缺少結帳日期");

        // 處理結帳日期
        var checkoutDate = ParseCheckoutDate(lines[^1]);
        lines.RemoveAt(lines.Count - 1);

        // 剩下為促銷與購物車：用空白行分段
        var promoLines = new List<string>();
        var cartLines = new List<string>();
        var current = promoLines;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                current = cartLines;
                continue;
            }
            current.Add(line);
        }

        return (
            ParsePromotions(promoLines),
            new ShoppingCart(ParseCartItems(cartLines)),
            checkoutDate,
            coupon
        );
    }

    private static bool IsCouponLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);
        return parts.Length == 3 &&
               DateOnly.TryParseExact(parts[0], "yyyy.M.d", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    private static List<PromotionRule> ParsePromotions(List<string> lines) =>
        lines.Count == 0
            ? []
            : lines.Select(line =>
            {
                var parts = line.Split('|', StringSplitOptions.TrimEntries);
                if (parts.Length != 3 || !DateOnly.TryParse(parts[0], out var date))
                    throw new FormatException($"促銷格式錯誤：{line}");

                return new PromotionRule(date, decimal.Parse(parts[1]), parts[2]);
            }).ToList();

    private static List<CartItem> ParseCartItems(List<string> lines) =>
        lines.Count == 0
            ? []
            : lines.Select(line =>
            {
                if (line.Split('*') is not [var qtyStr, var itemPart])
                    throw new FormatException($"購物車格式錯誤：{line}");

                if (itemPart.Split(':') is not [var name, var priceStr])
                    throw new FormatException($"商品價格格式錯誤：{itemPart}");

                return new CartItem(name, decimal.Parse(priceStr), int.Parse(qtyStr));
            }).ToList();

    private static DateOnly ParseCheckoutDate(string? line)
    {
        line = line?.Trim();
        if (string.IsNullOrWhiteSpace(line))
            return DateOnly.FromDateTime(DateTime.Today);

        return DateOnly.TryParseExact(line, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
            ? date
            : throw new FormatException($"結帳日期格式錯誤：{line}");
    }

    private static Coupon? ParseCoupon(string? raw)
    {
        var line = raw?.Trim();
        if (string.IsNullOrWhiteSpace(line)) return null;

        var parts = line.Split(' ', StringSplitOptions.TrimEntries);
        if (parts.Length != 3 || !DateOnly.TryParse(parts[0], out var expiry))
            throw new FormatException($"優惠券格式錯誤：{line}");

        return new Coupon(DateOnly.MinValue, expiry, decimal.Parse(parts[1]), decimal.Parse(parts[2]));
    }
}
