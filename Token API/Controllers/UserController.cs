
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class UserController : ControllerBase {
        
        private Context _context;
        private readonly IMapper _mapper;
        private readonly JWTService _tokenService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        
        public UserController(Context context, IMapper mapper, JWTService token_service) {
            _context = context;
            _mapper = mapper;
            _tokenService = token_service;
        }
        
        [MapToApiVersion(1)]
        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login(AuthenticationDTO data) {
            try {
                _logger.Info("UserController.Login Started");
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == data.Email);

                if (user == null)
                    return BadRequest(new {
                        message = "Invalid Values"
                    });

                var evaluate = HashingService.Verify(data.Password, user.Password, user.Salt);

                if (evaluate == false)
                    return BadRequest(new {
                        message = "Invalid Values"
                    });
                    
                var token = _tokenService.CreateToken(user);

                var response = new UserResponse {
                    Email = user.Email,
                    Token = token
                };

                return Ok(response);
            }
            catch (Exception ex) {
                _logger.Error(ex, "UserController.Login Error");
                return StatusCode(500, new { message = "Service Unavailable"});
            }
        }
        
        [Authorize]
        [MapToApiVersion(1)]
        [HttpPost("renew-token")]
        public async Task<ActionResult<UserResponse>> RenewToken(UserEmailDTO data) {
            try {
                _logger.Info("UserController.RenewToken Started");
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == data.Email);

                if (user == null)
                    return BadRequest(new {
                        message = "Invalid Values"
                    });


                var token = _tokenService.CreateToken(user);
                var response = new UserResponse {
                    Email = user.Email,
                    Token = token
                };

                return Ok(response);
            }
            catch (Exception ex) {
                _logger.Error(ex, "UserController.RenewToken Error");
                return StatusCode(500, new { message = "Service Unavailable"});
            }
        }

        [MapToApiVersion(1)]
        [HttpPost("Register")]
        public async Task<ActionResult> Register(AuthenticationDTO data) {
            try {
                _logger.Info("UserController.Register Started");
                
                var query = await _context.Users.FirstOrDefaultAsync(user => user.Email == data.Email);
                
                if (query != null)
                    return BadRequest(new {
                        message = "User with that email already exist."
                    });
                
                var hash = HashingService.Hash(data.Password, out var salt);
            
                var user = new User {
                    Email = data.Email,
                    Salt = salt,
                    Password = hash
                };
                
                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex) {
                _logger.Error(ex, "UserController.Register Error");
                return StatusCode(500, new { message = "Service Unavailable"});
            }
        }
    }
}
