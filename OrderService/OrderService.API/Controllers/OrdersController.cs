using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderService.Application.Commands;
using OrderService.Application.Queries;
using OrderService.Application.UseCases.Interfaces;
using OrderService.Domain.Entities;
using Shared.Common.DTOs;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICRUDOrderUseCase _iCRUDOrderUseCase;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;

        public OrdersController(ICRUDOrderUseCase iCRUDOrderUseCase, ILogger<OrdersController> logger, IMediator mediator)
        {
            _iCRUDOrderUseCase = iCRUDOrderUseCase;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderByEmployee()
        {
            _logger.LogInformation("Request received: Get all orders by employee");

            if (!Request.Headers.TryGetValue("Header-EmployeeId", out var empIdHeader) ||
                !Guid.TryParse(empIdHeader.FirstOrDefault(), out var employeeId))
            {
                _logger.LogWarning("EmployeeId not found or invalid in request headers.");
                return BadRequest(new { message = "Logged in user id not found in header." });
            }

            try
            {
                var orders = await _iCRUDOrderUseCase.GetAllOrdersByEmployeeIdAsync(employeeId);
                _logger.LogInformation("Retrieved {Count} orders for EmployeeId {EmployeeId}", orders.Count, employeeId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving orders for EmployeeId {EmployeeId}", employeeId);
                return StatusCode(500, "An error occurred while retrieving orders.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOrderCommand command)
        {
            _logger.LogInformation("Request received: Add order {@Order}", command);

            try
            {
                var employeeId = Guid.Parse(Request.Headers["Header-EmployeeId"].ToString());

                command.Id = Guid.NewGuid();
                command.LoggedInEmployeeId = employeeId;

                var addedGuid = await _mediator.Send(command);

                if (addedGuid != Guid.Empty)
                {
                    _logger.LogInformation("Order added successfully: {@Order} - {@Id})", command, addedGuid);
                    return Ok("Order added successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to add order: {@Order}", command);
                    return BadRequest("Failed to add order.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while adding order: {@Order}", command);
                return StatusCode(500, "An error occurred while adding the order.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] OrderDTO orderDto)
        {
            _logger.LogInformation("Request received: Update order {@Order}", orderDto);

            try
            {
                var success = await _iCRUDOrderUseCase.UpdateAsync(orderDto);

                if (success)
                {
                    _logger.LogInformation("Order updated successfully: {@Order}", orderDto);
                    return Ok("Order updated successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to update order: {@Order}", orderDto);
                    return BadRequest("Failed to update order.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating order: {@Order}", orderDto);
                return StatusCode(500, "An error occurred while updating the order.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Request received: Delete order {OrderId}", id);

            try
            {
                var success = await _iCRUDOrderUseCase.DeleteAsync(id);

                if (success)
                {
                    _logger.LogInformation("Order deleted successfully: {OrderId}", id);
                    return Ok("Order deleted successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to delete order: {OrderId}", id);
                    return BadRequest("Failed to delete order.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting order: {OrderId}", id);
                return StatusCode(500, "An error occurred while deleting the order.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            _logger.LogInformation("Request received: Get order by ID {OrderId}", id);

            try
            {
                //var order = await _iCRUDOrderUseCase.GetOrderById(id);
                var order = await _mediator.Send(new GetOrderByIdQuery(id));

                if (order is null)
                {
                    _logger.LogWarning("Order not found: {OrderId}", id);
                    return NotFound($"Order {id} not found");
                }

                _logger.LogInformation("Order retrieved successfully: {@Order}", order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while retrieving order: {OrderId}", id);
                return StatusCode(500, "An error occurred while retrieving the order.");
            }
        }
    }
}
