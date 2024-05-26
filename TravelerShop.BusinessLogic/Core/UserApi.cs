using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Product.DBModel;
using TravelerShop.Domain.Entities.Product;
using TravelerShop.Domain.Entities.User;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.Auth;
using TravelerShop.Domain.Entities.User.DBModel;
using TravelerShop.BusinessLogic.DBModel;
using Microsoft.EntityFrameworkCore;
using TravelerShop.Helpers;
using System.Web;
using TravelerShop.Domain.Entities.Auth.DBModel;
using System.Data.Entity.Validation;
using TravelerShop.Domain.Entities.Cart.DBModel;
using System.Runtime.Remoting.Contexts;

namespace TravelerShop.BusinessLogic.Core
{
    public class UserApi
    {
        internal RResponseData LoginUpService(ULoginData data)
        {
            User user;
            var password = LoginHelper.HashPassword(data.Password);
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Username == data.Username && u.Password == password);                
            }

            if (user == null)
                return new RResponseData { Status = false, ResponseMessage = "The username or password is incorrect." };

            using(var db = new UserContext())
            {
                user.LastIp = data.Ip;
                user.LastLogin = data.LoginDate;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return new RResponseData { Status = true, CurrentUser = user };
        }
        internal RResponseData RegisterService(URegisterData data)
        {
            var user = new User
            {
                Username = data.Username,
                Name = data.Name,
                Surname = data.Surname,
                Email = data.Email,
                Password = LoginHelper.HashPassword(data.Password),
                LastLogin = data.RegistrationDate,
                LastIp = data.Ip,
                Role = Domain.Enums.URole.User
            };

            using (var db = new UserContext())
            {
                var exists = db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (exists == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    return new RResponseData
                    {
                        Status = true,
                        CurrentUser = user
                    };
                }
                return new RResponseData
                {
                    Status = false,
                    ResponseMessage = "User " + user.Username + " already exists.",
                    CurrentUser = user
                };
            }
        }

        public User GetUserByUsernameService(string username)
        {
            User user;
            using(var db =  new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Username == username);
            }

            if(user == null)
            {
                throw new Exception(); // Потом оформить
            }

