using E_CommerceAPI.DTOs;
using E_CommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductServices productServices;
        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = new GeneralResponse<List<ProductReadDTO>>()
            {
                Data = productServices.GetAll(),
                IsSucceeded = true,
                Messsage = "The data returned successfully"
            };
            return Ok(response);
        }

        [HttpGet("GetById/{id:int}")]
        public IActionResult GetById(int id)
        {
            var product = productServices.GetById(id);
            var response = new GeneralResponse<ProductReadDTO>();
            if (product == null)
            {
                response.IsSucceeded = false;
                response.Data = null;
                response.Messsage = "This id is invalid";
                return NotFound(response);
            }
            response.IsSucceeded = true;
            response.Data = product;
            response.Messsage = "The data returned successfully";
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(ProductCreateDTO productCreateDTO)
        {
            var result = productServices.Add(productCreateDTO);
            var response = new GeneralResponse<ProductCreateDTO>();
            if (result == false)
            {
                response.Data = null;
                response.IsSucceeded = false;
                response.Messsage = "The category id is invalid";
                return BadRequest(response);
            }
            
                response.Data = productCreateDTO;
                response.IsSucceeded = true;
                response.Messsage = "The data was added successfully";
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id:int}")]
        public IActionResult Update(ProductCreateDTO productCreateDTO, int id)
        {
            var result = productServices.Update(productCreateDTO, id);
            var response = new GeneralResponse<ProductCreateDTO>();
            if (result == true)
            {
                response.IsSucceeded = true;
                response.Data = productCreateDTO;
                response.Messsage = "The data updated successfully";
                return Ok(response);
            }
            response.IsSucceeded = false;
            response.Data = null;
            response.Messsage = "This id is invalid";
            return NotFound(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = productServices.Delete(id);
            var response = new GeneralResponse<ProductCreateDTO>();
            if (result == true)
            {
                response.IsSucceeded = true;
                response.Data = null;
                response.Messsage = "The data deleted successfully";
                return Ok(response);
            }
            response.IsSucceeded = false;
            response.Data = null;
            response.Messsage = "This id is invalid";
            return NotFound(response);
        }
        [HttpGet("Search/{name}")]
        public IActionResult Search(string name)
        {
            var response = new GeneralResponse<List<ProductReadDTO>>()
            {
                Data = productServices.Search(name),
                IsSucceeded = true,
                Messsage = "Search results"
            };
            return Ok(response);
        }
        [HttpGet("FilterByPrice")]
        public IActionResult FilterByPrice(decimal min, decimal max)
        {
            var response = new GeneralResponse<List<ProductReadDTO>>()
            {
                Data = productServices.FilterByPrice(min,max),
                IsSucceeded = true,
                Messsage = "Search results"
            };
            return Ok(response);
        }
        [HttpGet("GetProductsByCategory/{categoryId:int}")]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            var response = new GeneralResponse<List<ProductReadDTO>>()
            {
                Data = productServices.GetProductsByCategory(categoryId),
                IsSucceeded = true,
                Messsage = "Search results"
            };
            return Ok(response);
        }
        [HttpPut("Restore/{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Restore(int id)
        {
            var result = productServices.Restore(id);
            var response = new GeneralResponse<ProductCreateDTO>();
            if (result == true)
            {
                response.IsSucceeded = true;
                response.Data = null;
                response.Messsage = "The data restored successfully";
                return Ok(response);
            }
            response.IsSucceeded = false;
            response.Data = null;
            response.Messsage = "This id is invalid";
            return NotFound(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateStock")]
        public IActionResult UpdateStock(int id, int stock)
        {
            var result = productServices.UpdateStock(id,stock);
            var response = new GeneralResponse<ProductCreateDTO>();
            if (result == true)
            {
                response.IsSucceeded = true;
                response.Data = null;
                response.Messsage = "The data updated successfully";
                return Ok(response);
            }
            response.IsSucceeded = false;
            response.Data = null;
            response.Messsage = "This id is invalid";
            return NotFound(response);
        }
    }
}
