using EhCase.Api.Services;
using System.Net.Http.Headers;

namespace EhCase.Api.ProductClientService;

public class ProductClientDelegatingHandler(ProductClient productClient, IConfiguration configuration) : DelegatingHandler
{
    private string? _token;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", await GetToken(cancellationToken));
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetToken(CancellationToken cancellationToken)
    {
        if (_token is not null)
        {
            return _token;
        }

        var loginRequest = new LoginRequest { Email = configuration["ProductApi:Email"] };
        var response = await productClient.LoginAsync(loginRequest, cancellationToken);
        return response.Token;
    }
}
