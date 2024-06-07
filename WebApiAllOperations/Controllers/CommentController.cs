using Microsoft.AspNetCore.Mvc;
using WebApiAllOperations.Dtos.Comment;
using WebApiAllOperations.Interfaces;
using WebApiAllOperations.Mappers;

namespace WebApiAllOperations.Controllers;
[Route("api/comment")]
[ApiController]
public class CommentController:ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();
        var commentDto = comments.Select(s => s.ToCommentDto());
        return Ok(commentDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment==null)
        {
            return NotFound();
        }

        return Ok(comment);
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
    {
        if (!await _stockRepository.StockExists(stockId))
        {
            return BadRequest("Stock does not exist");
        }

        var commentModel = commentDto.ToCommentFromCreate(stockId);
        await _commentRepository.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentRequestDto updateDto)
    {
        var comment = await _commentRepository.UpdateAsync(id,updateDto.ToCommentUpdate());
        if (comment==null)
        {
          return  NotFound("Comment not found");
        }

        return Ok(comment.ToCommentDto());
    }
}