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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для RegistrationControl.xaml
    /// </summary>
    public partial class RegistrationControl : Page
    {
        MainWindow _mainWindow;
        public RegistrationControl(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private bool GetUser(string login, string pass)
        {
            List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("1","2")
            };
            return keyValuePairs.Any(x => x.Key == login && x.Value == pass);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.OpenPage(PageType.Login);
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            //mainWindow.OpenPage(MainWindow.pages.regin);
            //В коде функции, которая вызывается при регистрации сначала выполняется проверка на заполняемость полей.

            if (tbxLogin.Text.Length > 0) // проверяем логин
            {
                if (pbxPassword.Password.Length > 0) // проверяем пароль
                {
                    if (pbxPassword_Copy.Password.Length > 0) // проверяем второй пароль
                    {


                    }
                    else MessageBox.Show("Повторите пароль");
                }
                else MessageBox.Show("Укажите пароль");
            }
            else MessageBox.Show("Укажите логин");
            //Далее проверка, на соответствие логина следующей форме записи:

            string[] dataLogin = tbxLogin.Text.Split('@'); // делим строку на две части
            if (dataLogin.Length == 2) // проверяем если у нас две части
            {
                string[] data2Login = dataLogin[1].Split('.'); // делим вторую часть ещё на две части
                if (data2Login.Length == 2)
                {

                }
                else MessageBox.Show("Укажите логин в форме х@x.x");
            }
            else MessageBox.Show("Укажите логин в форме х@x.x");
            //            Также стоит проверить, соответствует ли пароль заданным требованиям:

            //            должно быть 6 или более символов;
            //            допускается только английская раскладка;
            //            должен присутствовать один из следующих символов: «_», «-», «!»
            //должна быть цифра.
            //Этим проверкам соответствует код:

            if (pbxPassword.Password.Length >= 6)
            {
                bool en = true; // английская раскладка
                bool symbol = false; // символ
                bool number = false; // цифра

                for (int i = 0; i < pbxPassword.Password.Length; i++) // перебираем символы
                {
                    if (pbxPassword.Password[i] >= 'А' && pbxPassword.Password[i] <= 'Я') en = false; // если русская раскладка
                    if (pbxPassword.Password[i] >= '0' && pbxPassword.Password[i] <= '9') number = true; // если цифры
                    if (pbxPassword.Password[i] == '_' || pbxPassword.Password[i] == '-' || pbxPassword.Password[i] == '!') symbol = true; // если символ
                }

                if (!en)
                    MessageBox.Show("Доступна только английская раскладка"); // выводим сообщение
                else if (!symbol)
                    MessageBox.Show("Добавьте один из следующих символов: _ - !"); // выводим сообщение
                else if (!number)
                    MessageBox.Show("Добавьте хотя бы одну цифру"); // выводим сообщение
                if (en && symbol && number) // проверяем соответствие
                {
                }
            }
            else MessageBox.Show("пароль слишком короткий, минимум 6 символов");
            //Проверка на совпадение паролей:

            if (pbxPassword.Password == pbxPassword_Copy.Password) // проверка на совпадение паролей
            {
                MessageBox.Show("Пользователь зарегистрирован");
            }
            else MessageBox.Show("Пароли не совподают");
        }
    }
}
