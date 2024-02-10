using System.Windows;
using System.Windows.Controls;

namespace EM_Lab_1;

public partial class MainWindow
{
    private void LoadDataButtonClickHandler(object _, RoutedEventArgs __)
    {
        var loadResult = DataLoader.LoadValues();

        if (loadResult == null)
            return;

        _firstSelectionContainer = new SelectionContainer() { Datas = loadResult.Value.Item1 };
        _secondSelectionContainer = new SelectionContainer() { Datas = loadResult.Value.Item2 };

        SelectionComboBox.SelectedIndex = (int)SelectionNumber.None;
    }

    private void SelectionComboBoxSelectionChangedHandler(object _, SelectionChangedEventArgs __)
    {
        var areSomeSelectionsLoaded = _firstSelectionContainer != null && _secondSelectionContainer != null;

        if (!areSomeSelectionsLoaded && SelectionComboBox.SelectedIndex != (int)SelectionNumber.None)
        {
            SelectionComboBox.SelectedIndex = (int)SelectionNumber.None;
            return;
        }

        SetOneSelection((SelectionNumber)SelectionComboBox.SelectedIndex);
    }
}
