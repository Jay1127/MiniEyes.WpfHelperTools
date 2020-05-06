using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace MiniEyes.WpfHelperTools
{
    public class WatermarkAdorner : Adorner
    {
        private readonly ContentPresenter child;

        protected override int VisualChildrenCount => 1;

        public WatermarkAdorner(UIElement adornedElement, object watermark)
            : base(adornedElement)
        {
            this.IsHitTestVisible = false;

            child = new ContentPresenter
            {
                Content = watermark,
                Margin = ((Control)adornedElement).Padding
            };

            Binding binding = new Binding("Visibility");
            binding.Source = adornedElement;
            this.SetBinding(VisibilityProperty, binding);
        }

        protected override Visual GetVisualChild(int index)
        {
            return child;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var desiredSize = AdornedElement.RenderSize;
            child?.Measure(desiredSize);

            return desiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            child.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}