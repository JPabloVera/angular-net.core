using AutoMapper;
using Token_API.Dtos;
using Token_API.Models;

namespace Token_API;

public class UserMapper : Profile {
    public UserMapper() {
        CreateMap<User, AuthenticationDTO>().ReverseMap();
    }
}