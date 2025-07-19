using efto_model.Records;
using efto_model.Services;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace efto_window.ViewModels
{
    public class WindowVM : NotifyChangedService
    {
        #region Variables & Properties
        public MetaDataRecord MetaData { get; } = new("MADE BY Grannice", "VERSION 0.9");

        private bool disableNavigation = false;
        public bool DisableNavigation
        {
            get { return !this.disableNavigation; }
            set
            {
                this.disableNavigation = value;
                OnPropertyChanged(nameof(this.DisableNavigation));
            }
        }
        #endregion

        internal bool IsRunningAsAdminstrator()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using (WindowsIdentity id = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal principal = new WindowsPrincipal(id);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            else // If not Windows
            {
                return false;
            }
        }
    }
}
