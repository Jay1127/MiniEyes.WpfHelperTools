using System;
using System.Windows;
using System.Windows.Controls;

namespace MiniEyes.WpfHelperTools
{
    /// <summary>
    /// TreeView SelectedItem Binding Attached Properties
    /// </summary>
    public class TreeViewNodeTracker
    {  /// <summary>
       /// 트리뷰에서 선택한 아이템(TreeView's Selected Item)
       /// </summary>
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.RegisterAttached("SelectedNode", typeof(object), typeof(TreeViewNodeTracker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 트리뷰에 선택 아이템을 바인딩할지 여부(whether or not bind tree view selected item)
        /// </summary>
        /// <remarks>
        /// SelectedNodeProperty에 CallBack함수로 SelectedItemChanged이벤트를 초기화하려고 했지만, <para/>
        /// 처음에 SelectedNodeProperty에 값을 외부 코드에서 대입하지 않으면 CallBack함수가 호출되지 않음.<para/>
        /// KeepTrack 속성으로 처음에 SelectedItemChanged이벤트를 초기화시킴.
        /// </remarks>
        public static readonly DependencyProperty KeepTrackProperty =
            DependencyProperty.RegisterAttached("KeepTrack", typeof(bool), typeof(TreeViewNodeTracker),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnTreeViewKeepTrackChanged)));

        public static object GetSelectedNode(DependencyObject obj)
        {
            return obj.GetValue(SelectedNodeProperty);
        }

        public static void SetSelectedNode(DependencyObject obj, object value)
        {
            obj.SetValue(SelectedNodeProperty, value);
        }

        public static bool GetKeepTrack(DependencyObject obj)
        {
            return (bool)obj.GetValue(KeepTrackProperty);
        }

        public static void SetKeepTrack(DependencyObject obj, bool value)
        {
            obj.SetValue(KeepTrackProperty, value);
        }

        private static void OnTreeViewKeepTrackChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var treeView = (d as TreeView);

            if (treeView == null)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                treeView.SelectedItemChanged += OnTreeViewItemChanged;
            }
            else
            {
                treeView.SelectedItemChanged -= OnTreeViewItemChanged;
            }
        }

        private static void OnTreeViewItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem)
            {
                return;
            }

            SetSelectedNode((DependencyObject)sender, e.NewValue);
        }
    }
}