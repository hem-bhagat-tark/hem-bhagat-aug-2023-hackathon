using Microsoft.AspNetCore.Mvc;
using System.Net;
using Worker.Exceptions;
using Worker.Models;

namespace Worker.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TaskRunnerController : Controller
    {
        [HttpPost]
        [Route("run")]
        public async Task Run()
        {
            await GetFileBytesAsync();
            // Save file
        }

        private async Task GetFileBytesAsync()
        {
            var client = new HttpClient();
            var response = await client.GetFromJsonAsync<Response>("https://meme-api.com/gimme/wholesomememes");

            if (response.Nsfw)
                throw new BusinessException(HttpStatusCode.BadRequest, "nsfw is set to true");

            var imageResponse = await client.GetAsync(response.Url);
            imageResponse.EnsureSuccessStatusCode();
        }
    }
}
