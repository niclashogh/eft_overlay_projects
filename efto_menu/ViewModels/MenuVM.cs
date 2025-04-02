using efto_model.Services;
using efto_model.Models.Enums;
using System.IO.Pipes;
using System.Text;

namespace efto_menu.ViewModels
{
    public class MenuVM : NotifyChangedService
    {
        #region Variables & Properties
        private string currentTime = DateTime.Now.ToShortTimeString();
        public string CurrentTime
        {
            get { return this.currentTime; }
            set
            {
                this.currentTime = value;
                OnPropertyChanged(nameof(this.CurrentTime));
            }
        }
        #endregion

        public MenuVM()
        {
            DBService dBService = new();
        }

        public void SendCom(InterProcessComs com)
        {
            using (NamedPipeClientStream client = new(".", "efto", PipeDirection.Out))
            {
                client.Connect(2000);
                byte[] commandBytes = Encoding.UTF8.GetBytes(com.ToString());
                client.Write(commandBytes, 0, commandBytes.Length);
            }
        }
    }
}
