using Application.DTOs.ProductDTOs;
using Application.Interfaces.IProductInterface;
using Domain.Entities.ProductEntites;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Market.Modules.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repoProd;
        private readonly IProductImageRepository _repoProdImage;
        private readonly ICategoryRepository _repoCategory;

        private readonly IMinioService _minioService;
        public ProductController(
            IProductRepository repoProd,
            IProductImageRepository repoProdImage,
            ICategoryRepository repoCategory,

            IMinioService minioService
            )
        {
            _repoProd = repoProd;
            _repoProdImage = repoProdImage;
            _repoCategory = repoCategory;

            _minioService = minioService;
        }

        [HttpPost("create_category")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto)
        {
            var newCategory = new Category { Name = dto.Name, Description = dto.Description };
            await _repoCategory.SaveAsync(newCategory);
            return Ok();
        }

        [HttpPost("create_product")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
        {
            var newProd = new Product
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };
            await _repoProd.SaveAsync(newProd);
            return Ok(product);
        }

        [HttpPost("upload-images")]
        public async Task<IActionResult> UploadImages(
            [FromForm] UploadMultipleProductImagesDto dto)
        {
            var product = await _repoProd.GetByIdAsync(dto.ProductId);

            var uploadedUrls = await _minioService.UploadProductImagesAsync(dto.ProductId, dto.ProductScheme, dto.Files);
            
            var existingImages = await _repoProdImage.GetByProductIdAsync(product.Id);
            bool hasMainImage = existingImages.Any(img => img.IsMain);

            var savedImages = new List<ProductImage>();
            int startSortOrder = existingImages.Any() ? existingImages.Max(img => img.SortOrder) + 1 : 0;

            for (int i = 0; i < uploadedUrls.Count; i++)
            {
                var productImage = new ProductImage
                {
                    ImageUrl = uploadedUrls[i],
                    IsMain = !hasMainImage && i == 0,
                    SortOrder = startSortOrder + i,
                    ProductId = product.Id,
                };

                savedImages.Add(productImage);
                await _repoProdImage.SaveAsync(productImage);
            }


            return Ok(uploadedUrls);
        }
    }
}
