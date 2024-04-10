using CaseBankdata.Data.Dtos;
using CaseBankdata.Data.Models;
using CaseBankdata.Data;
using Microsoft.EntityFrameworkCore;
using CaseBankdata.Services.Interfaces;
using CaseBankdata.Common.Enums;
using CaseBankdata.Services.Utilities.Errors;

namespace CaseBankdata.Services
{
    //ITransferService is located within the Service layer, where it performs business logic operations. It operates upon the Data layer, by retrieving or storing information.
    public class TransferService : ITransferService
    {
        private readonly CaseBankdataDbContext _dbContext;
        private readonly ILogger<TransferService> _logger;

        //Dependency injection
        public TransferService(CaseBankdataDbContext dbContext, ILogger<TransferService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        //TransferFundsAsync begins a transaction, in which it performs certain checks to ensure rules are met. It attempts to validate the existence of both accounts
        //And ensure there are enough funds before committing to the transfer. It then records the transaction (one for each account) before returning 200.
        public async Task<OperationResult> TransferFundsAsync(TransferDto request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var fromAccount = await _dbContext.Accounts
                    .SingleOrDefaultAsync(account => account.AccountNumber == request.FromAccountNumber);
                var toAccount = await _dbContext.Accounts
                    .SingleOrDefaultAsync(account => account.AccountNumber == request.ToAccountNumber);

                if (fromAccount == null || toAccount == null)
                {
                    _logger.LogWarning("One or both accounts not found during transfer. From: {FromAccount}, To: {ToAccount}", request.FromAccountNumber, request.ToAccountNumber);
                    return OperationResult.Fail("One or both accounts not found.");
                }

                if (!fromAccount.Withdraw(request.Amount))
                {
                    _logger.LogWarning("Insufficient funds for transfer from account {FromAccount}.", fromAccount.AccountNumber);
                    return OperationResult.Fail("Insufficient funds.");
                }

                toAccount.Deposit(request.Amount);

                await RecordTransactionsAsync(fromAccount, toAccount, request.Amount);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return OperationResult.Ok();
            }
            //Example of catching and logging generic exception
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error during funds transfer from {FromAccount} to {ToAccount}.", request.FromAccountNumber, request.ToAccountNumber);
                return OperationResult.Fail("An error occurred during the transfer.");
            }
        }
        //Helper method to create new transaction entities and store them
        private async Task RecordTransactionsAsync(Account fromAccount, Account toAccount, decimal amount)
        {
            var debitTransaction = new Transaction
            {
                AccountId = fromAccount.AccountId,
                TransactionType = TransactionType.Debit,
                Amount = -amount,
                TransactionDate = DateTime.UtcNow,
                RecipientAccount = toAccount.AccountNumber
            };

            var creditTransaction = new Transaction
            {
                AccountId = toAccount.AccountId,
                TransactionType = TransactionType.Credit,
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                RecipientAccount = fromAccount.AccountNumber
            };

            _dbContext.Transactions.Add(debitTransaction);
            _dbContext.Transactions.Add(creditTransaction);

            await _dbContext.SaveChangesAsync();
        }
    }
}
