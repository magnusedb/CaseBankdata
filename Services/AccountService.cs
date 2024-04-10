using CaseBankdata.Data;
using CaseBankdata.Data.Dtos;
using CaseBankdata.Data.Models;
using CaseBankdata.Services.Utilities;
using CaseBankdata.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CaseBankData.Services
{

    //IAccountService is located within the service layer, representing specific business logic operations. It operates upon the Data layer, to retrieve or store.
    public class AccountService : IAccountService
    {
        //The db context used in this codecase is a database hosted on my 2nd pc, WLS2
        private readonly CaseBankdataDbContext _context;


        //Dependency injection
        public AccountService(CaseBankdataDbContext context)
        {
            _context = context;
        }

        //CreateAccountAsync attempts to create ressource, by mapping the dto to the Account entity. 'Account' contains data annotations (e.g required initial deposit)
        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            try
            {
                var account = new Account
                    (
                        AccountNumberUtil.GenerateAccountNumber(),
                        createAccountDto.InitialDeposit,
                        createAccountDto.CustomerId
                    );
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();

                //Returns a mapped accountDto if the attempt is successfull
                return new AccountDto
                {
                    AccountId = account.AccountId,
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance
                };
            }
            //An example of catching specific exception (without global exception handling)
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while creating the account.", ex);
            }
            //An example of catching generic exception
            catch (Exception ex)
            {
                throw new Exception("A general error occurred.", ex);
            }
        }

        //ListAccountsAsync retrieves all accounts associated with a customerId, whether any exists or not.
        public async Task<IEnumerable<AccountDto>> ListAccountsAsync(int customerId)
        {
            var accounts = await _context.Accounts
            .Where(a => a.CustomerId == customerId)
            .Select(a => new AccountDto
            {
                AccountId = a.AccountId,
                AccountNumber = a.AccountNumber,
                Balance = a.Balance
            })
            .ToListAsync();

            return accounts;
        }
    }
}
