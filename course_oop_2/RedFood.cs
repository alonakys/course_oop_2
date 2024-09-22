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
    public class RedFood : Food
    {
        static Random r = new Random();
        public RedFood()
        {
            scoreValue = 10;
            timeValue = 2;
            Objects objekt = (Objects)r.Next(2);
            Imagee = new ImageBrush();
            Position = new Point(r.Next(100, 1000), 0);
            if (Position.X > 500)
            {
                K = -4;
            }
            else
            {
                K = 4;
            }
            Speed = 9;
            switch (objekt)
            {
                case Objects.Apple:
                    Objects = Objects.Apple;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggApple.png"));
                    break;
                case Objects.Tomato:
                    Objects = Objects.Tomato;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggTomato.png"));
                    break;
            }
        }
        public override void Benefit(ref int score, int scoreValue, int timeValue, ref TimeSpan time)
        {
            score += scoreValue;
            time = time.Add(TimeSpan.FromSeconds(timeValue));
        }
        public override void Move()
        {
            Position = new Point(Position.X + K, Position.Y + Speed);
        }


    }
}
