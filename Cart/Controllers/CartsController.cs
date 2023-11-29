using ecomence_Cart.CartModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ecomence_Cart.CartModel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepo cartRepo;
        public CartsController(ICartRepo _cartRepo)
        {
            this.cartRepo = _cartRepo;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme,Roles="Admin,User")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAllCarts()
        {
            try
            {
                
                return Ok(await cartRepo.GetAllCarts());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public async Task<ActionResult<Cart>> GetByCartsId(int id)
        {
            try
            {



                var result = await cartRepo.GetCartById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound("doesnot exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }
       
        [HttpGet("getByName/{searchbyName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetItemByName(string itemName)
        {
            try
            {
                var result = await cartRepo.GetByItemName(itemName);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("getByQuantity/{searchbyQuantity}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetItemByQuantity(int Quantity)
        {
            try
            {
                var result = await cartRepo.GetByQuantity(Quantity);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("getByphoneNumber/{searchbyPhoneNumber}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]

        public async Task<ActionResult<IEnumerable<Cart>>> GetByPhoneNumber(int number)
        {
            try
            {
                var result = await cartRepo.GetByPhoneNumber(number);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }

        [HttpGet("getByTime/{searchbyTime}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]

        public async Task<ActionResult<IEnumerable<Cart>>> GetByTime(DateTime? time)
        {
            try
            {
                var result = await cartRepo.GetTime(time);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            try
            {
                if (cart == null)
                {
                    return BadRequest("CartItem cannot be null");
                }
                if(cart.UnitPrice <= 0 )
                {
                    return BadRequest("UnitPrice cannot be null");
                }
                if (cart.Quantity <= 0)
                {
                    return BadRequest("UnitPrice cannot be null");
                }

                var result = await cartRepo.AddCart(cart);

                // Return the newly created cart
                return CreatedAtAction(nameof(GetByCartsId), new { id = result.ItemID }, result);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> DeleteCart(int id)
        {
            try
            {


                var result = await cartRepo.GetCartById(id);
                if (result != null)
                {
                    await cartRepo.DeleteCartById(id);
                    return Ok($"id with number {id} deleted successfully");
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      

    }
}
