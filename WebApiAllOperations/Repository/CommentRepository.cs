using Microsoft.EntityFrameworkCore;
using WebApiAllOperations.Data;
using WebApiAllOperations.Interfaces;
using WebApiAllOperations.Model;

namespace WebApiAllOperations.Repository;

public class CommentRepository:ICommentRepository
{
    private readonly ApplicationDbContext _context;
    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comment.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comment.FindAsync(id);
    }
}