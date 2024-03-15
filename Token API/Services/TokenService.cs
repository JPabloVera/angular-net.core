using NLog;
using Token_API.Models;

namespace Token_API.Services;

public class TokenService{
    private readonly IHttpClientFactory _httpClient;
    private string api_key = Environment.GetEnvironmentVariable("ASPNETCORE_APIKEY");
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private TaskLimiter TaskLimiter;
    public TokenService(IHttpClientFactory httpClient) {
        _httpClient = httpClient;
        var client = _httpClient.CreateClient("bscs");
        TaskLimiter = new TaskLimiter(5, TimeSpan.FromSeconds(2.5f), client);
    }
    
    public async Task<HTTPResponse.TokenData> GetTotalSupply(string token)
    {
        try {
            _logger.Info("TokenService.GetTotalSupply called");
            var response = await TaskLimiter
                .FetchAsync("api?module=stats" +
                            "&action=tokensupply" +
                            $"&contractaddress={token}" +
                            $"&apikey={api_key}");

            if (response.IsSuccessStatusCode == false)
                return null;

            var data = await response.Content.ReadFromJsonAsync<HTTPResponse.TokenData>();
            return data;
        }
        catch (Exception ex) {
            _logger.Error(ex,"TokenService.GetTotalSupply error");
            return null;
        }
    }
    
    private async Task<HTTPResponse.TokenData> GetWalletTokenAmount(string token, string wallet) {
        try {
            _logger.Info("TokenService.GetWalletTokenAmount called");
            var response = await TaskLimiter
                .FetchAsync("api?module=account" +
                            "&action=tokenbalance" +
                            $"&contractaddress={token}" +
                            $"&apikey={api_key}" +
                            $"&address={wallet}" +
                            $"&tag=latest");

            if (response.IsSuccessStatusCode == false)
                return null;

            var data = await response.Content.ReadFromJsonAsync<HTTPResponse.TokenData>();
            return data;
        }
        catch(Exception ex){
            _logger.Error(ex,"TokenService.GetWalletTokenAmount error");
            return null;
        }
    }

    public async Task<List<HTTPResponse.TokenData>> GetMultipleWalletsData(IEnumerable<string> keys, string token) {
        var result = new List<HTTPResponse.TokenData>();
        
        foreach (var key in keys) {
            var wallet_data = await GetWalletTokenAmount(token, key);
                
            if (wallet_data?.status == "1")
                result.Add(wallet_data);
        }

        return result;
    }
}