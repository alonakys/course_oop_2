using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_oop_2
{
    public class Food : Thing
    {
        public override void Benefit(ref int score, int scoreValue, int timeValue, ref TimeSpan time)
        {
            score += scoreValue;
        }
    }
}
