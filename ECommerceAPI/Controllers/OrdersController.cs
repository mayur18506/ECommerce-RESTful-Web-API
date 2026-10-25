using System.Security.Claims;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var userId = GetUserId();
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
                return BadRequest("Your cart is empty.");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                OrderStatus = "Confirmed",
                TotalAmount = cartItems.Sum(c => c.Product!.Price * c.Quantity),
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    UnitPrice = c.Product!.Price
                }).ToList()
            };

            // Stock minus karo
            foreach (var item in cartItems)
            {
                if (item.Product!.StockQuantity < item.Quantity)
                    return BadRequest($"Insufficient stock for {item.Product.Name}");

                item.Product.StockQuantity -= item.Quantity;
            }

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems); // Cart clear karo
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order placed successfully!", orderId = order.Id, total = order.TotalAmount });
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserId();
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Ok(orders);
        }
    }
}