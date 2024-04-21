﻿using System;
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

            return new RResponseData { Status = true };
        }
        internal RResponseData RegisterService(URegisterData data)
        {
            var user = new User
            {
                Username = data.Username,
                Name = data.Name,
                Surname = data.Surname,
                Email = data.Email,
                Password = data.Password,
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
                string username = (db.Sessions.FirstOrDefault(s => s.CookieString == value)).Username;
                user = GetUserByUsernameService(username);
            }
            return user;
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

        
    }
}
