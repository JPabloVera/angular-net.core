using Token_API.Models;

namespace Token_API;

public static class TokenHelper{
    public static decimal GetNonCirculatingSupply(List<HTTPResponse.TokenData> data) {
        decimal result = 0;

        if (data == null) return 0;
        
        foreach (var wallet in data) {
            var try_parse = decimal.TryParse(wallet.result, out var amount);

            if (try_parse) result += amount;
        }
        
        return result;
    }
}