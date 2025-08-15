using Library.DTOs;
using Library.Models;
using Library.Repositories;

namespace Library.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IBookRepository _bookRepo;

    private readonly IMemberRepository _memberRepo;

    private readonly ICheckoutRepository _checkoutRepo;
    private readonly ILogger _logger;

    public CheckoutService(
        IBookRepository bookRepo,
        IMemberRepository memberRepo,
        ICheckoutRepository checkoutRepo,
        ILogger logger
    )
    {
        _bookRepo = bookRepo;
        _memberRepo = memberRepo;
        _checkoutRepo = checkoutRepo;
        _logger = logger;
    }

    public Checkout CheckoutBook(CheckoutRequestDTO checkoutRequest)
    {
        // //1. Validate that a book exists
        _logger.LogCritical("THIS IS FROM CHECKOUT BOOK");
        // Getting our list of books from the _bookRepo
        List<Book> bookList = _bookRepo.GetAllBooks();

        // Grab the book from the list (if it doesnt exist, foundBook is null)
        Book? foundBook = bookList.Find(b => b.Isbn == checkoutRequest.isbn);

        // If the book isnt found, this will run
        if (foundBook is null)
        {
            throw new Exception("Book not found");
        }

        // If the book is found but isnt available, this will run
        if (!foundBook.IsAvailable)
        {
            throw new Exception("Book is not available for checkout");
        }

        //2. Validate that the member exists

        List<Member> memberList = _memberRepo.GetAllMembers();

        // Grab the book from the list (if it doesnt exist, foundBook is null)
        Member? foundMember = memberList.Find(m => m.Email == checkoutRequest.memberEmail);

        // If the member isnt found, this will run
        if (foundMember is null)
        {
            throw new Exception("Member not found");
        }

        //3. Create the checkout record

        // One liner calling the implicitly created "empty constructor"
        // Anything with a default value (guid ids, datetimes we set to now, etc) gets set
        // Anything else is blank unless we give it a value
        Checkout checkoutToAdd = new() { isbn = foundBook.Isbn, MemberId = foundMember.MemberId };

        //4. Update Book availibiliy (update the book isAvailable to false)
        foundBook.IsAvailable = false;
        _bookRepo.UpdateBook(foundBook);

        //5. Save the checkout
        List<Checkout> checkouts = _checkoutRepo.GetAllCheckouts();

        // Remembering to actually add the new checkout to the list
        checkouts.Add(checkoutToAdd);

        _checkoutRepo.AddCheckout(checkouts);

        //Finally we return a checkout to the endpoint method that called this service method
        return checkoutToAdd;
    }
}
