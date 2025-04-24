namespace EhCase.Api.Services.Baskets;

public interface IBasketService
{
    public Task<CreateBasketResponse> CreateBasket(CancellationToken cancellationToken);
    public Task AddProductToBasket(AddProductToBasketRequest request, CancellationToken cancellationToken);
    public Task DeleteProductFromBasket(DeleteProductFromBasketRequest request, CancellationToken cancellationToken);
    public Task<GetBasketResponse> GetBasket(GetBasketRequest request, CancellationToken cancellationToken);
}
