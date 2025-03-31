using efto_model.Services;

namespace efto_menu.ViewModels
{
    public class MenuVM : BaseVM
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
    }
}
