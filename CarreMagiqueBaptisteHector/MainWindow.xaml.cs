using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CarreMagiqueBaptisteHector
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculButton_Click(object sender, RoutedEventArgs e)
        {
            int nbrOrdre = 0;
            try
            {
                nbrOrdre = Convert.ToInt32(OrdreTxt.Text);
            }
            catch (FormatException)
            {
                OrdreTxt.Text = "";
                MessageBox.Show("Please, choose an odd value.");
                return;
            }

            if (nbrOrdre % 2 == 0)
            {
                OrdreTxt.Text = "";
                MessageBox.Show("Please, choose an odd value.");
                return;
            }

            int nbrConstante = (nbrOrdre * ((nbrOrdre * nbrOrdre) + 1)) / 2;
            int nbrHigher = nbrOrdre * nbrOrdre;

            ConstanteTxt.Text = nbrConstante.ToString();
            HigherTxt.Text = nbrHigher.ToString();
            panel.Children.Clear();
            SquareGeneration(nbrConstante, nbrOrdre, nbrHigher);
            SolveSquare(nbrOrdre, nbrHigher);
        }

        private void SolveSquare(int nbrOrdre, int nbrHigher)
        {
            int i = 1;
            int x = 0;
            int y = nbrOrdre / 2;
            while (i <= nbrHigher)
            {
                while (!changeItem(x, y, i, nbrHigher))
                {
                    if (x == nbrOrdre - 2)
                        x = 0;
                    else if (x == nbrOrdre - 1)
                        x = 1;
                    else
                        x = x + 2;
                    if (y == 0)
                        y = nbrOrdre - 1;
                    else y = y - 1;
                }
                if (x == 0)
                    x = nbrOrdre - 1;
                else
                    x = x - 1;
                if (y == nbrOrdre - 1)
                    y = 0;
                else
                    y = y + 1;
                i++;
            }
        }

        private bool changeItem(int x, int y, int value, int nbrHigher)
        {
            foreach (var elem in this.panel.Children)
            {
                if (((TextBox)elem).Text == $"{x}, {y}")
                {
                    ((TextBox)elem).Text = value.ToString();
                    if (value == 1)
                        ((TextBox)elem).Background = Brushes.Blue;
                    else if (value == nbrHigher)
                        ((TextBox)elem).Background = Brushes.Green;
                    return true;
                }
            }
            return false;
        }

        private void SquareGeneration(int nbrConstante, int nbrOrdre, int nbrHigher)
        {
            int i = 0;
            int id = 0;
            while (i < nbrOrdre)
            {
                int j = 0;
                while (j < nbrOrdre)
                {
                    CreateTextBox(id, i, j);
                    id++;
                    j++;
                }
                i++;
            }
        }

        private void CreateTextBox(int id, int i, int j)
        {
            if (i == 0)
            {
                ColumnDefinition col = new ColumnDefinition();
                this.panel.ColumnDefinitions.Add(col);
            }
            if (j == 0)
            {
                RowDefinition row = new RowDefinition();
                this.panel.RowDefinitions.Add(row);
            }
            TextBox textBox = new TextBox();
            textBox.TabIndex = id;
            textBox.IsEnabled = false;
            textBox.Width = 30;
            textBox.Height = 20;
            textBox.TextAlignment = TextAlignment.Center;
            textBox.Text = $"{i}, {j}";
            Grid.SetColumn(textBox, j);
            Grid.SetRow(textBox, i);
            this.panel.Children.Add(textBox);
        }
    }
}
