using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linea11.Services.Interface;
using Linea11.Domain;
using Linea11.Common;
using Windows.Web.Http;
using Linea11.Services.Exceptions;
using Linea11.ViewModels.Interface;
using Linea11.ViewModels;
using Windows.Data.Json;

namespace Linea11.Services
{
    class ParadaRepository : IParadaRepository
    {
        const string DOMAIN = "http://itranvias.es/queryitr.php?";

        async public Task<IList<ViewModels.Interface.IParadaViewModel>> FindAll(int lineId)
        {
            // Request creation
            StringBuilder sb = new StringBuilder(DOMAIN);
            sb.Append("dato=").Append(lineId);
            sb.Append("&");
            sb.Append("func=2");
            sb.Append("&");
            sb.Append("_=").Append(DateTimeOffset.Now.Millisecond.ToString());
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
            IList<IParadaViewModel> allStopViewModels = new List<IParadaViewModel>();
            if (!string.Empty.Equals(response))
            {
                try
                {
                    IList<Domain.Parada> allStops = ParseAndListAllStops(response);
                    foreach (Domain.Parada stop in allStops)
                    {
                        allStopViewModels.Add(new ParadaViewModel(stop));
                    }
                }
                catch (Exception ex)
                {
                    throw new InternalErrorException(ex.Message, ex);
                }
            }

            return allStopViewModels;
        }

        private IList<Parada> ParseAndListAllStops(string json)
        {
            IList<Parada> allStops = new List<Parada>();

            JsonValue root;
            JsonArray stopsArray;

            if (JsonValue.TryParse(json, out root))
            {
                JsonObject obj = root.GetObject();
                JsonArray stops = obj.GetNamedArray("paradas");
                for (int i = 0, total = stops.Count; i < total; i++)
                {
                    JsonObject stopsInArray = stops.GetObjectAt((uint)i);
                    stopsArray = stopsInArray.GetNamedArray("paradas");
                    foreach (Parada p in ExtractStops(stopsArray, i))
                    {
                        allStops.Add(p);
                    }
                }
            }

            return allStops;
        }

        private IList<Parada> ExtractStops(JsonArray array, int direction)
        {
            IList<Parada> stops = new List<Parada>();

            if (array == null)
                throw new ArgumentNullException("array", "El array de paradas en respuesta JSON es nulo o no existe.");

            if (!array.Any())
                return stops;

            foreach (JsonValue value in array)
            {
                JsonObject entry = value.GetObject();
                Parada stop = new Parada()
                {
                    Id = Int32.Parse(entry.GetNamedString("id")),
                    NombreParada = entry.GetNamedString("parada"),
                    Sentido = direction == 0 ? Sentido.IDA : Sentido.VUELTA
                };
                stops.Add(stop);
            }

            return stops;
        }
    }
}
