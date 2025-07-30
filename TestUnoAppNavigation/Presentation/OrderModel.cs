using TestUnoAppNavigation.Enums;

namespace TestUnoAppNavigation.Presentation;

public partial record OrderModel
{

    private INavigator _navigator;
    private OrderPageShowParam _pageShowParam;

    public bool IsCodeForFindVisible => _pageShowParam.ShowVariant == ShowEntityDetailPageVariants.FindEntity;

    public IState<int?> CodeForFind => State<int?>.Value(this, () => null);

    public IState<OrderForView> Order => State.Async(this, GetOrderByCodeAsync);

    public OrderModel(
        OrderPageShowParam pageShowParam,
        INavigator navigatog)
    {
        _navigator = navigatog;
        _pageShowParam = pageShowParam;
    }

    private static OrderForView EmptyOrder() =>
        new(Code: null, Name: string.Empty);

    private async ValueTask<OrderForView?> GetOrderByCodeAsync(CancellationToken ct)
    {
        try {

            if (_pageShowParam.ShowVariant == ShowEntityDetailPageVariants.CreateEntity)
                return EmptyOrder();

            var codeForFind = await CodeForFind;

            if (_pageShowParam.ShowVariant == ShowEntityDetailPageVariants.FindEntity && codeForFind == null)
                return EmptyOrder();

            await Task.Delay(1_000, ct); // Simulate a delay for fetching the order

            if (_pageShowParam.Code == 40000000)
                return null; // Simulate not found

            var order = new OrderForView(
                Code: _pageShowParam.Code ?? codeForFind,
                Name: $"Order {_pageShowParam.Code ?? codeForFind}");

            Console.WriteLine($"Order [{order.Code} - {order.Name}] was loaded");

            return order;

        } catch (Exception ex) {
            Console.WriteLine($"Error loading order: {ex.Message}");
            throw;
        }
    }

    public async Task RefreshCmdAsync(CancellationToken ct)
    {
        await Order.TryRefreshAsync(ct);
    }

    public async Task SaveCmdAsync(CancellationToken ct)
    {

        // Check if Order was changed ???


        var order = await Order;

        Console.WriteLine($"Order [{order.Code} - {order.Name}] was saved");
    }

    public async Task CloseCmdAsync(CancellationToken ct)
    {
        await _navigator.NavigateBackAsync(this, cancellation: ct);
    }
}
