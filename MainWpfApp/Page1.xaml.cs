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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        MainWindow _mainWindow;
        public Page1(MainWindow mainWindow)
        {
            InitializeComponent();
            //InitializeCursorMonitoring();
            _mainWindow = mainWindow;
        }

        //private void Canvas_MouseMove(object sender, MouseEventArgs e)
        //{
        //    //var point = e.GetPosition(this);
        //    //label1_Copy.Content = String.Format("X:{0}  Y:{1}", point.X, point.Y);
        //    //if (e.LeftButton == MouseButtonState.Pressed)
        //    //{
        //    //    label1_Copy.Content = "holding...";
        //    //}
        //    //else if (e.LeftButton == MouseButtonState.Released)
        //    //{
        //    //    label1_Copy.Content = "released!";
        //    //}
        //}

        //private void InitializeCursorMonitoring()
        //{
        //    var point = Mouse.GetPosition(Application.Current.MainWindow);
        //    var timer = new System.Windows.Threading.DispatcherTimer();

        //    timer.Tick += delegate
        //    {
        //        Application.Current.MainWindow.CaptureMouse();
        //        if (point != Mouse.GetPosition(Application.Current.MainWindow))
        //        {
        //            point = Mouse.GetPosition(Application.Current.MainWindow);
        //            Console.WriteLine(String.Format("X:{0}  Y:{1}", point.X, point.Y));
        //            label1.Content = String.Format("X:{0}  Y:{1}", point.X, point.Y);
        //        }
        //        Application.Current.MainWindow.ReleaseMouseCapture();
        //    };

        //    timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        //    timer.Start();
        //}

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            label1_Copy1.Content = "Button is Checked";
        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            label1_Copy1.Content = "Button is unchecked.";
        }
    }
}
