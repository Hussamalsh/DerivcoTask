using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StudentConsumerService.helpers
{
    public interface IHttpClientAccessor
    {
        HttpClient HttpClient { get; }
    }
}
