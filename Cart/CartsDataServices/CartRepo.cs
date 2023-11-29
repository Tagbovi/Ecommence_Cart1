using ecomence_Cart.CartModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Wordprocessing;
using System;

namespace ecomence_Cart.CartModel
{
    public class CartRepo : ICartRepo
    {
        private readonly CartDb _cartdb;
        public CartRepo(CartDb cartdb)
        {
            this._cartdb = cartdb;
        }
        public async Task<Cart> AddCart(Cart cartItem)
        {
            {
                var existingCartItem = await _cartdb.CarT.FirstOrDefaultAsync(c => c.ItemID == cartItem.ItemID);

                // add new cart item
                if (existingCartItem == null)
                {

                    _cartdb.CarT.Add(cartItem);
                    await _cartdb.SaveChangesAsync();
                    return cartItem;
                }

                // update cart item
                existingCartItem.Quantity = existingCartItem.Quantity+cartItem.Quantity;
                existingCartItem.UnitPrice = existingCartItem.UnitPrice + cartItem.UnitPrice;
                //existingCart.UnitPrice += cartItem.UnitPrice;

                //existingCartItem.UpDatedTime = DateTime.UtcNow;
                _cartdb.CarT.Update(existingCartItem);
                await _cartdb.SaveChangesAsync();
                return existingCartItem;

            }
        }


        public async Task DeleteCartById(int id)
        {
            var result = await _cartdb.CarT.FirstOrDefaultAsync(e => e.ItemID == id);
            if (result != null)
            {
                _cartdb.CarT.Remove(result);
                await _cartdb.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            return await _cartdb.CarT.ToListAsync();
        }

        public async Task<IEnumerable<Cart>> GetByItemName([FromQuery] string name)
        {
            IQueryable<Cart> cartQuery = _cartdb.CarT;

            if (!string.IsNullOrEmpty(name))
            {

                cartQuery = cartQuery.Where(e => e.ItemName.Contains(name));
            }

            return await cartQuery.ToListAsync();
        }

        public async Task<IEnumerable<Cart>> GetByPhoneNumber(int phonenumber)
        {
            IQueryable<Cart> cartquery = _cartdb.CarT;
            if (phonenumber != 0)
            {
                cartquery = cartquery.Where(e => e.PhoneNumbers == phonenumber);
            }
            return await cartquery.ToListAsync();
        }

        public async Task<IEnumerable<Cart>> GetByQuantity([FromBody] int quantity)
        {
            IQueryable<Cart> cartQuery = _cartdb.CarT;

            if (quantity != 0)
            {

                cartQuery = cartQuery.Where(e => e.Quantity == quantity);
            }

            return await cartQuery.ToListAsync();
        }

        public async Task<Cart> GetCartById(int id)
        {
            return await _cartdb.CarT.FirstOrDefaultAsync(e => e.ItemID == id);
        }

        public async Task<IEnumerable<Cart>> GetTime(DateTime? time = null)
        {


            var itemsAtSpecifiedTime = _cartdb.CarT
              .Where(e => e.AddedTime <= time)
              .ToListAsync();

            return await itemsAtSpecifiedTime;

        }



    }
}
