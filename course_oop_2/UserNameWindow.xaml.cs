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
using System.Windows.Shapes;

namespace course_oop_2
{
    /// <summary>
    /// Логика взаимодействия для UserNameWindow.xaml
    /// </summary>
    public partial class UserNameWindow : Window
    {
        static public string userName;
        public UserNameWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow game = new GameWindow();

            Close();
            game.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            userName = textBox.Text;
        }

        private void TextBox_LimitTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length >= 30)
            {
                e.Handled = true;
            }
        }

    }
}
