using System.Collections.Generic;
using AutoMapper;
using ProductService.Data;
using ProductService.Models;
using ProductService.Profiles;
using ProductService.Controllers;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using FluentAssertions;

namespace ProductService.UnitTests;

public class TestStoresContoller
{
    [Fact]
    public void TestGetStoresMethod()
    {
        var testStores = new List<Store> { new Store {Id = 1, Name = "1", Address = "1"}, 
            new Store {Id = 2, Name = "2", Address = "2"} };
        var mockRepository = new Mock<IProductRepo>();
        mockRepository.Setup(x => x.GetAllStores()).Returns(testStores);
        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
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