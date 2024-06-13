using Microsoft.EntityFrameworkCore;
using WebApiAllOperations.Data;
using WebApiAllOperations.Dtos.Stock;
using WebApiAllOperations.Helpers;
using WebApiAllOperations.Interfaces;
using WebApiAllOperations.Model;

namespace WebApiAllOperations.Repository;

public class StockRepository:IStockRepository
{
    private readonly ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
    {
        var stocks= _context.Stock.Include(c => c.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
        {
            if (queryObject.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
            {
                stocks = queryObject.IsDescending
                    ? stocks.OrderByDescending(s => s.Symbol)
                    : stocks.OrderBy(s => s.Symbol);
            }
        }

        var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
        
        return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stock.Include(c=>c.Comments).FirstOrDefaultAsync(i=>i.Id==id);
    }

    public async Task<Stock?> GetBySymbolAsync(string symbol)
    {
        return await _context.Stock.FirstOrDefaultAsync(s => s.Symbol == symbol);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stock.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
    {
        var exsitingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

        if (exsitingStock==null)
        {
            return null;
        }

        exsitingStock.Symbol = stockDto.Symbol;
        exsitingStock.CompanyName = stockDto.CompanyName;
        exsitingStock.Purchase = stockDto.Purchase;
        exsitingStock.LastDiv = stockDto.LastDiv;
        exsitingStock.Industry = stockDto.Industry;
        exsitingStock.MarketCap = stockDto.MarketCap;

        await _context.SaveChangesAsync();

        return exsitingStock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

        if (stockModel==null)
        {
            return null;
        }

        _context.Stock.Remove(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public Task<bool> StockExists(int id)
    {
        return _context.Stock.AnyAsync(s => s.Id == id);
    }
}