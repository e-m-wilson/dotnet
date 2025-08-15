using Library.DTOs;
using Library.Models;

namespace Library.Repositories;

public interface ICheckoutService
{
    Checkout CheckoutBook(CheckoutRequestDTO checkoutRequest);
}
