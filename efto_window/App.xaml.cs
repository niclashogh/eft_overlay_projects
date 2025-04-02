using System;
using Microsoft.UI.Xaml;
using efto_window.Views.Windows;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Text;
using System.Diagnostics;
using Microsoft.UI.Dispatching;
using efto_model.Models.Enums;
using System.Collections.Generic;

namespace efto_window
{
    public partial class App : Application
    {
        private Window hiddenWindow;
        private DispatcherQueue? dispatcherQueue;
        private Dictionary<InterProcessComs, Window> activeWindows = new();

        #region App
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.dispatcherQueue = DispatcherQueue.GetForCurrentThread();

            Task.Run(RunICPServer);
        }
        #endregion

        #region OnLaunched
        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            this.hiddenWindow = new Window
            {
                Title = "Hidden Window",
            }; // Force-loading the app, when every other window are closed.
        }
        #endregion

        public void RemoveWindowFromList(InterProcessComs key)
        {
            if (activeWindows.ContainsKey(key))
            {
                activeWindows.Remove(key);
            }
        }

        private void RunICPServer()
        {
            try
            {
                using (NamedPipeServerStream server = new("efto", PipeDirection.In))
                {
                    while (true)
                    {
                        server.WaitForConnection();
                        byte[] buffer = new byte[256];
                        int bytesRead = server.Read(buffer, 0, buffer.Length);
                        string com = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        if (!string.IsNullOrEmpty(com) && dispatcherQueue != null)
                        {
                            if (Enum.TryParse<InterProcessComs>(com, out InterProcessComs parsedCom))
                            {
                                if (parsedCom == InterProcessComs.Close)
                                {
                                    dispatcherQueue?.TryEnqueue(() =>
                                    {
                                        Application.Current.Exit();
                                    });
                                }
                                else
                                {
                                    if (activeWindows.TryGetValue(parsedCom, out Window activePage))
                                    {
                                        dispatcherQueue?.TryEnqueue(() =>
                                        {
                                            if (!activePage.Visible)
                                            {
                                                activePage.Activate();
                                            }
                                        });
                                    }
                                    else
                                    {
                                        dispatcherQueue?.TryEnqueue(() =>
                                        {
                                            Window? newWindow = parsedCom switch
                                            {
                                                InterProcessComs.Map => new Map_Window(),
                                                InterProcessComs.Search => new Search_Window(),
                                                InterProcessComs.Setting => new Setting_Window(),
                                                InterProcessComs.Browser => new Browser_Window(),
                                                _ => null
                                            };

                                            if (newWindow != null)
                                            {
                                                this.activeWindows.Add(parsedCom, newWindow);
                                                newWindow.Activate();
                                            }
                                        });
                                    }
                                }
                            }
                        }

                        server.Disconnect();
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine($"\nIPC SERVER: \n{ex.ToString()}\n"); }
        }
    }
}
