using WebApiAllOperations.Dtos.Stock;
using WebApiAllOperations.Model;

namespace WebApiAllOperations.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();

    Task<Stock?> GetByIdAsync(int id);

    Task<Stock> CreateAsync(Stock stockModel);

    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);

    Task<Stock?> DeleteAsync(int id);

}