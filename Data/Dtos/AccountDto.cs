namespace CaseBankdata.Data.Dtos
{
    public class AccountDto
    {
        //I use Dtos to transfer data between layers
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
