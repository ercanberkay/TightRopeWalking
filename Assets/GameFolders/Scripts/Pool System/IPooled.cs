namespace PoolSystem
{
    public interface IPooled 
    {
        string PoolType { get; set; }
        int PoolId { get; set; }
    }
}

