using LiquorStoreApi.DTOs;
using LiquorStoreApi.Wrappers;

namespace LiquorStoreApi.Services
{

    public interface IAuthenticationService
    {
        Response<object> Login(LoginDto loginDto);

        Task<Response<object>> Register(RegisterDto registerDto);
    }

}
