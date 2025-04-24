using EhCase.Api.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EhCase.Api.Context;

public class BasketContext : DbContext
{
    public BasketContext(DbContextOptions<BasketContext> options) : base(options) 
    {

    }

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketEntry> BasketEntries { get; set; }
}
