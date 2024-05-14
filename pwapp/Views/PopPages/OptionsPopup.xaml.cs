using CommunityToolkit.Maui.Views;
using PWApp.ViewModels;

namespace PWApp.Views.PopPages;

public partial class OptionsPopup : Popup
{
	private bool _searchGenerated;
	public OptionsPopup(bool searchGenerated = true)
	{
		_searchGenerated = searchGenerated;
		InitializeComponent();
        SetUpChoice();
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		var radioButton = sender as RadioButton;
        if(_searchGenerated)
        {
            CardsPageViewModel.GeneratedSearchOption = radioButton.Content.ToString();
        }
        else
        {
           CardsPageViewModel.ScannedSearchOption = radioButton.Content.ToString();
        }
       
    }

    private void SetUpChoice()
    {
        string filter = _searchGenerated ? CardsPageViewModel.GeneratedSearchOption : CardsPageViewModel.ScannedSearchOption;
        foreach (var child in layout.Children)
        {
            if (child is RadioButton radioButton)
            {
                radioButton.IsChecked = radioButton.Content.ToString().Equals(filter);
            }
        }
    }
}