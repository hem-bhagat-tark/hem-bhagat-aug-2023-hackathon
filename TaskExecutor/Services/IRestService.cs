using RestSharp;

namespace TaskExecutor.Services
{
    public interface IRestService
    {
        Task PostAsync(string address);
    }
}
