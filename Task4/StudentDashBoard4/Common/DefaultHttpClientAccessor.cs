using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StudentDashBoard4.Common
{
    /// <summary>
    /// DefaultHttpClientAccessor
    /// This class initiate the HTTPClient and adds the request headers.
    /// </summary>
    public class DefaultHttpClientAccessor : IHttpClientAccessor
    {
        /// <summary>
        /// The http cleint used to do a get request for the AudioNetworkapi function.
        /// </summary>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// The appSettings that have all the config values.
        /// </summary>
        //private readonly AppSettings _appSetings;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettings">The appSettings that have all the config values.</param>
        public DefaultHttpClientAccessor(/*IOptions<AppSettings> appSetings*/)
        {
            //_appSetings = appSetings.Value;

            HttpClient = new HttpClient();
            string url = "http://localhost:31068/";
            if (!string.IsNullOrEmpty(url/*_appSetings.AudioNetworkApiUrl*/))
            {

                HttpClient.BaseAddress = new Uri(url/*_appSetings.AudioNetworkApiUrl*/);
                HttpClient.DefaultRequestHeaders.Accept.Clear();
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
    }
}
