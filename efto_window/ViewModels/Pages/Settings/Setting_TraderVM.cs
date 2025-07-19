using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Repositories.Base;
using efto_window.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace efto_window.ViewModels.Pages.Settings
{
    public class Setting_TraderVM : PageVM
    {
        private Trader_Repo traderRepo = new();

        #region Variables & Properties
        private ObservableCollection<Trader> traders = new();
        public ObservableCollection<Trader> Traders
        {
            get { return this.traders; }
            private set
            {
                this.traders = value;
                OnPropertyChanged(nameof(this.Traders));
                OnPropertyChanged(nameof(this.AnyTraders));
            }
        }
        public bool AnyTraders
        {
            get { return this.Traders.Any(); }
        }

        private Trader selectedTrader = new();
        public Trader SelectedTrader
        {
            get {  return this.selectedTrader; }
            set
            {
                this.selectedTrader = value;
                OnPropertyChanged(nameof(this.SelectedTrader));
                _ = UpdateTraderDateAsync();
            }
        }

        public string TraderDateFeedback { get; set; } = string.Empty;

        public string NewTraderName { get; set; } = string.Empty;
        #endregion

        public Setting_TraderVM() => _ = LoadTradersAsync();

        private async Task LoadTradersAsync()
        {
            this.Traders = await traderRepo.LoadAllAsync();

            if (this.Traders.Any())
            {
                this.SelectedTrader = this.Traders.FirstOrDefault();
            }
            else
            {
                this.SelectedTrader = new();
            }
        }

        private async Task UpdateTraderDateAsync()
        {
            if (this.SelectedTrader != null)
            {
                this.TraderDateFeedback = await ImageService.GetImageDateFeedback(ImageFolders.Traders, this.SelectedTrader.Name);
                OnPropertyChanged(nameof(this.TraderDateFeedback));
            }
        }

        internal async Task UpdateAsync()
        {
            if (this.SelectedTrader != null)
            {
                await this.traderRepo.UpdateAsync(this.SelectedTrader);

                Trader? old = this.Traders.FirstOrDefault(sorting => sorting.Id == this.SelectedTrader.Id);

                if (old != null)
                {
                    int index = this.Traders.IndexOf(old);

                    this.Traders[index] = await this.traderRepo.LoadSingleAsync(old.Id);
                    this.SelectedTrader = this.Traders[index];
                }
            }
        }

        internal async Task AddAsync()
        {
            if (!string.IsNullOrEmpty(this.NewTraderName))
            {
                await traderRepo.AddAsync(new(this.SelectedTrader.Name));

                this.NewTraderName = string.Empty;
                OnPropertyChanged(nameof(this.NewTraderName));

                this.Traders.Add(await traderRepo.LoadLastAsync());

                if (this.Traders.Count == 1)
                {
                    this.SelectedTrader = this.Traders.FirstOrDefault();
                    OnPropertyChanged(nameof(this.AnyTraders));
                }
            }
        }

        internal async Task RemoveAsync()
        {
            if (this.SelectedTrader != null)
            {
                await traderRepo.DeleteAsync(this.SelectedTrader.Id);
                _ = LoadTradersAsync();
            }
        }
    }
}
