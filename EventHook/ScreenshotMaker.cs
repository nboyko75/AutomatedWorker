using System;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using EventHook.Tools;

namespace EventHook
{
    public class ScreenshotMaker
    {
        #region Attributes
        /// <summary>
        /// Окно растягиваемое на экран
        /// </summary>
        public Window windowBackground;
        /// <summary>
        /// Первая точка на экране, с которой начинается выделение прямоугольника (Левая верхняя)
        /// </summary>
        Point firstPoint;
        /// <summary>
        /// Вторая точка на экране, с которой продолжается выделение прямоугольника (Правая нижняя)
        /// </summary>
        Point secondPoint;
        /// <summary>
        /// Прямоугольник для ручного выделения области для снятия скриншота с части экрана
        /// </summary>
        Rectangle selectedRect;
        /// <summary>
        /// Прямоугольная область для рисования не затенённой части изображения 
        /// </summary>
        Int32Rect rectForDraw;
        /// <summary>
        /// Канва на которой будут рассчитываться координаты прямоугольника и область для снятия скриншота
        /// </summary>
        Canvas canvas;
        /// <summary>
        /// Кисть определяющая цвет выделяемого прямоугольника
        /// </summary>
        public System.Windows.Media.Brush rectBrush { get; set; }
        /// <summary>
        /// Толщина линий прямоугольника
        /// </summary>
        Double rectThickness { get; set; }

        TextBlock txtBlock;
        /// <summary>
        /// Здесь должно содержаться изображение скриншота экрана
        /// </summary>
        public BitmapSource screenSource;
        /// <summary>
        /// Здесь должно содержаться изображение скриншота выделенного участка экрана
        /// </summary>
        public BitmapSource bitmapSource;
        /// <summary>
        /// Визуальный объект используемый для отрисовки
        /// </summary>
        DrawingVisual drawingVisual;
        /// <summary>
        /// Описывает визуальное содержимое с использованием команд рисования, проталкивания и выталкивания.
        /// </summary>
        DrawingContext drawingContext;
        /// <summary>
        /// То что преобразовывает объект в изображение
        /// </summary>
        RenderTargetBitmap renderTargetBitmap;
        /// <summary>
        /// Затенённое определённым цветом изображение
        /// </summary>
        ImageSource imgSourceShaded;
        /// <summary>
        /// Оригинальное изображение
        /// </summary>
        BitmapImage bmpImageOriginal;
        /// <summary>
        /// Кисть для затенения изображения == Brush for shaded image
        /// </summary>
        SolidColorBrush solidColorBrush;

        int mouseClickCount = 0;
        public Point MouseClickPoint { get { return mouseClickPoint; } }
        public Point MouseClickRelativePoint { get { return new Point(mouseClickPoint.X - firstPoint.X, mouseClickPoint.Y - firstPoint.Y); } }
        Point mouseClickPoint;
        #endregion

        #region Public methods
        public BitmapSource BeginSelectionImageFromScreen()
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
        public BitmapSource BeginSelectionImageFromScreen(System.Windows.Media.Brush br, Double RectThickness, Color backColor)
        {
            InitializeComponents();
            solidColorBrush = new SolidColorBrush(Color.FromArgb(120, backColor.R, backColor.G, backColor.B));

            rectBrush = br;
            rectThickness = RectThickness;

            screenSource = CaptureScreen(ImageFormat.Bmp);
            // bmpImageOriginal = ImageUtils.BitmapImageFromSource(screenSource);
            bmpImageOriginal = CaptureImageRect(ImageFormat.Bmp);

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

            selectedRect.Stroke = br;
            selectedRect.StrokeThickness = RectThickness;
            selectedRect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            selectedRect.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            Canvas.SetTop(selectedRect, firstPoint.Y);
            Canvas.SetLeft(selectedRect, firstPoint.X);
            canvas.Children.Add(selectedRect);

            windowBackground.ShowDialog();
            DisposeComponents();
            return bitmapSource;
        }

        /// <summary>
        /// Сделать снимок экрана
        /// </summary>
        /// <param name="format">Формат в котором вернуть изображение</param>
        /// <returns></returns>
        public BitmapSource CaptureScreen(ImageFormat format)
        {
            System.Drawing.Rectangle rect = GetScreenRect();
            return CaptureRect(rect, format);
        }

        /// <summary>
        /// Сделать снимок определённой области изображения
        /// </summary>
        /// <param name="rect">Прямоугольник в котором будет сделан снимок</param>
        /// <param name="format">Формат изображения</param>
        /// <returns></returns>
        public BitmapSource CaptureRect(System.Drawing.Rectangle rect, ImageFormat format)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(rect.Width, rect.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, System.Drawing.CopyPixelOperation.SourceCopy);
            
            return ImageUtils.SourceFromBitmap(bitmap);
        }

        public BitmapImage CaptureImageRect(ImageFormat format) 
        {
            System.Drawing.Rectangle rect = GetScreenRect();
            return CaptureImageRect(rect, format);
        }

