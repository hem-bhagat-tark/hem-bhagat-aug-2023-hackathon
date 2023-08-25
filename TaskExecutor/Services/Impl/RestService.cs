using RestSharp;
using System.Net;
using TaskExecutor.Exceptions;

namespace TaskExecutor.Services.Impl
{
    public class RestService : IRestService
    {
        private readonly RestClient _restClient;

        public RestService()
        {
            _restClient = new RestClient();
        }

        public async Task PostAsync(string address)
        {
            var restRequest = new RestRequest(address + "/api/run", Method.Post);

            var restResponse = await _restClient.ExecuteAsync(restRequest);
            ValidateRestResponse(restResponse);
        }

        private void ValidateRestResponse(RestResponse restResponse)
        {
            if (!restResponse.IsSuccessful)
                throw new BusinessException(HttpStatusCode.InternalServerError,
                    $"Rest request to {restResponse.Request} failed with status code {restResponse.StatusCode} and error message {restResponse.ErrorMessage}");
        }
    }
}
