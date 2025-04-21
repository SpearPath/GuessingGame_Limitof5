using System;
using System.Drawing;
using System.Windows.Forms;

namespace GuessingGameL5
{
    public partial class Form1 : Form
    {
        // Game variables
        private int targetNumber;
        private int guessCount;
        private readonly int maxGuesses = 5;
        private readonly Color higherColor = Color.LightCoral;
        private readonly Color lowerColor = Color.LightBlue;
        private readonly Color neutralColor = SystemColors.Control;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            Random r = new Random();
            targetNumber = r.Next(0, 101); // Random number 0-100
            guessCount = 0;

            // Reset UI
            lblHint.Text = "Guess a number between 0 and 100!";
            lblHint.TextAlign = ContentAlignment.MiddleCenter;
            lblGuessCount.Text = $"Guesses: {guessCount}/{maxGuesses}";
            this.BackColor = neutralColor;
            txtGuess.Text = "";
            btnGuess.Enabled = true;
            btnNewGame.Visible = false;
            txtGuess.Focus();
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtGuess.Text, out int userGuess))
            {
                guessCount++;
                lblGuessCount.Text = $"Guesses: {guessCount}/{maxGuesses}";

                if (userGuess == targetNumber)
                {
                    lblHint.Text = $"Correct! You guessed it in {guessCount} tries!";
                    this.BackColor = Color.LightGreen;
                    btnGuess.Enabled = false;
                    btnNewGame.Visible = true;
                }
                else if (guessCount >= maxGuesses)
                {
                    lblHint.Text = $"Game over! The number was {targetNumber}.";
                    this.BackColor = Color.LightGray;
                    btnGuess.Enabled = false;
                    btnNewGame.Visible = true;
                }
                else if (userGuess < targetNumber)
                {
                    lblHint.Text = "Higher! Try again.";
                    this.BackColor = lowerColor;
                }
                else
                {
                    lblHint.Text = "Lower! Try again.";
                    this.BackColor = higherColor;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number between 0 and 100.", "Invalid Input");
            }

            txtGuess.Text = "";
            txtGuess.Focus();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void txtGuess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
