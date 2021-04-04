namespace AKHWebshop.Models.Http.Request.DTO
{
    public interface IModelMerger
    {
        public void CopyValues<T>(T target, T source);
    }
}