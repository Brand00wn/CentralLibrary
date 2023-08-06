namespace CentralLibrary.ViewModels.User
{
    public class RegisterConfirmationViewModel
    {
        public string Email { get; set; }
        public bool DisplayConfirmAccountLink { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
