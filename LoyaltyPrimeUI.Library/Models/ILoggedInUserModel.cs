namespace LoyaltyPrimeUI.Library.Models
{
    public interface ILoggedInUserModel
    {
        string Email { get; set; }
        string Id { get; set; }
        string Token { get; set; }
    }
}