using efto_model.Models.Quests;
using efto_model.Repositories.Quests;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace efto_window.ViewModels.Pages.Settings
{
    public class Setting_Quest_Reward_CategoryVM : PageVM
    {
        private Quest_Reward_Category_Repo categoryRepo = new();

        #region Variables & Properties
        private ObservableCollection<Quest_Reward_Category> categories = new();
        public ObservableCollection<Quest_Reward_Category> Categories
        {
            get { return this.categories; }
            private set
            {
                this.categories = value;
                OnPropertyChanged(nameof(this.Categories));
                OnPropertyChanged(nameof(this.AnyCategories));
            }
        }
        public bool AnyCategories
        {
            get { return this.Categories.Any(); }
        }

        private Quest_Reward_Category selectedCateogry = new();
        public Quest_Reward_Category SelectedCategory
        {
            get { return this.selectedCateogry; }
            set
            {
                this.selectedCateogry = value;
                OnPropertyChanged(nameof(this.SelectedCategory));
            }
        }

        public string NewRewardCategory { get; set; } = string.Empty;
        #endregion

        public Setting_Quest_Reward_CategoryVM() => _ = LoadCategoriesAsync();

        private async Task LoadCategoriesAsync()
        {
            this.Categories = await categoryRepo.LoadAllAsync();

            if (this.Categories.Any())
            {
                this.SelectedCategory = this.Categories.FirstOrDefault();
            }
            else
            {
                this.SelectedCategory = new();
            }
        }

        internal async Task UpdateAsync()
        {
            if (this.SelectedCategory != null)
            {
                await this.categoryRepo.UpdateAsync(this.SelectedCategory);

                Quest_Reward_Category? old = this.Categories.FirstOrDefault(sorting => sorting.Id == this.SelectedCategory.Id);

                if (old != null)
                {
                    int index = this.Categories.IndexOf(old);

                    this.Categories[index] = await this.categoryRepo.LoadSingleAsync(old.Id);
                    this.SelectedCategory = this.Categories[index];
                }
            }
        }

        internal async Task AddAsync()
        {
            if (!string.IsNullOrWhiteSpace(this.NewRewardCategory))
            {
                await categoryRepo.AddAsync(new(this.NewRewardCategory));

                this.NewRewardCategory = string.Empty;
                OnPropertyChanged(nameof(this.NewRewardCategory));

                this.Categories.Add(await categoryRepo.LoadLastAsync());

                if (this.Categories.Count == 1)
                {
                    this.SelectedCategory = this.Categories.FirstOrDefault();
                    OnPropertyChanged(nameof(this.AnyCategories));
                }
            }
        }

        internal async Task RemoveAsync()
        {
            if (this.SelectedCategory != null)
            {
                await categoryRepo.DeleteAsync(this.SelectedCategory.Id);
                _ = LoadCategoriesAsync();
            }
        }
    }
}
