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

        _firstSelectionContainer = new SelectionContainer() { Values = loadResult.Value.Item1 };
        _secondSelectionContainer = new SelectionContainer() { Values = loadResult.Value.Item2 };
        _linearRegressionContainer = new LinearRegressionContainer { FirstSelection = _firstSelectionContainer, SecondSelection = _secondSelectionContainer };

        SelectionComboBox.SelectedIndex = (int)SelectionNumber.None;

        _twoSelectionsTab!.VisualizeSelections(_linearRegressionContainer);
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
