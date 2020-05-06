using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MiniEyes.WpfHelperTools
{
    public class WatermarkService
    {
        /// <summary>
        /// 워터마크 속성
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached("Watermark", typeof(object), typeof(WatermarkService),
                new PropertyMetadata(null, new PropertyChangedCallback(OnWatermarkChanged)));

        public static object GetWatermark(DependencyObject obj)
        {
            return (object)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, object value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.Loaded += TextBox_Loaded;
                textBox.TextChanged += TextBox_TextChanged;
            }
        }

        /// <summary>
        /// 텍스트박스에 워터마크 렌더링(초기값이 ""인 경우에만)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!CanRenderWatermark(sender as Control))
            {
                return;
            }

            RenderWatermark(sender as Control);
        }

        /// <summary>
        /// 텍스트박스에 타이핑하는 경우(텍스트가 "" 인 경우에만 워터마크 렌더링)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as Control;

            if (CanRenderWatermark(textBox))
            {
                RenderWatermark(textBox);
            }
            else
            {
                RemoveWatermark(textBox);
            }
        }

        /// <summary>
        /// 컨트롤에 워터마크 렌더링함.
        /// </summary>
        /// <param name="parent">렌더링할 컨트롤</param>
        private static void RenderWatermark(Control parent)
        {
            var layer = AdornerLayer.GetAdornerLayer(parent);

            if (layer != null)
            {
                layer.Add(new WatermarkAdorner(parent, GetWatermark(parent)));
            }
        }

        /// <summary>
        /// 컨트롤에서 워터마크 제거
        /// </summary>
        /// <param name="parent">워터마크 제거할 컨트롤</param>
        private static void RemoveWatermark(Control parent)
        {
            var layer = AdornerLayer.GetAdornerLayer(parent);

            if (layer == null)
            {
                return;
            }

            var adorners = layer.GetAdorners(parent)?.Where(adorner => adorner is WatermarkAdorner);

            if (adorners != null)
            {
                foreach (var adorner in adorners)
                {
                    layer.Remove(adorner);
                }
            }
        }

        /// <summary>
        /// 컨트롤에 워터마크 추가할수 있는지 판단
        /// </summary>
        /// <param name="parent">워터마크 추가할 컨트롤</param>
        /// <returns></returns>
        private static bool CanRenderWatermark(object parent)
        {
            if (parent is TextBox textBox)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    return true;
                }
            }

            return false;
        }
    }
}