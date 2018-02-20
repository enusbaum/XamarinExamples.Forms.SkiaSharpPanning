using System;
using Xamarin.Forms;
namespace XamarinExamples.Forms.SkiaSharpPanning.Effects
{
    /// <summary>
    ///     Touch Effects Excample from Xamarin
    ///     https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/effects/touch-tracking/
    /// </summary>
    public class TouchActionEventArgs : EventArgs
    {
        public TouchActionEventArgs(long id, TouchActionType type, Point location, bool isInContact)
        {
            Id = id;
            Type = type;
            Location = location;
            IsInContact = isInContact;
        }

        public long Id { private set; get; }

        public TouchActionType Type { private set; get; }

        public Point Location { private set; get; }

        public bool IsInContact { private set; get; }
    }
}