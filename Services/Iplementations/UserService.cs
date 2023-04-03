using LiquorStoreApi.Context;
using LiquorStoreApi.DTOs;
using LiquorStoreApi.Utilities;
using LiquorStoreApi.Wrappers;

namespace LiquorStoreApi.Services.Iplementations
{
    public class UserService : IUserService
    {

        private readonly LiquorStoreContext _context;

        public UserService(LiquorStoreContext context)
        {
            _context = context;
        }

        public Response<object> GetProfileData(string email)
        {
            var existUser = _context.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (existUser is null)
                return new Response<object>(false, "Usuario no encontrado.");

            return new Response<object>(true, existUser);
        }

        public async Task<Response<object>> UpdateProfileData(string email, UserDto userDto)
        {
            var existUser = _context.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (existUser is null)
                return new Response<object>(false, "Usuario no encontrado.");

            try
            {
                existUser.Name = userDto.Name;
                existUser.LastName = userDto.LastName;
                existUser.PhoneNumber = userDto.PhoneNumber;
                await _context.SaveChangesAsync();
                return new Response<object>(true, "Usuario actualizado con éxito!");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

        public async Task<Response<object>> DeleteAccount(string email)
        {
            var existUser = _context.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (existUser is null)
                return new Response<object>(false, "Usuario no encontrado.");

            try
            {
                this._context.Users.Remove(existUser);
                await this._context.SaveChangesAsync();
                return new Response<object>(true, "Usuario eliminado con éxito!");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

        public async Task<Response<object>> UpdatePassword(string email, PasswordUpdateDto passwordDto)
        {
            var existUser = _context.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (existUser is null)
                return new Response<object>(false, "Usuario no encontrado.");

            if (!BCrypt.Net.BCrypt.Verify(passwordDto.CurrentPassword, existUser.Password))
                return new Response<object>(false, "La contraseña ingresada no coincide con la contraseña actual.");

            try
            {
                existUser.Password = BCrypt.Net.BCrypt.HashPassword(passwordDto.NewPassword);
                await _context.SaveChangesAsync();
                return new Response<object>(true, "Contraseña actualizada con éxito!");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }
    }
}
