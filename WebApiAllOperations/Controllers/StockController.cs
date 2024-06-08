using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAllOperations.Data;
using WebApiAllOperations.Dtos.Stock;
using WebApiAllOperations.Helpers;
using WebApiAllOperations.Interfaces;
using WebApiAllOperations.Mappers;

namespace WebApiAllOperations.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController:ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepository;
    public StockController(ApplicationDbContext context, IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
    {
        var stocks = await _stockRepository.GetAllAsync(queryObject);
        
         var stocksDto=stocks.Select(s=>s.ToStockDto());

        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);

        if (stock==null)
        {
            return NotFound();
        }

        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockRequestDto)
    {
        var stockModel = stockRequestDto.ToStockFromCreateDTO();
        await _stockRepository.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
        var stockModel = await _stockRepository.UpdateAsync(id,updateStockRequestDto);
        
        if (stockModel==null)
        {
            return NotFound();
        }

       

        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await _stockRepository.DeleteAsync(id);

        if (stockModel == null)
        {
            return NotFound();
        }
     
        return NoContent();
    }
}