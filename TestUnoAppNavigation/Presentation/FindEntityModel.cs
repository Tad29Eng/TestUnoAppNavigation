namespace TestUnoAppNavigation.Presentation;

public partial record FindEntityModel
{

    private INavigator _navigator;

    public EntityForFindPage Entity { get; init; }

    public string NavigationBarContent => $"Input code {Entity.EntityTypeName}";

    public string TblcEntityCodeText => $"Code {Entity.EntityTypeName}";

    public FindEntityModel(
        EntityForFindPage entity, 
        INavigator navigator)
    {  
        _navigator = navigator;
        
        Entity = entity;
    }
    
    public async Task FindCmdAsync()
    {
        Console.WriteLine($"Entity code in FindEntityPage: {Entity.Code}");

        await _navigator.NavigateBackWithResultAsync(this, data: Entity);
    }

    public async Task CloseCmdAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

}
