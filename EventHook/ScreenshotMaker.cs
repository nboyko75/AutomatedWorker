using System;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace EventHook
{
    public static class ScreenshotMaker
    {
        #region Attributes
        /// <summary>
        /// Окно растягиваемое на экран
        /// </summary>
        public static Window windowBackground;
        /// <summary>
        /// Первая точка на экране, с которой начинается выделение прямоугольника (Левая верхняя)
        /// </summary>
        static Point firstPoint;
        /// <summary>
        /// Вторая точка на экране, с которой продолжается выделение прямоугольника (Правая нижняя)
        /// </summary>
        static Point secondPoint;
        /// <summary>
        /// Прямоугольник для ручного выделения области для снятия скриншота с части экрана
        /// </summary>
        static Rectangle rect;
        /// <summary>
        /// Прямоугольная область для рисования не затенённой части изображения 
        /// </summary>
        static Int32Rect rectForDraw;
        /// <summary>
        /// Канва на которой будут рассчитываться координаты прямоугольника и область для снятия скриншота
        /// </summary>
        static Canvas canvas;
        /// <summary>
        /// Кисть определяющая цвет выделяемого прямоугольника
        /// </summary>
        public static System.Windows.Media.Brush rectBrush { get; set; }
        /// <summary>
        /// Толщина линий прямоугольника
        /// </summary>
        static Double rectThickness { get; set; }

        static TextBlock txtBlock;
        /// <summary>
        /// 
        /// </summary>
        static System.Windows.Controls.Image image;
        /// <summary>
        /// Здесь должно содержаться изображение скриншота выделенного участка экрана
        /// </summary>
        public static BitmapSource bitmapSource;
        /// <summary>
        /// Визуальный объект используемый для отрисовки
        /// </summary>
        static DrawingVisual drawingVisual;
        /// <summary>
        /// Описывает визуальное содержимое с использованием команд рисования, проталкивания и выталкивания.
        /// </summary>
        static DrawingContext drawingContext;
        /// <summary>
        /// То что преобразовывает объект в изображение
        /// </summary>
        static RenderTargetBitmap renderTargetBitmap;
        /// <summary>
        /// Затенённое определённым цветом изображение
        /// </summary>
        static ImageSource imgSourceShaded;
        /// <summary>
        /// Оригинальное изображение
        /// </summary>
        static BitmapImage bmpImageOriginal;
        /// <summary>
        /// Кисть для затенения изображения == Brush for shaded image
        /// </summary>
        static SolidColorBrush solidColorBrush;

        static int mouseClickCount = 0;
        public static Point MouseClickPoint { get { return mouseClickPoint; } }
        public static Point MouseClickRelativePoint { get { return new Point(mouseClickPoint.X - firstPoint.X, mouseClickPoint.Y - firstPoint.Y); } }
        static Point mouseClickPoint;
        #endregion

        #region Public methods
        public static BitmapSource BeginSelectionImageFromScreen()
        {
            System.Windows.Media.Brush br = new SolidColorBrush(Color.FromRgb(210, 180, 140));
            Double RectThickness = 2;
            Color backColor = Color.FromRgb(135, 206, 250);
            return BeginSelectionImageFromScreen(br, RectThickness, backColor);
        }

        /// <summary>
        /// Запустить метод ручного выделения прямоугольной области пользователем, 
        /// для снятия скриншота с выбранного участка экрана
        /// </summary>
        /// <param name="br">Кисть определяющая цвет прямоугольника</param>
        /// <param name="rectThickness">Толщина линий прямоугольника</param>
        /// <param name="backBrush">Кисть определяющая цвет заливки фона скриншота</param>
        /// <returns></returns>
        public static BitmapSource BeginSelectionImageFromScreen(System.Windows.Media.Brush br, Double RectThickness, Color backColor)
        {
            InitializeComponents();
            solidColorBrush = new SolidColorBrush(Color.FromArgb(120, backColor.R, backColor.G, backColor.B));

            rectBrush = br;
            rectThickness = RectThickness;

            bmpImageOriginal = CaptureScreen(ImageFormat.Bmp);

            imgSourceShaded = bmpImageOriginal;
            Rect imgRect = new Rect(new Point(0, 0), new Point(bmpImageOriginal.Width, bmpImageOriginal.Height));
            drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawImage(imgSourceShaded, imgRect);
            drawingContext.DrawRectangle(solidColorBrush, null, imgRect);
            drawingContext.Close();

            renderTargetBitmap = new RenderTargetBitmap((int)bmpImageOriginal.Width, (int)bmpImageOriginal.Height, 96, 96,
             System.Windows.Media.PixelFormats.Pbgra32);
            renderTargetBitmap.Render(drawingVisual);

            imgSourceShaded = renderTargetBitmap;

            image = new System.Windows.Controls.Image();
            image.Source = bmpImageOriginal;

            ImageBrush imgBrush = new ImageBrush();
            imgBrush.AlignmentX = AlignmentX.Left;
            imgBrush.AlignmentY = AlignmentY.Top;
            imgBrush.Stretch = Stretch.None;
            imgBrush.TileMode = TileMode.None;
            imgBrush.ImageSource = renderTargetBitmap;
            windowBackground.Background = imgBrush;

            canvas = new Canvas();
            canvas.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            canvas.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //Canvas.SetTop(image, 0);
            //Canvas.SetLeft(image, 0);
            //canvas.Children.Add(image);
            canvas.Margin = new Thickness(0, 0, 0, 0);
            windowBackground.Content = canvas;
            windowBackground.Cursor = System.Windows.Input.Cursors.Cross;
            windowBackground.Focusable = true;

            windowBackground.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(windowBackground_MouseLeftButtonDown);
            windowBackground.MouseMove += new System.Windows.Input.MouseEventHandler(windowBackground_MouseMove);
            windowBackground.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(windowBackground_MouseLeftButtonUp);
            windowBackground.KeyDown += new System.Windows.Input.KeyEventHandler(windowBackground_KeyDown);

            rect.Stroke = br as System.Windows.Media.Brush;
            rect.StrokeThickness = RectThickness;
            rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            Canvas.SetTop(rect, firstPoint.Y);
            Canvas.SetLeft(rect, firstPoint.X);
            canvas.Children.Add(rect);

            windowBackground.ShowDialog();
            DisposeComponents();
            return bitmapSource;
        }

        /// <summary>
        /// Сделать снимок экрана
        /// </summary>
        /// <param name="format">Формат в котором вернуть изображение</param>
        /// <returns></returns>
        public static BitmapImage CaptureScreen(ImageFormat format)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(new System.Drawing.Point(0, 0),
             new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
              System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height));

            return CaptureRect(rect, format);
        }

        /// <summary>
        /// Сделать снимок определённой области изображения
        /// </summary>
        /// <param name="rect">Прямоугольник в котором будет сделан снимок</param>
        /// <param name="format">Формат изображения</param>
        /// <returns></returns>
        public static BitmapImage CaptureRect(System.Drawing.Rectangle rect, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(rect.Width, rect.Height,
                    System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
                graphics.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, System.Drawing.CopyPixelOperation.SourceCopy);
                bitmap.Save(ms, format);
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = ms;
                img.EndInit();
                return img;
            }
        }

        public static Point GetCenterPoint()
        {
            return new Point((firstPoint.X + secondPoint.X) / 2, (firstPoint.Y + secondPoint.Y) / 2);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Проинициализировать поля
        /// </summary>
        private static void InitializeComponents()
        {
            windowBackground = new Window();
            //windowBackground.Topmost = true;
            windowBackground.WindowStyle = WindowStyle.None;
            windowBackground.WindowState = WindowState.Maximized;
            windowBackground.ResizeMode = System.Windows.ResizeMode.NoResize; // обязательно должно быть, решает одну проблему

            rectForDraw = new Int32Rect();
            drawingVisual = new DrawingVisual();
            imgSourceShaded = new BitmapImage();
            rect = new Rectangle();
        }

        private static void CloseWindow()
        {
            windowBackground.Close();
        }

        /// <summary>
        /// Освободить поля
        /// </summary>
        private static void DisposeComponents()
        {
            windowBackground = null;
            drawingVisual = null;
            drawingContext = null;
            renderTargetBitmap = null;
            imgSourceShaded = null;
            bmpImageOriginal = null;
            rect = null;
            image = null;
            canvas = null;
            solidColorBrush = null;
            mouseClickCount = 0;
        }

        /// <summary>
        /// Нажатие левой кнопки мыши на окне содержащем скриншот всего экрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void windowBackground_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e == null) 
            {
                CloseWindow();
            }
            else if (mouseClickCount >= 1)
            {
                mouseClickPoint = e.GetPosition(windowBackground);
                CloseWindow();
            }
            else
            {
                firstPoint = e.GetPosition(windowBackground);
                CreateInfo();
                MoveDrawRectangle(firstPoint, firstPoint);
                mouseClickCount++;
            }
        }

        /// <summary>
        /// Отпускание левой кнопки мыши на окне содержащем скриншот всего экрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void windowBackground_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle();
            Int32Rect rectangle = new Int32Rect();

            if (firstPoint.X > secondPoint.X)
            {
                rectangle.X = (int)(secondPoint.X + rect.StrokeThickness);
                rectangle.Width = (int)(firstPoint.X - secondPoint.X - rect.StrokeThickness * 2);
                if (rectangle.Width < 1)
                {
                    rectangle.Width = 1;
                }
            }
            else
            {
                rectangle.X = (int)(firstPoint.X + rect.StrokeThickness);
                rectangle.Width = (int)(secondPoint.X - firstPoint.X - rect.StrokeThickness * 2);
                if (rectangle.Width < 1)
                {
                    rectangle.Width = 1;
                }
            }

            if (firstPoint.Y > secondPoint.Y)
            {
                rectangle.Y = (int)(secondPoint.Y + rect.StrokeThickness);
                rectangle.Height = (int)(firstPoint.Y - secondPoint.Y - rect.StrokeThickness * 2);
                if (rectangle.Height < 1)
                {
                    rectangle.Height = 1;
                }
            }
            else
            {
                rectangle.Y = (int)(firstPoint.Y + rect.StrokeThickness);
                rectangle.Height = (int)(secondPoint.Y - firstPoint.Y - rect.StrokeThickness * 2);
                if (rectangle.Height < 1)
                {
                    rectangle.Height = 1;
                }
            }

            CroppedBitmap cb = new CroppedBitmap((BitmapSource)image.Source, rectangle);
            bitmapSource = cb;

            // CloseWindow();
        }

        /// <summary>
        /// Движение курсора мыши по окну содержащему скриншот всего экрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void windowBackground_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                secondPoint = e.GetPosition(windowBackground);
                if (secondPoint.X > SystemParameters.PrimaryScreenWidth)
                {
                    secondPoint.X = SystemParameters.PrimaryScreenWidth;
                }
                MoveDrawRectangle(firstPoint, secondPoint);
            }
        }

        private static void windowBackground_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.Key == Key.Escape)
            {
                bitmapSource = null;
                CloseWindow();
            }
        }

        /// <summary>
        /// Рисует полностью всю картинку, осветвлённую область внутри прямоугольника и затенённую снаружи
        /// </summary>
        /// <param name="rectForDraw">Прямоугольная область для осветлённой картинки</param>
        private static void DrawLightRegtangle(Int32Rect rectForDraw)
        {
            if (rectForDraw.Width > 0 && rectForDraw.Height > 0)
            {
                Rect imgRect = new Rect(new Point(0, 0), new Point(bmpImageOriginal.Width, bmpImageOriginal.Height));
                drawingContext = drawingVisual.RenderOpen();
                drawingContext.DrawImage(imgSourceShaded, imgRect);

                CroppedBitmap cb = new CroppedBitmap((BitmapSource)image.Source, rectForDraw);
                Rect rectForLosso = new Rect((double)rectForDraw.X, (double)rectForDraw.Y,
                 (double)rectForDraw.Width, (double)rectForDraw.Height);
                drawingContext.DrawRectangle(solidColorBrush, null, rectForLosso);
                drawingContext.DrawImage(cb, rectForLosso);
                drawingContext.Close();

                renderTargetBitmap = new RenderTargetBitmap((int)bmpImageOriginal.Width, (int)bmpImageOriginal.Height, 96, 96,
                 System.Windows.Media.PixelFormats.Pbgra32);
                renderTargetBitmap.Render(drawingVisual);
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.AlignmentX = AlignmentX.Left;
                imgBrush.AlignmentY = AlignmentY.Top;
                imgBrush.Stretch = Stretch.None;
                imgBrush.TileMode = TileMode.None;
                imgBrush.ImageSource = renderTargetBitmap;
                windowBackground.Background = imgBrush;
            }
        }

        /// <summary>
        /// Растянуть/Переместить прямоугольник на указанные координаты
        /// </summary>
        /// <param name="pointLeftTop">Левый верхний угол</param>
        /// <param name="pointRightBottom">Правый нижний угол</param>
        private static void MoveDrawRectangle(Point pointLeftTop, Point pointRightBottom)
        {
            if (pointLeftTop.X > pointRightBottom.X)
            {
                Canvas.SetLeft(rect, pointRightBottom.X);
                rect.Width = pointLeftTop.X - pointRightBottom.X;
                rectForDraw.X = (int)pointRightBottom.X;
            }
            else
            {
                Canvas.SetLeft(rect, pointLeftTop.X);
                rect.Width = pointRightBottom.X - pointLeftTop.X;
                rectForDraw.X = (int)pointLeftTop.X;
            }

            if (pointLeftTop.Y > pointRightBottom.Y)
            {
                Canvas.SetTop(rect, pointRightBottom.Y);
                rect.Height = pointLeftTop.Y - pointRightBottom.Y;
                rectForDraw.Y = (int)pointRightBottom.Y;
            }
            else
            {
                Canvas.SetTop(rect, pointLeftTop.Y);
                rect.Height = pointRightBottom.Y - pointLeftTop.Y;
                rectForDraw.Y = (int)pointLeftTop.Y;
            }
            rectForDraw.Width = (int)rect.Width;
            rectForDraw.Height = (int)rect.Height;
            DrawLightRegtangle(rectForDraw);
            UpdateInfo(pointLeftTop, pointRightBottom);
        }

        private static void CreateInfo()
        {
            txtBlock = new TextBlock();
            txtBlock.TextWrapping = TextWrapping.Wrap;
            canvas.Children.Add(txtBlock);
            txtBlock.TextAlignment = TextAlignment.Left;
            Canvas.SetLeft(txtBlock, 50);
            Canvas.SetTop(txtBlock, 25);
            txtBlock.FontSize = 16;
            txtBlock.Foreground = Brushes.White;
            txtBlock.Background = Brushes.Tomato;
        }

        // Нужно только для выявления неточностей с координатами прямо во время работы программы
        private static void UpdateInfo(Point first, Point second)
        {
            txtBlock.Text = "Width: " + rect.Width.ToString() + " " + "Height" + rect.Height.ToString() + " \n"
                + "pointLeftTop.X: " + first.X.ToString() + " " + "pointLeftTop.Y: " + first.Y.ToString() + " \n"
                + "pointRightBottom.X: " + second.X.ToString() + " " + "pointRightBottom.Y: " + second.Y.ToString();
        }
        #endregion
    }
}