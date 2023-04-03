using LiquorStoreApi.DTOs;
using LiquorStoreApi.Wrappers;

namespace LiquorStoreApi.Services
{
    public interface IProductService
    {
        Task<Response<object>> GetById(int id);
        Task<Response<object>> GetAll();
        Task<Response<object>> Create(string email, ProductDtoRequest productDto);
        Task<Response<object>> Update(int id, ProductDtoRequest productDto);
        Task<Response<object>> DeleteById(int id);
    }


}
