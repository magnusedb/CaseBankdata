using CaseBankdata.Data.Dtos;
using CaseBankdata.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

//AccountsController handles account-related operations (in this code-case, that would be CREATE and LIST). It interacts with IAccountService to perform business logic operations
//And uses the 'out of the box' logger from asp.net core


[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountsController> _logger;

    //Dependency injection
    public AccountsController(IAccountService accountService, ILogger<AccountsController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    //Operation: CREATE, performs high-level validation (model) before attempting to create the account via IAccountService. Returns 201 if ressource was created (no CreatedAtAction)
    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto request)
    {
        //Checks request against rules (data annotations) in the dto 
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Model state is invalid. Request: {@Request}", request);
            return ValidationProblem(ModelState);
        }

        //Calls IAccountService to create account
        var result = await _accountService.CreateAccountAsync(request);

        //If the result is null, log the error and return generic 500
        if (result == null)
        {
            _logger.LogError("Failed to create account. Request: {@Request}", request);
            return StatusCode(500, "An internal error occurred while creating the account.");
        }

        //If ressource was created, return 201
        return StatusCode(201, result);
    }

    //Operation: LIST, returns accounts for a given customer ID through IAccountService
    [HttpGet("list")]
    public async Task<IActionResult> ListAccounts(int customerId)
    {   //No model validation prior to calling IAccountService
        var accounts = await _accountService.ListAccountsAsync(customerId);
        //If accounts equals null or is empty, return 404 notfound
        if (accounts == null || !accounts.Any())
        {
            return NotFound($"No accounts found for Customer ID: {customerId}.");
        }

        //return list
        return Ok(accounts);    
    }
}
