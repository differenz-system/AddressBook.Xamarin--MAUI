using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using AddressBook.MAUI.CustomControls;
using AddressBook.MAUI.Platforms.Android.Renderers;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Color = Android.Graphics.Color;
using Prism.Navigation;
using Microsoft.Maui.ApplicationModel;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace AddressBook.MAUI.Platforms.Android.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            try
            {
                if (e.NewElement != null)
                {
                    var view = (CustomEntry)Element;

                    Control.Background.SetColorFilter(App.Current.UserAppTheme == AppTheme.Dark ? Color.Black : Color.White, PorterDuff.Mode.SrcAtop);

                    if (view.IsCurvedCornersEnabled)
                    {
                        // creating gradient drawable for the curved background  
                        var _gradientBackground = new GradientDrawable();
                        _gradientBackground.SetShape(ShapeType.Rectangle);
                        _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                        // Thickness of the stroke line  
                        _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                        // Radius for the curves  
                        _gradientBackground.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));
                        _gradientBackground.SetGradientRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));

                        // set the background of the   
                        Control.SetBackground(_gradientBackground);
                    }
                    // Set padding for the internal text from border  
                    Control.SetPadding((int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop, (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            try
            {
                if (Element != null)
                {
                    var view = (CustomEntry)Element;

                    Control.Background.SetColorFilter(App.Current.UserAppTheme == AppTheme.Dark ? Color.Black : Color.White, PorterDuff.Mode.SrcAtop);

                    if (view.IsCurvedCornersEnabled)
                    {
                        // creating gradient drawable for the curved background  
                        var _gradientBackground = new GradientDrawable();
                        _gradientBackground.SetShape(ShapeType.Rectangle);
                        _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                        // Thickness of the stroke line  
                        _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                        // Radius for the curves  
                        _gradientBackground.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));
                        _gradientBackground.SetGradientRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));

                        // set the background of the   
                        Control.SetBackground(_gradientBackground);
                    }
                    // Set padding for the internal text from border  
                    Control.SetPadding((int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop, (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            try
            {
                DisplayMetrics metrics = context.Resources.DisplayMetrics;
                return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}

