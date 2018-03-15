using NovaSeqLimsTool.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovaSeqLimsTool.ViewModels
{
    public class GetRecipeViewModel : ViewModelBase
    {
        #region Fields
        private string _recipeDto;
        private string _flowCellId;
        private string _librarySerialNumber;
        #endregion

        #region Properties
        public string RecipeDto
        {
            get => _recipeDto;
            set => SetProperty(ref _recipeDto, value);
        }
        public string FlowCellId
        {
            get => _flowCellId;
            set => SetProperty(ref _flowCellId, value);
        }
        public string LibrarySerialNumber
        {
            get => _librarySerialNumber;
            set => SetProperty(ref _librarySerialNumber, value);
        }
        #endregion

        #region ICommands
        public ICommand GetRecipe => new AsyncCommand(GetRecipeDto, () => true);
        #endregion

        #region Constructor
        public GetRecipeViewModel(LimsService state) : base(state)
        {
        }
        #endregion

        #region Methods
        private async Task GetRecipeDto()
        {
            try
            {
                RecipeDto = await _service.GetRecipeDto(FlowCellId, LibrarySerialNumber);
            }
            catch (Exception e)
            {
                _service.ErrorMessage = e.Message;
            }
        }
        #endregion
    }
}
