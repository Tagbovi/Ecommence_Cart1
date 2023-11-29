using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ecomence_Cart.CartModel
{
    public interface ICartRepo
    {
        Task<IEnumerable<Cart>> GetAllCarts();
        
        Task <Cart> AddCart(Cart cart);
        Task<Cart> GetCartById(int id);
        Task DeleteCartById(int id);
        Task<IEnumerable<Cart>> GetByPhoneNumber(int phonenumber);
        Task<IEnumerable<Cart>> GetByQuantity(int quantity);   
        Task<IEnumerable<Cart>> GetByItemName(string name);
        Task<IEnumerable<Cart>> GetTime(DateTime? time );
    }
}
