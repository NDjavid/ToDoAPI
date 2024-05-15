using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MoqAPI.Controllers;
using MoqAPI.Model;
using MoqAPI.Data.Interfaces;

namespace ApiTest;
public class PostEmployeeTest
{
    /////////////////////////////////////////   Case 1 ////////////////////////////////////
    [Fact]
    public async Task Post_ReturnsOkWithPostedEmployee()
    {
        // Arranging the repository Mock
        var mockRepository = new Mock<IEmployeeRepository>();
        var employeeToPost = new Employee { FirstName = "Nima3", LastName = "Djavid3", Age = 31 };
        mockRepository.Setup(repo => repo.Post(It.IsAny<Employee>())).ReturnsAsync(employeeToPost);

        // Acting on the Endpoint
        var controller = new EmployeeController(mockRepository.Object);
        var result = await controller.PostEmployee(employeeToPost) as OkObjectResult;

        // Assertion
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(employeeToPost, result.Value);
    }

    /////////////////////////////////////////   Case 2 ////////////////////////////////////
    [Fact]
    public async Task Post_ReturnsBadRequestOnNullEmployee()
    {
        // Arranging the repository Mock
        var mockRepository = new Mock<IEmployeeRepository>();
        mockRepository.Setup(repo => repo.Post(It.IsAny<Employee>())).ReturnsAsync((Employee)null); 

        // Acting on the Endpoint
        var controller = new EmployeeController(mockRepository.Object);
        var result = await controller.PostEmployee(new Employee()) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }


    /////////////////////////////////////////   Case 3 ////////////////////////////////////
    [Fact]
    public async Task Post_ReturnsBadRequestOnException()
    {
        // Arranging the repository Mock
        var mockRepository = new Mock<IEmployeeRepository>();
        var employeeToPost = new Employee { Id = 1, FirstName = "Nima4", LastName = "Djavid4", Age = 31 };
        mockRepository.Setup(repo => repo.Post(It.IsAny<Employee>())).ThrowsAsync(new Exception("An error occurred."));

        // Acting on the Endpoint
        var controller = new EmployeeController(mockRepository.Object);
        var result = await controller.PostEmployee(employeeToPost) as BadRequestObjectResult;

        // Assertion
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("An error occurred.", result.Value);
    }
}

