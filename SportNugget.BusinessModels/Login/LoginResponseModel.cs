namespace SportNugget.BusinessModels.Login
{
    public class LoginResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public string BearerToken { get; set; }
    }
}
