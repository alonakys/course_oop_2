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

namespace course_oop_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Basket basket;
            ImageBrush backgroundImage;
            backgroundImage = new ImageBrush(); // new background image brush - will be used to show background
            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggBackground.jpg"));
            mainBackground.Fill = backgroundImage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserNameWindow user = new UserNameWindow();

            Close();
            user.ShowDialog();

        }
    }
}
