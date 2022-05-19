
using Duende.IdentityServer.Models;
using GeekShopping.CouponPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace CouponTest.StepDefinitions
{
    [Binding]
    public class VerificarStatusDeCupomStepDefinitions
    {
        private string url = "https://localhost:4450/api/v1/Coupon";

        private readonly ScenarioContext _scenarioContext;

        public VerificarStatusDeCupomStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"que eu tenho um cupom com status ativo")]
        public void GivenQueEuTenhoUmCupomComStatusAtivo()
        {
            var cupom = "EXEMPLO";
            _scenarioContext["cupom"] = cupom;
        }

        [Given(@"estou aunteticado")]
        public void GivenEstouAunteticado()
        {
            var cliente = new RestClient("https://localhost:4435/");
            var requeste = new RestRequest("connect/token", Method.Post);
            requeste.AddHeader("content-type", "application/x-www-form-urlencoded");
            requeste.AddParameter("client_id", "client");
            requeste.AddParameter("client_secret", "my_super_secret");
            requeste.AddParameter("grant_type", "client_credentials");

            string response = cliente.ExecuteAsync(requeste).Result.Content;

            var meuObjConvertido = JObject.Parse(response);
            var token = meuObjConvertido.First.First.ToString();

            _scenarioContext["token"] = token;
        }



        [When(@"eu executar o metodo Get no endpoint do cupom")]
        public void WhenEuExecutarOMetodoGetNoEndpointDoCupom()
        {
            var token = _scenarioContext["token"].ToString();
            var client = new HttpClient();
            var cupom = _scenarioContext["cupom"];
            url += $"/{cupom}";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Then(@"o status do cupom deve ser OK")]
        public void ThenOStatusDoCupomDeveSerOK()
        {
            var response = _scenarioContext["Response"];
            Assert.AreEqual(HttpStatusCode.OK, response);
        }

        [When(@"eu executar o metodo Get")]
        public void WhenEuExecutarOMetodoGet()
        {
            var token = _scenarioContext["token"].ToString();
            var client = new HttpClient();
            var cupom = _scenarioContext["cupom"];
            url += $"/{cupom}";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;

            JObject json = JObject.Parse(result);
            var b = json.GetType();

            var getCoupon = new Coupon()
            {
                Id = (int)json["id"],
                CouponCode = (string)json["couponCode"],
                DiscountAmount = (decimal)json["discountAmount"],
            };

            var dataAsString = System.Text.Json.JsonSerializer.Serialize(getCoupon);

            _scenarioContext["Coupon"] = dataAsString;


        }

        [Then(@"o retorno deve ser o desconto")]
        public void ThenORetornoDeveSerODesconto()
        {
            var response = _scenarioContext["Coupon"];
            var couponTeste = new Coupon()
            {
                Id = 1,
                CouponCode = "EXEMPLO",
                DiscountAmount = (decimal)10.0,
            };

            var dataAsString = System.Text.Json.JsonSerializer.Serialize(couponTeste);

            Assert.AreEqual(dataAsString, response);
        }


    }
}


//eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUyNzA3NzM3LCJpYXQiOjE2NTI3MDc3MzcsImV4cCI6MTY1MjcxMTMzNywiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI3ZmIwZmIzNS0wZTQ5LTRjODEtYjg1OC1mZGFiZDNkNWYyOTUiLCJhdXRoX3RpbWUiOjE2NTI3MDc3MzcsImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiR1BLVlBOMk43RjRQNUI0S1ZHQUZBR01UUFVWUUgyUDMiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IkFDMzJFMjNFNEJFQTAzQTIzNUY0MDA2NDJGRTRFQzA0IiwianRpIjoiQjdGOTIwNEQyNzlDNTJERTBBRDYxMUI0RUUyNDREQzQifQ.QlCE1ORlJ71Bp_gd0f8kqxUelWnypHCDhwPHjh-C0dtRIO1a4qP78XYyU0di9MOnC31QXmOpfj9K8fREZNJLMGBeBULB4dFYQUvlmiyl6B8EQPIC5R7-z5Oe9V-fTJKUIJml7zwId4BTJ217l3izUIuc6kJqXD6ha_y20MR_85PSvW8a2tSrLWU_XsltnnVjm33x92OARJcqxD6v4tf2hEk17m08s_S3u-y7Gezeip469A0MG8skCSoQ2KW8l-l5aKNSy5rZnmsuTcRAIPOVku1IfvWI99NadmyM1YdONuSb4BB_nXbr70TyWfdrkNX_gLs5ZVMPhIx8EDF3i7uttg

