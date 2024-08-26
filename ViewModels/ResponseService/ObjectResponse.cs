using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewModels.GlobalVariable;
using System.Net;
using Newtonsoft.Json;

namespace ViewModels.ResponseService
{
    public class ObjectResponse
    {
        public async Task<ResponseResult<ResponToken>> Login(object user)
        {
            using var httpClient = new HttpClient();
            StringContent? jsonContent = new(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var url = "http://localhost:5154/api/auth/token";
            try
            {
                HttpResponseMessage? responseMessage = await httpClient.PostAsync(url, jsonContent);
                return await HandlerResult<ResponToken>(responseMessage);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private HttpClient ApplyTokenData()
        {
            var httpClient = new HttpClient();
            var token = AccessToken.Instance.Token;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }

        public async Task<ResponseResult<T>> HandlerResult<T>(HttpResponseMessage responseMessage)
        {
            var value = await responseMessage.Content.ReadAsStringAsync();
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    var response = JsonConvert.DeserializeObject<T>(value);
                    return ResponseResult<T>.Success(response);

                case HttpStatusCode.Unauthorized:
                    //
                    return ResponseResult<T>.Failure(new Exception("Unauthorized"));

                case HttpStatusCode.Forbidden:
                    //
                    return ResponseResult<T>.Failure(new Exception("Không thể truy cập tài nguyên"));

                case HttpStatusCode.NotFound:
                    //
                    return ResponseResult<T>.Failure(new Exception("Uncatch API Url"));

                default:

                    var response4 = JsonConvert.DeserializeObject<ResponseProblemDetail>(value);
                    return ResponseResult<T>.Failure(new Exception(response4.title));
            }
        }

    }
}
