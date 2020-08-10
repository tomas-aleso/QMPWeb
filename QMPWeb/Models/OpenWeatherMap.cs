using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;

namespace QueMePongo
{
    class OpenWeatherMap
    {
        public String solicitarClima(String lugar)
        {
            LocationIQ loc = new LocationIQ();
            Coordenada coor = loc.solicitarCoordenadas(lugar);
            GetClimaOpenWeather clima = levantarJSON(coor);

            return clima.temp;
        }

        public GetClimaOpenWeather levantarJSON(Coordenada coor)
        {
            String l = "api.openweathermap.org/data/2.5/weather?lat=" + coor.lat + "&lon=" + coor.lon;
            //var json = new WebClient().DownloadString(l);
            var json = System.IO.File.ReadAllText(@"../../datosClimaOpenWeather.json");
            GetClimaOpenWeather clima = JsonConvert.DeserializeObject<JObject>(json).Value<JObject>("main").ToObject<GetClimaOpenWeather>();
            return clima;
        }
    }
}