        public BitmapImage CaptureImageRect(System.Drawing.Rectangle rect, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
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

        public System.Drawing.Bitmap GetBitmapRect(ImageFormat format)
        {
            System.Drawing.Rectangle rect = GetScreenRect();
            return GetBitmapRect(rect, format);
        }

        public System.Drawing.Bitmap GetBitmapRect(System.Drawing.Rectangle rect, ImageFormat format)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, System.Drawing.CopyPixelOperation.SourceCopy);
            return bitmap;
        }

        public Point GetCenterPoint()
        {
            return new Point((firstPoint.X + secondPoint.X) / 2, (firstPoint.Y + secondPoint.Y) / 2);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Проинициализировать поля
        /// </summary>
        private void InitializeComponents()
        {
            windowBackground = new Window();
            //windowBackground.Topmost = true;
            windowBackground.WindowStyle = WindowStyle.None;
            windowBackground.WindowState = WindowState.Maximized;
            windowBackground.ResizeMode = System.Windows.ResizeMode.NoResize; // обязательно должно быть, решает одну проблему

            rectForDraw = new Int32Rect();
            drawingVisual = new DrawingVisual();
            imgSourceShaded = new BitmapImage();
            selectedRect = new Rectangle();
        }

        private void CloseWindow()
        {
            windowBackground.Close();
        }

        /// <summary>
        /// Освободить поля
        /// </summary>
        private void DisposeComponents()
        {
            screenSource = null;
            // bitmapSource - повинен бути доступним для MainForm
            // bitmapSource = null;
            windowBackground = null;
            drawingVisual = null;
            drawingContext = null;
            renderTargetBitmap = null;
            imgSourceShaded = null;
            bmpImageOriginal = null;
            selectedRect = null;
            canvas = null;
            solidColorBrush = null;
            mouseClickCount = 0;
        }

        /// <summary>
        /// Нажатие левой кнопки мыши на окне содержащем скриншот всего экрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowBackground_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
        private void windowBackground_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle();
            Int32Rect rectangle = new Int32Rect();

            if (firstPoint.X > secondPoint.X)
            {
                rectangle.X = (int)(secondPoint.X + selectedRect.StrokeThickness);
                rectangle.Width = (int)(firstPoint.X - secondPoint.X - selectedRect.StrokeThickness * 2);
                if (rectangle.Width < 1)
                {
                    rectangle.Width = 1;
                }
            }
            else
            {
                rectangle.X = (int)(firstPoint.X + selectedRect.StrokeThickness);
                rectangle.Width = (int)(secondPoint.X - firstPoint.X - selectedRect.StrokeThickness * 2);
                if (rectangle.Width < 1)
                {
                    rectangle.Width = 1;
                }
            }

            if (firstPoint.Y > secondPoint.Y)
            {
                rectangle.Y = (int)(secondPoint.Y + selectedRect.StrokeThickness);
                rectangle.Height = (int)(firstPoint.Y - secondPoint.Y - selectedRect.StrokeThickness * 2);
                if (rectangle.Height < 1)
                {
                    rectangle.Height = 1;
                }
            }
            else
            {
                rectangle.Y = (int)(firstPoint.Y + selectedRect.StrokeThickness);
                rectangle.Height = (int)(secondPoint.Y - firstPoint.Y - selectedRect.StrokeThickness * 2);
                if (rectangle.Height < 1)
                {
                    rectangle.Height = 1;
                }
            }

            CroppedBitmap cb = new CroppedBitmap(screenSource, rectangle);
            bitmapSource = cb;

            // CloseWindow();
        }

        /// <summary>
        /// Движение курсора мыши по окну содержащему скриншот всего экрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowBackground_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
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

        private void windowBackground_KeyDown(object sender, KeyEventArgs e) 
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
        private void DrawLightRegtangle(Int32Rect rectForDraw)
        {
            if (rectForDraw.Width > 0 && rectForDraw.Height > 0)
            {
                Rect imgRect = new Rect(new Point(0, 0), new Point(bmpImageOriginal.Width, bmpImageOriginal.Height));
                drawingContext = drawingVisual.RenderOpen();
                drawingContext.DrawImage(imgSourceShaded, imgRect);

                CroppedBitmap cb = new CroppedBitmap(screenSource, rectForDraw);
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
        private void MoveDrawRectangle(Point pointLeftTop, Point pointRightBottom)
        {
            if (pointLeftTop.X > pointRightBottom.X)
            {
                Canvas.SetLeft(selectedRect, pointRightBottom.X);
                selectedRect.Width = pointLeftTop.X - pointRightBottom.X;
                rectForDraw.X = (int)pointRightBottom.X;
            }
            else
            {
                Canvas.SetLeft(selectedRect, pointLeftTop.X);
                selectedRect.Width = pointRightBottom.X - pointLeftTop.X;
                rectForDraw.X = (int)pointLeftTop.X;
            }

            if (pointLeftTop.Y > pointRightBottom.Y)
            {
                Canvas.SetTop(selectedRect, pointRightBottom.Y);
                selectedRect.Height = pointLeftTop.Y - pointRightBottom.Y;
                rectForDraw.Y = (int)pointRightBottom.Y;
            }
            else
            {
                Canvas.SetTop(selectedRect, pointLeftTop.Y);
                selectedRect.Height = pointRightBottom.Y - pointLeftTop.Y;
                rectForDraw.Y = (int)pointLeftTop.Y;
            }
            rectForDraw.Width = (int)selectedRect.Width;
            rectForDraw.Height = (int)selectedRect.Height;
            DrawLightRegtangle(rectForDraw);
            UpdateInfo(pointLeftTop, pointRightBottom);
        }

        private System.Drawing.Rectangle GetScreenRect()
        {
            return new System.Drawing.Rectangle(new System.Drawing.Point(0, 0),
                new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height));
        }

        private void CreateInfo()
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
        private void UpdateInfo(Point first, Point second)
        {
            txtBlock.Text = "Width: " + selectedRect.Width.ToString() + " " + "Height" + selectedRect.Height.ToString() + " \n"
                + "pointLeftTop.X: " + first.X.ToString() + " " + "pointLeftTop.Y: " + first.Y.ToString() + " \n"
                + "pointRightBottom.X: " + second.X.ToString() + " " + "pointRightBottom.Y: " + second.Y.ToString();
        }
        #endregion
    }
}