using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MoqAPI.Controllers;
using MoqAPI.Model;
using MoqAPI.Data.Interfaces;
using System.Drawing;

namespace ApiTest;

public class GetByIdTest
{
    [Fact]
    public async Task GetById_ExistingUser()
    {
        // Arranging the repository Mock
        int EmployeeId = 1;
        var MockRepository = new Mock<IEmployeeRepository>();
        MockRepository.Setup(repo => repo.GetById(EmployeeId)).ReturnsAsync(new Employee { Id = 1, FirstName = "Nima", LastName ="Djavid", Age = 31});
        
        // Acting on the Endpoint
        var Controller = new EmployeeController(MockRepository.Object);
        var result = await Controller.GetById(EmployeeId) as OkObjectResult;

        //Assertion
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("Nima", (result.Value as Employee)?.FirstName);
    }

    [Fact]
    public async Task GetbyId_nonExistingUser()
    {
        // Arranging the repository Mock
        int employeeId = 999;
        var MockRepository = new Mock<IEmployeeRepository>();
        MockRepository.Setup(repo => repo.GetById(employeeId)).ReturnsAsync((Employee?)null);

        // Acting on the Endpoint
        var Controller = new EmployeeController(MockRepository.Object);
        var result = await Controller.GetById(employeeId) as NotFoundResult;

        //Assertion
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode); // Expecting 404 for Not Found

    }

    [Fact]
    public async Task GetById_ExceptionThrown()
    {
        // Arranging the repository Mock
        int employeeId = 1;
        var MockRepository = new Mock<IEmployeeRepository>();
        MockRepository.Setup(repo => repo.GetById(employeeId)).ThrowsAsync(new Exception("An error occurred."));

        // Acting on the Endpoint
        var Controller = new EmployeeController(MockRepository.Object);
        var result = await Controller.GetById(employeeId) as BadRequestObjectResult;;

        //Assertion
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("An error occurred.", result.Value);
    }
}
