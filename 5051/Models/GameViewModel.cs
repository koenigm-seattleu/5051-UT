using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the Game Views to have the list of Games
    /// </summary>
    public class GameViewModel
    {
        /// <summary>
        /// The List of Games
        /// </summary>
        public List<GameModel> GameList = new List<GameModel>();
        public int ListLevel;
    }

    /// <summary>
    /// Adds a list of Game Lists per Level, making it easier to select
    /// </summary>
    public class GameListViewModel : GameViewModel
    {
        public List<GameViewModel> GameLevelList;
        public int MaxLevel;
    }

    /// <summary>
    /// Returns the selected Game and the Game List
    /// </summary>
    public class SelectedGameViewModel : GameListViewModel
    {
        public GameModel SelectedGame;
    }

    /// <summary>
    /// Adds the Student Information to the View Model for the Games availble for the student to select
    /// </summary>
    public class SelectedGameForStudentViewModel : SelectedGameViewModel
    {
        public StudentModel Student;
    }
}