﻿using Newtonsoft.Json;

namespace CSMapi.Models
{
    public class JwtSetting
    {
        public string Key { get; set; } 
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
