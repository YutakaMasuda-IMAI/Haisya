
namespace HaisyaDesktop.Behavior.DragAndDrop
{
    using HaisyaDesktop.Utils;
    using Microsoft.Xaml.Behaviors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// ドラッグアドナー
    /// </summary>
    internal class DragAdorner : Adorner
    {
#region Fields
        protected UIElement child;
        protected double XCenter;
        protected double YCenter;
        private double _leftOffset;
        private double _topOffset;
#endregion Fields
#region Properties
        public double LeftOffset
        {
            get { return _leftOffset; }
            set
            {
                _leftOffset = value - XCenter;
                UpdatePosition();
            }
        }

        public double TopOffset
        {
            get { return _topOffset; }
            set
            {
                _topOffset = value - YCenter;
                UpdatePosition();
            }
        }
#endregion Properties
#region Construct
        public DragAdorner(UIElement owner) : base(owner) { }

        public DragAdorner(UIElement owner, UIElement adornElement, double opacity, Point dragPos)
            : base(owner)
        {
            var _brush = new VisualBrush(adornElement) { Opacity = opacity };
            var b = VisualTreeHelper.GetDescendantBounds(adornElement);
            var r = new Rectangle() { Width = b.Width, Height = b.Height };

            XCenter = dragPos.X;
            YCenter = dragPos.Y;

            r.Fill = _brush;
            child = r;
        }
#endregion Construct
#region Methods
        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            var result = new GeneralTransformGroup();
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(_leftOffset, _topOffset));
            return result;
        }

        protected override Visual GetVisualChild(int index)
        {
            return child;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Size MeasureOverride(Size finalSize)
        {
            child.Measure(finalSize);
            return child.DesiredSize;
        }
        
        protected override Size ArrangeOverride(Size finalSize)
        {

            child.Arrange(new Rect(child.DesiredSize));
            return finalSize;
        }

        private void UpdatePosition()
        {
            var adorner = Parent as AdornerLayer;
            if (adorner != null)
            {
                adorner.Update(AdornedElement);
            }
        }
#endregion Method
    }

    /// <summary>
    /// ドラッグ対象オブジェクト用ビヘイビア
    /// <see cref="http://b.starwing.net/?p=131"/>
    /// </summary>
    public class DragStartBehavior : Behavior<FrameworkElement>
    {
#region Fields
        private Point origin;
        private bool isButtonDown;
        private IInputElement dragItem;
        private Point dragStartPos;
        private DragAdorner dragGhost;
        public static readonly DependencyProperty AllowedEffectsProperty =
            DependencyProperty.Register("AllowedEffects", typeof(DragDropEffects),
                    typeof(DragStartBehavior), new UIPropertyMetadata(DragDropEffects.All));

        public static readonly DependencyProperty DragDropDataProperty =
            DependencyProperty.Register("DragDropData", typeof(object),
                    typeof(DragStartBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty IsDragEnableProperty =
            DependencyProperty.Register("IsDragEnable", typeof(bool),
                    typeof(DragStartBehavior), new UIPropertyMetadata(true));

#endregion Fields
#region Properties
        public DragDropEffects AllowedEffects
        {
            get { return (DragDropEffects)GetValue(AllowedEffectsProperty); }
            set { SetValue(AllowedEffectsProperty, value); }
        }
        public object DragDropData
        {
            get { return GetValue(DragDropDataProperty); }
            set { SetValue(DragDropDataProperty, value); }
        }
        public bool IsDragEnable
        {
            get { return (bool)GetValue(IsDragEnableProperty); }
            set { SetValue(IsDragEnableProperty, value); }
        }
#endregion Properties
#region Methods
        /// <summary>
        /// 初期化
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseDown += PreviewMouseDownHandler;
            AssociatedObject.PreviewMouseMove += PreviewMouseMoveHandler;
            AssociatedObject.PreviewMouseUp += PreviewMouseUpHandler;
            AssociatedObject.QueryContinueDrag += QueryContinueDragHandler;
            base.OnAttached();
        }

        /// <summary>
        /// 後始末
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseDown -= PreviewMouseDownHandler;
            AssociatedObject.PreviewMouseMove -= PreviewMouseMoveHandler;
            AssociatedObject.PreviewMouseUp -= PreviewMouseUpHandler;
            AssociatedObject.QueryContinueDrag -= QueryContinueDragHandler;
            base.OnDetaching();
        }

        /// <summary>
        /// マウスボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if(!IsDragEnable)
            {
                return;
            }
            origin = e.GetPosition(AssociatedObject);
            isButtonDown = true;

            if (sender is IInputElement)
            {
                // マウスダウンされたアイテムを記憶
                dragItem = sender as IInputElement;
                // マウスダウン時の座標を取得
                dragStartPos = e.GetPosition(dragItem);
            }
        }

        /// <summary>
        /// マウス移動処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewMouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (!IsDragEnable)
            {
                return;
            }
            if (e.LeftButton != MouseButtonState.Pressed || !isButtonDown)
            {
                return;
            }
            var point = e.GetPosition(AssociatedObject);

            if (CheckDistance(point, origin))
            {
                // アクティブWindowの直下のContentに対して、Adornerを付加する
                var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                if(window != null)
                {
                    var root = window.Content as UIElement;
                    var layer = AdornerLayer.GetAdornerLayer(root);
                    dragGhost = new DragAdorner(root, (UIElement)sender, 0.5, dragStartPos);
                    layer.Add(dragGhost);
                    DragDrop.DoDragDrop(AssociatedObject, DragDropData, AllowedEffects);
                    layer.Remove(dragGhost);
                }
                else
                {
                    DragDrop.DoDragDrop(AssociatedObject, DragDropData, AllowedEffects);
                }
                isButtonDown = false;
                e.Handled = true;
                dragGhost = null;
                dragItem = null;
            }
        }

        /// <summary>
        /// マウスボタンリリース処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewMouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            isButtonDown = false;
        }

        /// <summary>
        /// 座標検査
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool CheckDistance(Point x, Point y)
        {
            return Math.Abs(x.X - y.X) >= SystemParameters.MinimumHorizontalDragDistance ||
                   Math.Abs(x.Y - y.Y) >= SystemParameters.MinimumVerticalDragDistance;
        }

        /// <summary>
        /// ゴーストの移動処理
        /// Window全体に、ゴーストが移動するタイプのドラッグを想定している
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryContinueDragHandler(object sender, QueryContinueDragEventArgs e)
        {
            if (!IsDragEnable)
            {
                return;
            }
            if (dragGhost != null)
            {
                var p = CursorInfo.GetNowPosition((Visual)dragItem);
                dragGhost.LeftOffset = p.X;
                dragGhost.TopOffset = p.Y - 32;
            }
        }
#endregion Methods
    }
}
