namespace CaseBankdata.Services.Utilities.Errors
{
    //Example of an object in which error messages can be tailored to specific needs. I decided to just leave it at (true, no message) and (false, message) 
    public class OperationResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        //Factories
        public static OperationResult Ok() => new OperationResult { Success = true };
        public static OperationResult Fail(string errorMessage) => new OperationResult { Success = false, ErrorMessage = errorMessage };
    }

}
