using Newtonsoft.Json;

namespace Api.Models.UIComponents {
    public class ProfileImageComp {
        [JsonProperty("imageData")]
        public string Base64ImageData;
    }
}
