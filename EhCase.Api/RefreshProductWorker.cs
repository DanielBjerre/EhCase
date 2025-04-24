
using EhCase.Api.Services.Products.Store;

namespace EhCase.Api;

public class RefreshProductWorker : BackgroundService
{
    private readonly IProductStore _productStore;
    private readonly TimeSpan _timeBetweenProductRefresh = TimeSpan.FromMinutes(5);

    public RefreshProductWorker(IProductStore productStore)
    {
        _productStore = productStore;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_timeBetweenProductRefresh);
        do
        {
            await _productStore.RefreshProducts(stoppingToken);
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}
