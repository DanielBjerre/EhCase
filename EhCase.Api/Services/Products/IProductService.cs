namespace EhCase.Api.Services.Products;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetTop100RankedProducts(CancellationToken cancellationToken);
    public Task<IEnumerable<Product>> GetTop10CheapestProducts(CancellationToken cancellationToken);
    public Task<IEnumerable<Product>> GetProducts(IEnumerable<int> productIds, CancellationToken cancellationToken);
}
