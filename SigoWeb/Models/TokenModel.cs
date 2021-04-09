using System;

namespace SigoWeb.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
    }
}
