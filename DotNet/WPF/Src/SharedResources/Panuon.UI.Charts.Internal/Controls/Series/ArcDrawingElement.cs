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
        public ArcDrawingElement(PieChartPanel pieChartPanel, PieChartSeriesBase series)
            : base(pieChartPanel, series)
        {
        }
        #endregion

        #region Properties

        #region StartAnglePercent
        public double StartAnglePercent
        {
            get { return (double)GetValue(StartAnglePercentProperty); }
            set { SetValue(StartAnglePercentProperty, value); }
        }

        public static readonly DependencyProperty StartAnglePercentProperty =
            DependencyProperty.Register("StartAnglePercent", typeof(double), typeof(ArcDrawingElement), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #region AnglePercent
        public double AnglePercent
        {
            get { return (double)GetValue(AnglePercentProperty); }
            set { SetValue(AnglePercentProperty, value); }
        }

        public static readonly DependencyProperty AnglePercentProperty =
            DependencyProperty.Register("AnglePercent", typeof(double), typeof(ArcDrawingElement), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #endregion

        #region Overrides
        protected override void OnInvalidDrawing()
        {
            InvalidateArrange();
            base.OnInvalidDrawing();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Series is DoughnutSeries doughnutSeries)
            {
                DrawFromDoughnutSeries(drawingContext, doughnutSeries);
            }
            else if (Series is PieSeries pieSeries)
            {
                DrawFromPieSeries(drawingContext, pieSeries);
            }
        }
        #endregion

        #region Functions
        private void DrawFromPieSeries(DrawingContext drawingContext, PieSeries pieSeries)
        {
            if (AnglePercent < 0)
            {
                return;
            }

            var pieChartPanel = (PieChartPanel)ChartPanel;
            var radius = pieChartPanel.Radius
                ?? Math.Min(RenderSize.Width / 2, RenderSize.Height / 2);
            var spacingAngle = ShapeUtil.AnglePercentFromArcLength(pieChartPanel.SeriesSpacing, radius);
            var anglePercent = AnglePercent - spacingAngle;

            var cornerRadius = pieChartPanel.CornerRadius;
            var outerRadius = radius;

            var centerPoint = new Point(RenderSize.Width / 2, RenderSize.Height / 2);

            var outerStartPoint = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent);
            var outerHalfPoint = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + anglePercent / 2);
            var outerEndPoint = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + anglePercent);
            var innerCenterPoint = ShapeUtil.PointFromDistance(centerPoint, outerHalfPoint, Math.Sqrt(pieChartPanel.SeriesSpacing / 2 * pieChartPanel.SeriesSpacing / 2 + pieChartPanel.SeriesSpacing / 2 * pieChartPanel.SeriesSpacing / 2));

            var pathBuilder = new StringBuilder();

            if (cornerRadius == 0)
            {
                pathBuilder.Append($@"M {outerStartPoint.X},{outerStartPoint.Y} A{outerRadius},{outerRadius} 0 {(anglePercent > 0.5 ? 1 : 0)} 1 {outerEndPoint.X},{outerEndPoint.Y}
                                                        L {innerCenterPoint.X},{innerCenterPoint.Y}
                                                        Z");
            }
            else
            {
                var outerAngleOffset = ShapeUtil.AnglePercentFromArcLength(cornerRadius, outerRadius);
                var innerAngleOffset = ShapeUtil.AnglePercentFromArcLength(cornerRadius, cornerRadius);

                var outerStartPoint1 = ShapeUtil.PointFromDistance(outerStartPoint, innerCenterPoint, cornerRadius);
                var outerStartPoint2 = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + outerAngleOffset);
                var outerEndPoint1 = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + anglePercent - outerAngleOffset);
                var outerEndPoint2 = ShapeUtil.PointFromDistance(outerEndPoint, innerCenterPoint, cornerRadius);

                pathBuilder.Append($@"M {outerStartPoint1.X},{outerStartPoint1.Y} 
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {outerStartPoint2.X},{outerStartPoint2.Y} 
                                                        A {outerRadius},{outerRadius} 0 {(anglePercent > 0.5 ? 1 : 0)} 1 {outerEndPoint1.X},{outerEndPoint1.Y}
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {outerEndPoint2.X},{outerEndPoint2.Y}
                                                        L {innerCenterPoint.X},{innerCenterPoint.Y}
                                                        Z");
            }

            try
            {
                var path = Geometry.Parse(pathBuilder.ToString());
                drawingContext.DrawGeometry(pieSeries.Fill, new Pen(pieSeries.Stroke, pieSeries.StrokeThickness), path);
            }
            catch { }
        }

        private void DrawFromDoughnutSeries(DrawingContext drawingContext, DoughnutSeries doughnutSeries)
        {
            if(AnglePercent < 0)
            {
                return;
            }

            var pieChartPanel = (PieChartPanel)ChartPanel;
            var radius = pieChartPanel.Radius
              ?? Math.Min(RenderSize.Width / 2, RenderSize.Height / 2);
            var spacingAngle = ShapeUtil.AnglePercentFromArcLength(pieChartPanel.SeriesSpacing, radius);
            var anglePercent = AnglePercent - spacingAngle;

            var cornerRadius = pieChartPanel.CornerRadius;

            var outerRadius = radius;
            var innerRadius = outerRadius - doughnutSeries.Thickness;

            var centerPoint = new Point(RenderSize.Width / 2, RenderSize.Height / 2);

            var innerStartPoint = ShapeUtil.PointOnCircle(centerPoint, innerRadius, StartAnglePercent);
            var innerEndPoint = ShapeUtil.PointOnCircle(centerPoint, innerRadius, StartAnglePercent + anglePercent);
            var outerStartPoint = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent);
            var outerEndPoint = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + anglePercent);

            var pathBuilder = new StringBuilder();

            if(cornerRadius == 0)
            {
                pathBuilder.Append($@"M {outerStartPoint.X},{outerStartPoint.Y} A{outerRadius},{outerRadius} 0 {(anglePercent > 0.5 ? 1 : 0)} 1 {outerEndPoint.X},{outerEndPoint.Y}
                                                        L {innerEndPoint.X},{innerEndPoint.Y} A{innerRadius},{innerRadius} 0 {(anglePercent > 0.5 ? 1 : 0)} 0 {innerStartPoint.X},{innerStartPoint.Y}  
                                                        Z");
            }
            else
            {
                var outerAngleOffset = ShapeUtil.AnglePercentFromArcLength(cornerRadius, outerRadius);
                var innerAngleOffset = ShapeUtil.AnglePercentFromArcLength(cornerRadius, innerRadius);

                var outerStartPoint1 = ShapeUtil.PointFromDistance(outerStartPoint, innerStartPoint, cornerRadius);
                var outerStartPoint2 = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + outerAngleOffset);
                var outerEndPoint1 = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + anglePercent - outerAngleOffset);
                var outerEndPoint2 = ShapeUtil.PointFromDistance(outerEndPoint, innerEndPoint, cornerRadius);
                var innerEndPoint1 = ShapeUtil.PointFromDistance(innerEndPoint, outerEndPoint, cornerRadius);
                var innerEndPoint2 = ShapeUtil.PointOnCircle(centerPoint, innerRadius, StartAnglePercent + anglePercent - innerAngleOffset);
                var innerStartPoint1 = ShapeUtil.PointOnCircle(centerPoint, innerRadius, StartAnglePercent + innerAngleOffset);
                var innerStartPoint2 = ShapeUtil.PointFromDistance(innerStartPoint, outerStartPoint, cornerRadius);

                pathBuilder.Append($@"M {outerStartPoint1.X},{outerStartPoint1.Y} 
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {outerStartPoint2.X},{outerStartPoint2.Y} 
                                                        A {outerRadius},{outerRadius} 0 {(anglePercent > 0.5 ? 1 : 0)} 1 {outerEndPoint1.X},{outerEndPoint1.Y}
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {outerEndPoint2.X},{outerEndPoint2.Y}
                                                        L {innerEndPoint1.X},{innerEndPoint1.Y}
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {innerEndPoint2.X},{innerEndPoint2.Y}
                                                        A {innerRadius},{innerRadius} 0 {(anglePercent > 0.5 ? 1 : 0)} 0 {innerStartPoint1.X},{innerStartPoint1.Y}
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {innerStartPoint2.X},{innerStartPoint2.Y}
                                                        Z");
            }

            try
            {
                var path = Geometry.Parse(pathBuilder.ToString());
                drawingContext.DrawGeometry(doughnutSeries.Fill, new Pen(doughnutSeries.Stroke, doughnutSeries.StrokeThickness), path);
            }
            catch { }
        }


        #endregion

    }
}
