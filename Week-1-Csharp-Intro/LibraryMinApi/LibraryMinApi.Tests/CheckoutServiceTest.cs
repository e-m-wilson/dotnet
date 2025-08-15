using Library.DTOs;
using Library.Models;
using Library.Repositories;
using Library.Services;
using Moq;

namespace LibraryMinApi.Tests;

public class CheckoutServiceTests
{
    //Mock Dependencies: We mock dependencies, in this case, to isolate our service layer.
    //We don't want to write tests that may fail because of code outside of the specific unit we are testing.
    //We can also avoid unwanted secondary effects of our tests being run.

    //In order to test CheckoutService I need to satisfy its dependencies, since we need to construct a
    //CheckoutService object. We are going to mock these dependencies

    private readonly Mock<IBookRepository> _mockBookRepo = new();
    private readonly Mock<IMemberRepository> _mockMemberRepo = new();
    private readonly Mock<ICheckoutRepository> _mockCheckoutRepo = new();

    //Finally, we create an instance of the object who's code we are going to be testing.
    private readonly CheckoutService _checkoutService;

    // Test data constants: Centralized test data can make tests more maintainable and easier to write.

    private const string ValidIsbn = "99921-58-10-7";
    private const string ValidEmail = "test@library.com";
    private readonly Book _availableBook = new() { Isbn = ValidIsbn, IsAvailable = true };
    private readonly Member _validMember = new() { Email = ValidEmail, MemberId = Guid.NewGuid() };

    // Unit testing class constructor
    //We need to initalize it with our CheckoutService, that itself needs our mock dependencies
    //in order to be created
    public CheckoutServiceTests()
    {
        _checkoutService = new CheckoutService(
            _mockBookRepo.Object, //Satisfying our CheckoutService class's constructor
            _mockMemberRepo.Object, // with our mock objects
            _mockCheckoutRepo.Object
        );
    }

    //We are now ready to test.

    [Theory]
    [InlineData(null, ValidEmail)] // Null isbn
    [InlineData(ValidIsbn, null)] //null email
    [InlineData("", ValidEmail)] //empty ISBN string
    [InlineData(ValidIsbn, "")] //empty email string
    public void CheckoutBook_InvalidRequest_ThrowsExpection(string _isbn, string _email)
    {
        //Arrange - Creating a CheckoutRequestDTO we will purposfully populate with bad data
        var invalidRequest = new CheckoutRequestDTO() { isbn = _isbn, memberEmail = _email };
        //Act

        //Assert - In this case our assert is also our act. When the method runs, if the request is invalid
        //we are asserting an exception is thrown. If the exception is thrown, the test passes.
        Assert.Throws<NullReferenceException>(() => _checkoutService.CheckoutBook(invalidRequest));
    }

    [Fact]
    public void CheckoutBook_MemberNotFound_ThrowsException()
    {
        //Arrange - Valid book, invalid member with wrong email
        var request = new CheckoutRequestDTO() { isbn = ValidIsbn, memberEmail = "wrong@email.com" };

        //Setting up a valid book, with empty member list
        //Here we define our return when our mockBookRepo calls its mock implementation of GetAllBooks
        _mockBookRepo.Setup(r => r.GetAllBooks()).Returns(new List<Book> { _availableBook });
        _mockMemberRepo.Setup(r => r.GetAllMembers()).Returns(new List<Member>()); //No members are in this list
        //ergo, wrong@mail.com is not a valid member

        //Act & Assert
        var ex = Assert.Throws<Exception>(() => _checkoutService.CheckoutBook(request));
        Assert.Equal("Member not found", ex.Message);

        //Since we can throw an Exception from multiple places inside CheckoutBook depending what actually caused
        //it to fail, we want to verify the exception stopped code execution where we expected it to. 

        //Verify that the book was found, but checkout wasn't created
        _mockBookRepo.Verify(r => r.GetAllBooks(), Times.Once);

        // If the member caused the Exception, the checkout list is never generated in our CheckoutBook method
        _mockCheckoutRepo.Verify(r => r.AddCheckout(It.IsAny<List<Checkout>>()), Times.Never);

    }
}
