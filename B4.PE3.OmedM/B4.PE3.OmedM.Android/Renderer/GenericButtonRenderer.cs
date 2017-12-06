using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using B4.PE3.OmedM.Droid.Renderer;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(GenericButtonRenderer))]
namespace B4.PE3.OmedM.Droid.Renderer
{
    public class GenericButtonRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
        {
        }
    }