            return user;
        }

        internal HttpCookie CoockieGenerationService(string username)
        {
            //COOCKIE GENERATION
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(username)
            };

            using(var db = new SessionContext())
            {
                Session current = db.Sessions.FirstOrDefault(s => s.Username == username);
                if(current != null)
                {
                    current.CookieString = apiCookie.Value;
                    current.ExpirationDate = DateTime.Now.AddMinutes(60);
                    using(var todo = new SessionContext())
                    {
                        todo.Entry(current).State = System.Data.Entity.EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = username,
                        CookieString = apiCookie.Value,
                        ExpirationDate = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }
            return apiCookie;            
        }
        // Проработать все случаи неудач
        public User GetUserByCookieService(string value)
        {
            User user;
            using(var db = new SessionContext())
            {
                Session session = db.Sessions.FirstOrDefault(s => s.CookieString == value);
                if(session == null)
                {
                    return null;
                }
                if(session.ExpirationDate <= DateTime.Now)
                {
                    db.Sessions.Remove(session);
                    db.SaveChanges();
                }
                else
                {
                    user = GetUserByUsernameService(session.Username);
                    return user;
                }
            }
            return null;
        }


        //PRODUCT

        internal ProductDataModel ProductActionGetToList()
        {
            var products = new List<Product>();
            using(var db = new ProductContext())
            {
                products = db.Products.ToList();
            }
            return new ProductDataModel { Products = products };
        }
        internal ProductDataModel GetSingleProductAction(int id)
        {
            var product = new Product();
            using(var db = new ProductContext())
            {
                product = db.Products.Find(id);
            }
            return new ProductDataModel { SingleProduct = product };
        }

        //-----------------------------УБРАТЬ ЭТОТ МЕТОД ОТСЮДА------------------------------------//
        internal ProdResponseData AddProductToDbAction(Product prod)
        {
            using (var db = new ProductContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Name == prod.Name);
                if (product == null)
                {
                    db.Products.Add(prod);
                    db.SaveChanges();
                    return new ProdResponseData
                    {
                        Status = true,
                        ResponseMessage = "Product " + prod.Name + " was added succesfully.",
                        CurrentProduct = prod
                    };
                }
            }
            return new ProdResponseData
            {
                Status = false,
                ResponseMessage = "Unable to add " + prod.Name,
                CurrentProduct = prod
            };
        }

        internal ProdResponseData DeleteProductAction(int id)
        {
            using(var db = new ProductContext())
            {
                var product = db.Products.FirstOrDefault(p => p.ProductId == id);
                db.Products.Remove(product);
                db.SaveChanges();
                return new ProdResponseData
                {
                    Status = true,
                    ResponseMessage = "Product " + product.Name + " was removed successfully."
                };
            }
        }

        internal ProdResponseData EditProductAction(Product product)
        {
            using (var db = new ProductContext())
            {
                var existingProduct = db.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

                if (existingProduct != null)
                {
                    db.Entry(existingProduct).CurrentValues.SetValues(product);
                    db.SaveChanges();

                    return new ProdResponseData
                    {
                        Status = true,
                        ResponseMessage = "Product '" + existingProduct.Name + "' was updated successfully.",
                        CurrentProduct = existingProduct
                    };
                }
                else
                {
                    return new ProdResponseData
                    {
                        Status = false,
                        ResponseMessage = "Product '" + product.Name +"' doesn't exist."
                    };
                }
            }
        }

        //------------------------------CART-------------------------------//
        internal Cart FindOrCreateCartService(int userId)
        {
            using (var db = new CartContext())
            {
                var cart = db.Carts
                             .Include(c => c.Items)
                             .FirstOrDefault(c => c.UserId == userId);

                if (cart != null)
                {
                    return cart;
                }
                else
                {
                    var newCart = new Cart
                    {
                        UserId = userId,
                        DateModified = DateTime.Now
                    };
                    db.Carts.Add(newCart);
                    db.SaveChanges();
                    return newCart;
                }
            }
        }

        internal Cart GetByUserIdService(int userId)
        {
            using (var db = new CartContext())
            {
                var cart = db.Carts.FirstOrDefault(c => c.UserId == userId);
                if (cart != null)
                {
                    db.Entry(cart).Collection(c => c.Items).Load();
                }
                return cart;
            }
        }



        internal ProdResponseData AddToCartService(CartItem cartItem)
        {
            using (var db = new CartContext())
            {
                // Обновляем корзину и элементы
                var cart = GetByUserIdService(cartItem.CartId);

                if (cart != null)
                {
                    db.Carts.Attach(cart);
                    var item = db.CartItems.FirstOrDefault(c => c.CartId == cart.Id && c.ProductId == cartItem.ProductId);
                    if (item != null)
                    {
                        item.Quantity += cartItem.Quantity;
                        item.SubTotal += cartItem.SubTotal;
                        cart.Total += cartItem.SubTotal;
                        db.SaveChanges();
                    }
                    else
                    {
                        cart.Items.Add(cartItem);
                        cart.DateModified = DateTime.Now;
                        cart.Total += cartItem.SubTotal;
                        db.SaveChanges();
                    }
                    return new ProdResponseData { Status = true, ResponseMessage = "Item was added successfully." };
                }
                else
                {
                    cart = new Cart
                    {
                        UserId = cartItem.CartId,
                        DateModified = DateTime.Now,
                        Items = new List<CartItem>() { cartItem },
                        Total = cartItem.SubTotal
                    };
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    return new ProdResponseData { Status = true, ResponseMessage = "Item was added successfully." };
                }
            }
        }

        internal ProdResponseData DeleteItemService(int itemId)
        {
            using (var db = new CartContext())
            {
                var item = db.CartItems.FirstOrDefault(p => p.Id == itemId);
                var cart = db.Carts.FirstOrDefault(c => c.Id == item.CartId);
                cart.Total -= item.SubTotal;
                db.CartItems.Remove(item);
                db.SaveChanges();
                return new ProdResponseData
                {
                    Status = true,
                    ResponseMessage = "Product " + item.Name + " was removed successfully."
                };
            }
        }

        internal ProdResponseData UpdateItemService(int itemId, int itemQuantity)
        {
            using(var db = new CartContext())
            {
                var item = db.CartItems.FirstOrDefault(i => i.Id == itemId);
                if(item != null)
                {
                    if (item.Quantity == itemQuantity)
                        return new ProdResponseData { Status = true, ResponseMessage = "No need to update." };

                    var cart = db.Carts.FirstOrDefault(c => c.Id == item.CartId);
                    decimal subtotalDifference = (itemQuantity - item.Quantity) * item.Price;
                    item.Quantity = itemQuantity;
                    item.SubTotal += subtotalDifference;
                    cart.Total += subtotalDifference;
                    db.SaveChanges();
                    return new ProdResponseData() { Status = true };
                }
                else
                {
                    return new ProdResponseData() { Status = false, ResponseMessage = "Item not found." };
                }
            }
        }
    }
}
