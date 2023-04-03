using LiquorStoreApi.Context.Entities;
using LiquorStoreApi.DTOs;
using LiquorStoreApi.Wrappers;

namespace LiquorStoreApi.Services
{
    public interface ICategoryService
    {
        Task<Response<object>> GetById(int id);
        Task<IEnumerable<Category>> GetAll();
        Task<Response<object>> Create(CategoryDto categoryDto);
        Task<Response<object>> Update(int id, CategoryDto category);
        Task<Response<object>> DeleteById(int id);
    }

}
