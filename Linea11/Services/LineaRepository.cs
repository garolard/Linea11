using Linea11.Domain;
using Linea11.Services;
using Linea11.Services.Exceptions;
using Linea11.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;
using Linea11.ViewModels.Interface;
using Linea11.ViewModels;

namespace Linea11.Services
{
    class LineaRepository : ILineaRepository
    {
        const string DOMAIN = "http://itranvias.es/queryitr.php?";

        async public Task<IList<ILineaViewModel>> ListAllAsync()
        {
            // Request creation
            StringBuilder sb = new StringBuilder(DOMAIN);
            sb.Append("dato=0");
            sb.Append("&");
            sb.Append("func=1");
            sb.Append("&");
            sb.Append("_=" + DateTimeOffset.Now.Millisecond.ToString());
            HttpRequestMessage req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(sb.ToString(), UriKind.Absolute)
            };

            // Request execution
            IApiConsumer consumer = SimpleApiConsumer.GetSingleton();
            string response = string.Empty;
            try
            {
                response = await consumer.ExecuteApiRequestAsync(req);
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message, ex);
            }

            // Response parsing
            IList<ILineaViewModel> allLinesViewModels = new List<ILineaViewModel>();
            if (!string.Empty.Equals(response))
            {
                try
                {
                    IList<Domain.Linea> allLines = ParseAndListAllLines(response);
                    foreach (Domain.Linea line in allLines)
                    {
                        allLinesViewModels.Add(new LineaViewModel(line));
                    }
                }
                catch (Exception ex)
                {
                    throw new InternalErrorException(ex.Message, ex);
                }
            }

            return allLinesViewModels;
        }

        private IList<Linea> ParseAndListAllLines(string json)
        {
            IList<Linea> allLines = new List<Linea>();
            JsonValue root;
            JsonArray linesArray;

            if (JsonValue.TryParse(json, out root))
            {
                JsonObject obj = root.GetObject();
                if (obj.ContainsKey("lineas"))
                {
                    linesArray = obj.GetNamedArray("lineas");
                    allLines = ExtractLines(linesArray);
                }
            }

            return allLines;
        }

        private IList<Linea> ExtractLines(JsonArray array)
        {
            IList<Linea> lines = new List<Linea>();

            if (array == null)
                throw new ArgumentNullException("array", "El array de lineas en respuesta JSON es nulo o no existe.");

            if (array == null || !array.Any())
                return lines;

            foreach (JsonValue value in array)
            {
                JsonObject entry = value.GetObject();
                Linea line = new Linea()
                {
                    Id = Int32.Parse(entry.GetNamedString("id")),
                    NombreComercial = entry.GetNamedString("nom_comer"),
                    ColorLinea = entry.GetNamedString("color_linea"),
                    OrigenLinea = entry.GetNamedString("orig_linea"),
                    DestinoLinea = entry.GetNamedString("dest_linea"),
                    DestinoIda = entry.GetNamedString("dest_ida"),
                    DestinoVuelta = entry.GetNamedString("dest_vuelta")
                };
                lines.Add(line);
            }

            return lines;
        }
    }
}
