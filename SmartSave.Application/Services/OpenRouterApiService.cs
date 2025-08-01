using SmartSave.Application.Interfaces.Services;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace SmartSave.Application.Services
{
    public class OpenRouterApiService : IOpenRouterApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "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";

        public OpenRouterApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendMessageAsync(string prompt)
        {
            var url = "https://openrouter.ai/api/v1/chat/completions";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://localhost:7024");
            _httpClient.DefaultRequestHeaders.Add("X-Title", "SmartSaveApp");

            var body = new
            {
                model = "mistralai/mistral-7b-instruct",
                messages = new[] { new { role = "user", content = prompt } }
            };

            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API Error: {json}");
            }

            dynamic result = JsonConvert.DeserializeObject(json);
            return result.choices[0].message.content;
        }
    }
}
