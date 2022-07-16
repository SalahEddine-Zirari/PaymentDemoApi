namespace PaymentDemoApi.IRepositories
{
    public interface  IGenericRepository<T> where T:class 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task DeleteAsync(int id);

        Task<bool> Insert(T entity);
        
    }
}
