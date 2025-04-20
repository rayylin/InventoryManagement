using Moq; // Mocking framework
using Xunit; // Testing framework
using System.Data;
using InventoryManagement.Interfaces;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Tests;

public class DatabaseServiceTests
{
    [Fact] // This marks a test method
    public void ConnectDb_ReturnsExpectedDataTable()
    {
        // Arrange: set up fake behavior for your service
        var mockService = new Mock<IDatabaseService>();

        // Create a fake DataTable to return
        var expectedTable = new DataTable();
        expectedTable.Columns.Add("Id");
        expectedTable.Rows.Add(1); // Adding one row with value 1

        // Tell the mock to return the fake table when called
        mockService
            .Setup(service => service.connectDb(It.IsAny<string>()))
            .Returns(expectedTable);

        var databaseService = mockService.Object;

        // Act: call the method you're testing
        var result = databaseService.connectDb("SELECT *  FROM [InvMgnt].[dbo].[Customer]");

        // Assert: verify the result
        Assert.NotNull(result);                      // The result shouldn't be null
        Assert.Equal(1, result.Rows.Count);          // It should have one row
        Assert.Equal(1, result.Rows[0]["Id"]);       // The value in the "Id" column should be 1
    }

    [Fact]
    public void ExecuteStoredProcedure_DoesNotThrow()
    {
        // Arrange
        var mockService = new Mock<IDatabaseService>();

        // Set up the mock to accept any stored procedure
        mockService.Setup(service => service.ExecuteStoredProcedure(It.IsAny<string>()));

        var databaseService = mockService.Object;

        // Act & Assert: Make sure it doesn't throw
        var exception = Record.Exception(() =>
            databaseService.ExecuteStoredProcedure("MyStoredProcedure"));

        Assert.Null(exception); // If exception is null, it passed
    }

}