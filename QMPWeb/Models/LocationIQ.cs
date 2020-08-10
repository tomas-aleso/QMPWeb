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
    class LocationIQ
    {
        public Coordenada solicitarCoordenadas(String lugar)
        {
            GetCoordinate geolocalizacion = new GetCoordinate();
            Coordenada coor = new Coordenada();
            geolocalizacion = levantarJSON(lugar);
            coor.lat = geolocalizacion.lat;
            coor.lon = geolocalizacion.lon;
            return coor;

        }
        public GetCoordinate levantarJSON(String lugar)
        {
            String l = "https://us1.locationiq.com/v1/search.php?key=50e401aae63357&q=" + lugar + "%20State%20Building&format=json";
            //var json = new WebClient().DownloadString(l);
            var json = System.IO.File.ReadAllText(@"../../datosLocationIQ.json");
            List<GetCoordinate> geolocalizacion = JsonConvert.DeserializeObject<JArray>(json).Value<JArray>().ToObject<List<GetCoordinate>>();
            return geolocalizacion.First();
        }
    }
}