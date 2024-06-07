using WebApiAllOperations.Dtos.Stock;
using WebApiAllOperations.Model;

namespace WebApiAllOperations.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto()
        {
           Id = stockModel.Id,
           Symbol = stockModel.Symbol,
           CompanyName = stockModel.CompanyName,
           Purchase = stockModel.Purchase,
           LastDiv = stockModel.LastDiv,
           Industry = stockModel.Industry,
           MarketCap = stockModel.MarketCap,
           Comments = stockModel.Comments.Select(c=>c.ToCommentDto()).ToList()
           
        };
    }

    public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockRequestDto)
    {
        return new Stock()
        {
             Symbol = stockRequestDto.Symbol,
             CompanyName = stockRequestDto.CompanyName,
             Purchase = stockRequestDto.Purchase,
             LastDiv = stockRequestDto.LastDiv,
             Industry = stockRequestDto.Industry,
             MarketCap = stockRequestDto.MarketCap
        };
    }
}