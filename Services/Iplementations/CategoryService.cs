using LiquorStoreApi.Context.Entities;
using LiquorStoreApi.Context;
using LiquorStoreApi.DTOs;
using LiquorStoreApi.Wrappers;
using Microsoft.EntityFrameworkCore;
using LiquorStoreApi.Utilities;

namespace LiquorStoreApi.Services.Iplementations
{
    public class CategoryService : ICategoryService
    {
        private readonly LiquorStoreContext _context;

        public CategoryService(LiquorStoreContext context)
        {
            _context = context;
        }

        public async Task<Response<object>> GetById(int id)
        {
            var existCategory = await _context.Categorys.FindAsync(id);

            if (existCategory is null)
                return new Response<object>(false, $"No existe la categoria con id: {id}");

            return new Response<object>(true, existCategory);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categorys.ToListAsync();
        }

        public async Task<Response<object>> Create(CategoryDto categoryDto)
        {
            if (_context.Categorys.Any(c =>
                    c.Name.ToLower().Trim().Trim() == categoryDto.Name.ToLower().Trim().Trim()
                    )
                )
                return new Response<object>(false, $"La categoria '{categoryDto.Name}' ya existe.");

            try
            {
                Category category = new()
                {
                    Name = categoryDto.Name
                };

                _context.Add(category);
                await _context.SaveChangesAsync();
                return new Response<object>(true, $"Categoría '{category.Name}' creada con éxito.");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

        public async Task<Response<object>> Update(int id, CategoryDto category)
        {

            if (_context.Categorys.Any(c =>
                    c.Name.ToLower().Trim().Trim() == category.Name.ToLower().Trim().Trim()
                    )
                )
                return new Response<object>(false, $"La categoria '{category.Name}' ya existe.");

            var existingCategory = await _context.Categorys.FindAsync(id);

            if (existingCategory is null)
                return new Response<object>(false, $"La categoria con id {id} no fue encontrada.");

            try
            {
                existingCategory.Name = category.Name;
                await _context.SaveChangesAsync();
                return new Response<object>(true, $"Categoria actualizada con éxito.");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

        public async Task<Response<object>> DeleteById(int id)
        {
            var existingCategory = await _context.Categorys.FindAsync(id);

            if (existingCategory is null)
                return new Response<object>(false, $"La categoria con id {id} no fue encontrada.");

            try
            {
                _context.Categorys.Remove(existingCategory);
                await _context.SaveChangesAsync();
                return new Response<object>(true, $"Categoría eliminada con éxito.");
            }
            catch (Exception ex)
            {
                return Utils.InexpectedError(ex);
            }
        }

    }
}
