using System.Windows;
using System.Windows.Controls;

namespace BlobSmart.Uploader
{
    public class GridLayout : Grid
    {
        public static readonly DependencyProperty ChildMarginProperty =
            DependencyProperty.Register("ChildMargin", typeof(Thickness),
            typeof(GridLayout), new FrameworkPropertyMetadata(new Thickness(4))
            {
                AffectsArrange = true,
                AffectsMeasure = true
            });

        public Thickness ChildMargin
        {
            get
            {
                return (Thickness)GetValue(ChildMarginProperty);
            }
            set
            {
                SetValue(ChildMarginProperty, value);

                UpdateChildMargins();
            }
        }

        public void UpdateChildMargins()
        {
            int maxColumn = 0;
            int maxRow = 0;

            foreach (UIElement element in InternalChildren)
            {
                int row = GetRow(element);
                int rowSpan = GetRowSpan(element);
                int column = GetColumn(element);
                int columnSpan = GetColumnSpan(element);

                if (row + rowSpan > maxRow)
                    maxRow = row + rowSpan;

                if (column + columnSpan > maxColumn)
                    maxColumn = column + columnSpan;
            }

            foreach (UIElement element in InternalChildren)
            {
                var fe = element as FrameworkElement;

                if (null != fe)
                {
                    int row = GetRow(fe);
                    int rowSpan = GetRowSpan(fe);
                    int column = GetColumn(fe);
                    int columnSpan = GetColumnSpan(fe);
                    double factorLeft = 0.5;
                    double factorTop = 0.5;
                    double factorRight = 0.5;
                    double factorBottom = 0.5;

                    if (row == 0)
                        factorTop = 0;

                    if (row + rowSpan >= maxRow)
                        factorBottom = 0;

                    if (column == 0)
                        factorLeft = 0;
                    if (column + columnSpan >= maxColumn)
                        factorRight = 0;

                    fe.Margin = new Thickness(
                        ChildMargin.Left * factorLeft, ChildMargin.Top * factorTop,
                        ChildMargin.Right * factorRight, ChildMargin.Bottom * factorBottom);
                }
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            UpdateChildMargins();
            return base.MeasureOverride(availableSize);
        }
    }
}
