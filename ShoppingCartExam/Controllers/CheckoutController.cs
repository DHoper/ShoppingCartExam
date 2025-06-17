using Microsoft.AspNetCore.Mvc;
using ShoppingCartExam.Interfaces;
using ShoppingCartExam.Utils;

namespace ShoppingCartExam.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController(
    ICheckoutService checkoutService,
    IProductCategoryLookup categoryLookup) : ControllerBase
{
    private readonly ICheckoutService _checkoutService = checkoutService;
    private readonly IProductCategoryLookup _categoryLookup = categoryLookup;

    /// <summary>
    /// �p���ʪ��`���B
    /// </summary>
    [HttpPost("calculate")]
    [Consumes("text/plain")]
    public IActionResult CalculateTotal([FromBody] string input)
    {
        try
        {
            var parsed = InputParser.ParseInput(input);

            var total = _checkoutService.CalculateTotal(
                parsed.Cart,
                parsed.CheckoutDate,
                parsed.Promotions,
                parsed.Coupon
            );

            return Ok(total);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest($"�޿���~�G{ex.Message}");
        }
        catch (FormatException ex)
        {
            return BadRequest($"�榡���~�G{ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"�������~�G{ex.Message}");
        }
    }
}
