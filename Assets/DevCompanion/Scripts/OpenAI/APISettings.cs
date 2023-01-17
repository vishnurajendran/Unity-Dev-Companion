using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevCompanion
{
    [System.Serializable]
    public enum GPTEngine
    {
        Davinci,
        Curie,
        Babbage,
        Ada
    }
    
    [CreateAssetMenu(fileName = "APISettings", menuName = "Dev Console/API Settings")]
    public class APISettings : ScriptableObject
    {
        public const string API_ENDPOINT = "https://api.openai.com/v1/completions";
        
        public string APIKey;
        public GPTEngine engine;
        [Range(128, 1000)]
        [Header("The higher the tokens, the more the you will be charged")]
        public int MaxTokens = 1000;

        public static string GetEngineKey(GPTEngine engine)
        {
            switch (engine)
            {
                case GPTEngine.Davinci : return "text-davinci-003";
                case GPTEngine.Curie : return "text-curie-001";
                case GPTEngine.Babbage : return "text-babbage-001";
                default: return "text-ada-001";
            }
        }
    }
}

