using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Token_API.Data;
using Token_API.Dtos;
using Token_API.Models;
using Token_API.Services;

namespace Token_API.Controllers
{

    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class TokenController : ControllerBase
    {
        TokenService TService;
        private Context _context;
        private IMapper _mapper;
        
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string token;
        private readonly List<string> wallet_addresses;
        private readonly string token_name;

        public TokenController(IHttpClientFactory _httpClient, Context context, IMapper mapper, IConfiguration config) {
            TService = new TokenService(_httpClient);
            _context = context;
            _mapper = mapper;
            
            token = config["TokenKey"];
            wallet_addresses = config.GetSection("WalletAddresses")?.Get<List<string>>();
            token_name = config["TokenName"];
        }
        
        [MapToApiVersion(1)]
        [HttpGet("get-data")]
        public async Task<ActionResult<Token>> GetData() {
            try {
                
                _logger.Info("TokenController.GetData Started");
                var query = _context.Tokens.OrderByDescending(x => x.Date).FirstOrDefault();

                if (query == null)
                    return NoContent();

                var dto = _mapper.Map<TokenDTO>(query);

                return Ok(dto);
            }
            catch (Exception ex) {
                _logger.Error(ex, "TokenController.GetData Error");
                return StatusCode(500, new { message = "Service Unavailable"});
            }
        }
        
        [Authorize]
        [MapToApiVersion(1)]
        [HttpPost("update-data")]
        public async Task<ActionResult<Token>> UpdateData() {
            try {
                _logger.Info( "TokenController.UpdateData Called");
                var token_supply = await TService.
                    GetTotalSupply(token);
            
                if (token_supply == null)
                    return StatusCode(500, new { message = "Unable to fetch data"});


                var try_parse_total = decimal.TryParse(token_supply.result, out var total_supply);
            
                if (try_parse_total == false)
                    return StatusCode(500, new { message = "Service Unavailable"});
            
                var wallets_tokens = await TService.GetMultipleWalletsData(wallet_addresses, token);
            
                var non_circulating_supply = TokenHelper.GetNonCirculatingSupply(wallets_tokens);
            
            
                var token_data = new Token {
                    Name = token_name,
                    TotalSupply = total_supply.ToString(),
                    CirculatingSupply = (total_supply - non_circulating_supply).ToString(),
                    Date = DateTime.Today
                };

                _context.Tokens.Add(token_data);

                await _context.SaveChangesAsync();
            
                return Ok(token_data);
            }
            catch (Exception ex){
                _logger.Error(ex, "TokenController.UpdateData Error");
                return StatusCode(500, new { message = "Service Unavailable"});
            }
        }
        
    }
}
