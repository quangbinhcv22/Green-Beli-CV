namespace Manager.Inventory
{
    public static class ResourceFactory
    {
        public static IResource Create(int resourceType, int resourceId)
        {
            switch (resourceType)
            {
                case ResourceType.Money:
                    switch (resourceId)
                    {
                        case (int)MoneyType.GFruit:
                            return new GFRUITToken();
                        default:
                            return new Money(resourceId);
                    }
                default:
                    return null;
            }
        }
    }
}