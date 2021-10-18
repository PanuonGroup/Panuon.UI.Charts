using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts.Internal.Utils
{
    internal static class ArcUtil
    {
        public static Point PointOnCircle(Point center, double radius, double anglePercent)
        {
            var x = radius * (Math.Cos((2 * anglePercent - 0.5) * Math.PI)) + center.X;
            var y = center.Y - radius * Math.Sin((2 * anglePercent + 0.5) * Math.PI);
            return new Point(x, y);
        }
    }
}
