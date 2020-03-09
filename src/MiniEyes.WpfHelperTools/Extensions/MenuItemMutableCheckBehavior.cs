using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace MiniEyes.WpfHelperTools
{
    /// <summary>
    /// sub checkable menuitems act like radio button.
    /// </summary>
    public class MenuItemMutableCheckBehavior : Behavior<MenuItem>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            // Loaded 이벤트 후에 Items를 읽을 수 있음.
            AssociatedObject.Loaded += OnMenuItemLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Unloaded -= OnMenuItemLoaded;

            foreach (var item in FindCheckableItems())
            {
                item.Click -= OnMenuItemClicked;
            }
        }

        private void OnMenuItemLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var item in FindCheckableItems())
            {
                item.Click -= OnMenuItemClicked;
                item.Click += OnMenuItemClicked;
            }
        }

        private void OnMenuItemClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            if (!menuItem.IsChecked)
            {
                menuItem.IsChecked = true;
                return;
            }

            var itemsToUnChecked = FindCheckableItems().Where(item => item != menuItem);
            foreach (var item in itemsToUnChecked)
            {
                item.IsChecked = false;
            }
        }

        private List<MenuItem> FindCheckableItems()
        {
            return AssociatedObject.Items
                                   .OfType<MenuItem>()
                                   .Where(item => item.IsCheckable)
                                   .ToList();
        }
    }
}