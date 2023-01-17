using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace DevCompanion
{
    public class ChatGPTUsage {
        [JsonProperty("prompt_tokens")]
        public int PromptTokens {
            get;
            set;
        }
        [JsonProperty("completion_token")]
        public int CompletionTokens {
            get;
            set;
        }
        [JsonProperty("total_tokens")]
        public int TotalTokens {
            get;
            set;
        }
    }
    
    public class ChatGPTChoice {
        [JsonProperty("text")]
        public string ? Text {
            get;
            set;
        }
    }
    
    public class CompletionResponse {
        [JsonProperty("choices")]
        public List < ChatGPTChoice > ? Choices {
            get;
            set;
        }
        [JsonProperty("usage")]
        public ChatGPTUsage ? Usage {
            get;
            set;
        }
    }
}