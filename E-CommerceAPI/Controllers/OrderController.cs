using E_CommerceAPI.DTOs;
using E_CommerceAPI.Models;
using E_CommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices orderServices;

        public OrderController(IOrderServices orderServices)
        {
            this.orderServices = orderServices;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("GetAll")]
        public IActionResult GetAllOrders()
        {
            var response = new GeneralResponse<List<OrderDTO>>()
            {
                Data = orderServices.GetAllOrders(),
                IsSucceeded = true,
                Messsage = "The data returned successfully"
            };
            return Ok(response);
        }

        [HttpGet("GetUserOrders")]
        public IActionResult GetUserOrders(string userId)
        {
            var response = new GeneralResponse<List<OrderDTO>>()
            {
                Data = orderServices.GetUserOrders(userId),
                IsSucceeded = true,
                Messsage = "The data returned successfully"
            };
            return Ok(response);
        }

        [HttpGet("GetOrderById/{orderId:int}")]
        public IActionResult GetOrderById(int orderId)
        {
            var order = orderServices.GetOrderById(orderId);
            var response = new GeneralResponse<OrderDTO>();
            if (order != null)
            {
                response.Data = order;
                response.IsSucceeded = true;
                response.Messsage = "The data returned successfully";
                return Ok(response);
            }
            response.Data = null;
            response.IsSucceeded = false;
            response.Messsage = "The id is invalid";
            return NotFound(response);
        }
        [HttpPost("CreateFromCart")]
        public IActionResult CreateOrder(string userId)
        {
            var result = orderServices.CreateOrder(userId);
            var response = new GeneralResponse<OrderDTO>();
            response.Data = null;
            if (result == true)
            {
                response.IsSucceeded = true;
                response.Messsage = "The order was added successfully";
                return Ok(response);
            }
            response.IsSucceeded = false;
            response.Messsage = "There user id is invalid";
            return BadRequest(response);
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("UpdateStatus/{orderId:int}")]
        public IActionResult UpdateStatus(Status status, int orderId)
        {
            var result = orderServices.UpdateOrderStatus(status, orderId);
            var response = new GeneralResponse<OrderDTO>();
            response.Data = null;
            if (result == true)
            {
                response.IsSucceeded = true;
                response.Messsage = "The order was updated successfully";
                return Ok(response);
            }
            response.IsSucceeded = false;
            response.Messsage = "There order id is invalid";
            return BadRequest(response);
        }
    }
}
