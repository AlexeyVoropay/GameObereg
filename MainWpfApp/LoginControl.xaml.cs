using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Логика взаимодействия для LoginControl.xaml
    /// </summary>
    public partial class LoginControl : Page
    {
        MainWindow _mainWindow;
        public LoginControl(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private bool GetUser(string login, string pass)
        {
            var url = @$"{GlobalData.HostApi}/api/authenticate/login";
            var body = @"{""username"": ""UserName"", ""password"": ""Password@123""}";
            var userToken = WebRequestHelper.GetResponseString(url, "POST", body, "application/json");
            GlobalData.UserToken = JsonConvert.DeserializeObject<UserToken>(userToken);
            var tokenInfo = ApiClient.GetTokenInfo();
            if(tokenInfo?.Status == "Success")
            {
                System.IO.File.WriteAllText(@"C:\Temp\UserToken.txt", userToken);
                return tokenInfo?.Status == "Success";
            }
            return false;
            


            //////var token = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVXNlck5hbWUiLCJqdGkiOiI2MjI5NDdlMS1iZGZjLTRjYjMtOTdhOC1kZGMxYjg3MjlhZjciLCJleHAiOjE2MTI5OTcwMTEsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjE5NTUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.QCTsQqW8O7Pkw71zMZVk3nukiUKaUanx6XdIF6aePhM";
            //////var url2 = @"https://localhost:44359/weatherforecast";
            //////var headers = new Dictionary<string, string>
            //////{
            //////    { "Authorization", $"Bearer {token}" }
            //////};
            //////try
            //////{
            //////    var test2 = WebRequestHelper.GetResponseString(url2, "GET", null, "application/json", headers);
            //////}
            //////catch (Exception ex)
            //////{
            //////    throw;
            //////}


            //List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>
            //{
            //    new KeyValuePair<string, string>("1","2")
            //};
            //return keyValuePairs.Any(x => x.Key == login && x.Value == pass);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text.Length > 0) // проверяем введён ли логин     
            {
                if (PasswordTextBox.Password.Length > 0) // проверяем введён ли пароль         
                {             // ищем в базе данных пользователя с такими данными         
                    //DataTable dt_user = _mainWindow.Select("SELECT * FROM [dbo].[users] WHERE [login] = '" + textBox_login.Text + "' AND [password] = '" + password.Password + "'");
                    //if (dt_user.Rows.Count > 0) // если такая запись существует       
                    if(GetUser(LoginTextBox.Text, PasswordTextBox.Password))
                    {
                        MessageBox.Show("Пользователь авторизовался"); // говорим, что авторизовался         
                    }
                    else MessageBox.Show("Пользователя не найден"); // выводим ошибку  
                }
                else MessageBox.Show("Введите пароль"); // выводим ошибку    
            }
            else MessageBox.Show("Введите логин"); // выводим ошибку 
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.OpenPage(PageType.Registration);
        }
    }
}
