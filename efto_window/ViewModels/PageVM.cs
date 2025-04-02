using efto_model.Models.DataTransferObjects;
using efto_model.Services;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace efto_window.ViewModels
{
    public class PageVM : NotifyChangedService
    {
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
