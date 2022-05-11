using GeekShopping.CouponAPI.Data.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace CouponTest.StepDefinitions {
    [Binding]
    public class VerificarStatusDeCupomStepDefinitions {
        private string url = "https://localhost:4450/api/v1/Coupon";

        private readonly ScenarioContext _scenarioContext;

        public VerificarStatusDeCupomStepDefinitions(ScenarioContext scenarioContext) {
            _scenarioContext = scenarioContext;
        }

        #region token
        public readonly string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUyMjk0MDgwLCJpYXQiOjE2NTIyOTQwODAsImV4cCI6MTY1MjI5NzY4MCwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI4ODcxMDFkYy0xZWIyLTRlOTQtYWMyNi03OTE1YzNmNzE4ZTgiLCJhdXRoX3RpbWUiOjE2NTIyOTI4MjgsImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiWjJIWllCMk1FUTY1S0VFV1c2STMzRTVXRVhQTDRWTDIiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IjBGMzFFNzNFQzQyNDZCNzFCQTVDNUM5OTc3QzNDMTQ4IiwianRpIjoiMDlGQjMwN0I3RTEzMEY0MDZGREI0Q0RFM0Y1NEVEMEMifQ.Srs6F1NPyZaIXLclnIpmcC5j5GyvP-drJOydPUIj6ZoWpVn7vBDdSV6f6DhNLMapnZ_BVdDCuYNRrWzIm8btNPr8qa_8NQZz4wAIuO3HjHtxYjBGaA_3FXQNDebhRLUqXSTmkHhpgKQr_9L2j296EGZ_x5V45HdoA3SdOMXvRxdv_n85nR9_hHz1n12DqJm7gfKw50-GkfVsZLBaur27WHhHIiibVv7_c6qtwNuoRrxExYFEGVC3LOBndKEU5w44uKpxYeg5ka8RkUTZTsV1fbCF-i7DNva7s8rwGKN2le8bxIRAvpDh7iOWDU80tiqvXcsx-b6L971L6hqCC3cHxQ";
        #endregion

        [Given(@"que eu tenho um cupom com status ativo")]
        public void GivenQueEuTenhoUmCupomComStatusAtivo() {
            var cupom = "EXEMPLO";
            _scenarioContext["cupom"] = cupom;
        }

        [Given(@"estou aunteticado")]
        public void GivenEstouAunteticado() {
            _scenarioContext["token"] = token;
        }


        [When(@"eu executar o metodo Get no endpoint do cupom")]
        public void WhenEuExecutarOMetodoGetNoEndpointDoCupom() {
            var client = new HttpClient();
            var cupom = _scenarioContext["cupom"];
            url += $"/{cupom}";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = client.GetAsync(url).Result;
            _scenarioContext["Response"] = result.StatusCode;
        }

        [Then(@"o status do cupom deve ser OK")]
        public void ThenOStatusDoCupomDeveSerOK() {
            var response = _scenarioContext["Response"];
            Assert.AreEqual(HttpStatusCode.OK, response);
        }

        [When(@"eu executar o metodo Get")]
        public void WhenEuExecutarOMetodoGet() {
            var client = new HttpClient();
            var cupom = _scenarioContext["cupom"];
            url += $"/{cupom}";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            
            var dataAsString = JsonSerializer.Serialize(result);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            
            _scenarioContext["Response"] = result.ToLower();

        }

        [Then(@"o retorno deve ser o desconto")]
        public void ThenORetornoDeveSerODesconto() {
            var response = _scenarioContext["Response"];;
            var coupon = new CouponVO() {
                Id = 1,
                CouponCode = "EXEMPLO",
                DiscountAmount = 10.000000000000000000000000000,
            };
            var content = JsonSerializer.Serialize(coupon);
            
            Assert.AreEqual(content.ToLower(), response);
        }
    }
}
