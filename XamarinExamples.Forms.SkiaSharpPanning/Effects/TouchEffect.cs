using Xamarin.Forms;
namespace XamarinExamples.Forms.SkiaSharpPanning.Effects
{
    /// <summary>
    ///     Touch Effects Example from Xamarin
    ///     https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/effects/touch-tracking/
    /// </summary>
    public class TouchEffect: RoutingEffect
    {
        public delegate void TouchActionEventHandler(object sender, TouchActionEventArgs args);
        public event TouchActionEventHandler TouchAction;

        public TouchEffect() : base("XamarinDocs.TouchEffect")
        {
        }

        public bool Capture { set; get; }

        public void OnTouchAction(Element element, TouchActionEventArgs args)
        {
            TouchAction?.Invoke(element, args);
        }
    }

    public enum TouchActionType
    {
        Entered,
        Pressed,
        Moved,
        Released,
        Exited,
        Cancelled
    }
}
