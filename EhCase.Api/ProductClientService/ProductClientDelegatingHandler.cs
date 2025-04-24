using EhCase.Api.Services;
using System.Net.Http.Headers;

namespace EhCase.Api.ProductClientService;

public class ProductClientDelegatingHandler : DelegatingHandler
{
    public ProductClientDelegatingHandler(HttpClient httpClient, IConfiguration configuration)
    {
        _productClient = httpClient;
        _configuration = configuration;
    }

    private readonly string? _token;
    private readonly HttpClient _productClient;
    private readonly IConfiguration _configuration;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken(cancellationToken));
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetToken(CancellationToken cancellationToken)
    {
        if (_token is not null)
        {
            return _token;
        }

        var loginRequest = new LoginRequest { Email = _configuration["ProductApi:Email"] };
        var response = await _productClient.PostAsJsonAsync("api/login", loginRequest, cancellationToken);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: cancellationToken);
        return loginResponse!.Token;
    }
}
