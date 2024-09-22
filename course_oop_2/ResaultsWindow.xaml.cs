using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для ResultsWindow.xaml
    /// </summary>
    public partial class ResaultsWindow : Window
    {
        List<User> userList = new List<User>();
        public string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "users.json");
        public ResaultsWindow(User newuser)
        {
            InitializeComponent();

            userList = GetUsersFull(path);

            scoreValue.Content = newuser.Score;

            if (IsOldUser(userList, newuser.Name))
            {
                int myId = GetUserID(userList, newuser.Name);
                if (newuser.Score > userList[myId].Score)
                    userList[myId].Score = newuser.Score;
            }
            else
            {
                if (userList.Count < 5)//adding new users
                {
                    newuser.SaveToJSON(path);
                    userList.Add(newuser);
                }
                else
                {
                    if (userList[4].Score < newuser.Score)//kicking either the lowest place or the current user's score
                    {
                        userList[4].Name = newuser.Name;
                        userList[4].Score = newuser.Score;
                    }
                }
            }
            userList = SortYourself(userList);
            //now adding them to the table
            foreach (User u in userList)
            {
                UserList.Items.Add(u);
            }
        }

        private List<User> SortYourself(List<User> list)
        {
            List<User> result = list;

            result.Sort((left, right) => right.Score.CompareTo(left.Score));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            foreach (User u in result)
            {
                u.SaveToJSON(path);
            }

            return result;
        }

        private bool IsOldUser(List<User> list, string name)
        {
            bool result = false;
            if (list.Count > 0)
                for (int i = 0; i < list.Count; i++)
                {
                    if (name == list[i].Name)
                        result = true;
                }
            return result;
        }

        private int GetUserID(List<User> list, string name)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (name == list[i].Name)
                    return i;
            }
            return 0;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close the game?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Close();
                //if (File.Exists(path))
                //{
                //    File.Delete(path);//Deletes the Leaderboard after every session, if it's need.
                //}
            }

        }

        private void userChange_Click(object sender, RoutedEventArgs e)
        {
            UserNameWindow newUser = new UserNameWindow();
            Close();
            newUser.ShowDialog();
        }

        private void playAgain_Click(object sender, RoutedEventArgs e)
        {
            GameWindow game = new GameWindow();
            Close();
            game.ShowDialog();
        }
        private List<User> GetUsersFull(string filePath)
        {
            List<User> users = new List<User>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    User user = new User(); // Створюємо новий екземпляр User для кожного рядка JSON
                    user = LoadFromJSON(line);
                    users.Add(user);
                }
                return users;
            }

            return users;

        }
        private User LoadFromJSON(string line)
        {
            string userName = "empty"; // default values
            int score = 0;
            using (JsonDocument document = JsonDocument.Parse(line))
            {
                JsonElement root = document.RootElement;
                userName = root.GetProperty("Name").GetString();
                score = root.GetProperty("Score").GetInt32();
            }

            User usser = new User(userName, score);
            return usser;
        }
    }
}
