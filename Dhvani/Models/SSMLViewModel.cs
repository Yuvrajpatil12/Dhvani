using Newtonsoft.Json;

namespace Dhvani.Models
{
	public class SSMLViewModel
	{
		[JsonProperty("Locale")]
		public string Locale { get; set; }

		[JsonProperty("UserAPIKey")]
		public string UserAPIKey { get; set; }

		[JsonProperty("UserText")]
		public string UserText { get; set; }

		[JsonProperty("UserAlternateText")]
		public string UserAlternateText { get; set; }

        [JsonProperty("ShortName")]
        public string ShortName { get; set; }
    }
}
