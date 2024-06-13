using WebApiAllOperations.Dtos.Stock;
using WebApiAllOperations.Helpers;
using WebApiAllOperations.Model;

namespace WebApiAllOperations.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject queryObject);

    Task<Stock?> GetByIdAsync(int id);
    Task<Stock?> GetBySymbolAsync(string symbol);

    Task<Stock> CreateAsync(Stock stockModel);

    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);

    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int id);

}