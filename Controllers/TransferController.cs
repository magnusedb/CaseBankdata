using CaseBankdata.Data.Dtos;
using CaseBankdata.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseBankdata.Controllers
{

    //TransferController handles transfer-related operations (in this code-case, that would be TRANSFER). It interacts with ITransferService to perform business logic operations
    //And uses the 'out of the box' logger from asp.net core

    [ApiController]
    [Route("api/[controller]")]
    public class TransfersController : ControllerBase
    {
        private readonly ITransferService _transferService;

        //Dependency injection
        public TransfersController(ITransferService transferService)
        {
            _transferService = transferService;
        }


        //Performs high-level validation (model) before attempting to Transfer funds through ITransferService. Returns 400 badrequest (with the validationproblem)
        //200 for successfull transfer or 422 (unprocessable content) along with specific error.
        [HttpPost]
        public async Task<IActionResult> TransferFunds([FromBody] TransferDto request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var result = await _transferService.TransferFundsAsync(request);

            return result.Success ? Ok("Transfer was successfully completed") : StatusCode(422, result.ErrorMessage);
        }

    }
}
