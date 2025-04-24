namespace EhCase.Api.Context.Entities;

public class BasketEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required int ProductId { get; set; }

    public Guid BasketId { get; set; }
    public Basket? Basket { get; set; }
}
