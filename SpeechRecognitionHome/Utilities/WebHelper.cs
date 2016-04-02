using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRPi.Utilities
{
    public static class WebHelper
    {
        private const string HttpPrefix = "http://";

        public static bool TryGetValidUrl(string url, out Uri validUrl)
        {
            validUrl = null;
            if (!string.IsNullOrEmpty(url))
            {
                url = url.Trim();
                if (!url.StartsWith(HttpPrefix))
                {
                    url = string.Concat(HttpPrefix, url);
                }

                if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    validUrl = new Uri(url, UriKind.RelativeOrAbsolute);
                    return true;
                }
            }

            return false;
        }
    }
}
