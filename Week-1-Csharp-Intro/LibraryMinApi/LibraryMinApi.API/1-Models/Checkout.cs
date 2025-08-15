namespace Library.Models;

public class Checkout
{
    public Guid CheckoutId { get; set; } = Guid.NewGuid();

    public string isbn { get; set; }
    public Guid MemberId { get; set; }

    public DateTime CheckoutDate { get; set; } = DateTime.Now;

    public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);
}
