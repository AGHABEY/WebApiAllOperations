using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiAllOperations.Extensions;
using WebApiAllOperations.Interfaces;
using WebApiAllOperations.Model;

namespace WebApiAllOperations.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController:ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository,IPortfolioRepository portfolioRepository)
    {
        _userManager = userManager;
        _stockRepository = stockRepository;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> addPortfolio(string symbol)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var stock = _stockRepository.GetBySymbolAsync(symbol);

        if (stock == null)
            return BadRequest("Stock not found");
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

        if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            return BadRequest("Cannot add same stock to portfolio");

        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            AppUserId = appUser.Id
        };

        await _portfolioRepository.CreateAsync(portfolioModel);

        if (portfolioModel==null)
        {
            return StatusCode(500, "Could not create");
        }
        else
        {
            return Created();
        }

    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);

        var userPortflio = await _portfolioRepository.GetUserPortfolio(appUser);

        var filterefStock = userPortflio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

        if (filterefStock.Count() == 1)
        {
            await _portfolioRepository.DeletePortfolio(appUser, symbol);
        }
        else
        {
            return BadRequest("Stock not is your portfolio");
        }

        return Ok();
    }
    
}