using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceViewModel.VoiceConversion
{
    public class Voice
    {
        [JsonProperty("VoiceText")]
        public string VoiceText { get; set; }

        [JsonProperty("DisplayName")]
        public string? DisplayName { get; set; }

        [JsonProperty("Locale")]
        public string Locale { get; set; }

        [JsonProperty("ShortName")]
        public string ShortName { get; set; }

        [JsonProperty("SpeakingStyle")]
        public string SpeakingStyle { get; set; }

        [JsonProperty("SpeakingSpeed")]
        public string SpeakingSpeed { get; set; }

        [JsonProperty("Pitch")]
        public string Pitch { get; set; }

        [JsonProperty("UserAPIKey")]
        public string? UserAPIKey { get; set; }

        [JsonProperty("CallBackUrl")]
        public string CallBackUrl { get; set; }

        [JsonProperty("TTSType")]
        public string? TTSType { get; set; }
    }
}
