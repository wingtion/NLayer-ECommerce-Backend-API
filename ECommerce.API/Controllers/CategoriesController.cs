using AutoMapper;
using ECommerce.API.Controllers;
using ECommerce.Core.DTOs;
using ECommerce.Core.Services;
using ECommerce.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.API.Controllers
{
    [Authorize] // Korumayı açıyoruz (Token şart)
    public class CategoriesController : CustomBaseController
    {
        private readonly IService<Category> _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(IService<Category> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var newCategory = await _categoryService.AddAsync(category);
            var newCategoryDto = _mapper.Map<CategoryDto>(newCategory);
            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, newCategoryDto));
        }

        
    }
}