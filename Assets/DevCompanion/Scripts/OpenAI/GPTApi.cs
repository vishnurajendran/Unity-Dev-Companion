using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace DevCompanion
{
    public static class GPTApi
    {
        private const string SETTINGS_PATH = "APISettings";

        private static APISettings settings;

        public static async Task<string> MakeRequest(string query)
        {
            if (settings == null)
                settings = Resources.Load<APISettings>(SETTINGS_PATH);

            CompletionRequest completionRequest = new CompletionRequest
            {
                Model = APISettings.GetEngineKey(settings.engine),
                Prompt = query,
                MaxTokens = settings.MaxTokens
            };

            CompletionResponse completionResponse = null;

            using (HttpClient httpClient = new HttpClient())
            {
                using (var httpReq = new HttpRequestMessage(HttpMethod.Post, APISettings.API_ENDPOINT))
                {
                    httpReq.Headers.Add("Authorization", $"Bearer {settings.APIKey}");
                    string requestString = JsonConvert.SerializeObject(completionRequest);
                    httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");
                    using (HttpResponseMessage? httpResponse = await httpClient.SendAsync(httpReq))
                    {
                        if (httpResponse is not null)
                        {
                            if (httpResponse.IsSuccessStatusCode)
                            {
                                string responseString = await httpResponse.Content.ReadAsStringAsync();
                                {
                                    if (!string.IsNullOrWhiteSpace(responseString))
                                    {
                                        completionResponse =
                                            JsonConvert.DeserializeObject<CompletionResponse>(responseString);
                                    }
                                }
                            }
                        }

                        if (completionResponse is not null)
                        {
                            string? completionText = completionResponse.Choices?[0]?.Text;
                            return completionText;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}