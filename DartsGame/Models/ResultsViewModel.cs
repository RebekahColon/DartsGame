using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DartsGame.Models
{
    public class ResultsViewModel
    {
        public Player Player { get; set; }
        public List<DartScoreViewModel> DartScoreViewModels { get; set; }

        public ResultsViewModel()
        {
            DartScoreViewModels = new List<DartScoreViewModel>();
        }
    }
}