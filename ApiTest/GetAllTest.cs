using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoqAPI.Controllers;
using MoqAPI.Model;
using MoqAPI.Data.Interfaces;

namespace ApiTest;
public class GetAllTest
{
    /////////////////////////////////////////   Case 1 ////////////////////////////////////
    [Fact]
    public async Task Get_ReturnsOkWithData()
    {
        // Arranging the repository Mock
        var mockRepository = new Mock<IEmployeeRepository>();
        mockRepository.Setup(repo => repo.Get()).ReturnsAsync(new List<Employee> { new() { Id = 1, FirstName = "Nima", LastName = "Djavid", Age = 31 } });

        // Acting on the Endpoint
        var controller = new EmployeeController(mockRepository.Object);
        var result = await controller.GetAll() as OkObjectResult;

        //Assertion
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.NotEmpty((IEnumerable<Employee>)result.Value!);
    }

    /////////////////////////////////////////   Case 2 ////////////////////////////////////
    [Fact]
    public async Task Get_ReturnsOkWithEmptyData()
    {
        // Arranging the repository Mock
        var mockRepository = new Mock<IEmployeeRepository>();
        mockRepository.Setup(repo => repo.Get()).ReturnsAsync(new List<Employee>());

        // Acting on the Endpoint
        var controller = new EmployeeController(mockRepository.Object);
        var result = await controller.GetAll() as OkObjectResult;

        // Assertion
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Empty((IEnumerable<Employee>)result.Value!);
    }

    /////////////////////////////////////////   Case 3 ////////////////////////////////////
    [Fact]
    public async Task Get_ReturnsNotFoundWhenResultIsNull()
    {
        // Arranging the repository Mock
        var mockRepository = new Mock<IEmployeeRepository>();
        mockRepository.Setup(repo => repo.Get()).ReturnsAsync((List<Employee>)null);

        // Acting on the Endpoint
        var controller = new EmployeeController(mockRepository.Object);
        var result = await controller.GetAll() as NotFoundResult;

        //Assertion
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    /////////////////////////////////////////   Case 4 ////////////////////////////////////
    [Fact]
    public async Task Get_ReturnsBadRequestOnException()
    {
        // Arranging the repository Mock
        var mockRepository = new Mock<IEmployeeRepository>();
        mockRepository.Setup(repo => repo.Get()).ThrowsAsync(new Exception("An error occurred."));

        // Acting on the Endpoint
        var controller = new EmployeeController(mockRepository.Object);
        var result = await controller.GetAll() as BadRequestObjectResult;

        // Assertion
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("An error occurred.", result.Value);
    }
}

