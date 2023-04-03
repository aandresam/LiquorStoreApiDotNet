using LiquorStoreApi.Context;
using LiquorStoreApi.Context.Entities;
using LiquorStoreApi.DTOs;
using LiquorStoreApi.Utilities;
using LiquorStoreApi.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace LiquorStoreApi.Services.Iplementations
{
    public class BrandService : IBrandService
    {
        private readonly LiquorStoreContext _context;

        public BrandService(LiquorStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Response<object>> GetById(int id)
        {
            var existingBrand = await _context.Brands.FindAsync(id);

            if (existingBrand is null)
                return new Response<object>(false, $"No existe la marca con id: {id}");

            return new Response<object>(true, existingBrand);
        }

        public async Task<Response<object>> Create(BrandDto brandDto)
        {
            if (_context.Brands.Any(b =>
                    b.Name.ToLower().Trim().Trim() == brandDto.Name.ToLower().Trim().Trim()
                    )
                )
                return new Response<object>(false, $"La Marca '{brandDto.Name}' ya existe.");

            try
            {
                Brand brand = new()
                {
                    Name = brandDto.Name
                };
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return new Response<object>(true, $"La marca '{brand.Name}' fue creada con éxito.");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

        public async Task<Response<object>> Update(int id, BrandDto brandDto)
        {
            if (_context.Brands.Any(b =>
                    b.Name.ToLower().Trim().Trim() == brandDto.Name.ToLower().Trim().Trim()
                    )
                )
                return new Response<object>(false, $"La Marca '{brandDto.Name}' ya existe.");

            var existingBrand = await _context.Brands.FindAsync(id);

            if (existingBrand is null)
                return new Response<object>(false, $"No existe la marca con id: {id}");

            try
            {
                existingBrand.Name = brandDto.Name;
                await _context.SaveChangesAsync();
                return new Response<object>(true, "Marca actualizada con éxito.");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

        public async Task<Response<object>> DeleteById(int id)
        {
            var existingBrand = await _context.Brands.FindAsync(id);

            if (existingBrand is null)
                return new Response<object>(false, $"No existe la marca con id: {id}");

            try
            {
                _context.Brands.Remove(existingBrand);
                await _context.SaveChangesAsync();
                return new Response<object>(true, "Marca eliminada con éxito.");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

    }
}
