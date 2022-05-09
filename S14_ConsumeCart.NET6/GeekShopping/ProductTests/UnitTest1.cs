using GeekShopping.ProductAPI.Controllers;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GeekShopping.ProductAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ProductTests
{
    public class Tests
    {
        RestClient client = new RestClient("https://localhost:4440/");
                
        string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUxODQ5MDY3LCJpYXQiOjE2NTE4NDkwNjcsImV4cCI6MTY1MTg1MjY2NywiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI4ODcxMDFkYy0xZWIyLTRlOTQtYWMyNi03OTE1YzNmNzE4ZTgiLCJhdXRoX3RpbWUiOjE2NTE4NDkwNjcsImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiWjJIWllCMk1FUTY1S0VFV1c2STMzRTVXRVhQTDRWTDIiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IkNGQzBBQjMwOUZBNzJDMzI2NzIwOTYwNDA4RjBFOEZEIiwianRpIjoiRjJFQkU1QzRGQkRBRDg3Mjc4QjAxMjJDQTVDM0NDNjgifQ.hN0l-l72YSx9Hh1ViM6wk7mS3sw6qpXq_5WQtxlC_7tTHdpDWw-JYPkssQv2qewt5q63aSivDF9C9cPpguQfnqSG9-4YKMIsrWEKr2H1n6ql4Yu9kGHkJzMrehTxkHhGTGcDmlgB2hchNXPVZdUdNq49pEy7ZhN5bNod8zA4NJBzQPoqNlMX7ww6j9MMHTNnGWLYfHNBjYmkIVsP1StiKfYAqy3SxiPFfDvq6E_PxMhePUzUIDYLFWF89h8BCLuMnrkm4H8LjHd9UmI99XfkEx0BlVlU_Fuz3r7qsQLTzjgZk3O9Iwv-Km9OMmuwFusjEeg3XbUJckDhCR_0A74hjw";

        [Test]
        public void StatusCodeAllProduct()
        {
            RestRequest request = new RestRequest("api/v1/Product", Method.Get);

            //Realiza a requisi��o
            var response = client.ExecuteAsync(request);

            //Compara o StatusCode OK com o StatusCode da requisi��o - True se forem iguais
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Test]
        public void GetStatusCodeProduct()
        {
            var id = 3;
            RestRequest request = new RestRequest("api/v1/Product/" + id, Method.Get);

            //Realiza a requisicao
            request.AddHeader("Authorization", "Bearer " + token);
            var response = client.ExecuteGetAsync(request);
            //Compara o StatusCode OK com o StatusCode da requisicao - True se forem iguais
            
          
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Test]
        public void DeleteStatusCodeProduct()
        {
            var id = 16;
            RestRequest request = new RestRequest("api/v1/Product/" + id, Method.Delete);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.ExecuteAsync(request);

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Test]
        public void PostProduct()
        {
            RestRequest request = new RestRequest("api/v1/Product", Method.Post);

            request.AddHeader("Authorization", "Bearer " + token);

            //Objeto Json com o body do novo produto
            request.AddJsonBody(new Product
            {
                Name = "Testando",
                Description = "Nenhuma",
                Price = 50,
                CategoryName = "Teste",
                ImageURL = "nenhuma"
            });

            var response = client.ExecuteAsync(request);

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}