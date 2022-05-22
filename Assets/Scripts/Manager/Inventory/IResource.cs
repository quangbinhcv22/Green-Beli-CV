namespace Manager.Inventory
{
    public interface IResource
    {
        int Type { get;}
        
        int Id { get; }
        
        int Get();
        void Set(int value);
        void Add(int value);
        void Sub(int value);
    }
}