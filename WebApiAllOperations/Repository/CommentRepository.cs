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

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comment.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment> UpdateAsync(int id, Comment commentModel)
    {
        var existingComment = await _context.Comment.FindAsync(id);

        if (existingComment==null)
        {
            return null;
        }

        existingComment.Title = commentModel.Title;
        existingComment.Content = commentModel.Content;

        await _context.SaveChangesAsync();

        return existingComment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);

        if (commentModel==null)
        {
            return null;
        }

        _context.Comment.Remove(commentModel);
        await _context.SaveChangesAsync();

        return commentModel;

    }
}