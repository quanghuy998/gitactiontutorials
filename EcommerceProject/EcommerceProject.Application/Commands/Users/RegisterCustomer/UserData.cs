namespace EcommerceProject.Application.Commands.Users.RegisterCustomer
{
    public class UserData
    {
        public Guid UserId { get; }
        public int CartId { get; }
        public UserData(Guid userId, int cartId)
        {
            this.UserId = userId;
            this.CartId = cartId;
        }
    }
}
