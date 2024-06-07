using WebApiAllOperations.Dtos.Comment;
using WebApiAllOperations.Model;

namespace WebApiAllOperations.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId
        };
    }

    public static Comment ToCommentFromCreate(this CreateCommentDto createCommentDto, int stockId)
    {
        return new Comment
        {
            Title = createCommentDto.Title,
            Content = createCommentDto.Content,
            StockId = stockId
        };
    }
    
    public static Comment ToCommentUpdate(this UpdateCommentRequestDto updateCommentDto)
    {
        return new Comment
        {
            Title = updateCommentDto.Title,
            Content = updateCommentDto.Content
           
        };
    }
}