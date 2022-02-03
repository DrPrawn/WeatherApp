using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string apiKey = "3cf72ba952eda0c663182cc9e046ea10";
        private readonly string requestURL = "https://api.openweathermap.org/data/2.5/weather";

        public MainWindow()
        {
            InitializeComponent();

            string city = "Frankfurt am Main";

            WeatherMapResponse result = GetWeatherData(city);

            textInputCity.Text = city.ToString();

            string finalImage = "sun.png";
            string finalIcon = "sunny.png";
            string currentWeather = result.weather[0].main.ToLower();
            DateTime localDate = DateTime.Now;

            if (currentWeather.Contains("cloud"))
            {
                finalImage = "cloud.png";
                finalIcon = "cloudy.png";
            }
            else if (currentWeather.Contains("rain"))
            {
                finalImage = "rain.png";
                finalIcon = "rainy.png";
            }
            else if (currentWeather.Contains("snow"))
            {
                finalImage = "snow.png";
                finalIcon = "snow.png";
            }

            backgroundImage.ImageSource = new BitmapImage(new Uri("Images/" + finalImage, UriKind.Relative));
            weatherIcon.ImageSource = new BitmapImage(new Uri("Images/Icons/" + finalIcon, UriKind.Relative));

            labelTemperature.Content = (result.main.temp).ToString("F1") + "°C";
            labelFeelsLike.Content = "Feels like: " + (result.main.feels_like).ToString("F1") + "°C";
            labelTempMin.Content = "Min. Temp.: " + (result.main.temp_min).ToString("F1") + "°C";
            labelTempMax.Content = "Max. Temp.: " + (result.main.temp_max).ToString("F1") + "°C";
            labelPressure.Content = "Pressure:" + (result.main.pressure).ToString("F1") + "°C";
            labelHumidity.Content = "Humidity: " + (result.main.humidity).ToString("F1") + "°C";
            labelWindSpeed.Content = "Windspeed: " + (result.wind.speed).ToString() + "km/h";
            labelInfotext.Content = result.weather[0].main;

        }

        public WeatherMapResponse GetWeatherData(string city)
        {
            HttpClient httpClient = new();
            var finalURI = requestURL + "?q=" + city + "&appid=" + apiKey + "&units=metric";
            HttpResponseMessage httpResponse = httpClient.GetAsync(finalURI).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response);

            return weatherMapResponse;
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
