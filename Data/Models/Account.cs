namespace CaseBankdata.Data.Models
{
    public class Account
    {

        public int AccountId { get; set; } //pk
        public string AccountNumber { get; set; } //kontonr
        public decimal Balance { get; set; } //balance
        public int CustomerId { get; set; } //kundenr

        //Constructor for accounts ensures it is always instantiated with accountnumber, balance and customerid
        public Account(string accountNumber, decimal balance, int customerId) 
        {
            this.AccountNumber = accountNumber;
            this.Balance = balance;
            this.CustomerId = customerId;
        }

        //Encapsulating the core logic of withdrawing and deposing in here, as opposed to the Service layer
        public bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        //Måske valuta, kontostatus (frosset, tilgængelig), type (lønkonto, fælleskonto)?, DateTimes til oprettet/historie, 
    }
}
