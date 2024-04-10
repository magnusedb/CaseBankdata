using CaseBankdata.Data.Dtos;

namespace CaseBankdata.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> CreateAccountAsync(CreateAccountDto request);
        Task<IEnumerable<AccountDto>> ListAccountsAsync(int customerId);
    }

}
