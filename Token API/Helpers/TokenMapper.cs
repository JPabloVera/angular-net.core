using AutoMapper;
using Token_API.Dtos;
using Token_API.Models;

namespace Token_API;

public class TokenMapper : Profile {
    public TokenMapper() {
        CreateMap<Token, TokenDTO>().ReverseMap();
    }
}