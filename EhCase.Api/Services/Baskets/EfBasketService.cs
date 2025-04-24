using EhCase.Api.Context;
using EhCase.Api.Context.Entities;
using EhCase.Api.Services.Products;
using Microsoft.EntityFrameworkCore;

namespace EhCase.Api.Services.Baskets;

public class EfBasketService : IBasketService
{
    private readonly BasketContext _basketContext;
    private readonly IProductService _productService;

    public EfBasketService(BasketContext basketContext, IProductService productService)
    {
        _basketContext = basketContext;
        _productService = productService;
    }

    public async Task AddProductToBasket(AddProductToBasketRequest request, CancellationToken cancellationToken)
    {
        var basketEntry = new BasketEntry
        {
            ProductId = request.ProductId,
            BasketId = request.BasketId
        };

        _basketContext.Add(basketEntry);
        await _basketContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<CreateBasketResponse> CreateBasket(CancellationToken cancellationToken)
    {
        var basket = new Basket();

        _basketContext.Add(basket);
        await _basketContext.SaveChangesAsync(cancellationToken);

        var response = new CreateBasketResponse(basket.Id);
        return response;
    }

    public async Task DeleteProductFromBasket(DeleteProductFromBasketRequest request, CancellationToken cancellationToken)
    {
        var basketEntry = await _basketContext.BasketEntries
            .Where(x => x.Id == request.BasketEntryId)
            .FirstOrDefaultAsync(cancellationToken);

        if (basketEntry is null)
        {
            return;
        }

        _basketContext.BasketEntries.Remove(basketEntry);
        await _basketContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetBasketResponse> GetBasket(GetBasketRequest request, CancellationToken cancellationToken)
    {
        var productIds = await _basketContext.BasketEntries
            .Where(x => x.BasketId == request.BasketId)
            .Select(x => x.ProductId)
            .ToListAsync(cancellationToken);

        var products = await _productService.GetProducts(productIds, cancellationToken);

        var sum = products.Sum(x => x.Price);
        var productModels = products.Select(x => new GetBasketProductModel(
            x.Id,
            x.Price,
            x.Name,
            x.Size,
            x.Stars));

        var response = new GetBasketResponse(sum, products.Count(), productModels);
        return response;
    }
}
