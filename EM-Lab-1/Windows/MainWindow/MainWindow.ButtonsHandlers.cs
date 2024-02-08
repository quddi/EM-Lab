using System.Windows;

namespace EM_Lab_1
{
    public partial class MainWindow
    {
        private void LoadDataButtonClickHandler(object _, RoutedEventArgs __)
        {
            var loadResult = DataLoader.LoadValues();

            if (loadResult == null)
                return;

            _firstSelectionContainer = new SelectionContainer() { Datas = loadResult.Value.Item1 };
            _secondSelectionContainer = new SelectionContainer() { Datas = loadResult.Value.Item2 };
        }
    }
}
