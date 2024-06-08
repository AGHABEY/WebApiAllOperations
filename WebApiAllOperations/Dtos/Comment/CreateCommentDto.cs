using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace WebApiAllOperations.Dtos.Comment;

public class CreateCommentDto
{
    [Required]
    [MinLength(5,ErrorMessage = "Title must be 5 characters")]
    [MaxLength(150,ErrorMessage = "Title cannot be over 150 characters")]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}