using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;

namespace EM_Lab_1
{
    public static class DataLoader
    {
        public static (List<double>, List<double>)? LoadValues()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;

                try
                {
                    return ReadNumbersFromFile(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при читанні файлу: {ex.Message}");
                }
            }

            return null;
        }

        private static (List<double>, List<double>) ReadNumbersFromFile(string filePath)
        {
            var firstSelection = new List<double>();
            var secondSelection = new List<double>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] tokens = line
                        .Replace('.', ',')
                        .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (tokens.Length != 2)
                        MessageBox.Show($"Помилка при зчитуванні рядка: {line}");

                    if (double.TryParse(tokens[0], out double firstValue)) 
                        firstSelection.Add(firstValue);
                    else
                        MessageBox.Show($"Помилка при зчитуванні числа: {tokens[0]}");

                    if (double.TryParse(tokens[1], out double secondValue))
                        secondSelection.Add(secondValue);
                    else
                        MessageBox.Show($"Помилка при зчитуванні числа: {tokens[1]}");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при зчитуванні файлу: {ex.Message}");
            }

            return (firstSelection, secondSelection);
        }
    }
}
