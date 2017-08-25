using DartsGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DartsGame
{
    #region rules
    // Load Application
    // Ask the user what is the winning threshhold - DONE
    // Ask user for players names
    // Assign values to variables

    // If option 1 ask for how many rounds - DONE
    // Assign value to variable
    // If option 2 ask for max points - DONE
    // Assign value to variable

    // Display start game button

    // Player hts start game.
    // Create game object based on available data
    // Call start game

    // Questions:
    // 1. Are we going to get the constant max and min values from the DartGame object to use for webpage messages of what is allowed and validation of user input?
    // 2. What are we making required?  Seems like just Type of Game and and players?
    // 3. Do we have validation on number of players?
    // 4. Are we going to dynamically load the dropdown with the values from the TypeOfGame enum?
    #endregion
    public partial class Darts : System.Web.UI.Page
    {
        private const int WINNING_ROUND_DDL_INDEX = 1;
        private const int WINNING_POINTS_DDL_INDEX = 2;
        private const string NoWinnerText = "No Winner found.";
        private const string TieFormatting = " & ";
        private const string MIN_PLAYER_ERROR = "At least 1 player must be entered to play.";
        public List<ResultsViewModel> Results;

        protected void Page_Load(object sender, EventArgs e)
        {
            Results = new List<ResultsViewModel>();
        }

        protected void ddlWinningThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide or show the textboxes to enter in the values based on user end game selection
            // Hide both boxes if it is not a MaxRound or MaxPoint selection
            divEnterMaxData.Visible = true;
            var ddl = sender as DropDownList;
            switch (ddl.SelectedIndex)
            {
                case WINNING_ROUND_DDL_INDEX://MaxRounds
                    divMaxRounds.Visible = true;
                    divMaxPoints.Visible = false;
                    break;
                case WINNING_POINTS_DDL_INDEX://MaxPoints
                    divMaxPoints.Visible = true;
                    divMaxRounds.Visible = false;
                    break;
                default:
                    divEnterMaxData.Visible = false;
                    break;
            }
        }

        protected void btnStartGame_Click(object sender, EventArgs e)
        {
            try
            {
                divErrors.Visible = false;
                PlayGame();
            }
            catch (Exception ex)
            {
                // TODO: IMPLEMENT BETTER EXCEPTION HANDLING
                divErrors.Visible = true;
                lblErrorInfo.Text = ex.Message;
            }
        }

        private void PlayGame()
        {
            // Get the list of player names entered
            // If there is text, create a new player object and set its Name property
            List<Player> players = new List<Player>();
            List<TextBox> playerNames = new List<TextBox>() { txtPlayer1, txtPlayer2, txtPlayer3, txtPlayer4 };
            playerNames.ForEach(pn => { if (!string.IsNullOrWhiteSpace(pn.Text)) players.Add(new Player(pn.Text)); });

            // Throw an error if no players have been set
            if (players.Count == 0)
                throw new Exception(MIN_PLAYER_ERROR);

            // Get the gametype selected
            TypeOfGame gameType = ddlWinningThreshold.SelectedIndex == WINNING_ROUND_DDL_INDEX ? TypeOfGame.High_Score_At_End_Of_Fixed_Rounds : TypeOfGame.High_Score_Beats_Max_Score;

            // Create the game object now that we have players and gameType
            DartGame game = new DartGame(players, gameType);

            // Set max rounds or points if user entered valid data, else use the game's default
            int userMax;
            if (gameType == TypeOfGame.High_Score_At_End_Of_Fixed_Rounds)
            {
                if (!IsValueGreaterThanZero(txtMaxRounds.Text, out userMax))
                    game.SetDefaultRounds();
                else
                    game.WinningRound = userMax;
            }
            else
            {
                if (!IsValueGreaterThanZero(txtMaxPoints.Text, out userMax))
                    game.SetDefaultScore();
                else
                    game.WinningScore = userMax;
            }

            // Start the game
            game.Start();

            // Display the winner's name and handle if there is no winner or there was a tie
            List<Player> winners = game.Players.Where(p => p.IsWinner).ToList();
            StringBuilder sb = new StringBuilder();
            lblWinnerName.Text = (winners.Count == 0) ? NoWinnerText
                : (winners.Count == 1) ? winners[0].Name
                : ConcatWinnerNames(winners);

            // Populate results viewmodel to display on UI
            foreach (Player p in game.Players)
            {
                ResultsViewModel resultsModel = new ResultsViewModel() { Player = p };
                List<DartScoreViewModel> dartScoreModels = new List<DartScoreViewModel>();
                for (int i = 0; i < p.DartThrowScores.Count; i++)
                {
                    dartScoreModels.Add(new DartScoreViewModel()
                    {
                        Throw = i + 1,
                        Points = p.DartThrowScores[i].ThrowPoints,
                        BoardPosition = p.DartThrowScores[i].ToString()
                    });
                }
                resultsModel.DartScoreViewModels = dartScoreModels;
                Results.Add(resultsModel);
            }

            // Show Results
            divGameSetup.Visible = false;
            divGameResults.Visible = true;
        }

        /// <summary>
        /// Method to properly format the Winner text in case of a tie
        /// </summary>
        /// <param name="winners">List of player objects that contain the game winners</param>
        /// <returns>Formatted string of the winner names with ampersands between each name.</returns>
        private string ConcatWinnerNames(List<Player> winners)
        {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            foreach (Player p in winners)
            {
                if (counter > 0)
                    sb.Append(TieFormatting);
                sb.Append(p.Name);
            }
            return sb.ToString();
        }

        private bool IsValueGreaterThanZero(string text, out int max)
        {
            // If the text value has a number and it is greater than 0, return true
            return (int.TryParse(text, out max) && max > 0) ? true : false;
        }

        protected void btnPlayAgain_Click(object sender, EventArgs e)
        {
            // Restart the game with the previously inputted data
            PlayGame();
        }

        protected void btnStartOver_Click(object sender, EventArgs e)
        {
            // Set Page back to default game setup view
            divGameResults.Visible = false;
            divGameSetup.Visible = true;
            divEnterMaxData.Visible = false;

            // Clear previous data
            ddlWinningThreshold.SelectedIndex = 0;
            txtMaxPoints.Text = string.Empty;
            txtMaxRounds.Text = string.Empty;
            txtPlayer1.Text = string.Empty;
            txtPlayer2.Text = string.Empty;
            txtPlayer3.Text = string.Empty;
            txtPlayer4.Text = string.Empty;
        }
    }
}