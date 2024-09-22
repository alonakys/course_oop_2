using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace course_oop_2
{
    public class User
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public List<User> userList;

        public User()
        {
            userList = new List<User>();
        }
        public User(string name, int score)
        {
            Name = name;
            Score = score;
        }
        public void SaveToJSON(string fileName)
        {
            var userData = new
            {
                Name,
                Score,
            };

            var jsonString = JsonSerializer.Serialize(userData);

            using (StreamWriter streamWriter = new StreamWriter(fileName, append: true))
            {
                streamWriter.WriteLine(jsonString);
            }
        }

        public List<User> GetUsersFull(string filePath)
        {
            List<User> users = new List<User>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    User user = new User(); // Створюємо новий екземпляр User для кожного рядка JSON
                    LoadFromJSON(line);
                    users.Add(user);
                }
                return users;
            }
            else
            {
                MessageBox.Show("Наразі немає даних для виведення!");
            }

            return users;

        }
        public void LoadFromJSON(string line)
        {

            using (JsonDocument document = JsonDocument.Parse(line))
            {
                JsonElement root = document.RootElement;

                string userName = root.GetProperty("Name").GetString();
                int score = Convert.ToInt32(root.GetProperty("Score").GetString());
                Name = userName;
                Score = score;
                User usser = new User(userName, score);
                userList.Add(usser);
            }


        }
    }
}