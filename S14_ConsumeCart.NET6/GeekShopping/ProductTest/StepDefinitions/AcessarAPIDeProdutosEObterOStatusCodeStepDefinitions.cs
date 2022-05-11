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

namespace ProductTest.StepDefinitions {
    [Binding]
    public class AcessarAPIDeProdutosEObterOStatusCodeStepDefinitions {

        private string url = "https://localhost:4440/api/v1/Product";

        private readonly ScenarioContext _scenarioContext;

        public AcessarAPIDeProdutosEObterOStatusCodeStepDefinitions(ScenarioContext scenarioContext) {
            _scenarioContext = scenarioContext;
        }

        #region token
        public readonly string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUyMjk0MDgwLCJpYXQiOjE2NTIyOTQwODAsImV4cCI6MTY1MjI5NzY4MCwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI4ODcxMDFkYy0xZWIyLTRlOTQtYWMyNi03OTE1YzNmNzE4ZTgiLCJhdXRoX3RpbWUiOjE2NTIyOTI4MjgsImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiWjJIWllCMk1FUTY1S0VFV1c2STMzRTVXRVhQTDRWTDIiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IjBGMzFFNzNFQzQyNDZCNzFCQTVDNUM5OTc3QzNDMTQ4IiwianRpIjoiMDlGQjMwN0I3RTEzMEY0MDZGREI0Q0RFM0Y1NEVEMEMifQ.Srs6F1NPyZaIXLclnIpmcC5j5GyvP-drJOydPUIj6ZoWpVn7vBDdSV6f6DhNLMapnZ_BVdDCuYNRrWzIm8btNPr8qa_8NQZz4wAIuO3HjHtxYjBGaA_3FXQNDebhRLUqXSTmkHhpgKQr_9L2j296EGZ_x5V45HdoA3SdOMXvRxdv_n85nR9_hHz1n12DqJm7gfKw50-GkfVsZLBaur27WHhHIiibVv7_c6qtwNuoRrxExYFEGVC3LOBndKEU5w44uKpxYeg5ka8RkUTZTsV1fbCF-i7DNva7s8rwGKN2le8bxIRAvpDh7iOWDU80tiqvXcsx-b6L971L6hqCC3cHxQ";
        #endregion

        [Given(@"que não estou autenticado")]
        public void GivenQueNaoEstouAutenticado() {
            _scenarioContext["Token"] = "";
        }


        [When(@"executo o metodo Get na URL")]
        public void WhenExecutoOMetodoGetNaURL() {
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Then(@"o statusCode deve retornar OK")]
        public void ThenOStatusCodeDeveRetornarOK() {
            var response = _scenarioContext["Response"];
            Assert.AreEqual(HttpStatusCode.OK, response);
        }

        [Given(@"que estou autenticado no sistema")]
        public void GivenQueEstouAutenticadoNoSistema() {
            _scenarioContext["Token"] = token;

        }

        [Given(@"acesso produto com o id '([^']*)'")]
        public void GivenAcessoProdutoComOId(string p0) {
            _scenarioContext["Id"] = p0;
        }

        [When(@"adiciono o id na URL")]
        public void WhenAdicionoOIdNaURL() {
            var id = _scenarioContext["Id"];
            var client = new HttpClient();
            url += $"/{id}";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Given(@"quero criar um novo product")]
        public void GivenQueroCriarUmNovoProduct() {
            var product = new Product() {
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
        public void WhenExecutoOMetodoPostNaURL() {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_scenarioContext["Token"]}");
            var product = (HttpContent?)_scenarioContext["Product"];
            var result = client.PostAsync(url, product);

            _scenarioContext["Response"] = result.Result.StatusCode;
        }

        [When(@"executo o metodo Delete na URL")]
        public void WhenExecutoOMetodoDeleteNaURL() {
            var id = _scenarioContext["Id"];
            url += $"/{id}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_scenarioContext["Token"]}");
            var result = client.DeleteAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Given(@"atualizo um produto existente")]
        public void GivenAtualizoUmProdutoExistente() {
            var product = new Product() {
                Id = 18,
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
        public void WhenExecutoOMetodoPutNaURL() {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_scenarioContext["Token"]}");
            var product = (HttpContent?)_scenarioContext["Product"];
            var result = client.PutAsync(url, product);

            _scenarioContext["Response"] = result.Result.StatusCode;
        }
    }
}
