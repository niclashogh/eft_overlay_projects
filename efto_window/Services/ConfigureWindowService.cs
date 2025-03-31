using efto_model.Models.DataTransferObjects;
using efto_model.Models.Enums;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.Runtime.InteropServices;
using Windows.Graphics;

namespace efto.Services
{
    public class ConfigureWindowService
    {
        private AppWindow Window { get; set; }

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        public ConfigureWindowService(int width, AppWindow window)
        {
            this.Window = window;
            GetDisplay(this.Window.Id);
            window.Resize(new(width, this.ScreenHeight));
            window.Move(new(0, 5));
        }

        public ConfigureWindowService(DimensionRecord<int> dimension, AppWindow window)
        {
            this.Window = window;
            GetDisplay(this.Window.Id);
            window.Resize(new(dimension.Width, dimension.Height));
        }

        public ConfigureWindowService(DimensionRecord<int> dimension, PositionRecord<int, Vertical_Alignments> position, AppWindow window) : this(dimension, window)
        {
            int verticalPlacement = 5;

            switch (position.VerticalPlacement)
            {
                case Vertical_Alignments.Top:
                    verticalPlacement = 5;
                    break;
                case Vertical_Alignments.Center:
                    verticalPlacement = (this.ScreenHeight / 2) - (dimension.Height / 2);
                    break;
                case Vertical_Alignments.Bottom:
                    verticalPlacement = this.ScreenHeight - dimension.Height;
                    break;
            }

            window.Move(new(position.HorizontalPlacement, verticalPlacement));
        }

        private void GetDisplay(WindowId windowId)
        {
            DisplayArea screenResolution = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Primary);
            this.ScreenWidth = screenResolution.OuterBounds.Width;
            this.ScreenHeight = screenResolution.OuterBounds.Height;
        }

        public void SetTopMost(nint hWnd) // Send Arg: WindowNative.GetWindowHandle(this);
        {
            [DllImport("user32.dll", SetLastError = true)]
            static extern bool SetWindowPos(nint hWnd, int hWndInsertAfter, int x, int y, int cs, int cy, uint uFlags);

            const int HWND_TOPMOST = -1;
            const uint SWP_NOSIZE = 0x0001;
            const uint SWP_NOMOVE = 0x0002;
            const uint TOPMOST_FLAGS = SWP_NOSIZE | SWP_NOMOVE;

            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        public void DisableDrag() => this.Window.TitleBar.SetDragRectangles(new RectInt32[] { new(0, 0, 0, 0) });

        public void DisableCaptionButtons(nint hWnd) // Send Arg: WindowNative.GetWindowHandle(this);
        {
            const int GWL_STYLE = -16;
            const int WS_MINIMIZEBOX = 0x00020000;
            const int WS_MAXIMIZEBOX = 0x00010000;

            [DllImport("user32.dll", SetLastError = true)]
            static extern int GetWindowLong(nint hWnd, int nIndex);

            [DllImport("user32.dll", SetLastError = true)]
            static extern int SetWindowLong(nint hWnd, int nIndex, int dwNewLong);

            int style = GetWindowLong(hWnd, GWL_STYLE);
            SetWindowLong(hWnd, GWL_STYLE, style & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX);
        }

        public void ConfigureTitleBar<T>(T window) where T : class
        {
            nint hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

            if (appWindow != null)
            {
                appWindow.TitleBar.ExtendsContentIntoTitleBar = true; // Removes the Title & Icon

                appWindow.TitleBar.BackgroundColor = Colors.DarkSlateGray;
                appWindow.TitleBar.ButtonBackgroundColor = Colors.DarkSlateGray;

                //appWindow.TitleBar.ForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonForegroundColor = Colors.White;
            }
        }
    }
}
