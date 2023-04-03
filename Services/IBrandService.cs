using LiquorStoreApi.Context.Entities;
using LiquorStoreApi.DTOs;
using LiquorStoreApi.Wrappers;

namespace LiquorStoreApi.Services
{
    public interface IBrandService
    {
        Task<Response<object>> GetById(int id);
        Task<IEnumerable<Brand>> GetAll();
        Task<Response<object>> Create(BrandDto brandDto);
        Task<Response<object>> Update(int id, BrandDto brandDto);
        Task<Response<object>> DeleteById(int id);
    }

}
