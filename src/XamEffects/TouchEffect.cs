﻿using System.Linq;
using Xamarin.Forms;

namespace XamEffects
{
    public static class TouchEffect
    {
        public static readonly BindableProperty OnProperty =
            BindableProperty.CreateAttached(
                "On",
                typeof(bool),
                typeof(TouchEffect),
                false,
                propertyChanged: OnOffChanged
            );

        public static void SetOn(BindableObject view, bool value)
        {
            view.SetValue(OnProperty, value);
        }

        public static readonly BindableProperty ColorProperty =
            BindableProperty.CreateAttached(
                "Color",
                typeof(Color),
                typeof(TouchEffect),
                Color.Default
            );

        public static void SetColor(BindableObject view, Color value)
        {
            view.SetValue(ColorProperty, value);
        }

        public static Color GetColor(BindableObject view)
        {
            return (Color)view.GetValue(ColorProperty);
        }

        private static void OnOffChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view == null)
                return;

            if ((bool)newValue)
            {
                view.Effects.Add(new AddEffectRoutingEffect());
            }
            else
            {
                var toRemove = view.Effects.FirstOrDefault(e => e is AddEffectRoutingEffect);
                if (toRemove != null)
                    view.Effects.Remove(toRemove);
            }
        }

        private class AddEffectRoutingEffect : RoutingEffect
        {
            public AddEffectRoutingEffect() : base("XamEffects." + nameof(TouchEffect)) { }
        }
    }
}