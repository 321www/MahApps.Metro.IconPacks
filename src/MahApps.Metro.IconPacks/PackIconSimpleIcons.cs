﻿using System;
using System.Collections.Generic;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All SVG icons for popular brands, maintained by Dan Leech <see><cref>https://twitter.com/bathtype</cref></see>.
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/danleech/simple-icons</cref></see>.
    /// </summary>
    public class PackIconSimpleIcons : PackIconControlBase
    {
        private static Lazy<IDictionary<PackIconSimpleIconsKind, string>> _dataIndex;

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconSimpleIconsKind), typeof(PackIconSimpleIcons), new PropertyMetadata(default(PackIconSimpleIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PackIconSimpleIcons)dependencyObject).UpdateData();
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconSimpleIconsKind Kind
        {
            get { return (PackIconSimpleIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconSimpleIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconSimpleIcons), new FrameworkPropertyMetadata(typeof(PackIconSimpleIcons)));
        }
#endif

        public PackIconSimpleIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconSimpleIcons);
#endif

            if (_dataIndex == null)
            {
                _dataIndex = new Lazy<IDictionary<PackIconSimpleIconsKind, string>>(PackIconSimpleIconsDataFactory.Create);
            }
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconSimpleIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            string data = null;
            _dataIndex.Value?.TryGetValue(Kind, out data);
            this.Data = data;
        }
    }
}