using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("XamarinDocs")]
[assembly: ExportEffect(typeof(XamarinExamples.Forms.SkiaSharpPanning.iOS.Effects.TouchEffect), "TouchEffect")]
namespace XamarinExamples.Forms.SkiaSharpPanning.iOS.Effects
{
    /// <summary>
    ///     Touch Effects Example from Xamarin
    ///     https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/effects/touch-tracking/
    /// </summary>
    public class TouchEffect : PlatformEffect
    {
        UIView view;
        TouchRecognizer touchRecognizer;

        protected override void OnAttached()
        {
            // Get the iOS UIView corresponding to the Element that the effect is attached to
            view = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the PCL
            XamarinExamples.Forms.SkiaSharpPanning.Effects.TouchEffect effect = (XamarinExamples.Forms.SkiaSharpPanning.Effects.TouchEffect)Element.Effects.FirstOrDefault(e => e is XamarinExamples.Forms.SkiaSharpPanning.Effects.TouchEffect);

            if (effect != null && view != null)
            {
                // Create a TouchRecognizer for this UIView
                touchRecognizer = new TouchRecognizer(Element, view, effect);
                view.AddGestureRecognizer(touchRecognizer);
            }
        }

        protected override void OnDetached()
        {
            if (touchRecognizer != null)
            {
                // Clean up the TouchRecognizer object
                touchRecognizer.Detach();

                // Remove the TouchRecognizer from the UIView
                view.RemoveGestureRecognizer(touchRecognizer);
            }
        }
    }
}