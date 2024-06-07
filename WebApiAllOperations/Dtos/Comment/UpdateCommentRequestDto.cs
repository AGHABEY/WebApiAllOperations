using System.Security;

namespace WebApiAllOperations.Dtos.Comment;

public class UpdateCommentRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}