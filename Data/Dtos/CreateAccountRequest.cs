using System.ComponentModel.DataAnnotations;

namespace CaseBankdata.Data.Dtos
{
    public class CreateAccountDto
    {
        //I use Dtos to transfer data between layers
        //Certain data annotations are used to enforce business rules (e.g initial deposit)
        public int CustomerId { get; set; }

        [Required]
        [Range(500, double.MaxValue, ErrorMessage = "InitialDeposit must be at least 500.")]
        public decimal InitialDeposit { get; set; }
    }
}
