using E_CommerceAPI.DTOs;
using E_CommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices cartServices;

        public CartController(ICartServices cartServices)
        {
            this.cartServices = cartServices;
        }

        [HttpGet("User/Cart/{userId}")]
        public IActionResult GetUserCart(string userId)
        {
            var response = new GeneralResponse<CartDTO>();
            var x = cartServices.GetCart(userId);
            if (x == null)
            {
                response.Data = null;
                response.Messsage = "No cart for this user";
                response.IsSucceeded = false;
                return BadRequest(response);
            }
            response.Data = x;
            response.Messsage = "The data returned successfully";
            response.IsSucceeded = true;
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(CartItemCreateDTO cartItemCreateDTO, string userId)
        {
            var s = cartServices.AddItemToCart(cartItemCreateDTO, userId);
            var response = new GeneralResponse<CartItemCreateDTO>();
            if (s == true)
            {
                response.IsSucceeded = true;
                response.Messsage = "The item was added successfully";
                response.Data = cartItemCreateDTO;
                return Ok(response);
            }
            response.IsSucceeded = false;
            response.Messsage = "product id is invalid or stock is not enough";
            response.Data = null;
            return BadRequest(response);
        }
        [HttpPut("Update")]
        public IActionResult Update(int itemId, int newQuantity)
        {
            var result = cartServices.UpdateItemQuantity(itemId, newQuantity);
            var response = new GeneralResponse<CartItemReadDTO>();
            if (result == true)
            {
                response.IsSucceeded = true;
                response.Data = null;
                response.Messsage = "The data updated successfully";
                return Ok(response);
            }
            response.IsSucceeded = false;
            response.Data = null;
            response.Messsage = "This id is invalid or stock is not enough";
            return NotFound(response);
        }
        [HttpDelete("Delete/{userId}")]
        public IActionResult Delete(string userId)
        {
            var result = cartServices.ClearCart(userId);
            var response = new GeneralResponse<CartItemReadDTO>();
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
        [HttpDelete("RemoveItem/{itemId:int}")]
        public IActionResult RemoveItem(int itemId)
        {
            var result = cartServices.RemoveItemFromCart(itemId);
            var response = new GeneralResponse<CartItemReadDTO>();
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
    }
}
