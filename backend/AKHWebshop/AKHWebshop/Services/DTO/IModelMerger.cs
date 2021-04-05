namespace AKHWebshop.Services.DTO
{
    public interface IModelMerger
    {
        public void CopyValues<T>(T target, T source);
    }
}