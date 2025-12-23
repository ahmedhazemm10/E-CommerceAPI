using E_CommerceAPI.DTOs;
using E_CommerceAPI.Models;
using E_CommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _services;

        public CategoryController(ICategoryServices services)
        {
            _services = services;
        }
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = new GeneralResponse<List<CategoryDTO>>()
            {
                Data = _services.GetAll(),
                IsSucceeded = true,
                Messsage = "The data returned successfully"
            };
            return Ok(response);
        }

        [HttpGet("GetById/{id:int}")]
        public IActionResult GetById(int id)
        {
            var cat = _services.GetById(id);
            var response = new GeneralResponse<CategoryDTO>();
            if (cat == null)
            {
                response.IsSucceeded = false;
                response.Data = null;
                response.Messsage = "This id is invalid";
                return NotFound(response);
            }
            response.IsSucceeded = true;
            response.Data = cat;
            response.Messsage = "The data returned successfully";
            return Ok(response);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Add(CategoryDTO categoryDTO)
        {
            _services.Add(categoryDTO);
            var response = new GeneralResponse<CategoryDTO>()
            {
                Data = categoryDTO,
                IsSucceeded = true,
                Messsage = "The data was added successfully"
            };
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id:int}")]
        public IActionResult Update(CategoryDTO categoryDTO, int id)
        {
            var result = _services.Update(categoryDTO, id);
            var response = new GeneralResponse<CategoryDTO>();
            if (result == true)
            {
                response.IsSucceeded = true;
                response.Data = categoryDTO;
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
            var result = _services.Delete(id);
            var response = new GeneralResponse<CategoryDTO>();
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
            var response = new GeneralResponse<List<CategoryDTO>>()
            {
                Data = _services.Search(name),
                IsSucceeded = true,
                Messsage = "Search results"
            };
            return Ok(response);
        }
    }
}
