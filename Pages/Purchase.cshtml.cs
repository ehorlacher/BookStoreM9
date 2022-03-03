using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreM9.Infrastructure;
using BookStoreM9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreM9.Pages
{
    public class PurchaseModel : PageModel
    {
        private IBookStoreRepository repo { get; set; }
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        public PurchaseModel (IBookStoreRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        // Isbn is a string, not an integer
        public IActionResult OnPost(string isbn, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.Isbn == isbn);

            cart.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove (string isbn, string returnUrl)
        {
            cart.RemoveItem(cart.Items.First(x => x.Book.Isbn == isbn).Book);

            return RedirectToPage(new {ReturnUrl = returnUrl });
        }
    }
}
