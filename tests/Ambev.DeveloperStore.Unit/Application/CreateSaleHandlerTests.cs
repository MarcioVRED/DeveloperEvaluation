using Ambev.DeveloperStore.Application.Sales.CreateSale;
using Ambev.DeveloperStore.Application.Sales.CreateSaleItem;
using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Domain.Repositories;
using AutoMapper;
using NSubstitute;
using FluentAssertions;
using Xunit;
using FluentValidation;

namespace Ambev.DeveloperStore.Unit.Application;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper);
    }

    [Theory]
    [ClassData(typeof(CreateSaleHandlerTestData))]
    public async Task Handle_Should_Create_Sale_Successfully(CreateSaleCommand command)
    {
        var items = command.Items
            .Select(item => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), item.ProductName, item.Quantity, item.UnitPrice))
            .ToList();

        var sale = new Sale(command.CustomerName, command.BranchName, items, command.SaleDate);
        var result = new CreateSaleResult
        {
            Id = sale.Id,
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _saleRepository.CreateAsync(Arg.Any<Sale>()).Returns(Task.FromResult(sale));

        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_CustomerName_Is_LessThan()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow.AddDays(-1),
            "AA",
            "Filial SP",
            new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 10, 5.00M)
            }
        );

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("Customer name must be between 3 and 100 characters", exception.Message);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_CustomerName_Is_Null()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow.AddDays(-1),
            null!,
            "Filial SP",
            new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 10, 5.00M)
            }
        );

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("Customer name cannot be null.", exception.Message);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_BranchName_Is_Null()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow.AddDays(-1),
            "Marcio Martins",
            null!,
            new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 10, 5.00M)
            }
        );

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("Branch name cannot be null.", exception.Message);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_BranchName_Is_LessThan()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow.AddDays(-1),
            "Marcio Martins",
            "AA",
            new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 10, 5.00M)
            }
        );

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("Branch name must be between 3 and 100 characters.", exception.Message);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Product_Quantity_Is_Zero()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow.AddDays(-1),
            "Marcio Martins",
            "Filial SP",
            new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 0, 5.00M)
            }
        );

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("The product quantity cannot be zero.", exception.Message);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_SaleDate_Is_In_Future()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow.AddDays(1),
            "Marcio Martins",
            "Filial SP",
            new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 10, 5.00M)
            }
        );

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("The sale date cannot be in the future.", exception.Message);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Items_List_Is_Empty()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow.AddDays(-1),
            "Marcio Martins",
            "Filial SP",
            new List<CreateSaleItemCommand>() // Lista vazia
        );

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("Sale must have at least one item.", exception.Message);
    }
}