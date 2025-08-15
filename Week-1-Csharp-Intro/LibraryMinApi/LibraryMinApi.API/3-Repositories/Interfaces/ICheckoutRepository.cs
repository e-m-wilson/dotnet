using Library.Models;

namespace Library.Repositories;

public interface ICheckoutRepository
{
    void AddCheckout(List<Checkout> updatedCheckouts);
    List<Checkout> GetAllCheckouts();
    void SaveCheckouts(List<Checkout> checkoutList);
}
