using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MiniEyes.WpfHelperTools
{
    public class SelectorSyncService
    {
        public static readonly DependencyProperty UseSyncSelectionProperty =
          DependencyProperty.RegisterAttached("UseSyncSelection", typeof(bool), typeof(SelectorSyncService),
              new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnUseSyncSelectionChanged)));

        public static bool GetUseSyncSelection(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseSyncSelectionProperty);
        }

        public static void SetUseSyncSelection(DependencyObject obj, bool value)
        {
            obj.SetValue(UseSyncSelectionProperty, value);
        }

        private static void OnUseSyncSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ButtonBase;
            var useSync = (bool)e.NewValue;

            if (useSync)
            {
                control.Click += OnChildButtonClicked;
            }
            else
            {
                control.Click -= OnChildButtonClicked;
            }
        }

        private static void OnChildButtonClicked(object sender, RoutedEventArgs e)
        {
            var parent = sender as DependencyObject;
            var ancestors = new List<DependencyObject>();

            // 클릭된 컨트롤의 상위 부모 찾기
            while (parent != null)
            {
                if (parent is Selector)
                    break;

                parent = VisualTreeHelper.GetParent(parent);
                ancestors.Add(parent);
            }

            // Selector에서 선택된 위치 찾기
            if (parent != null)
            {
                var selector = (Selector)parent;

                for (int i = 0; i < selector.Items.Count; i++)
                {
                    var item = selector.ItemContainerGenerator.ContainerFromIndex(i);

                    if (ancestors.Contains(item))
                    {
                        selector.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}