using EhCase.Api.Services.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace EhCase.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPut]
    public async Task<ActionResult<CreateBasketResponse>> CreateBasket(CancellationToken cancellationToken)
    {
        var response = await _basketService.CreateBasket(cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetBasketResponse>> GetBasket(GetBasketRequest request, CancellationToken cancellationToken)
    {
        var response = await _basketService.GetBasket(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("Product")]
    public async Task<ActionResult> AddProductToBasket(AddProductToBasketRequest request, CancellationToken cancellationToken)
    {
        await _basketService.AddProductToBasket(request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("Product")]
    public async Task<ActionResult> DeleteProductFromBasket([FromQuery] DeleteProductFromBasketRequest request, CancellationToken cancellationToken)
    {
        await _basketService.DeleteProductFromBasket(request, cancellationToken);
        return NoContent();
    }


}
