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

public class TestProductsContoller
{
    [Fact]
    public void TestGetProductsForStoreMethod()
    {
        var testProducts = new List<Product> {new Product {Id = 1, StoreId = 1, Name = "1", Description = "1", Price = 1},
            new Product {Id = 2, StoreId = 2, Name = "2", Description = "2", Price = 2}};
        var mockRepository = new Mock<IProductRepo>();
        mockRepository.Setup(x => x.StoreExits(1)).Returns(true);
        mockRepository.Setup(x => x.GetProductsForStore(1)).Returns(testProducts);
        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        IMapper mapper = new Mapper(configuration);
        var controller = new ProductsController(mockRepository.Object, mapper);

        var actionResult = controller.GetProductsForStore(1);
        var result = actionResult.Result as OkObjectResult;
        var resultColletion = result.Value as List<ProductReadDto>;

        resultColletion.Should().NotBeNullOrEmpty();
        resultColletion.Should().BeOfType<List<ProductReadDto>>();
        resultColletion.Should().BeEquivalentTo(mapper.Map<IEnumerable<ProductReadDto>>(testProducts));
    }

    [Fact]
    public void TestGetProductForStoreMethod()
    {
        var testProduct = new Product {Id = 1, StoreId = 1, Name = "1", Description = "1", Price = 1};
        var mockRepository = new Mock<IProductRepo>();
        mockRepository.Setup(x => x.StoreExits(1)).Returns(true);
        mockRepository.Setup(x => x.GetProduct(1, 1)).Returns(testProduct);
        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        IMapper mapper = new Mapper(configuration);
        var controller = new ProductsController(mockRepository.Object, mapper);

        var actionResult = controller.GetProductForStore(1, 1);
        var result = actionResult.Result as OkObjectResult;
        var resultValue = result.Value as ProductReadDto;

        resultValue.Should().NotBeNull();
        resultValue.Should().BeOfType<ProductReadDto>();
        resultValue.Should().BeEquivalentTo(mapper.Map<ProductReadDto>(testProduct));
    }
    [Fact]
    public void TestCreateProductForStoreMethod()
    {
        var testProduct = new ProductWriteDto { Name = "1", Description = "1", Price = 1};
        var mockRepository = new Mock<IProductRepo>();
        mockRepository.Setup(x => x.StoreExits(1)).Returns(true);
        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        IMapper mapper = new Mapper(configuration);
        var controller = new ProductsController(mockRepository.Object, mapper);

        var actionResult = controller.CreateProductForStore(1, testProduct);
        var result = actionResult.Result as CreatedAtRouteResult;

        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(mapper.Map<ProductReadDto>(mapper.Map<Product>(testProduct)));
        Assert.Equal("GetProductForStore", result.RouteName);
    }
}