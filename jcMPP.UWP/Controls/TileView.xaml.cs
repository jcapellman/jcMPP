using System;

using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace jcMPP.UWP.Controls {
    public sealed partial class TileView : Panel {
        protected override Size MeasureOverride(Size availableSize) {
            if (Columns == 0) {
                Columns = (int)(availableSize.Width / MaxItemWidth);
            }

            double finalWidth, finalHeight;

            if (this.Orientation == Orientation.Horizontal) {
                finalWidth = availableSize.Width;
                var itemWidth = Math.Floor(availableSize.Width / Columns);
                var actualRows = Math.Ceiling((double)Children.Count / Columns);
                var actualHeight = Math.Floor((double)availableSize.Height / actualRows);
                var itemHeight = Math.Min(actualHeight, itemWidth);

                foreach (var child in Children) {
                    child.Measure(new Size(itemWidth, itemHeight));
                }

                finalHeight = itemHeight * actualRows;
            } else {

                finalHeight = availableSize.Height;
                var itemHeight = Math.Floor(availableSize.Height / Rows);
                var actualColumns = Math.Ceiling((double)Children.Count / Rows);
                var actualWidth = Math.Floor((double)availableSize.Width / actualColumns);
                var itemWidth = Math.Min(actualWidth, itemHeight);
                finalWidth = itemWidth * actualColumns;

                foreach (var child in Children) {
                    child.Measure(new Size(itemWidth, itemHeight));
                }
            }

            return new Size(finalWidth, finalHeight);
        }

        protected override Size ArrangeOverride(Size finalSize) {
            if (Columns == 0) {
                Columns = (int)(finalSize.Width / MaxItemWidth);
            }

            if (Orientation == Orientation.Horizontal) {
                var actualRows = Math.Ceiling((double)Children.Count / Columns);
                var cellWidth = Math.Floor(finalSize.Width / Columns);
                var cellHeight = Math.Floor(finalSize.Height / actualRows);
                var cellSize = new Size(cellWidth, cellHeight);
                int row = 0, col = 0;

                foreach (UIElement child in Children) {
                    child.Arrange(new Rect(new Point(cellSize.Width * col, cellSize.Height * row), cellSize));
                    var element = child as FrameworkElement;
                    if (element != null) {
                        element.Height = cellSize.Height;
                        element.Width = cellSize.Width;
                    }

                    if (++col == Columns) {
                        row++;
                        col = 0;
                    }
                }
            } else {
                var actualColumns = Math.Ceiling((double)Children.Count / Rows);
                var cellWidth = Math.Floor(finalSize.Width / actualColumns);
                var cellHeight = Math.Floor(finalSize.Height / Rows);
                var cellSize = new Size(cellWidth, cellHeight);
                int row = 0, col = 0;

                foreach (UIElement child in Children) {
                    child.Arrange(new Rect(new Point(cellSize.Width * col, cellSize.Height * row), cellSize));

                    var element = child as FrameworkElement;

                    if (element != null) {
                        element.Height = cellSize.Height;
                        element.Width = cellSize.Width;
                    }

                    if (++row != Rows) {
                        continue;
                    }

                    col++;
                    row = 0;
                }
            }

            return finalSize;
        }

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public int MaxItemWidth {
            get { return (int)GetValue(MaxItemWidthProperty); }
            set { SetValue(MaxItemWidthProperty, value); }
        }

        public Orientation Orientation {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty MaxItemWidthProperty =
        DependencyProperty.Register("MaxItemWidth", typeof(int), typeof(TileView), new PropertyMetadata(1, OnMaxItemWidthChanged));


        public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register("Columns", typeof(int), typeof(TileView), new PropertyMetadata(1, OnColumnsChanged));


        public static readonly DependencyProperty RowsProperty =
        DependencyProperty.Register("Rows", typeof(int), typeof(TileView), new PropertyMetadata(1, OnRowsChanged));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TileView), new PropertyMetadata(1, OnOrientationChanged));

        static void OnColumnsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            int cols = (int)e.NewValue;
            if (cols < 0)
                ((TileView)obj).Columns = 1;
        }

        static void OnRowsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            int rows = (int)e.NewValue;
            if (rows < 0)
                ((TileView)obj).Rows = 1;
        }

        static void OnMaxItemWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            int maxItemWidth = (int)e.NewValue;
            if (maxItemWidth < 0)
                ((TileView)obj).MaxItemWidth = 1;
        }

        static void OnOrientationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
        }

    }
}