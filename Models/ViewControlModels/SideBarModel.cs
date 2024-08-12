

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Models.ViewControlModels;

public partial class SideBarModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<string> _sideBarItemList;

    [ObservableProperty]
    private string? _sideBarItemSelected;

    public SideBarModel()
    {
        SideBarItemList = new ObservableCollection<string>
        {
            "Agi", // Agility
            "Str", // Strength
            "Ene", // Energy
            "Vit"  // Vitality
        };

    }

}
