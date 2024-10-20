using CartSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartSystem.Controllers
{
    public class CartController : Controller
    {
        int count = 0;

        private readonly MyDbContext _context;
        public CartController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(string productId)
        {
            if (Guid.TryParse(productId, out Guid productGuid))
            {
                List<CartItems> cartItems = HttpContext.Session.Get<List<CartItems>>("Cart") ?? new List<CartItems>();

                int index = ProductIsInCart(productId);

                if (index != -1)  
                {
                    TempData["CartMessage"] = "Product already exists in the Cart!";
                    return RedirectToAction("Index");
                }
                else
                {
                    var product = _context.Products.Find(productGuid);

                    if (product != null)
                    {
                        if (product.Quantity >= 1)
                        {
                            cartItems.Add(new CartItems
                            {
                                Product = product,
                                Qty = 1
                            });
                        }
                    }
                }

                HttpContext.Session.Set("Cart", cartItems);
            }
            else
            {
                return BadRequest("Invalid product ID.");
            }

            return RedirectToAction("FetchData", "Home");
        }


        public int ProductIsInCart(string productId)
        {
            if (Guid.TryParse(productId, out Guid productGuid))
            {
                List<CartItems> cartItems = HttpContext.Session.Get<List<CartItems>>("Cart") ?? new List<CartItems>();

                for (int i = 0; i < cartItems.Count; i++)
                {
                    if (cartItems[i].Product!.Id == productGuid)
                    {
                        return i; 
                    }
                }
            }
            return -1;  
        }

        [HttpPost]
        public JsonResult UpdateCartQty(string productId, string action)
        {
            if (Guid.TryParse(productId, out Guid productGuid))
            {
                List<CartItems> cartItems = HttpContext.Session.Get<List<CartItems>>("Cart") ?? new List<CartItems>();

                var cartItem = cartItems.FirstOrDefault(c => c.Product.Id == productGuid);

                if (cartItem != null)
                {
                    if (action == "increase")
                    {
                        cartItem.Qty++;
                    }
                    else if (action == "decrease" && cartItem.Qty > 1)
                    {
                        cartItem.Qty--;
                    }

                    HttpContext.Session.Set("Cart", cartItems);

                    var totalAmount = cartItems.Sum(c => c.Product.Price * c.Qty);

                    return Json(new { totalAmount });
                }
            }

            return Json(new { totalAmount = 0 });
        }

        public IActionResult CheckOut()
        {
            List<CartItems> cartItems = HttpContext.Session.Get<List<CartItems>>("Cart") ?? new List<CartItems>();

            Orders orders = new Orders
            {
                OrderDate = DateTime.Now,
                TotalAmount = cartItems.Sum(x => x.Qty * x.Product.Price),
                OrderData = new List<OrdersItems>(),
            };

            foreach (var item in cartItems)
            {
                OrdersItems ordersItems = new OrdersItems
                {
                    ProductId = item.Product!.Id.ToString(),
                    Quantity = item.Qty,
                    Price = item.Qty * item.Product.Price,
                    Orders = orders,
                };

                orders.OrderData.Add(ordersItems);

                var products = _context.Products.Find(item.Product.Id);
                if (products != null)
                {
                    if (products.Quantity >= item.Qty)
                    {
                        products.Quantity -= item.Qty;
                    }
                    else
                    {
                        TempData["Qty"] = $"Product {products.ProductName} is out of stock!";
                        RedirectToAction("FetchData","Home");
                    }
                }

            }
            _context.Orders.Add(orders);
            _context.SaveChanges();
            HttpContext.Session.Set("Cart", "");
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return View();
        }

    }
}
