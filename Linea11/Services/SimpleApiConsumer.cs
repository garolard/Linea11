using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Linea11.Services.Interface;

namespace Linea11.Services
{
    class SimpleApiConsumer : IApiConsumer
    {
        private static SimpleApiConsumer THE_INSTANCE = null;

        HttpClient _client;

        private SimpleApiConsumer()
        {
            HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
            filter.AutomaticDecompression = true;
            _client = new HttpClient(filter);
        }

        static public SimpleApiConsumer GetSingleton()
        {
            if (THE_INSTANCE == null)
                THE_INSTANCE = new SimpleApiConsumer();

            return THE_INSTANCE;
        }

        async public Task<string> ExecuteApiRequestAsync(Windows.Web.Http.HttpRequestMessage req)
        {
            try
            {
                HttpResponseMessage response = await _client.SendRequestAsync(req);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
