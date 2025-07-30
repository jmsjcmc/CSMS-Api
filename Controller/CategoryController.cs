using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Models;
using CSMapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSMapi.Controller
{
    public class CategoryController : BaseApiController
    {
        private readonly CategoryService _service;
        public CategoryController(AppDbContext context, IMapper mapper, CategoryService service) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all categories with optional filter for category name
        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryResponse>>> categorieslist(string? searchTerm = null)
        {
            try
            {
                var response = await _service.categorieslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific category
        [HttpGet("category/{id}")]
        public async Task<ActionResult<CategoryResponse>> getcategory(int id)
        {
            try
            {
                var response = await _service.getcategory(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new category
        [HttpPost("category")]
        public async Task<ActionResult<CategoryResponse>> createcategory([FromBody] CategoryRequest request)
        {
            try
            {
                var response = await _service.createcategory(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific category
        [HttpPatch("category/update/{id}")]
        public async Task<ActionResult<CategoryResponse>> updatecategory([FromBody] CategoryRequest request, int id)
        {
            try
            {
                var response = await _service.updatecategory(request, id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove category without deleting in database (soft delete)
        [HttpPatch("category/hide/{id}")]
        public async Task<ActionResult<CategoryResponse>> removecategory(int id)
        {
            try
            {
                var response = await _service.removecategory(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific category from database
        [HttpDelete("category/delete/{id}")]
        public async Task<ActionResult<CategoryResponse>> deletecategory(int id)
        {
            try
            {
                var response = await _service.deletecategory(id);   
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
