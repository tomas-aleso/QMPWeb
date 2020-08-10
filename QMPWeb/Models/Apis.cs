using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    class Apis
    {
        public int solicitarClima(String lugar)
        {
            OpenWeatherMap ow = new OpenWeatherMap();
            AccuWeather aw = new AccuWeather();

            try
            {
                String l = ow.solicitarClima(lugar);
                int result = Convert.ToInt32(l);
                return result - 273;
            }
            catch (Exception e)
            {
                String l = aw.solicitarClima(lugar);
                int result = Convert.ToInt32(l);
                return result;
            }
        }
    }
}