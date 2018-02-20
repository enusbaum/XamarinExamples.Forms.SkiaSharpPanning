using System;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinExamples.Forms.SkiaSharpPanning.Effects;

namespace XamarinExamples.Forms.SkiaSharpPanning
{
    public partial class XamarinExamples_Forms_SkiaSharpPanningPage : ContentPage
    {
        //Track Chart Properties
        float _height = 4000;
        float _width = 4000;

        //Track Chart Delta for Touch/Drag
        float _YDelta = 0;
        float _XDelta = 0;

        //Previous point sampled
        Point _previousTouchPoint;

        public XamarinExamples_Forms_SkiaSharpPanningPage()
        {
            InitializeComponent();

            //Set safe areas for IOS
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        ///     Event fired when Canvas needs to be drawn after invalidation
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void Handle_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            e.Surface.Canvas.Clear(SKColors.White);

            //Setup SKPaint Object
            var _chartPaint = new SKPaint()
            {
                Color = SKColors.Red,
                TextSize = 26,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            //Draw X-Lines
            for (var i = 0 - _width; i < _width; i+= 100)
            {
                e.Surface.Canvas.DrawLine(i + _XDelta, 0, i + _XDelta, _height, _chartPaint);
                e.Surface.Canvas.DrawText($"X:{i}", i + _XDelta, _chartPaint.FontSpacing, _chartPaint);
            }

            //Draw Y-Lines
            for (var i = 0 - _height; i < _height; i += 100)
            {
                e.Surface.Canvas.DrawLine(0, i + _YDelta, _width, i + _YDelta, _chartPaint);
                e.Surface.Canvas.DrawText($"Y:{i}", 0, i + _YDelta, _chartPaint);
            }
        }

        /// <summary>
        ///     Events sent from the TouchEffect attached to the SkiaSharp Canvas (detects drag)
        /// </summary>
        /// <param name="args">Arguments.</param>
        void Handle_TouchAction(object sender, TouchActionEventArgs args)
        {
            //First touch, just mark the location
            if (args.Type == TouchActionType.Pressed)
            {
                _previousTouchPoint = args.Location;
                return;
            }

            //User is dragging/moving the chart
            if (args.Type == TouchActionType.Moved)
            {
                //Calculate Deltas in the drag
                var _touchYDelta = (float)Math.Round((_previousTouchPoint.Y - args.Location.Y) * 2, 0);
                var _touchXDelta = (float)Math.Round((_previousTouchPoint.X - args.Location.X) * 2, 0);

                //Re-Draw the Chart
                if (_touchYDelta >= 1 || _touchYDelta <= -1 || _touchXDelta >= 1 || _touchXDelta <= 1)
                {
                    _YDelta -= _touchYDelta;
                    _XDelta -= _touchXDelta;
                    _previousTouchPoint = args.Location;
                }
            }
            canvasChart.InvalidateSurface();
        }
    }
}
