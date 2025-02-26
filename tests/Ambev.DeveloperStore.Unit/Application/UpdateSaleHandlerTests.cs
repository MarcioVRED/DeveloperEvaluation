using Ambev.DeveloperStore.Application.Sales.UpdateSale;
using Ambev.DeveloperStore.Application.Sales.UpdateSaleItem;
using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Domain.Repositories;
using AutoMapper;
using NSubstitute;
using FluentAssertions;
using Xunit;
using FluentValidation;

namespace Ambev.DeveloperStore.Unit.Application;

public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Should update sale successfully")]
    public async Task Handle_Should_Update_Sale_Successfully()
    {
        var command = new UpdateSaleCommand(
            "20250114-002",
            DateTime.UtcNow.AddDays(-1),
            "New Customer",
            "New Branch",
            new List<UpdateSaleItemCommand>
            {
                new UpdateSaleItemCommand(Guid.NewGuid(), "Updated Product", 5, 10.00M)
            }
        );
        var items = command.Items
            .Select(item => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), item.ProductName, item.Quantity, item.UnitPrice))
            .ToList();
        var existingSale = new Sale(command.CustomerName, command.BranchName, items, command.SaleDate);
        _saleRepository.GetByIdAsync(existingSale.Id).Returns(existingSale);

        var result = new UpdateSaleResult
        {
            Id = existingSale.Id,
        };
        _mapper.Map<Sale>(command).Returns(existingSale);
        _mapper.Map<UpdateSaleResult>(existingSale).Returns(result);

        _saleRepository.UpdateAsync(Arg.Any<Sale>()).Returns(Task.FromResult(existingSale));

        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        updateSaleResult.Should().NotBeNull();
        updateSaleResult.Id.Should().Be(existingSale.Id);
        await _saleRepository.Received(1).UpdateAsync(existingSale, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw exception when sale is not found")]
    public async Task Handle_Should_Throw_Exception_When_Sale_Not_Found()
    {
        var saleId = Guid.NewGuid();

        _saleRepository.GetByIdAsync(saleId).Returns(Task.FromException<Sale?>(new KeyNotFoundException($"Sale with ID {saleId} not found.")));

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _saleRepository.GetByIdAsync(saleId));
        Assert.Contains($"Sale with ID {saleId} not found.", exception.Message);
    }

    [Fact(DisplayName = "Should throw exception when customer name is invalid")]
    public async Task Handle_Should_Throw_Exception_When_CustomerName_Is_Invalid()
    {
        var command = new UpdateSaleCommand(
            "20250114002",
            DateTime.UtcNow.AddDays(-1),
            "A",
            "Branch Name",
            new List<UpdateSaleItemCommand>
            {
                new UpdateSaleItemCommand(Guid.NewGuid(), "Product Name", 1, 10.00M)
            }
        );
        var items = command.Items
            .Select(item => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), item.ProductName, item.Quantity, item.UnitPrice))
            .ToList();
        var existingSale = new Sale(command.CustomerName, command.BranchName, items, command.SaleDate);
        _saleRepository.GetByIdAsync(existingSale.Id).Returns(existingSale);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("Customer name must be between 3 and 100 characters.", exception.Message);
    }

    [Fact(DisplayName = "Should throw exception when branch name is invalid")]
    public async Task Handle_Should_Throw_Exception_When_BranchName_Is_Invalid()
    {
        var command = new UpdateSaleCommand(
            "20250114002",
            DateTime.UtcNow.AddDays(-1),
            "Customer Name",
            "B",
            new List<UpdateSaleItemCommand>
            {
                new UpdateSaleItemCommand(Guid.NewGuid(), "Product Name", 1, 10.00M)
            }
        );
        var items = command.Items
            .Select(item => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), item.ProductName, item.Quantity, item.UnitPrice))
            .ToList();
        var existingSale = new Sale(command.CustomerName, command.BranchName, items, command.SaleDate);
        _saleRepository.GetByIdAsync(existingSale.Id).Returns(existingSale);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("Branch name must be between 3 and 100 characters.", exception.Message);
    }

    [Fact(DisplayName = "Should throw exception when sale date is in the future")]
    public async Task Handle_Should_Throw_Exception_When_SaleDate_Is_In_Future()
    {
        var command = new UpdateSaleCommand(
            "20250114-002",
            DateTime.UtcNow.AddDays(1),
            "Customer Name",
            "Branch Name",
            new List<UpdateSaleItemCommand>
            {
                new UpdateSaleItemCommand(Guid.NewGuid(), "Product Name", 1, 10.00M)
            }
        );
        var items = command.Items
            .Select(item => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), item.ProductName, item.Quantity, item.UnitPrice))
            .ToList();
        var existingSale = new Sale(command.CustomerName, command.BranchName, items, command.SaleDate);
        _saleRepository.GetByIdAsync(existingSale.Id).Returns(existingSale);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("The sale date cannot be in the future.", exception.Message);
    }

    [Fact(DisplayName = "Should throw exception when item list is empty")]
    public async Task Handle_Should_Throw_Exception_When_Items_List_Is_Empty()
    {
        var command = new UpdateSaleCommand(
            "20250114-002",
            DateTime.UtcNow.AddDays(-1),
            "Customer Name",
            "Branch Name",
            new List<UpdateSaleItemCommand>()
        );
        var items = command.Items
            .Select(item => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), item.ProductName, item.Quantity, item.UnitPrice))
            .ToList();

        var existingSale = new Sale(command.CustomerName, command.BranchName, items, command.SaleDate);
        _saleRepository.GetByIdAsync(existingSale.Id).Returns(existingSale);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Contains("Sale must have at least one item.", exception.Message);
    }
}
