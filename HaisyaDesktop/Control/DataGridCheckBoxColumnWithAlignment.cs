using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HaisyaDesktop.Control
{
    class DataGridCheckBoxColumnWithAlignment: DataGridCheckBoxColumn
    {
        // 依存関係プロパティ 垂直方向のアラインメント（デフォルトは中央）
        public VerticalAlignment VerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalAlignmentProperty); }
            set { SetValue(VerticalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty VerticalAlignmentProperty =
            DependencyProperty.Register("VerticalAlignment",
                typeof(VerticalAlignment), typeof(DataGridCheckBoxColumnWithAlignment), new PropertyMetadata(VerticalAlignment.Center));


        // 依存関係プロパティ 水平方向のアラインメント（デフォルトは左）
        public HorizontalAlignment HorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
            set { SetValue(HorizontalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.Register("HorizontalAlignment",
                typeof(HorizontalAlignment), typeof(DataGridCheckBoxColumnWithAlignment), new PropertyMetadata(HorizontalAlignment.Center));

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            // 未編集状態の時はTextBlock
            var tbl = base.GenerateElement(cell, dataItem) as CheckBox;

            if (tbl != null)
            {
                tbl.HorizontalAlignment = this.HorizontalAlignment;
                tbl.VerticalAlignment = this.VerticalAlignment;
                return tbl;
            }

            throw new NullReferenceException();
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            // 編集状態の時はTextBox
            var tb = base.GenerateEditingElement(cell, dataItem) as CheckBox;

            if (tb != null)
            {
                tb.HorizontalAlignment = this.HorizontalAlignment;
                tb.VerticalAlignment = this.VerticalAlignment;
                return tb;
            }

            throw new NullReferenceException();
        }
    }


}
