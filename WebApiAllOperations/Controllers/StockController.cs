using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAllOperations.Data;
using WebApiAllOperations.Dtos.Stock;
using WebApiAllOperations.Mappers;

namespace WebApiAllOperations.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController:ControllerBase
{
    private readonly ApplicationDbContext _context;
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _context.Stock.ToListAsync();
        
         var stocksDto=stocks.Select(s=>s.ToStockDto());

        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _context.Stock.FindAsync(id);

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
        await _context.Stock.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        
        if (stockModel==null)
        {
            return NotFound();
        }

        stockModel.Symbol = updateStockRequestDto.Symbol;
        stockModel.CompanyName = updateStockRequestDto.CompanyName;
        stockModel.Purchase = updateStockRequestDto.Purchase;
        stockModel.LastDiv = updateStockRequestDto.LastDiv;
        stockModel.Industry = updateStockRequestDto.Industry;
        stockModel.MarketCap = updateStockRequestDto.MarketCap;

        await _context.SaveChangesAsync();

        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

        if (stockModel == null)
        {
            return NotFound();
        }
        
        _context.Stock.Remove(stockModel);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}