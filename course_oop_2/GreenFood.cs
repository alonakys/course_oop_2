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
    public class GreenFood : Food
    {
        static Random r = new Random();
        public GreenFood()
        {
            scoreValue = 10;
            timeValue = 0;
            Objects objekt = (Objects)r.Next(2, 2);
            Imagee = new ImageBrush();
            Position = new Point(r.Next(100, 1000), 0);
            Speed = 10;

            Objects = Objects.Cucumber;
            Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggCucumber.png"));

        }
        public override void Benefit(ref int score, int scoreValue, int timeValue, ref TimeSpan time)
        {
            score += scoreValue * 2;
        }
        public override void Move()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }
    }
}
