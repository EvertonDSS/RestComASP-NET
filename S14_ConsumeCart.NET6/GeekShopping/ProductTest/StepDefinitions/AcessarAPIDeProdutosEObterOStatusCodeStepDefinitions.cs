using GeekShopping.ProductAPI.Model;
using NUnit.Framework;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace ProductTest.StepDefinitions
{
    [Binding]
    public class AcessarAPIDeProdutosEObterOStatusCodeStepDefinitions
    {

        private string url = "https://localhost:4440/api/v1/Product";

        private readonly ScenarioContext _scenarioContext;

        public AcessarAPIDeProdutosEObterOStatusCodeStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"que não estou autenticado")]
        public void GivenQueNaoEstouAutenticado()
        {
            _scenarioContext["Token"] = "";
        }


        [When(@"executo o metodo Get na URL")]
        public void WhenExecutoOMetodoGetNaURL()
        {
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Then(@"o statusCode deve retornar OK")]
        public void ThenOStatusCodeDeveRetornarOK()
        {
            var response = _scenarioContext["Response"];
            Assert.AreEqual(HttpStatusCode.OK, response);
        }

        [Given(@"que estou autenticado no sistema")]
        public void GivenQueEstouAutenticadoNoSistema()
        {
            var cliente = new RestClient("https://localhost:4435/");
            var requeste = new RestRequest("connect/token", Method.Post);
            requeste.AddHeader("content-type", "application/x-www-form-urlencoded");
            requeste.AddParameter("client_id", "cliente");
            requeste.AddParameter("client_secret", "secret");
            requeste.AddParameter("grant_type", "client_credentials");

            string response = cliente.ExecuteAsync(requeste).Result.Content;

            var meuObjConvertido = JObject.Parse(response);
            var token = meuObjConvertido.First.First.ToString();

            _scenarioContext["Token"] = token;

        }

        [Given(@"acesso produto com o id '([^']*)'")]
        public void GivenAcessoProdutoComOId(string p0)
        {
            _scenarioContext["Id"] = p0;
        }

        [When(@"adiciono o id na URL")]
        public void WhenAdicionoOIdNaURL()
        {
            var token = _scenarioContext["Token"].ToString();
            var id = _scenarioContext["Id"];
            var client = new HttpClient();
            url += $"/{id}";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Given(@"quero criar um novo product")]
        public void GivenQueroCriarUmNovoProduct()
        {
            var product = new Product()
            {
                Name = "Testando",
                Description = "Nenhuma",
                Price = 50,
                CategoryName = "Teste",
                ImageURL = "nenhuma"
            };

            var dataAsString = JsonSerializer.Serialize(product);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            _scenarioContext["Product"] = content;
        }

        [When(@"executo o metodo Post na URL")]
        public void WhenExecutoOMetodoPostNaURL()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_scenarioContext["Token"]}");
            var product = (HttpContent?)_scenarioContext["Product"];
            var result = client.PostAsync(url, product);

            _scenarioContext["Response"] = result.Result.StatusCode;
        }

        [When(@"executo o metodo Delete na URL")]
        public void WhenExecutoOMetodoDeleteNaURL()
        {
            var token = _scenarioContext["Token"].ToString();
            var id = _scenarioContext["Id"];
            url += $"/{id}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = client.DeleteAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Given(@"atualizo um produto existente")]
        public void GivenAtualizoUmProdutoExistente()
        {
            var product = new Product()
            {
                Id = 26,
                Name = "Testando atualizar de novo",
                Description = "Nada atualizado",
                Price = 50,
                CategoryName = "Teste",
                ImageURL = "nenhuma"
            };

            var dataAsString = JsonSerializer.Serialize(product);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            _scenarioContext["Product"] = content;
        }

        [When(@"executo o metodo Put na URL")]
        public void WhenExecutoOMetodoPutNaURL()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_scenarioContext["Token"]}");
            var product = (HttpContent?)_scenarioContext["Product"];
            var result = client.PutAsync(url, product);

            _scenarioContext["Response"] = result.Result.StatusCode;
        }
    }
}
