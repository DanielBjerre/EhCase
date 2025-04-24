namespace EhCase.Api.Services.Baskets;

public record CreateBasketResponse(Guid Id);

public record AddProductToBasketRequest(Guid BasketId, int ProductId);

public record DeleteProductFromBasketRequest(Guid BasketEntryId);



public record GetBasketRequest(Guid BasketId);
public record GetBasketResponse(double TotalPrice, int AmountOfProducts, IEnumerable<GetBasketProductModel> Entries);
public record GetBasketProductModel(int Id, double Price, string Name, int Size, int Stars);


