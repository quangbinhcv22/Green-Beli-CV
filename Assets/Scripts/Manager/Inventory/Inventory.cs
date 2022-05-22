using System.Collections.Generic;
using Manager.Inventory;

public class Inventory
{
    private readonly Dictionary<int, Dictionary<int, IResource>> _dictionary =
        new Dictionary<int, Dictionary<int, IResource>>();

    public Dictionary<int, IResource> GetResources(int resourceType)
    {
        var result = _dictionary.TryGetValue(resourceType, out var resourceDict);

        if (result == false)
        {
            resourceDict = new Dictionary<int, IResource>();
            _dictionary.Add(resourceType, resourceDict);
        }

        return resourceDict;
    }

    private IResource Get(int resourceType, int resourceId)
    {
        var resourceDict = GetResources(resourceType);
        var result = resourceDict.TryGetValue(resourceId, out var resource);
        if (result == false)
        {
            resource = ResourceFactory.Create(resourceType, resourceId);
            resourceDict.Add(resourceId, resource);
        }

        return resource;
    }


    public int GetMoney(MoneyType moneyType) => GetMoney((int)moneyType);

    public int GetMoney(int moneyType)
    {
        var money = Get(ResourceType.Money, moneyType);
        return money.Get();
    }


    public void SetMoney(MoneyType moneyType, int value) => SetMoney((int)moneyType, value);

    private void SetMoney(int moneyType, int value)
    {
        var money = Get(ResourceType.Money, moneyType);
        money.Set(value);
    }

    
    public int AddMoney(MoneyType moneyType, int value) => AddMoney((int)moneyType, value);

    private int AddMoney(int moneyType, int value)
    {
        var money = Get(ResourceType.Money, moneyType);
        money.Add(value);

        return money.Get();
    }

    
    public int SubMoney(MoneyType moneyType, int value) => SubMoney((int)moneyType, value);

    public int SubMoney(int moneyType, int value)
    {
        var money = Get(ResourceType.Money, moneyType);
        money.Sub(value);

        return money.Get();
    }
}