using GeekShopping.CartAPI.Model;
using NUnit.Framework;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace CartTest.StepDefinitions
{
    [Binding]
    public class TestarCartAPIStepDefinitions
    {
        public string url = "https://localhost:4445";

        private readonly ScenarioContext _scenarioContext;

        public TestarCartAPIStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"que acesso a rota '([^']*)'")]
        public void GivenQueAcessoARota(string route)
        {
            _scenarioContext["route"] = route;
        }

        [Given(@"o id de usuario '([^']*)'")]
        public void GivenOIdDeUsuario(string userId)
        {
            _scenarioContext["userId"] = userId;
        }


        [When(@"executar o metodo get")]
        public void WhenExecutarOMetodoGet()
        {
            var route = _scenarioContext["route"];
            var userId = _scenarioContext["userId"];
            var client = new HttpClient();
            url += $"{route}/{userId}";
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Then(@"o status code deve ser '([^']*)'")]
        public void ThenOStatusCodeDeveSer(string p0)
        {
            var response = _scenarioContext["Response"];
            Assert.AreEqual(HttpStatusCode.OK, response);
        }

        [Given(@"quero criar um novo carrinho")]
        public void GivenQueroCriarUmNovoCarrinho()
        {

            var userId = _scenarioContext["userId"];
            var cart = new Cart()
            {
                CartHeader = new CartHeader()
                {
                    UserId = (string)userId
                },
                CartDetails = new List<CartDetail>()
                {
                    new CartDetail() {
                    Product = new Product() { Name = "Produto 1", Price = 10 },
                    },
                }
            };
            var dataAsString = JsonSerializer.Serialize(cart);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            _scenarioContext["Cart"] = content;
        }

        [When(@"executar o metodo post")]
        public void WhenExecutarOMetodoPost()
        {

        }

    }
}
