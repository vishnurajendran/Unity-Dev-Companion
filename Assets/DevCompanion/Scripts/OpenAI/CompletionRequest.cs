using Newtonsoft.Json;

namespace DevCompanion
{
    [System.Serializable]
    public class CompletionRequest {
        [JsonProperty("model")]
        public string ? Model {
            get;
            set;
        }
        [JsonProperty("prompt")]
        public string ? Prompt {
            get;
            set;
        }
        [JsonProperty("max_tokens")]
        public int ? MaxTokens {
            get;
            set;
        }
    }
}