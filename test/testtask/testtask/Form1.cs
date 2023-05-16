using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace testtask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label8.Visible = false;
                //записываем uri
                Uri url = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={textBox1.Text}&appid=d412ca871148a70766640c6f80e9313b&lang=ru");

                //создаем get запрос
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                //получаем ответ
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd(); //записываем ответ
                }

                //десериализуем ответ в json
                var parsed = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(response);
                //получаем инф-цию о погоде
                var weather = parsed.Weather[0];

                //получаем описание погоды
                var description = weather.Description;

                //получаем температуру
                var main = parsed.Main;
                var temperature = main["temp"] - 273.15;

                //получаем скорость ветра
                var wind = parsed.Wind;
                var windSpeed = wind["speed"];

                //вывод температуры
                label5.Text = $"{temperature:f1} °C";

                //вывод описания погоды
                label6.Text = $"{windSpeed:f1} м/с";

                //вывод скорости ветра
                label7.Text = $"{description}";
                label8.Text = $"Город - {textBox1.Text}";
                textBox1.Text = "Введите город";
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
            }
            catch
            {
                label8.Text = $"Вы неправильно\n ввели название города, \nпопробуйте еще раз.";
                label8.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
            }
        }

        public class OpenWeatherMapResponse
        {
            public Dictionary<string, double> Coord { get; set; }
            public List<Weather> Weather { get; set; }
            public string Base { get; set; }
            public Dictionary<string, double> Main { get; set; }
            public double Visibility { get; set; }
            public Dictionary<string, double> Wind { get; set; }
            public Dictionary<string, double> Clouds { get; set; }
            public double Dt { get; set; }
            public Dictionary<string, string> Sys { get; set; }
            public double Timezone { get; set; }
            public double Id { get; set; }
            public string Name { get; set; }
            public double Cod { get; set; }
        }


        public class Weather
        {
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
