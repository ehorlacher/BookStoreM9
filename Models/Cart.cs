﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreM9.Models
{
    public class Cart
    {
        public List<CartLineItem> Items { get; set; }  = new List<CartLineItem>();

        public virtual void AddItem (Book bok, int qty)
        {
            CartLineItem line = Items
                .Where(b => b.Book.Isbn == bok.Isbn)
                .FirstOrDefault();


            if (line == null)
            {
                Items.Add(new CartLineItem
                {
                    Book = bok,
                    Quantity = qty,
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }

        public virtual void RemoveItem (Book bok)
        {
            Items.RemoveAll(x => x.Book.Isbn == bok.Isbn);
        }

        public virtual void ClearCart()
        {
            Items.Clear();
        }


        public double CalculateTotal()
        {
            //Change 25 to reflect the actual price (but how?)
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);

            return sum;
        }
    }

    public class CartLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        
    }
}
