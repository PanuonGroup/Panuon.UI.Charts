using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts.Internal.Utils
{
    internal static class ShapeUtil
    {
        public static Point PointOnCircle(Point center, double radius, double anglePercent)
        {
            var x = radius * (Math.Cos((2 * anglePercent - 0.5) * Math.PI)) + center.X;
            var y = center.Y - radius * Math.Sin((2 * anglePercent + 0.5) * Math.PI);
            return new Point(x, y);
        }

        public static double AnglePercentFromArcLength(double arcLength, double radius)
        {
            var angle = arcLength / radius;
            return angle / (2 * Math.PI);
        }

        public static Point PointFromDistance(Point point1, Point point2, double distance)
        {
            var magnitude = Math.Sqrt(Math.Pow((point2.Y - point1.Y), 2) + Math.Pow((point2.X - point1.X), 2));
            var x = point1.X + (distance * ((point2.X - point1.X) / magnitude));
            var y = point1.Y + (distance * ((point2.Y - point1.Y) / magnitude));
            return new Point(x, y);
        }
    }
}
