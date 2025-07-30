using TestUnoAppNavigation.Enums;

namespace TestUnoAppNavigation.Presentation;

public partial record MainModel
{
    private INavigator _navigator;

    public MainModel(INavigator navigator)
    {
        _navigator = navigator;
    }

    public async Task FindLocationCmdAsync(CancellationToken ct)
    {
        var entityForFind = new EntityForFindPage(
            EntityTypeName: "location",
            Code: string.Empty);

        //await _navigator.NavigateViewModelAsync<FindEntityModel>(this, data: entityForFind);

        var returnedEntityForFind = await _navigator.GetDataAsync<EntityForFindPage>(this, data: entityForFind);
        // If press cancel button in FindEntityPage, returnedEntityForFind will be null
        if (returnedEntityForFind is null)
        {
            Console.WriteLine("User pressed Cancel button");
            return;
        }   

        Console.WriteLine($"Entity code in MainPage: {returnedEntityForFind!.Code}");
    }

    public async Task FindAssetCmdAsync(CancellationToken ct)
    {
        var entityForFind = new EntityForFindDialog(
            EntityTypeName: "asset",
            Code: string.Empty);

        await _navigator.NavigateViewModelAsync<FindEntityDialogModel>(this, data: entityForFind);

        //_ = _navigator.NavigateViewAsync<FindEntityDialog>(this, qualifier: Qualifiers.Dialog, data: entityForFind);

        //var returnedEntityForFind = await _navigator.GetDataAsync<EntityForFindDialog>(this, qualifier: Qualifiers.Dialog, data: entityForFind);
        //if (returnedEntityForFind is null) {
        //    Console.WriteLine("User pressed Cancel button");
        //    return;
        //}

        //Console.WriteLine($"Entity code in MainPage: {returnedEntityForFind!.Code}");

        Console.WriteLine("FindAssetCmdAsync end");
    }

    public async Task FindOrderCmdAsync(CancellationToken ct)
    {
        var pageShowParam = new OrderPageShowParam(
            ShowVariant: ShowEntityDetailPageVariants.FindEntity,
            Code: null);

        await _navigator.NavigateViewModelAsync<OrderModel>(this, data: pageShowParam);

        Console.WriteLine($"FindOrderCmdAsync ended");
    }

    public async Task CreateOrderCmdAsync(CancellationToken ct)
    {
        var pageShowParam = new OrderPageShowParam(
            ShowVariant: ShowEntityDetailPageVariants.CreateEntity,
            Code: null);

        await _navigator.NavigateViewModelAsync<OrderModel>(this, data: pageShowParam);

        Console.WriteLine($"CreateOrderCmdAsync ended");
    }

    public async Task ViewOrderCmdAsync(CancellationToken ct)
    {
        var pageShowParam = new OrderPageShowParam(
           ShowVariant: ShowEntityDetailPageVariants.ViewEntity,
           Code: 40000001);

        await _navigator.NavigateViewModelAsync<OrderModel>(this, data: pageShowParam);

        Console.WriteLine($"ViewOrderCmdAsync ended");
    }

}
