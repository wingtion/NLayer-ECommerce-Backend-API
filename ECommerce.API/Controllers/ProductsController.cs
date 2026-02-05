using AutoMapper;
using ECommerce.API.Controllers; 
using ECommerce.Core.DTOs;
using ECommerce.Core;
using ECommerce.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.API.Controllers
{
    [Authorize] // BU SATIR KAPIYI KİLİTLER
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IService<Product> _service;
        private readonly IMapper _mapper;

        public ProductsController(IService<Product> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());

            // Veriyi "Zarf"ın içine koyup gönderiyoruz.
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var newProduct = await _service.AddAsync(product);
            var newProductDto = _mapper.Map<ProductDto>(newProduct);

            // 201 Created dönerken:
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, newProductDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _service.UpdateAsync(product);

            // Güncelleme başarılı ama veri dönmüyoruz (204)
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);

            // Silme başarılı (204)
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}