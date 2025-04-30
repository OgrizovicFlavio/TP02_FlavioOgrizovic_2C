public interface IPooleable
{
    void OnGetFromPool();
    void OnReturnToPool();
    void Disable();
    void ResetToDefault();
}
