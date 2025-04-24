using EhCase.Api.Services.Products.Store;

namespace EhCase.Api.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductStore _productStore;

    public ProductService(IProductStore productStore)
    {
        _productStore = productStore;
    }

    public Task<IEnumerable<Product>> GetProducts(IEnumerable<int> productIds, CancellationToken cancellationToken)
    {
        var products = _productStore.Products
            .Where(x => productIds.Contains(x.Id));

        return Task.FromResult(products);
    }

    public Task<IEnumerable<Product>> GetTop100RankedProducts(CancellationToken cancellationToken)
    {
        var products = _productStore.Products
            .OrderByDescending(x => x.Stars)
            .Take(100);

        return Task.FromResult(products);
    }

    public Task<IEnumerable<Product>> GetTop10CheapestProducts(CancellationToken cancellationToken)
    {
        var products = _productStore.Products
            .OrderBy(x => x.Price)
            .Take(10);

        return Task.FromResult(products);
    }
}
