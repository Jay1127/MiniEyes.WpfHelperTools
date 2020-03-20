using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MiniEyes.WpfHelperTools.Controls.Buttons
{
    internal class FluentCheckBox : CheckBox
    {
        public DataTemplate AccentTemplate
        {
            get { return (DataTemplate)GetValue(AccentTemplateProperty); }
            set { SetValue(AccentTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AccentTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AccentTemplateProperty =
            DependencyProperty.Register("AccentTemplate", typeof(DataTemplate), typeof(MiniCheckBox));

    }
}