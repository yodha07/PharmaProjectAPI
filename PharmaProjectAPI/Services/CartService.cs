﻿using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class CartService:ICartRepository
    {
        private readonly ApplicationDbContext db;
        public CartService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddToCart(Cart cart)
        {
            db.Carts.Add(cart);
            db.SaveChanges();
        }

        public void DeleteCart(int id)
        {
            var data=db.Carts.Find(id);
            db.Carts.Remove(data);
            db.SaveChanges();
        }

        public List<CartDTO2> GetCartItems(int id)
        {
            return db.Carts.Include(c => c.Medicine).Include(x => x.User).Where(c => c.UserId == id).
                Select(c => new CartDTO2()
                {
                    CartId = c.CartId,
                    userName = c.User.Username,
                    medicineName=c.Medicine.Name,
                    Quantity= c.Quantity,
                }).ToList();
        }
    }
}
