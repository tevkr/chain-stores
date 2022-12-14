using System.Collections.Generic;
using AutoMapper;
using EmployeeService.Data;
using EmployeeService.Models;
using EmployeeService.Profiles;
using EmployeeService.Controllers;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.Dtos;
using FluentAssertions;

namespace EmployeeService.UnitTests;

public class TestStoresContoller
{
    [Fact]
    public void TestGetStoresMethod()
    {
        var testStores = new List<Store> { new Store {Id = 1, Name = "1", Address = "1"}, 
            new Store {Id = 2, Name = "2", Address = "2"} };
        var mockRepository = new Mock<IEmployeeRepo>();
        mockRepository.Setup(x => x.GetAllStores()).Returns(testStores);
        var employeeProfile = new EmployeeProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(employeeProfile));
        IMapper mapper = new Mapper(configuration);
        var controller = new StoresController(mockRepository.Object, mapper);

        var actionResult = controller.GetStores();
        var result = actionResult.Result as OkObjectResult;

        var resultColletion = result.Value as List<StoreReadDto>;
        resultColletion.Should().NotBeNullOrEmpty();
        resultColletion.Should().BeOfType<List<StoreReadDto>>();
        resultColletion.Should().BeEquivalentTo(mapper.Map<IEnumerable<StoreReadDto>>(testStores));
    }
}