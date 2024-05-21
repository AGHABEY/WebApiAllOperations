using WebApiAllOperations.Model;

namespace WebApiAllOperations.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
}