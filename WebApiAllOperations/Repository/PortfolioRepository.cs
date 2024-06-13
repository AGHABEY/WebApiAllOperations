using Microsoft.EntityFrameworkCore;
using WebApiAllOperations.Data;
using WebApiAllOperations.Interfaces;
using WebApiAllOperations.Model;

namespace WebApiAllOperations.Repository;

public class PortfolioRepository:IPortfolioRepository
{
    private readonly ApplicationDbContext _context;
    public PortfolioRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
    }

    public async Task<Portfolio> CreateAsync(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();

        return portfolio;
    }
}