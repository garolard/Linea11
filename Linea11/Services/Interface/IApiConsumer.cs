using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Linea11.Services.Interface
{
    interface IApiConsumer
    {
        Task<string> ExecuteApiRequestAsync(HttpRequestMessage req);
    }
}
