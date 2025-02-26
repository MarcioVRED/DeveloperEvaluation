using Ambev.DeveloperStore.Application.Sales.GetSale;
using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperStore.Unit.Application;

public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Should return a sale when found")]
    public async Task Handle_Should_Return_Sale_When_Found()
    {
        var saleId = Guid.NewGuid();
        var saleItems = new List<SaleItem>
        {
            new (Guid.NewGuid(), saleId, "Product A", 2, 15.00M),
            new (Guid.NewGuid(), saleId, "Product B", 3, 25.00M)
        };
        var sale = new Sale("Marcio Martins", "Filial SP", saleItems, DateTime.UtcNow);

        var expectedResult = new GetSaleResult
        {
            Id = sale.Id,
            SaleDate = sale.SaleDate,
            Customer = sale.CustomerName,
            Branch = sale.BranchName,
            Items = sale.Items,
            IsCancelled = sale.IsCancelled,
            TotalAmount = sale.TotalSaleAmount
        };

        _saleRepository.GetByIdAsync(sale.Id).Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(expectedResult);   

        var command = new GetSaleCommand(sale.Id);
        var getSaleResult = await _handler.Handle(command, CancellationToken.None);

        getSaleResult.Should().NotBeNull();
        getSaleResult.Id.Should().Be(sale.Id);
    }

    [Fact(DisplayName = "Should throw an exception when sale is not found")]
    public async Task Handle_Should_Throw_Exception_When_Sale_Not_Found()
    {
        var saleId = Guid.NewGuid();

        _saleRepository.GetByIdAsync(saleId).Returns(Task.FromException<Sale?>(new KeyNotFoundException($"Sale with ID {saleId} not found.")));

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _saleRepository.GetByIdAsync(saleId));
        Assert.Contains($"Sale with ID {saleId} not found.", exception.Message);
    }

    [Fact(DisplayName = "Should return a sale with items")]
    public async Task Handle_Should_Return_Sale_With_Items()
    {
        var saleId = Guid.NewGuid();
        var saleItems = new List<SaleItem>
        {
            new (Guid.NewGuid(), saleId, "Product A", 2, 15.00M),
            new (Guid.NewGuid(), saleId, "Product B", 3, 25.00M)
        };
        var sale = new Sale("Marcio Martins", "Filial SP", saleItems, DateTime.UtcNow);

        var expectedResult = new GetSaleResult
        {
            Id = sale.Id,
            SaleDate = sale.SaleDate,
            Customer = sale.CustomerName,
            Branch = sale.BranchName,
            Items = sale.Items,
            IsCancelled = sale.IsCancelled,
            TotalAmount = sale.TotalSaleAmount
        };

        _saleRepository.GetByIdAsync(sale.Id).Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(expectedResult);

        var command = new GetSaleCommand(sale.Id);
        var getSaleResult = await _handler.Handle(command, CancellationToken.None);

        getSaleResult.Should().NotBeNull();
        getSaleResult.Id.Should().Be(expectedResult.Id);
        getSaleResult.Items.Should().HaveCount(2);
    }
}
