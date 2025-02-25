using Ambev.DeveloperStore.Application.Sales.CreateSale;
using Ambev.DeveloperStore.Application.Sales.CreateSaleItem;
using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Domain.Repositories;
using AutoMapper;
using NSubstitute;
using Xunit;

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

        _saleRepository.CreateAsync(Arg.Any<Sale>()).Returns(Task.FromResult(sale));

        _mapper.Map<Sale>(command).Returns(sale); // Mapeamento do AutoMapper

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_CustomerName_Is_Empty()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow,
            "",
            "Filial SP",
            new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 10, 5.00M)
            }
        );

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Items_Are_Empty()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow,
            "Marcio Martins",
            "Filial SP",
            new List<CreateSaleItemCommand>()
        );

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_SaleNumber_Is_Invalid()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow,
            "Marcio Martins",
            "Filial SP",
            new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 10, 5.00M)
            }
        );

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_BranchName_Is_Empty()
    {
        var command = new CreateSaleCommand(
            DateTime.UtcNow,
            "Marcio Martins",
            "",
            new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 10, 5.00M)
            }
        );

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
