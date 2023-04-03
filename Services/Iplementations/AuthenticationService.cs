using LiquorStoreApi.Context.Entities;
using LiquorStoreApi.Context;
using LiquorStoreApi.DTOs;
using System.Text.RegularExpressions;
using LiquorStoreApi.Wrappers;
using LiquorStoreApi.Utilities;

namespace LiquorStoreApi.Services.Iplementations
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly LiquorStoreContext _context;

        private JwtService _jwtService;

        public AuthenticationService(LiquorStoreContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public Response<object> Login(LoginDto loginDto)
        {
            var existUser = _context.Users.SingleOrDefault(u => u.Email.ToLower() == loginDto.Email.ToLower());
            

            if (existUser is null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, existUser.Password))
                return new Response<object>(false, "Credenciales incorrectas!");

            string token = _jwtService.GenerateToken(existUser);

            return new Response<object>(true, $"Bearer {token}");
        }

        public async Task<Response<object>> Register(RegisterDto registerDto)
        {

            string regExpEmail = "^[a-zA-Z0-9._-]{2,}@[a-zA-Z0-9._-]{2,}\\.[a-zA-Z0-9._-]{2,}$";

            Regex regex = new(regExpEmail);

            if (!regex.IsMatch(registerDto.Email))
                return new Response<object>(false, "El email debe tener un formato válido.");

            if (registerDto.Password.Length < 8)
                return new Response<object>(false, "La contraseña debe ser mayor a 8 caracteres.");

            if (_context.Users.Any(u => u.Email == registerDto.Email))
                return new Response<object>(false, $"El email {registerDto.Email} ya existe.");

            string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            try
            {
                User user = new()
                {
                    Email = registerDto.Email,
                    Password = encryptedPassword
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return new Response<object>(true, "Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }
    }
}
