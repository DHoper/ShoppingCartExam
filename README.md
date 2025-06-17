# 🛒 ShoppingCartExam

這是一個 ASP.NET Core Web API 專案，用於計算購物車結帳金額，並包含單元測試。

---

## 🚀 如何使用

1. 啟動專案：

2. 開啟瀏覽器造訪：

   ```
   https://localhost:7144/swagger
   ```
3. 使用 Swagger 的API測試功能，輸入購物車文本。

---

## 🧪 Swagger 測試方法

### ✅ API Endpoint

```
POST /api/checkout/calculate
Content-Type: text/plain
```

### 使用方式

![image](https://github.com/user-attachments/assets/080f6501-b9e5-47b3-905f-33250647dfe8)

1. 展開 Swagger 中的 `Checkout` 項目  
2. 點擊 `Try it out`  
3. 在 Request body 中貼上下方 A 或 B 範例  
4. 點選 Execute 執行並查看回應金額結果

---

## 📂 測試輸入範例

### ✅ Case A：含促銷與優惠券

```
2015.11.11|0.7|電子

1*ipad:2399.00
1*螢幕:1799.00
12*啤酒:25.00
5*麵包:9.00

2015.11.11
2016.3.2 1000 200

```

**預期輸出：**

```
3083.60
```

---

### ✅ Case B：無促銷、無優惠券

```


3*蔬菜:5.98
8*餐巾紙:3.20

2015.01.01


```

**預期輸出：**

```
43.54
```

---

## 🧪 單元測試

本專案使用 [xUnit](https://xunit.net/) 撰寫單元測試，測試項目包括：

- ✅ `InputParserTests.cs`：驗證文字輸入解析為物件的正確性
- ✅ `CheckoutServiceTests.cs`：驗證購物車邏輯計算 的正確性

### 執行測試

請在根目錄執行：

```bash
dotnet test
```

---

## 📁 專案目錄結構簡介

- `Controllers/`：API 控制器  
- `Domain/`：業務模型與邏輯
- `Utils/`：文本輸入解析工具 `InputParser`，將輸入文本解析為相關購物車模型  
- `Interfaces/`：存放介面  
- `Infrastructure/`：實作類別，如 `ProductCategoryLookup`  
