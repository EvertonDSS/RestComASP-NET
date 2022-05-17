using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
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
            #region token
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUyNzA3NzM3LCJpYXQiOjE2NTI3MDc3MzcsImV4cCI6MTY1MjcxMTMzNywiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI3ZmIwZmIzNS0wZTQ5LTRjODEtYjg1OC1mZGFiZDNkNWYyOTUiLCJhdXRoX3RpbWUiOjE2NTI3MDc3MzcsImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiR1BLVlBOMk43RjRQNUI0S1ZHQUZBR01UUFVWUUgyUDMiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IkFDMzJFMjNFNEJFQTAzQTIzNUY0MDA2NDJGRTRFQzA0IiwianRpIjoiQjdGOTIwNEQyNzlDNTJERTBBRDYxMUI0RUUyNDREQzQifQ.QlCE1ORlJ71Bp_gd0f8kqxUelWnypHCDhwPHjh-C0dtRIO1a4qP78XYyU0di9MOnC31QXmOpfj9K8fREZNJLMGBeBULB4dFYQUvlmiyl6B8EQPIC5R7-z5Oe9V-fTJKUIJml7zwId4BTJ217l3izUIuc6kJqXD6ha_y20MR_85PSvW8a2tSrLWU_XsltnnVjm33x92OARJcqxD6v4tf2hEk17m08s_S3u-y7Gezeip469A0MG8skCSoQ2KW8l-l5aKNSy5rZnmsuTcRAIPOVku1IfvWI99NadmyM1YdONuSb4BB_nXbr70TyWfdrkNX_gLs5ZVMPhIx8EDF3i7uttg";
            #endregion
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
