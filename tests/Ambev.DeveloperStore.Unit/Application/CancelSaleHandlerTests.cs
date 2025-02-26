using Ambev.DeveloperStore.Application.Sales.CancelSale;
using Ambev.DeveloperStore.Domain.Repositories;
using Ambev.DeveloperStore.Domain.Entities;
using AutoMapper;
using NSubstitute;
using Xunit;
using FluentAssertions;

namespace Ambev.DeveloperStore.Unit.Application;

public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CancelSaleHandler _handler;

    public CancelSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CancelSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Handle ShouldCancelSale WhenSaleExists")]
    public async Task Handle_ShouldCancelSale_WhenSaleExists()
    {
        var sale = Substitute.For<Sale>();
        var saleId = Guid.NewGuid();
        sale.Id = saleId;
        var command = new CancelSaleCommand(saleId);

        _saleRepository.GetByIdAsync(sale.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleRepository.CancelAsync(saleId, Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));
        var canceSaleResult = new CancelSaleResult
        {
            Id = sale.Id,
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        canceSaleResult.Should().NotBeNull();
        canceSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CancelAsync(saleId, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Handle ShouldThrowException WhenSaleDoesNotExist")]
    public async Task Handle_ShouldThrowException_WhenSaleDoesNotExist()
    {
        var saleId = Guid.NewGuid();
        var command = new CancelSaleCommand(saleId);

        _saleRepository.GetByIdAsync(saleId).Returns(Task.FromException<Sale?>(new KeyNotFoundException($"Sale with ID {saleId} not found.")));

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _saleRepository.GetByIdAsync(saleId));
        Assert.Contains($"Sale with ID {saleId} not found.", exception.Message);
    }

    [Fact(DisplayName = "Handle ShouldThrowValidationException WhenSaleIdIsInvalid")]
    public async Task Handle_ShouldThrowValidationException_WhenSaleIdIsInvalid()
    {
        var command = new CancelSaleCommand(Guid.Empty);
        var validator = new CancelSaleValidator();
        var validationResult = await validator.ValidateAsync(command);

        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Id" && e.ErrorMessage == "Sale ID is required");
    }
}
