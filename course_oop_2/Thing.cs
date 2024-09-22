using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace course_oop_2
{
    public abstract class Thing
    {
        public int scoreValue = 0;
        public int timeValue = 0;
        public Rectangle myRectangle;
        public ImageBrush Imagee { get; set; }
        public Point Position { get; set; }
        protected double K { get; set; }
        protected double Speed { get; set; }
        protected Objects Objects { get; set; }
        protected int score = 0;
        public abstract void Benefit(ref int score, int scoreValue, int timeValue, ref TimeSpan time);

        public virtual void Move()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

    }
}
