using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace CartTest.StepDefinitions {
    [Binding]
    public class TestarCartAPIStepDefinitions {
        public string url = "https://localhost:4445";


        private readonly ScenarioContext _scenarioContext;

        public TestarCartAPIStepDefinitions(ScenarioContext scenarioContext) {
            _scenarioContext = scenarioContext;
        }


        public string Setup()
        {
            var cliente = new RestClient("https://localhost:4435/");
            var requeste = new RestRequest("connect/token", Method.Post);
            requeste.AddHeader("content-type", "application/x-www-form-urlencoded");
            requeste.AddParameter("client_id", "client");
            requeste.AddParameter("client_secret", "my_super_secret");
            requeste.AddParameter("grant_type", "client_credentials");

            string response = cliente.ExecuteAsync(requeste).Result.Content;

            var meuObjConvertido = JObject.Parse(response);
            string token = meuObjConvertido.First.First.ToString();

           return token;
        }


        [Given(@"que acesso a rota '([^']*)'")]
        public void GivenQueAcessoARota(string route) {
            _scenarioContext["route"] = route;
        }

        [Given(@"o id de usuario '([^']*)'")]
        public void GivenOIdDeUsuario(string userId) {
            _scenarioContext["userId"] = userId;
        }


        [When(@"executar o metodo get")]
        public void WhenExecutarOMetodoGet() {
            var route = _scenarioContext["route"];
            var userId = _scenarioContext["userId"];
            var client = new HttpClient();
            url += $"{route}/{userId}";
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Then(@"o status code deve ser '([^']*)'")]
        public void ThenOStatusCodeDeveSer(string p0) {
            var response = _scenarioContext["Response"];
            Assert.AreEqual(HttpStatusCode.OK, response);
        }

        [Given(@"quero criar um novo carrinho")]
        public void GivenQueroCriarUmNovoCarrinho() {

            var userId = (string)_scenarioContext["userId"];
            var client = new HttpClient();

            var token = Setup();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var id = 6;
            var result = client.GetAsync($"https://localhost:4440/api/v1/Product/{id}").Result.Content.ReadAsStringAsync().Result;
            JObject json = JObject.Parse(result);



            var product = new ProductVO {
                CategoryName = (string)json["categoryName"],
                Description = (string)json["description"],
                Id = (long)json["id"],
                Name = (string)json["name"],
                Price = (double)json["price"],
                ImageURL = (string)json["imageURL"],
            };
            
            CartVO cart = new CartVO {
                CartHeader = new CartHeaderVO {
                    Id = 0,
                    UserId = userId,
                    CouponCode = "",
                },
                CartDetails = new CartDetailVO[] {
                    new CartDetailVO {
                    Id = 0,
                    CartHeaderId = 0,
                    CartHeader = new CartHeaderVO {
                    Id = 0,
                    UserId = userId,
                    CouponCode = "",
                },
                    ProductId = product.Id,
                    Product = product,
                        Count = 3,
                    }
            }
            };


            var dataAsString = JsonSerializer.Serialize(cart);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            _scenarioContext["Cart"] = content;
        }

        [When(@"executar o metodo post")]
        public void WhenExecutarOMetodoPost() {
            var route = _scenarioContext["route"];
            var client = new HttpClient();
            url += $"{route}";
            var cart = _scenarioContext["Cart"];


            var result = client.PostAsync(url, (HttpContent?)cart).Result;


            _scenarioContext["Response"] = result.StatusCode;
        }


        [Given(@"o id do carrinho é '([^']*)'")]
        public void GivenOIdDoCarrinhoE(string id) {
            _scenarioContext["Id"] = id;
        }

        [When(@"executar o metodo delete")]
        public void WhenExecutarOMetodoDelete() {
            var client = new HttpClient();
            var route = _scenarioContext["route"];
            var id = _scenarioContext["Id"];
            url += $"{route}/{id}";
            var result = client.DeleteAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

    }
}



// api / v1 / Cart / apply - coupon