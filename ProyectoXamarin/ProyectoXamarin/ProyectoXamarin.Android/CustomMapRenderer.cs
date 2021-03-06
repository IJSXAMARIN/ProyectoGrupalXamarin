﻿using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using ProyectoXamarin.Droid.Renderers;

using ProyectoXamarin.Maps;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace ProyectoXamarin.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        public List<Pin> pins;
        public CustomMapRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                pins = formsMap.Pins.ToList();
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            //CustomPin custom = (CustomPin)pin;

            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            if (pin.Type == PinType.SavedPin)
            {
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.star));
            }

            //  marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.star));
            return marker;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
           
           
        }

        private Pin GetCustomPin(Marker marker)
        {
            return pins.FirstOrDefault(x => x.Position.Latitude == marker.Position.Latitude && x.Position.Longitude == marker.Position.Longitude);
        }

        //public Android.Views.View GetInfoContents(Marker marker)
        //{
        //    var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
        //    if (inflater != null)
        //    {
        //        Android.Views.View view;

        //        var customPin = GetCustomPin(marker);
        //        if (customPin == null)
        //        {
        //            throw new Exception("Custom pin not found");
        //        }

        //        // Lets load the MapInfoWindow to show the place information into the map
        //        view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);

        //        TextView infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
        //        var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

        //        if (infoTitle != null)
        //        {
        //            infoTitle.Text = marker.Title;
        //        }
        //        if (infoSubtitle != null)
        //        {
        //            infoSubtitle.Text = marker.Snippet;
        //        }

        //        return view;
        //    }
        //    return null;
        //}

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            return null;
        }
    }
}