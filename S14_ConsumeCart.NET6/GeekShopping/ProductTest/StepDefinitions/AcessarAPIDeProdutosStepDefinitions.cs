using GeekShopping.ProductAPI.Model;
using NUnit.Framework;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace ProductTest.StepDefinitions
{
    [Binding]
    public class AcessarAPIDeProdutosStepDefinitions
    {
        private string url = "https://localhost:4440/";
       
        private readonly ScenarioContext _scenarioContext;

        public AcessarAPIDeProdutosStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        #region token
        public readonly string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUyMjcxMjk1LCJpYXQiOjE2NTIyNzEyOTUsImV4cCI6MTY1MjI3NDg5NSwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI4ODcxMDFkYy0xZWIyLTRlOTQtYWMyNi03OTE1YzNmNzE4ZTgiLCJhdXRoX3RpbWUiOjE2NTIyNzEyOTUsImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiWjJIWllCMk1FUTY1S0VFV1c2STMzRTVXRVhQTDRWTDIiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IjExRTAwMDQ4OUZCRkE0QURFQUE3MjAwNjc2Qjc0ODkwIiwianRpIjoiNDM2RDFBNjkxRjk4N0Y2RDU0MEE0ODhCQTgyRjZGNzMifQ.nZxWeh1eZMZlzVScD2JPP0Z15qpJquUu6ktru0ySIDgoFGSMU_qoQpgFLdvwjcM7FuL1aXQjNWObx0ZITexL0ADXP0r9p9DKIMt9rhZMJ-0YuFOdjb0lyQ4UCUXIqlYfc_VV1pFEv3icNQDpsETJH4eR8oiMFGvuHDlNqP5PqBaW1Afs1vv_tQPZd4k-j6UuI_YBEjxZ44H0pl1Aux8lv4EnJ2Q7UKP0DWlqQsBWRHN53nc5uGLgv6Q2cIlcDO9d6XtNppEEGO5IrAt9DmRHy5GczB3mE64mJuhNgLFXcy6KO72LdFx6E8D1U4xZTIQ8KAqIUnZdxlK1NaXk5sW2Dg";
        #endregion
        
        [Given(@"que eu acesso a rota '([^']*)'")]
        public void GivenQueEuAcessoARota(string route)
        {
            _scenarioContext["Route"] = "api/v1/Product";

        }

        [When(@"realizo o metodo '([^']*)'")]
        public void WhenRealizoOMetodo(string method)
        {
            _scenarioContext["Method"] = method;
            string route = (string)_scenarioContext["Route"];
            url += $"{route}";
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Then(@"o statuscode deve ser '([^']*)'")]
        public void ThenOStatuscodeDeveSer(string oK)
        {
            var response = _scenarioContext["Response"];
            Assert.AreEqual(HttpStatusCode.OK, response);
        }

        [Given(@"estou autenticado")]
        public void GivenEstouAutenticado()
        {
            _scenarioContext["Token"] = token;
        }
        
        
        [Given(@"estou acessando rota '([^']*)'")]
        public void GivenEstouAcessandoRota(string route)
        {
            _scenarioContext["Route"] = "api/v1/Product";
        }

        [Given(@"o produto com o id '([^']*)'")]
        public void GivenOProdutoComOId(string p0)
        {
            _scenarioContext["Id"] = "3";
            string route = (string)_scenarioContext["Route"]; 
        }

        [When(@"realizo o metodo '([^']*)' no produto")]
        public void WhenRealizoOMetodoNoProduto(string method)
        {
            _scenarioContext["Method"] = method;
            string route = (string)_scenarioContext["Route"];
            string id = (string)_scenarioContext["Id"];
            url += $"{route}/{id}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Given(@"que estou autenticado")]
        public void GivenQueEstouAutenticado()
        {
            _scenarioContext["Token"] = token;
        }

        [Given(@"quero criar um novo produto")]
        public void GivenQueroCriarUmNovoProduto()
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

        [When(@"executar o método '([^']*)'")]
        public void WhenExecutarOMetodo(string method)
        {
            _scenarioContext["Method"] = method;
            string route = (string)_scenarioContext["Route"];
            //url += $"{route}";
            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_scenarioContext["Token"]}");
            //var product = (HttpContent?)_scenarioContext["Product"];

            //var result = client.PostAsync(url, product);
            //_scenarioContext["Response"] = result.Result.StatusCode;
        }

        

        [Given(@"o produto o id '([^']*)'")]
        public void GivenOProdutoOId(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"executo o metodo '([^']*)'")]
        public void WhenExecutoOMetodo(string delete)
        {
            throw new PendingStepException();
        }
    }
}
