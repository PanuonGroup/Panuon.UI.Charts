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
        public ArcDrawingElement(PieChartSeries series) 
            : base(series)
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
        protected override void OnInvalidDrawing()
        {
            InvalidateArrange();
            base.OnInvalidDrawing();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (SeriesBase is DoughnutChartSeries doughnutSeries)
            {
                //var radius = Radius - doughnutSeries.Thickness / 2;
                //var centerPoint = new Point(RenderSize.Width / 2, RenderSize.Height / 2);
                //var startPoint = ArcUtil.PointOnCircle(centerPoint, radius, StartAnglePercent);
                //var endPoint = ArcUtil.PointOnCircle(centerPoint, radius, StartAnglePercent + AnglePercent);

                //var pathBuilder = new StringBuilder();
                //if (AnglePercent > 0)
                //{
                //    pathBuilder.Append($"M{startPoint.X},{startPoint.Y} A{radius},{radius} 0 {(AnglePercent > 0.5 ? 1 : 0)} 1 ");
                //        pathBuilder.Append($"{endPoint.X},{endPoint.Y}");

                //}
                //var path = Geometry.Parse(pathBuilder.ToString());
                //drawingContext.DrawGeometry(null, new Pen(doughnutSeries.Fill, doughnutSeries.Thickness), path);
                DrawDoughnutChartSeries(drawingContext, doughnutSeries);
            }
            else if (SeriesBase is PieChartSeries pieSeries)
            {

            }
        }
        #endregion

        #region Functions
        private void DrawDoughnutChartSeries(DrawingContext drawingContext, DoughnutChartSeries doughnutSeries)
        {
            if(AnglePercent < 0)
            {
                return;
            }

            var cornerRadius = doughnutSeries.CornerRadius;

            var outerRadius = Radius;
            var innerRadius = outerRadius - doughnutSeries.Thickness;

            var centerPoint = new Point(RenderSize.Width / 2, RenderSize.Height / 2);

            var innerStartPoint = ShapeUtil.PointOnCircle(centerPoint, innerRadius, StartAnglePercent);
            var innerEndPoint = ShapeUtil.PointOnCircle(centerPoint, innerRadius, StartAnglePercent + AnglePercent);
            var outerStartPoint = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent);
            var outerEndPoint = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + AnglePercent);

            var pathBuilder = new StringBuilder();

            if(cornerRadius == 0)
            {
                pathBuilder.Append($@"M {outerStartPoint.X},{outerStartPoint.Y} A{outerRadius},{outerRadius} 0 {(AnglePercent > 0.5 ? 1 : 0)} 1 {outerEndPoint.X},{outerEndPoint.Y}
                                                        L {innerEndPoint.X},{innerEndPoint.Y} A{innerRadius},{innerRadius} 0 {(AnglePercent > 0.5 ? 1 : 0)} 0 {innerStartPoint.X},{innerStartPoint.Y}  
                                                        Z");
            }
            else
            {
                var outerAngleOffset = ShapeUtil.AnglePercentFromArcLength(cornerRadius, outerRadius);
                var innerAngleOffset = ShapeUtil.AnglePercentFromArcLength(cornerRadius, innerRadius);

                var outerStartPoint1 = ShapeUtil.PointFromDistance(outerStartPoint, innerStartPoint, cornerRadius);
                var outerStartPoint2 = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + outerAngleOffset);
                var outerEndPoint1 = ShapeUtil.PointOnCircle(centerPoint, outerRadius, StartAnglePercent + AnglePercent - outerAngleOffset);
                var outerEndPoint2 = ShapeUtil.PointFromDistance(outerEndPoint, innerEndPoint, cornerRadius);
                var innerEndPoint1 = ShapeUtil.PointFromDistance(innerEndPoint, outerEndPoint, cornerRadius);
                var innerEndPoint2 = ShapeUtil.PointOnCircle(centerPoint, innerRadius, StartAnglePercent + AnglePercent - innerAngleOffset);
                var innerStartPoint1 = ShapeUtil.PointOnCircle(centerPoint, innerRadius, StartAnglePercent + innerAngleOffset);
                var innerStartPoint2 = ShapeUtil.PointFromDistance(innerStartPoint, outerStartPoint, cornerRadius);

                pathBuilder.Append($@"M {outerStartPoint1.X},{outerStartPoint1.Y} 
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {outerStartPoint2.X},{outerStartPoint2.Y} 
                                                        A {outerRadius},{outerRadius} 0 {(AnglePercent > 0.5 ? 1 : 0)} 1 {outerEndPoint1.X},{outerEndPoint1.Y}
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {outerEndPoint2.X},{outerEndPoint2.Y}
                                                        L {innerEndPoint1.X},{innerEndPoint1.Y}
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {innerEndPoint2.X},{innerEndPoint2.Y}
                                                        A {innerRadius},{innerRadius} 0 {(AnglePercent > 0.5 ? 1 : 0)} 0 {innerStartPoint1.X},{innerStartPoint1.Y}
                                                        A {cornerRadius},{cornerRadius} 0 0 1 {innerStartPoint2.X},{innerStartPoint2.Y}
                                                        Z");
            }

            var path = Geometry.Parse(pathBuilder.ToString());
            drawingContext.DrawGeometry(doughnutSeries.Fill, new Pen(doughnutSeries.Stroke, doughnutSeries.StrokeThickness), path);
        }
        #endregion

    }
}
