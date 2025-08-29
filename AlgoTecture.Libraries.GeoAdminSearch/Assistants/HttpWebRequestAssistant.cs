﻿using System.Net;

namespace Algotecture.Libraries.GeoAdminSearch.Assistants
{
    public class HttpWebRequestAssistant
    {
        [Obsolete("Obsolete")]
        public static async Task<string> GetResponse(string baseUrl)
        {
            var request = (HttpWebRequest)WebRequest.Create(baseUrl);

            var response = (HttpWebResponse)await request.GetResponseAsync();

            if (response.StatusCode != HttpStatusCode.OK) return null!;

            var data = response.GetResponseStream();

            var reader = new StreamReader(data);

            var responseFromServer = await reader.ReadToEndAsync();

            response.Close();

            return responseFromServer;
        }
    }
}