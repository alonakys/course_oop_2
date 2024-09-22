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
    public class YellowFood : Food
    {
        static Random r = new Random();
        public YellowFood()
        {
            scoreValue = 30;
            timeValue = 0;
            Objects objekt = (Objects)r.Next(3, 6);
            Imagee = new ImageBrush();
            Position = new Point(r.Next(100, 1000), 0);
            Speed = 8;
            switch (objekt)
            {
                case Objects.Banana:
                    Objects = Objects.Banana;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggBanana.png"));
                    break;
                case Objects.Orange:
                    Objects = Objects.Orange;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggOrange.png"));
                    break;
            }
        }
        public override void Benefit(ref int score, int scoreValue, int timeValue, ref TimeSpan time)
        {
            score += scoreValue;
        }
        public override void Move()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

    }
}
