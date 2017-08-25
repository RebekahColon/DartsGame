using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DartsGame
{
    //TODO (Yaniv)- Initialize and Reset Player Sore
    //When the player is initialized the score is automatically 
    //zero because it is an int. What happens if we restart the 
    //game? Who will reset the value?
    public class Player
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        //private int _score;
        public int Score
        {
            //get
            //{
            //    return _score;
            //}

            //set
            //{
            //    if (value < 0)
            //        _score = 0;
            //    else
            //        _score = value;  // Should we be testing anything else here?
            //}

            get
            {
                return DartThrowScores.Sum(ds => ds.ThrowPoints);
            }
        }

        public bool IsWinner { get; set; }

        public List<DartScore> DartThrowScores { get; set; }

        public Player(string name)
        {
            _name = name;
            DartThrowScores = new List<DartScore>();
        }

        public void Throw()
        {
            DartThrowScores.Add(Dart.Throw());
        }
    }
}