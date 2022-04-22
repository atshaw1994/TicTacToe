using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game newGame;
        public MainWindow()
        {
            InitializeComponent();
            newGame = new Game();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Win_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (Button gameButton in FindVisualChildren<Button>(this))
                gameButton.Content = " ";
            newGame = new Game();
            WinBorder.Visibility = Visibility.Collapsed;
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button ClickedButton && !ClickedButton.Content.Equals(newGame.WhichTurn))
            {
                ClickedButton.Content = newGame.WhichTurn;
                if (CheckWin())
                {
                    WinDisplay.Text = $"{newGame.WhichTurn} WINS!";
                    WinBorder.Visibility = Visibility.Visible;
                }
                else if (AllButtonsClicked() && !CheckWin())
                {
                    WinDisplay.Text = $"DRAW!";
                    WinBorder.Visibility = Visibility.Visible;
                }
                newGame.NextTurn();
                TurnDisplay.Text = $"{newGame.WhichTurn}'s Turn";
            }
        }

        private bool CheckWin()
        {
            // Check for X
            // Check Horizontals
            if (R0C0.Content.Equals("X") && R0C1.Content.Equals("X") && R0C2.Content.Equals("X")) return true;
            if (R1C0.Content.Equals("X") && R1C1.Content.Equals("X") && R1C2.Content.Equals("X")) return true;
            if (R2C0.Content.Equals("X") && R2C1.Content.Equals("X") && R2C2.Content.Equals("X")) return true;
            // Check Verticals
            if (R0C0.Content.Equals("X") && R1C0.Content.Equals("X") && R2C0.Content.Equals("X")) return true;
            if (R0C1.Content.Equals("X") && R1C1.Content.Equals("X") && R2C1.Content.Equals("X")) return true;
            if (R0C2.Content.Equals("X") && R1C2.Content.Equals("X") && R2C2.Content.Equals("X")) return true;
            // Check Diagonals
            if (R0C0.Content.Equals("X") && R1C1.Content.Equals("X") && R2C2.Content.Equals("X")) return true;
            if (R0C2.Content.Equals("X") && R1C1.Content.Equals("X") &&R2C0.Content.Equals("X")) return true;
            // Check for Y
            // Check Horizontals
            if (R0C0.Content.Equals("O") && R0C1.Content.Equals("O") && R0C2.Content.Equals("O")) return true;
            if (R1C0.Content.Equals("O") && R1C1.Content.Equals("O") && R1C2.Content.Equals("O")) return true;
            if (R2C0.Content.Equals("O") && R2C1.Content.Equals("O") && R2C2.Content.Equals("O")) return true;
            // Check Verticals
            if (R0C0.Content.Equals("O") && R1C0.Content.Equals("O") && R2C0.Content.Equals("O")) return true;
            if (R0C1.Content.Equals("O") && R1C1.Content.Equals("O") && R2C1.Content.Equals("O")) return true;
            if (R0C2.Content.Equals("O") && R1C2.Content.Equals("O") && R2C2.Content.Equals("O")) return true;
            // Check Diagonals
            if (R0C0.Content.Equals("O") && R1C1.Content.Equals("O") && R2C2.Content.Equals("O")) return true;
            if (R0C2.Content.Equals("O") && R1C1.Content.Equals("O") && R2C0.Content.Equals("O")) return true;
            return false;
        }

        private bool AllButtonsClicked()
        {
            foreach (Button gameButton in FindVisualChildren<Button>(this))
                if (gameButton.Content.Equals(" ")) return false;
            return true;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }
    }

    internal class Game
    {
        private readonly MainWindow GameWindow;
        public string WhichTurn;
        public Game()
        {
            GameWindow = Application.Current.MainWindow is MainWindow mainWindow ? mainWindow : new MainWindow();

            GameWindow.TurnDisplay.Text = "X's Turn";
            WhichTurn = "X";
        }

        public void NextTurn() => WhichTurn = WhichTurn.Equals("X") ? "O" : "X";

    }
}
