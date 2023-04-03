using Dapper;
using LiquorStoreApi.Context;
using LiquorStoreApi.DTOs;
using LiquorStoreApi.Utilities;
using LiquorStoreApi.Wrappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LiquorStoreApi.Services.Iplementations
{
    public class ProductService : IProductService
    {
        private readonly LiquorStoreContext _context;
        private readonly IConfiguration _configuration;

        public ProductService(LiquorStoreContext contex, IConfiguration configuration)
        {
            _context = contex;
            _configuration = configuration;
        }

        public async Task<Response<object>> GetAll()
        {
            try
            {
                List<ProductDtoResponse> products = new();
                using SqlConnection con = new(_configuration.GetConnectionString("Connection"));
                con.Open();
                var coleccion = await con.QueryAsync<ProductDtoResponse>("dbo.GetProducts", commandType: CommandType.StoredProcedure);

                con.Close();

                return new Response<object>(true, coleccion);
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

        public async Task<Response<object>> GetById(int id)
        {

            using SqlConnection con = new(_configuration.GetConnectionString("Connection"));
            con.Open();
            var parametros = new DynamicParameters();

            parametros.Add("Id", id);

            var result = await con.QueryAsync<ProductDtoResponse>("dbo.GetProducts", param: parametros, commandType: CommandType.StoredProcedure);

            con.Close();
            return new Response<object>(true, result);
        }

        public async Task<Response<object>> Create(string email, ProductDtoRequest productDto)
        {
            var existUser = await _context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (existUser is null)
                return new Response<object>(false, "Usuario no encontrado.");

            if (productDto.Price < 1 || productDto.Stock < 1)
                return new Response<object>(false, "El Precio o la Cantidad no pueden ser inferior a 1.");

            try
            {
                using SqlConnection con = new(_configuration.GetConnectionString("Connection"));
                con.Open();
                var parametros = new DynamicParameters();

                parametros.Add("Name", productDto.Name);
                parametros.Add("UserId", existUser.Id);
                parametros.Add("CategoryId", productDto.CategoryId);
                parametros.Add("BrandId", productDto.BrandId);
                parametros.Add("Price", productDto.Price);
                parametros.Add("Stock", productDto.Stock);

                var resultQuery = await con.QueryAsync("dbo.InsertProduct", param: parametros, commandType: CommandType.StoredProcedure);

                con.Close();
                return new Response<object>(true, "Producto registrado con éxito!");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }
        public async Task<Response<object>> Update(int id, ProductDtoRequest productDto)
        {
            var existProduct = await _context.Products.FindAsync(id);
            if (existProduct is null)
                return new Response<object>(false, "Producto no encontrado.");

            try
            {
                using SqlConnection con = new(_configuration.GetConnectionString("Connection"));
                con.Open();
                var parametros = new DynamicParameters();

                parametros.Add("Id", id);
                parametros.Add("Name", productDto.Name);
                parametros.Add("CategoryId", productDto.CategoryId);
                parametros.Add("BrandId", productDto.BrandId);
                parametros.Add("Price", productDto.Price);
                parametros.Add("Stock", productDto.Stock);

                var resultQuery = await con.QueryAsync("dbo.UpdateProduct", param: parametros, commandType: CommandType.StoredProcedure);

                con.Close();
                return new Response<object>(true, "Producto actualizado con éxito!");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

        public async Task<Response<object>> DeleteById(int id)
        {
            var existProduct = await _context.Products.FindAsync(id);
            if (existProduct is null)
                return new Response<object>(false, "Producto no encontrado.");

            try
            {
                using SqlConnection con = new(_configuration.GetConnectionString("Connection"));
                con.Open();
                var parametros = new DynamicParameters();

                parametros.Add("Id", id);

                var resultQuery = await con.QueryAsync("dbo.DeleteProduct", param: parametros, commandType: CommandType.StoredProcedure);

                con.Close();
                return new Response<object>(true, "Producto eliminado con éxito!");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

    }
}
