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
    class AccuWeather
    {
        public GetLocationAccuweather levantarJSonLocation(String lugar)
        {
            String l = "http://dataservice.accuweather.com/locations/v1/search?apikey=vYGosO3qyX44Gb5mggC1Q25XPi2i2Xeu&q=" + lugar;
            //var json = new WebClient().DownloadString(l);
            var json = System.IO.File.ReadAllText(@"../../datosLocationAccu.json");
            GetLocationAccuweather geolocalizacion = JsonConvert.DeserializeObject<JObject>(json).Value<JObject>().ToObject<GetLocationAccuweather>();
            Console.WriteLine(geolocalizacion.idKey);
            return geolocalizacion;
        }
        public GetClimaAccuweather levantarJSonTemerature(String idKey)
        {
            String l = "http://dataservice.accuweather.com/currentconditions/v1/" + idKey + "?apikey=vYGosO3qyX44Gb5mggC1Q25XPi2i2Xeu";
            //var json = new WebClient().DownloadString(l);
            var json = System.IO.File.ReadAllText(@"../../datosClimaAccuweather.json");
            GetClimaAccuweather clima = JsonConvert.DeserializeObject<JObject>(json).Value<JObject>("Temperature").ToObject<GetClimaAccuweather>();
            return clima;
        }

        public String solicitarClima(String lugar)
        {
            return levantarJSonTemerature(levantarJSonLocation(lugar).idKey).temp;
        }
    }
}