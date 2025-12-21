namespace OrderService.Domain.Models;

public class Order
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }

    public Order(string id, string userId, decimal totalAmount)
    {
        Id = id;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        TotalAmount = totalAmount;
    }
}