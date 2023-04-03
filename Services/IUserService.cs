using LiquorStoreApi.DTOs;
using LiquorStoreApi.Wrappers;

namespace LiquorStoreApi.Services
{

    public interface IUserService
    {

        Response<object> GetProfileData(string email);

        Task<Response<object>> UpdateProfileData(string email, UserDto userDto);

        Task<Response<object>> DeleteAccount(string email);

        Task<Response<object>> UpdatePassword(string email, PasswordUpdateDto passwordDto);

    }



}
