using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloSlamPOC.TestObjects
{
    class Hypothesis:GameObject
    {
        public double Angle { get; set; }
        public double Weight { get; set; }
        public override DrawAtPoint DrawFunction
        {
            get {
                return (x, y, g) => {
                    const int arrowLength = 3;
                    var deltaX = (int)(arrowLength * Math.Sin(Angle * Math.PI / 180));
                    var deltaY = (int)(arrowLength * Math.Cos(Angle * Math.PI / 180));
                    g.FillEllipse(Brushes.Lavender, x - 2, y - 2, 4, 4);
                    g.DrawLine(Pens.Lavender, x, y, x + deltaX, y + deltaY);
                };
            }
        }

        public Hypothesis(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Hypothesis(int x, int y, int angle) : base(x, y)
        {
            Angle = angle;
        }
    }
}
