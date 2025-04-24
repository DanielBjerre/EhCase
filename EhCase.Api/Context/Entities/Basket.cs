namespace EhCase.Api.Context.Entities;

public class Basket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ICollection<BasketEntry> BasketEntries { get; set; } = [];
}
