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

public class TestEmployeesContoller
{
    [Fact]
    public void TestGetEmployeesForStoreMethod()
    {
        var testEmployees = new List<Employee> {new Employee {Id = 1, StoreId = 1, Fullname = "1", JobPosition = "1", Salary = 1},
            new Employee {Id = 2, StoreId = 2, Fullname = "2", JobPosition = "2", Salary = 2}};
        var mockRepository = new Mock<IEmployeeRepo>();
        mockRepository.Setup(x => x.StoreExits(1)).Returns(true);
        mockRepository.Setup(x => x.GetEmployeesForStore(1)).Returns(testEmployees);
        var employeeProfile = new EmployeeProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(employeeProfile));
        IMapper mapper = new Mapper(configuration);
        var controller = new EmployeesController(mockRepository.Object, mapper);

        var actionResult = controller.GetEmployeesForStore(1);
        var result = actionResult.Result as OkObjectResult;
        var resultColletion = result.Value as List<EmployeeReadDto>;

        resultColletion.Should().NotBeNullOrEmpty();
        resultColletion.Should().BeOfType<List<EmployeeReadDto>>();
        resultColletion.Should().BeEquivalentTo(mapper.Map<IEnumerable<EmployeeReadDto>>(testEmployees));
    }

    [Fact]
    public void TestGetEmployeeForStoreMethod()
    {
        var testEmployee = new Employee {Id = 1, StoreId = 1, Fullname = "1", JobPosition = "1", Salary = 1};
        var mockRepository = new Mock<IEmployeeRepo>();
        mockRepository.Setup(x => x.StoreExits(1)).Returns(true);
        mockRepository.Setup(x => x.GetEmployee(1, 1)).Returns(testEmployee);
        var employeeProfile = new EmployeeProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(employeeProfile));
        IMapper mapper = new Mapper(configuration);
        var controller = new EmployeesController(mockRepository.Object, mapper);

        var actionResult = controller.GetEmployeeForStore(1, 1);
        var result = actionResult.Result as OkObjectResult;
        var resultValue = result.Value as EmployeeReadDto;

        resultValue.Should().NotBeNull();
        resultValue.Should().BeOfType<EmployeeReadDto>();
        resultValue.Should().BeEquivalentTo(mapper.Map<EmployeeReadDto>(testEmployee));
    }
    [Fact]
    public void TestCreateEmployeeForStoreMethod()
    {
        var testEmployee = new EmployeeWriteDto { Fullname = "1", JobPosition = "1", Salary = 1};
        var mockRepository = new Mock<IEmployeeRepo>();
        mockRepository.Setup(x => x.StoreExits(1)).Returns(true);
        var employeeProfile = new EmployeeProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(employeeProfile));
        IMapper mapper = new Mapper(configuration);
        var controller = new EmployeesController(mockRepository.Object, mapper);

        var actionResult = controller.CreateEmployeeForStore(1, testEmployee);
        var result = actionResult.Result as CreatedAtRouteResult;

        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(mapper.Map<EmployeeReadDto>(mapper.Map<Employee>(testEmployee)));
        Assert.Equal("GetEmployeeForStore", result.RouteName);
    }
}