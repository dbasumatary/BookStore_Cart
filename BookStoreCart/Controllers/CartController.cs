﻿using BookStoreCart.Entity;
using BookStoreCart.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Security.Claims;

namespace BookStoreCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IBookService _bookService;
        public ResponseEntity response;

        public CartController(ICartService cartService, IBookService bookService)
        {
            _cartService = cartService;
            response = new ResponseEntity();
            _bookService = bookService;
        }

        [Authorize]
        [HttpPost]
        [Route("AddToCart")]
        public async Task<ResponseEntity> AddToCart(int bookId, int quantity)
        {
            //var success = _cartService.AddToCart(string token, int userId, int bookId, int cartQuantity);

            string jwtTokenWithBearer = HttpContext.Request.Headers["Authorization"];
            string jwtToken = jwtTokenWithBearer.Substring("Bearer ".Length);

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));

            CartEntity cartResponse = await _cartService.AddToCart(jwtToken, userId, bookId, quantity);
            if (cartResponse != null)
            {
                response.Data = cartResponse;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }

            return response;
        }


        /*[Authorize]
        [HttpPut]
        [Route("UpdateCart")]
        public ResponseEntity UpdateCart(int bookId, CartEntity cart)
        {
            CartEntity updatedCart = _cartService.UpdateCart(bookId, cart);

            if (updatedCart != null)
            {
                response.Data = updatedCart;
                response.Message = "Book is edited successfully";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Cannot edit the book";
            }
            return response;
        }*/


        [Authorize]
        [HttpDelete]
        [Route("DeleteBookInCart")]
        public ResponseEntity DeleteBookInCart(int bookId)
        {
            bool result = _cartService.DeleteFromCart(bookId);

            if (result)
            {
                response.Data = result;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong in deleting the book in cart";
            }
            return response;
        }


        [HttpGet]
        [Route("GetCartItems")]
        public ResponseEntity GetCartItems()
        {
            IEnumerable<CartEntity> books = _cartService.GetCartDetails();

            if (books.Any())
            {
                response.Data = books;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "No items in cart";
            }
            return response;
        }

    }
}
