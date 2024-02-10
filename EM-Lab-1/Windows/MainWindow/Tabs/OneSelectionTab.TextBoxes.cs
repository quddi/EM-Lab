using System.Windows.Controls;

namespace EM_Lab_1
{
    public partial class OneSelectionTab
    {
        private TextBox? _meanTextBox;
        private TextBox? _meanIntervalTextBox;
        private TextBox? _medianTextBox;
        private TextBox? _medianIntervalTextBox;
        private TextBox? _standardDeviationTextBox;
        private TextBox? _standardDeviationIntervalTextBox;
        private TextBox? _skewnessTextBox;
        private TextBox? _skewnessIntervalTextBox;
        private TextBox? _kurtosisTextBox;
        private TextBox? _kurtosisIntervalTextBox;

        public OneSelectionTab SetMeanTextBoxes(TextBox meanTextBox, TextBox meanIntervalTextBox)
        {
            _meanTextBox = meanTextBox;
            _meanIntervalTextBox = meanIntervalTextBox;

            return this;
        }

        public OneSelectionTab SetMedianTextBoxes(TextBox medianTextBox, TextBox medianIntervalTextBox)
        {
            _medianTextBox = medianTextBox;
            _medianIntervalTextBox = medianIntervalTextBox;

            return this;
        }

        public OneSelectionTab SetStandardDeviationTextBoxes(TextBox standardDeviationTextBox, TextBox standardDeviationIntervalTextBox)
        {
            _standardDeviationTextBox = standardDeviationTextBox;
            _standardDeviationIntervalTextBox = standardDeviationIntervalTextBox;

            return this;
        }

        public OneSelectionTab SetSkewnessTextBoxes(TextBox skewnessTextBox, TextBox skewnessIntervalTextBox)
        {
            _skewnessTextBox = skewnessTextBox;
            _skewnessIntervalTextBox = skewnessIntervalTextBox;

            return this;
        }

        public OneSelectionTab SetKurtosisTextBoxes(TextBox kurtosisTextBox, TextBox kurtosisIntervalTextBox)
        {
            _kurtosisTextBox = kurtosisTextBox;
            _kurtosisIntervalTextBox = kurtosisIntervalTextBox;

            return this;
        }
    }
}
