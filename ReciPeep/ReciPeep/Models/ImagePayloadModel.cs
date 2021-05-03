using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReciPeep.Models
{
    public class ImagePayloadModel
    {
        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
