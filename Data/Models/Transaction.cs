using CaseBankdata.Common.Enums;

namespace CaseBankdata.Data.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; } //pk
        public int AccountId { get; set; } //fk
        public TransactionType TransactionType { get; set; } //enum fra common
        public decimal Amount { get; set; } //credit eller debit på baggrund af TransactionType
        public DateTime TransactionDate { get; set; } //Dato
        public string? RecipientAccount { get; set; } //modtager, kan være null hvis eks. deposit/withdraw
    }
}
