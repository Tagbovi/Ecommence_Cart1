using ecomence_Cart.CartModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecomence_Cart.CartsDataServices
{
    public class UserDataRepo
    {
        public static  List<User> users = new List<User>()
        {
            new User
            {
                Username = "Albertine", EmailAddress="Albert@gmail.com", GivenName="Kwesi", 
                Password="AmGood", Role="Admin", Surname=" Ferguson"
            },
            new User
            {
                Username = "Michael", EmailAddress="Kojo@gmail.com", GivenName="Kwame",
                Password="Young", Role="User", Surname=" Jackson"
            }
        };
    }
}
