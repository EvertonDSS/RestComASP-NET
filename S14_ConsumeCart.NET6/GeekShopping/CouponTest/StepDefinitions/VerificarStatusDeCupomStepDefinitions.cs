
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
        string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUyNDY1ODEwLCJpYXQiOjE2NTI0NjU4MTAsImV4cCI6MTY1MjQ2OTQxMCwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI4ODcxMDFkYy0xZWIyLTRlOTQtYWMyNi03OTE1YzNmNzE4ZTgiLCJhdXRoX3RpbWUiOjE2NTI0NjUwMjcsImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiWjJIWllCMk1FUTY1S0VFV1c2STMzRTVXRVhQTDRWTDIiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IjJDMzRBODA4NjBDMDY3Nzk0RDcyRDFBQkIwRTBGM0E0IiwianRpIjoiMkYwQUU2Mzg4QUM0RjAzQzMzNTVDN0I5QjYxRTgwRDYifQ.f8EE-jOxfx_TFpl9XnnCTA3N6io-2pkKRa357C7QBUPtH-AB4DxAA596HQj3MxiCpZvKkdmckNKjMgPNg0sxpuBqxsqRGJMH15oO-5VRUD5G6g_Ules21BgKs7hdH44eZyYPbjx-vdxAg_5KuW9lUmiNGOMrZQPXEhC7xjV-wDclioAETfJFWSl7hK_vLpwpiK2XEO1h9teno61YS8nWpYnFU5Gc3rEISaphW-rVj3crheJGvnY0a3gvBRmPla0BDWFhfXyHsHqCy303uP-4TULG8U2C741z_rzCgD3GXPaOKKy7mIuPUC2_la24N4jomAt9ujn8mSQs6K24jaWP0Q";
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
            
            var getCoupon = new Coupon()
            {
                Id = (int)json["id"],
                CouponCode = (string)json["couponCode"],
                DiscountAmount = (decimal)json["discountAmount"],
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
