namespace EhCase.Api.Services.Products.Store;

public class ProductStore : IProductStore
{
    private readonly IProductClient _productClient;
    private List<Product> _products = [];

    public ProductStore(IProductClient productClient)
    {
        _productClient = productClient;
    }

    public List<Product> Products => _products;

    public async Task RefreshProducts(CancellationToken cancellationToken)
    {
        var response = await _productClient.GetAllProductsAsync(cancellationToken);

        var products = response.Select(x => new Product(x.Id, x.Name, x.Price, x.Size, x.Stars));
        _products = products.ToList();
    }
}
