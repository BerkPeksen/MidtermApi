namespace MidtermApi.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "admin", EmailAddress = "admin@email.com", Password = "123321", GivenName = "admin", Surname = "Admin", Role = "Administrator" },
            new UserModel() { Username = "berkp", EmailAddress = "Berkpeksen@hotmail.com", Password = "MyPass_w0rd", GivenName = "Berk", Surname = "Peksen", Role = "Administrator" },
            new UserModel() { Username = "hsan", EmailAddress = "hsan@email.com", Password = "MyPass_w0rd", GivenName = "Hasan", Surname = "Caliskan", Role = "Bank Manager" },
        };
    }
}
