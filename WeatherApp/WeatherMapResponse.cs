using System.Collections.Generic;

namespace WeatherApp
{
    public class WeatherMapResponse
    {
        public Main main;
        public Wind wind;
        public List<Weather> weather;
    }
}
