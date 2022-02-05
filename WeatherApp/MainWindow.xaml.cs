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
        private readonly string weatherRequestURL = "https://api.openweathermap.org/data/2.5/weather";

        public MainWindow()
        {
            InitializeComponent();
            UpdateData("Glauburg");
        }

        public void UpdateData(string city)
        {
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

            labelTemperature.Content = result.main.temp.ToString("F1") + "°C";
            labelFeelsLike.Content = result.main.feels_like.ToString("F1") + "°C";
            labelTempMin.Content = result.main.temp_min.ToString("F1") + "°C";
            labelTempMax.Content = result.main.temp_max.ToString("F1") + "°C";
            labelPressure.Content = result.main.pressure.ToString("F1") + "hPa";
            labelHumidity.Content = result.main.humidity.ToString("F1") + "%";
            labelWindSpeed.Content = result.wind.speed.ToString() + "km/h";
            labelInfotext.Content = result.weather[0].description;
        }
        
        public WeatherMapResponse GetWeatherData(string city)
        {
            HttpClient httpClient = new();
            var weatherFinalURI = weatherRequestURL + "?q=" + city + "&appid=" + apiKey + "&units=metric&lang=de";
            HttpResponseMessage httpResponse = httpClient.GetAsync(weatherFinalURI).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response);

            return weatherMapResponse;
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            string query = textInputCity.Text;
            UpdateData(query);
        }
    }
}
