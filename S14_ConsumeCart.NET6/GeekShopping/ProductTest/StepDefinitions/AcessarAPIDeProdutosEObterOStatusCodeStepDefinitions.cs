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
        string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUyNDY4NDU5LCJpYXQiOjE2NTI0Njg0NTksImV4cCI6MTY1MjQ3MjA1OSwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI4ODcxMDFkYy0xZWIyLTRlOTQtYWMyNi03OTE1YzNmNzE4ZTgiLCJhdXRoX3RpbWUiOjE2NTI0Njg0NTksImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiWjJIWllCMk1FUTY1S0VFV1c2STMzRTVXRVhQTDRWTDIiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IkU2N0I3MDgxQkYyRDlEMzg4Mzc1MkVCQkUxOUZDQUM2IiwianRpIjoiRjIyRUJFREMxREU5MjA1RTQyMDAxRTM5NDdBRDlEQzkifQ.bLCUz1NVd31xZXxeuwsGr5jpnibtHcd1wrCODxywWbngHVjfbbtZ288jgPPeIlkpITKHx0bGtpwOhN8sfTRPgSJ85aMmz0jEiSG4ivJygVTAn7OZaebMH9gaipFjn9dbHoU-X2G3gOummK3OqjYn6wXrZynZ-2emGQfEUrsMh43PScC5cAYSwHFq82x92KQ55AVmOOB081uJvyTNPW6jz_AWjufS6C-tpIrrNx-OzeH2_IFX2uAsWLu5rA-euHiS4pNmblzHu1-FRI2nR7nrYYEr_JnTVNWFHvh5rr08b31NInKVxOSeuSs3TotGCg8t8bfr9x0I56qXcJYeS7QJpA";
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
                Id = 23,
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
