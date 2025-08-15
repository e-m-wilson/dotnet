using Library.Models;
using Library.Repositories;
using Library.Services;

namespace LibraryMinApi.Tests;

public class MathServiceTests
{
    [Fact] // A fact test, tests one potential input against the method
    public void SimpleAddFactTest()
    {
        //Arrange - Arrange any objects (or mock objects) that we will need
        //This can be things like connection strings to a testing database
        //Mocking of dependencies, or other object setup

        MathService mathService = new();

        //Act
        int sum = mathService.SimpleAdd(3, 2);

        //Assert
        Assert.Equal(5, sum);
    }

    [Theory]
    [InlineData(-2, 5)]
    [InlineData(1, 2)]
    [InlineData(-111, 114)]
    public void SimpleAddTheoryTest(int x, int y)
    {
        //Arrange
        MathService mathService = new();
        //Act
        int sum = mathService.SimpleAdd(x, y);

        //Assert
        Assert.Equal(3, sum);
    }
}
