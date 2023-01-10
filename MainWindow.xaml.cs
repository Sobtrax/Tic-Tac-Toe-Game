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

namespace Tic_Tac_Toe_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MarkType[] mResults;

        private bool mPlayer1Turn;

        private bool mGameEnded;
        public MainWindow()
        {
            InitializeComponent();
            newGame();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                newGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);
            // if button was pressed before in the current round, return
            if (mResults[index] != MarkType.Free)
                return;

            if (mPlayer1Turn)
            {
                mResults[index] = MarkType.Cross;
                button.Content = "X";
            }
            else
            {
                button.Foreground = Brushes.Red;
                mResults[index] = MarkType.Nought;
                button.Content="O";
            }

            // switch players turn
            mPlayer1Turn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            // check for winner by comparing values in rows, column and diagonals
            #region vertical wins
            //Column 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //Column 2
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //Column 3
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region horizontal wins
            //Row 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //Row 2
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //Row 3
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion
            #region Diagonal wins
            // Upper Left to Bottom Right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            // Bottom Left to Upper Right
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[4] & mResults[2]) == mResults[6])
            {
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion
            //when noone wins
            if (mResults.All(x => x != MarkType.Free))
            {
                mGameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
                return;
            }
        }

        //clear all buttons on new game start and set playerturn to cross
        void newGame()
        {
            mResults = new MarkType[9];
            for (int i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }
            mPlayer1Turn = true;
            mGameEnded = false;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
        }
    }
}
