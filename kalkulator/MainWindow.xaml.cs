using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace kalkulator
{
    public class przeliczarka
    {
        public static double przelicz(double brutto)
        {
            return brutto * 0.72;
        }
        public static double przeliczpit0(double brutto)
        {
            return brutto * 0.88;
        }
        public static double przeliczarkawalutapi(double waluta)
        {
            
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create("http://api.nbp.pl/api/exchangerates/rates/a/eur/?format=JSON");
            wr.Method = "GET";
            wr.ContentType = "application/json; charset=utf-8";
            HttpWebResponse wres = wr.GetResponse() as HttpWebResponse;
            using (Stream resstream = wres.GetResponseStream()) 
            {
                StreamReader reader = new StreamReader(resstream, Encoding.UTF8);
                JObject json = JObject.Parse(reader.ReadToEnd());
                
                return waluta/(double)json["rates"][0]["mid"];
            }
            
            
        }
    }




    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public void wyswietl()
        {
          
            double zarobki = int.Parse(textbox_brutto.Text);
            double netto;
            if (pit0.IsChecked == false)
            {
                netto = przeliczarka.przelicz(zarobki);

            }
            else
            {
                netto = przeliczarka.przeliczpit0(zarobki);
            }
            textblock_zarobkinetto.Text = Convert.ToString(netto);
            textbox_waluta.Text = Convert.ToString(Math.Round(przeliczarka.przeliczarkawalutapi(netto),2));
            textblock_netto.Visibility = Visibility.Visible;
            textblock_zarobkinetto.Visibility = Visibility.Visible;
            textbox_waluta.Visibility = Visibility.Visible;
            textbox_waluta2.Visibility = Visibility.Visible;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void click_button(object sender, RoutedEventArgs e)
        {
            wyswietl();
        } 
    }
}
