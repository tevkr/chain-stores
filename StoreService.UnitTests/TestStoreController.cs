using Xunit;
using StoreService.Controllers;
using Moq;
using StoreService.Data;
using System.Collections.Generic;
using StoreService.Models;
using StoreService.Profiles;
using StoreService.SyncDataServices.Http;
using StoreService.Dtos;
using StoreService.AsyncDataServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace StoreService.UnitTests;

public class TestStoreContoller
{
    [Fact]
    public void TestGetStoresMethod()
    {
        var testStores = new List<Store> { new Store {Id = 1, Name = "1", Address = "1"}, 
            new Store {Id = 2, Name = "2", Address = "2"} };
        var mockRepository = new Mock<IStoreRepo>();
        mockRepository.Setup(x => x.GetAllStores()).Returns(testStores);
        var storeProfile = new StoreProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(storeProfile));
        IMapper mapper = new Mapper(configuration);
        var mockCommandDataClient = new Mock<ICommandDataClient>();
        var mockMessageBusClient = new Mock<IMessageBusClient>();
        var controller = new StoresController(mockRepository.Object, mapper, mockCommandDataClient.Object, mockMessageBusClient.Object);

        var actionResult = controller.GetStores();
        var result = actionResult.Result as OkObjectResult;

        var resultColletion = result.Value as List<StoreReadDto>;
        resultColletion.Should().NotBeNullOrEmpty();
        resultColletion.Should().BeOfType<List<StoreReadDto>>();
        resultColletion.Should().BeEquivalentTo(mapper.Map<IEnumerable<StoreReadDto>>(testStores));
    }

    [Fact]
    public void TestGetStoreByIdMethod()
    {
        var testStore = new Store {Id = 1, Name = "1", Address = "1"};
        var mockRepository = new Mock<IStoreRepo>();
        mockRepository.Setup(x => x.GetStoreById(1)).Returns(testStore);
        var storeProfile = new StoreProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(storeProfile));
        IMapper mapper = new Mapper(configuration);
        var mockCommandDataClient = new Mock<ICommandDataClient>();
        var mockMessageBusClient = new Mock<IMessageBusClient>();
        var controller = new StoresController(mockRepository.Object, mapper, mockCommandDataClient.Object, mockMessageBusClient.Object);

        var actionResult = controller.GetStoreById(1);
        var result = actionResult.Result as OkObjectResult;
        var resultValue = result.Value as StoreReadDto;

        resultValue.Should().NotBeNull();
        resultValue.Should().BeOfType<StoreReadDto>();
        resultValue.Should().BeEquivalentTo(mapper.Map<StoreReadDto>(testStore));
    }

    [Fact]
    public void TestCreateStoreMethod()
    {
        var testStore = new StoreWriteDto { Name = "1", Address = "1"};
        var mockRepository = new Mock<IStoreRepo>();
        var storeProfile = new StoreProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(storeProfile));
        IMapper mapper = new Mapper(configuration);
        var mockCommandDataClient = new Mock<ICommandDataClient>();
        var mockMessageBusClient = new Mock<IMessageBusClient>();
        var controller = new StoresController(mockRepository.Object, mapper, mockCommandDataClient.Object, mockMessageBusClient.Object);

        var actionResult = controller.CreateStore(testStore);
        actionResult.Wait();
        var result = actionResult.Result.Result as CreatedAtRouteResult;

        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(mapper.Map<StoreReadDto>(mapper.Map<Store>(testStore)));
        Assert.Equal(0, result.RouteValues["id"]);
        Assert.Equal("GetStoreById", result.RouteName);
    }
}