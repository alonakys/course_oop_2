using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace course_oop_2
{
    public class Toy : Thing
    {
        static Random r = new Random();
        public Toy()
        {
            scoreValue = 10;
            timeValue = -1;
            Objects objekt = (Objects)r.Next(6, 9);
            Imagee = new ImageBrush();
            Position = new Point(r.Next(100, 1000), 0);
            Speed = 9;
            switch (objekt)
            {
                case Objects.Ball:
                    Objects = Objects.Ball;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggBall.png"));
                    break;
                case Objects.Bear:
                    Objects = Objects.Bear;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggBear.png"));
                    break;
                case Objects.Car:
                    Objects = Objects.Car;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggCar.png"));
                    break;
            }
        }
        public override void Benefit(ref int score, int scoreValue, int timeValue, ref TimeSpan time)
        {
            score -= scoreValue;
        }
        public override void Move()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

    }
}
