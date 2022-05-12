using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Net.Http;

namespace CartAPI.Test
{
    public class CartAPI
    {

        RestClient client = new RestClient("https://localhost:4450/");

        #region cupom
        string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBFM0VBN0E5RTVGMTUzQTA0OTlBQUQ0QzZDNDM3QTc4IiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1IiwibmJmIjoxNjUyMTIxMjM4LCJpYXQiOjE2NTIxMjEyMzgsImV4cCI6MTY1MjEyNDgzOCwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNS9yZXNvdXJjZXMiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZ2Vla19zaG9wcGluZyJdLCJhbXIiOlsicHdkIl0sImNsaWVudF9pZCI6ImdlZWtfc2hvcHBpbmciLCJzdWIiOiI3ZmIwZmIzNS0wZTQ5LTRjODEtYjg1OC1mZGFiZDNkNWYyOTUiLCJhdXRoX3RpbWUiOjE2NTIxMjEyMzcsImlkcCI6ImxvY2FsIiwiZW1haWwiOiJldmVydG9uLWFkbWluQGdtYWlsLmNvbS5iciIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiR1BLVlBOMk43RjRQNUI0S1ZHQUZBR01UUFVWUUgyUDMiLCJuYW1lIjoiRXZlcnRvbiBBZG1pbiIsImdpdmVuX25hbWUiOiJFdmVydG9uIiwiZmFtaWx5X25hbWUiOiJBZG1pbiIsInJvbGUiOiJBZG1pbiIsInByZWZlcnJlZF91c2VybmFtZSI6ImV2ZXJ0b24tYWRtaW4iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicGhvbmVfbnVtYmVyIjoiKzU1ICg0MSkgMTIzNDUtMTIzNCIsInBob25lX251bWJlcl92ZXJpZmllZCI6ZmFsc2UsInNpZCI6IjEwMDY2MjdDMjY1MEM1NkE2QTJDMjU3QkE4N0FEQ0VEIiwianRpIjoiRTlDQjA5MTQ2NDU1RkM2MUE4QTlCNDg0MkJFMDJCODIifQ.MEQzPYhx4yCcQ3DegWlItUnf3CFiCEE8tux4FzNLO0owbAb0DmnpklmTHGZN7_pEa_Bk2-k7y6zzpoO6RYSi8aUvT7t1gO-PvU_hQRldREVf_KpLmqtMZgEB5tFekGuFNVHW0AjDrc5uOCe9QR6z4ldfl8kkhqbIeSGIg-AB0ardARfzjcSkd3AX91L8zwdM4tqtKHYYlJQeVWj5To3BBrMP6HhgJ20z73pdIIOesN4CiAx0CgFl-0hm7AYWyRR3Xri50ispXuzBr_M0R0flccEkXC8V5nU1esG4jYc-HhC7h-4WSl88CZ_dYd6KVUDw8ExUqkxU--8SLJBqiQQXBw";

        #endregion


        [Test]
        public void GetCartStatus()
        {

            var userId = "7fb0fb35 - 0e49 - 4c81 - b858 - fdabd3d5f295";
            RestRequest request = new RestRequest($"api/v1/Cart/find-cart/{2}", Method.Get);

            request.AddHeader("UserId", userId);
            request.AddHeader("Authorization", "Bearer " + token);

            //Realiza a requisi��o
            var response = client.ExecuteAsync(request);

            //Compara o StatusCode OK com o StatusCode da requisi��o - True se forem iguais
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);

        }

    }

}