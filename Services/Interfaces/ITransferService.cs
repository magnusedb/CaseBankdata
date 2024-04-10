using CaseBankdata.Data.Dtos;
using CaseBankdata.Services.Utilities.Errors;

namespace CaseBankdata.Services.Interfaces
{
    public interface ITransferService
    {
        public Task<OperationResult> TransferFundsAsync(TransferDto request);

    }
}
