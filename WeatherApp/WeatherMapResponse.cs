using System.Collections.Generic;

namespace WeatherApp
{
    public class WeatherMapResponse
    {
        public Main main;
        public Wind wind;
        public Coord coord;
        public Sys sys;
        public List<Weather> weather;
    }
}
