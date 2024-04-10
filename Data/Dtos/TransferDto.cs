using System.ComponentModel.DataAnnotations;

namespace CaseBankdata.Data.Dtos
{
    public class TransferDto
    {
        //I use Dtos to transfer data between layers
        //Certain data annotations are used to enforce business rules (e.g transfer cant be zero)
        [Required]
        public string FromAccountNumber { get; set; }

        [Required]
        public string ToAccountNumber { get; set; } // Adjust types as necessary

        [Range(0.01, double.MaxValue, ErrorMessage = "Transfer amount must be greater than zero.")]
        public decimal Amount { get; set; }

    }

}

