using efto_model.Models.DataTransferObjects;
using efto_model.Models.Enums;
using efto_window;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Runtime.InteropServices;
using Windows.Graphics;

namespace efto.Services
{
    public class WindowController
    {
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        #region Contructors
        public WindowController(AppWindow appWindow) => GetDisplay(appWindow.Id);

        public WindowController(int width, AppWindow appWindow) : this(appWindow)
        {
            appWindow.Resize(new(width, this.ScreenHeight));
            appWindow.Move(new(0, 4)); // Better Vertical Alignment
        }

        public WindowController(DimensionRecord<int> dimension, AppWindow appWindow) : this(appWindow) => appWindow.Resize(new(dimension.Width, dimension.Height));

        public WindowController(DimensionRecord<int> dimension, PositionRecord<int, Vertical_Alignments> position, AppWindow appWindow) : this(dimension, appWindow)
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

            appWindow.Move(new(position.HorizontalPlacement, verticalPlacement));
        }
        #endregion

        #region Private/Internal Methods
        private void GetDisplay(WindowId windowId)
        {
            DisplayArea screenResolution = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Primary);
            this.ScreenWidth = screenResolution.OuterBounds.Width;
            this.ScreenHeight = screenResolution.OuterBounds.Height;
        }
        #endregion

        #region Configuration Methods
        /// <summary>
        /// Send: WindowNative.GetWindowHandle(this)
        /// </summary>
        /// <param name="hWnd"></param>
        internal void SetTopMost(nint hWnd)
        {
            [DllImport("user32.dll", SetLastError = true)]
            static extern bool SetWindowPos(nint hWnd, int hWndInsertAfter, int x, int y, int cs, int cy, uint uFlags);

            const int HWND_TOPMOST = -1;
            const uint SWP_NOSIZE = 0x0001;
            const uint SWP_NOMOVE = 0x0002;
            const uint TOPMOST_FLAGS = SWP_NOSIZE | SWP_NOMOVE;

            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        internal void DisableDrag(AppWindow appWindow) => appWindow.TitleBar.SetDragRectangles(new RectInt32[] { new(0, 0, 0, 0) });

        /// <summary>
        /// Send: WindowNative.GetWindowHandle(this)
        /// </summary>
        /// <param name="hWnd"></param>
        internal void DisableCaptionButtons(nint hWnd)
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

        internal void ConfigureTitleBar(Window window)
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
        #endregion

        #region Base Window Controllers
        internal void InitialNavigation(Frame contentFrame, Page page) => contentFrame.Navigate(page.GetType());

        internal void OnClose(InterProcessComs com)
        {
            if (Application.Current is App app) app.RemoveWindowFromList(com);
        }

        internal void Menu_Toggle(SplitView menuPanel) => menuPanel.IsPaneOpen = !menuPanel.IsPaneOpen;

        internal void Menu_SelectionChanged(object sender, Frame contentFrame)
        {
            if (sender is ListBox list)
            {
                if (list != null)
                {
                    ViewRecord<Page> page = (ViewRecord<Page>)list.SelectedItem;
                    contentFrame.Navigate(page.View.GetType());
                }
            }
        }
        #endregion
    }
}
