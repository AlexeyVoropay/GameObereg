using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Newtonsoft.Json;
using WpfApp1.Models;

namespace WpfApp1
{
    public enum PageType
    {
        Login = 1,
        Registration = 2,
        MainFramePage1 = 3,
    }

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeCursorMonitoring();

            var userTokenFile = System.IO.File.ReadAllText(@"C:\Temp\UserToken.txt");
            GlobalData.UserToken = JsonConvert.DeserializeObject<UserToken>(userTokenFile);
            var tokenInfo = ApiClient.GetTokenInfo();
            if (false)//tokenInfo?.Status != "Success")
            {
                OpenPage(PageType.Login);
            }
            else
            {
                OpenPage(PageType.MainFramePage1);
            }
        }

        private void InitializeCursorMonitoring()
        {
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            var timer = new System.Windows.Threading.DispatcherTimer();

            timer.Tick += delegate
            {
                Application.Current.MainWindow.CaptureMouse();
                if (point != Mouse.GetPosition(Application.Current.MainWindow))
                {
                    point = Mouse.GetPosition(Application.Current.MainWindow);
                    Console.WriteLine(String.Format("X:{0}  Y:{1}", point.X, point.Y));
                }
                Application.Current.MainWindow.ReleaseMouseCapture();
            };

            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }
        /*
         * Re: WebAPI individual account - register and authent works, get fails
May 26, 2015 11:53 PM|LINK


You will get Access Token as JSON object. Please delete your code using cookie and add following code
 var result = login.Content.ReadAsStringAsync().Result;
 // De-Serialize into a dictionary and return:
 Dictionary<string, string> tokenDictionary =
 JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
 string accessToken = tokenDictionary["access_token"];

// Call Web Api using access Token
client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
 var response = client.GetAsync("Api/values").Result;
         */
        public static void WebApiAuthentThenGet()
        {
            string baseAddr = "http://localhost:58991/";
            string username = "a2@user.com";
            string password = "PASS_word1";

            Console.WriteLine("Create request");
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(baseAddr);

            Console.WriteLine("Register");
            var register = client.PostAsync("api/account/register",
                new FormUrlEncodedContent(
                    new Dictionary<string, string> { { "Email", username }, { "Password", password }, { "ConfirmPassword", password } }
                )).Result;
            register.EnsureSuccessStatusCode();

            Console.WriteLine("Login");
            var login = client.PostAsync("/token",
                new FormUrlEncodedContent(
                    new Dictionary<string, string> { { "grant_type", "password" }, { "username", username }, { "password", password } }
                )).Result;
            login.EnsureSuccessStatusCode();

            Console.WriteLine("Display cookie");
            IEnumerable<Cookie> responseCookies = cookies.GetCookies(client.BaseAddress).Cast<Cookie>();
            foreach (Cookie cookie in responseCookies)
            {
                Console.WriteLine(cookie.Name + ": " + cookie.Value);
            }

            Console.WriteLine("Get authorized data");
            var getall = client.GetAsync("api/values").Result;
            getall.EnsureSuccessStatusCode();
        }

        public void OpenPage(PageType login)
        {
            switch (login)
            {
                case PageType.Login:
                    frame.Navigate(new LoginControl(this));
                    break;
                case PageType.Registration:
                    frame.Navigate(new RegistrationControl(this));
                    break;
                case PageType.MainFramePage1:
                    frame.Navigate(new Page1(this));
                    break;
                default:
                    break;
            }
            //throw new NotImplementedException();
        }
    }
}
