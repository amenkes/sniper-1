﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static Sniper.Http.HttpProtocols;
using static Sniper.Http.HttpResponseFormats;

namespace Sniper.Http
{
    public interface ISiteInfo
    {
        string ApiUrl { get; } // Base API url for all commands for the site
        string BaseUrl { get; } // Base URL
        string HostName { get; }
        bool IsApiIncluded { get; } //include "/api" in route

        [JsonConverter(typeof(StringEnumConverter))]
        ResponseFormat DefaultResponseFormat { get; }

        bool IsVersionIncluded { get; } //include version number "/1"
        bool IsVersionLetterIncluded { get; } //include "v", as in "/v1" or "/v2" instead of "/1"
        int Port { get; } // Set if not using default ports (80/443)

        [JsonConverter(typeof(StringEnumConverter))]
        HttpProtocol Protocol { get; } // Http, Https,  etc.

        int Version { get; set; }
    }
}