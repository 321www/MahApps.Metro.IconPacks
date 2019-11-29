﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public abstract class PackIconKindToImageConverterBase : MarkupConverter
    {
        /// <summary>
        /// Gets or sets the brush to draw the icon.
        /// </summary>
        public Brush Brush { get; set; } = Brushes.Black;

        /// <summary>
        /// Gets the path data for the given kind.
        /// </summary>
        protected abstract string GetPathData(object iconKind);

        /// <summary>
        /// Gets the <see cref="T:System.Windows.Media.DrawingGroup" /> object that will be used for the <see cref="T:System.Windows.Media.DrawingImage" />.
        /// </summary>
        protected virtual DrawingGroup GetDrawingGroup(object iconKind, Brush foregroundBrush, string path)
        {
            var geometryDrawing = new GeometryDrawing
            {
                Geometry = Geometry.Parse(path),
                Brush = foregroundBrush,
            };

            var transform = new ScaleTransform(1, 1);

            var drawingGroup = new DrawingGroup
            {
                Children = {geometryDrawing},
                Transform = transform
            };

            return drawingGroup;
        }

        /// <summary>
        /// Gets the ImageSource for the given kind.
        /// </summary>
        protected ImageSource CreateImageSource(object iconKind, Brush foregroundBrush)
        {
            string path = this.GetPathData(iconKind);

            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            var drawingImage = new DrawingImage(GetDrawingGroup(iconKind, foregroundBrush, path));
            drawingImage.Freeze();

            return drawingImage;
        }

        /// <inheritdoc />
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Enum))
            {
                return DependencyProperty.UnsetValue;
            }

            var imageSource = CreateImageSource(value, parameter as Brush ?? this.Brush ?? Brushes.Black);
            return imageSource ?? DependencyProperty.UnsetValue;
        }

        /// <inheritdoc />
        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}