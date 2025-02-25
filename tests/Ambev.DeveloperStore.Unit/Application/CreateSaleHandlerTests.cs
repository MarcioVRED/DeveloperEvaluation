using Ambev.DeveloperStore.Application.Sales.CreateSale;
using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Domain.Repositories;
using NSubstitute;
using Xunit;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new CreateSaleHandler(_saleRepository);
    }

    [Theory]
    [ClassData(typeof(CreateSaleHandlerTestData))]
    public async Task Handle_Should_Create_Sale_Successfully(CreateSaleCommand command)
    {
        // Arrange
        var sale = new Sale { Id = Guid.NewGuid(), SaleNumber = command.SaleNumber, SaleDate = command.SaleDate };
        _saleRepository.CreateSaleAsync(Arg.Any<Sale>()).Returns(Task.FromResult(sale));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(command.SaleNumber, result.SaleNumber);
        Assert.Equal(command.SaleDate, result.SaleDate);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_CustomerName_Is_Empty()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            SaleNumber = "123456",
            CustomerName = "",
            BranchName = "Filial SP",
            SaleDate = DateTime.UtcNow,
            Items = new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand { ProductId = Guid.NewGuid(), ProductName = "Cerveja Pilsen", Quantity = 10, UnitPrice = 5.00 }
            }
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Items_Are_Empty()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            SaleNumber = "123456",
            CustomerName = "Marcio Martins",
            BranchName = "Filial SP",
            SaleDate = DateTime.UtcNow,
            Items = new List<CreateSaleItemCommand>()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
