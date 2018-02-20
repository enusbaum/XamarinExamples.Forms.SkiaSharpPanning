# XamarinExamples.Forms.SkiaSharpPanning

This repository holds an example of the best way I've been able to come up with to handle panning a drawing/graphic rendered with SkiaSharp. It takes from the Xamarin example of applying custom TouchEffects for Finger Painting and applies it to a 2D surface for panning.

Using this method allows you to scroll through larger charts/graphics or even handle pannig on a "zoomed in" image.

Cheers!

# How it works


### Xamarin.Forms

In the Xamarin.Forms project we implement the custom TouchEffects example as described over at the Xamarin documentation ([link](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/effects/touch-tracking/))

```xml
<views:SKCanvasView x:Name="canvasChart" HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand" EnableTouchEvents="true" PaintSurface="Handle_PaintSurface">
    <views:SKCanvasView.Effects>
        <e:TouchEffect TouchAction="Handle_TouchAction" Capture="true" />
    </views:SKCanvasView.Effects>
</views:SKCanvasView>
```

Deltas are calculated by comparing the touch points between "Moved" touch events. This allows us to continue aggregation of the total X/Y delta during the move. We then apply this delta to all elements painted to the surface thus giving us the "panning" effect

```csharp
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
```

### Xamarin.iOS

For the Xamarin.iOS bit, we simply implement the custom TouchEffect as outlined in the Xamarin documentation.

#### FYI

* You need to have the [EnableTouchEvents](https://developer.xamarin.com/api/property/SkiaSharp.Views.Forms.SKCanvasView.EnableTouchEvents/) set to **TRUE** on the SkiaSharp Canvas in order for the Touch Effects to be invoked.

* Other Xamarin.Forms controls that make use of "Moved' Touch Effects can interfere with this and cause the drag to be interrupted. An example being the slide out action for a MasterDetail menu. To fix this, you would need to set [IsGestureEnabled](https://developer.xamarin.com/api/property/Xamarin.Forms.MasterDetailPage.IsGestureEnabled/) to **FALSE** on the MasterDetail page.

* A good general rule of thumb when working with Touch Actions and Gestures is to keep track on a page which gestures are used where in order to make sure multiple controls are not going to be conflicting with one another.

Cheers!

![Example of Panning](https://d2ffutrenqvap3.cloudfront.net/items/2y2u0X122V32310l2t3v/Screen%20Recording%202018-02-19%20at%2009.34%20PM.gif "Example of Panning")