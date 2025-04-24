namespace EhCase.Api.Services.Products.Store;

public interface IProductStore
{
    public Task RefreshProducts(CancellationToken cancellationToken);
    public List<Product> Products { get; }
}
