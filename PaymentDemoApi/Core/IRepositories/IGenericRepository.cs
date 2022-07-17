namespace PaymentDemoApi.IRepositories
{
    public interface  IGenericRepository<T> where T:class 
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Delete(int id);
        
        Task<bool> Add(T entity);
        
    }
}
