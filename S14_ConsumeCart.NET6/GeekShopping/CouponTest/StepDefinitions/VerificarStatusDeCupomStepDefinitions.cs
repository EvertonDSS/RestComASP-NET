
using GeekShopping.CouponPI.Model;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
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

        #region token
        public readonly string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUyMzcwNjgyLCJpYXQiOjE2NTIzNzA2ODIsImV4cCI6MTY1MjM3NDI4MiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI3ZmIwZmIzNS0wZTQ5LTRjODEtYjg1OC1mZGFiZDNkNWYyOTUiLCJhdXRoX3RpbWUiOjE2NTIzNzA2ODIsImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiR1BLVlBOMk43RjRQNUI0S1ZHQUZBR01UUFVWUUgyUDMiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IjM5M0QyQTAyOUZCOEM3NzM1NkE5MkUxOTM3MzZFRTk5IiwianRpIjoiNDQ5RjY5RkFENzFFNzBDQUJGNzE5OEI2NDRDQjVCNTkifQ.JhZorKgdHNFvh63y8YnWh5hFq-BJGzaTUzNjb85HOw7OazjzF5yC1NiUg08xFVl6I9xC2auo_PXFUNLU1L-OD6lGQfqkbnbC0XDnSwiwqvqeHnbJDfIeU3HrR0rtKYgQqhK4sMlKUnSH397pjMMcXtQNj0v0ivYJCXEQueYKTLcTMZ-_UjXsxiPOAlc1IxNiqCQbdOoEOh0Nmcuuxk76Appo2gNJeS3DVIRcNDNxAHqOpbx0fC_KsDzd4NvNkEIkiowh3BdzEY4GOfZ_RqJ-Gjr4Gw7u3L3Q3vTfVBJ5vHc3mnbIZSeGZoxicLinm3dAvSoiKiVx9aQ-s3hWNhYDMQ";
        #endregion

        [Given(@"que eu tenho um cupom com status ativo")]
        public void GivenQueEuTenhoUmCupomComStatusAtivo()
        {
            var cupom = "EXEMPLO";
            _scenarioContext["cupom"] = cupom;
        }

        [Given(@"estou aunteticado")]
        public void GivenEstouAunteticado()
        {
            _scenarioContext["token"] = token;
        }


        [When(@"eu executar o metodo Get no endpoint do cupom")]
        public void WhenEuExecutarOMetodoGetNoEndpointDoCupom()
        {
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
            var client = new HttpClient();
            var cupom = _scenarioContext["cupom"];
            url += $"/{cupom}";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;

            JObject json = JObject.Parse(result);
            var b = json.GetType();

            decimal discountAmount = (decimal)json["discountAmount"];
            int id = (int)json["id"];
            string couponCode = (string)json["couponCode"];
            var getCoupon = new Coupon()
            {
                Id = id,
                CouponCode = couponCode,
                DiscountAmount = discountAmount,
            };

            var dataAsString = JsonSerializer.Serialize(getCoupon);

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

            var dataAsString = JsonSerializer.Serialize(couponTeste);
            
            Assert.AreEqual(dataAsString, response);
        }
    }
}
