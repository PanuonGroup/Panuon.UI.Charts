using Panuon.UI.Charts.Internal.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Panuon.UI.Charts.Internal.Controls
{
    internal class ArcDrawingElement : DrawingElementBase
    {
        #region Ctor
        public ArcDrawingElement(ChartSeriesBase seriesBase) 
            : base(seriesBase)
        {
        }
        #endregion

        #region Properties

        #region Percent
        public double Percent
        {
            get { return (double)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }

        public static readonly DependencyProperty PercentProperty =
            DependencyProperty.Register("Percent", typeof(double), typeof(ArcDrawingElement), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #region Radius
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(ArcDrawingElement), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #endregion


        #region Overrides
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (SeriesBase is DoughnutSeries doughnutSeries)
            {
                var centerPoint = new Point(50, 50);
                var radius = 50;
                var startPoint = ArcUtil.PointOnCircle(centerPoint, radius, 0);
                var endPoint = ArcUtil.PointOnCircle(centerPoint, radius, 0.25);


                //var centerX = RenderSize.Width / 2;
                //var centerY = RenderSize.Height / 2;
                //var startX = centerX;
                //var startY = centerY - Radius;
                //var endX = Radius * (Math.Cos((2 * Percent - 0.5) * Math.PI)) + centerX;
                //var endY = centerY - Radius * Math.Sin((2 * Percent + 0.5) * Math.PI);

                //var pathBuilder = new StringBuilder();
                //if (Percent > 0)
                //{
                //    pathBuilder.Append($"M{startX},{startY} A{Radius},{Radius} 0 0 1 ");
                //    if (Percent <= 0.5)
                //    {
                //        pathBuilder.Append($"{endX},{endY}");
                //    }
                //    else
                //    {
                //        pathBuilder.Append($"{centerX},{centerY + Radius} A{Radius},{Radius} 0 0 1 {endX},{endY}");
                //    }
                //}
                //var path = PathGeometry.Parse(pathBuilder.ToString());
            }
        }
        #endregion

    }
}
