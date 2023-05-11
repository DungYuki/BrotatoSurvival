using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts
{
    public class ScoreElement : IComparable<ScoreElement>
    {
        public string PlayerName;
        public int Score;

        public ScoreElement(string PlayerName, int Score)
        {
            this.PlayerName = PlayerName;
            this.Score = Score;
        }

        public int CompareTo(ScoreElement other)
        {
            return other.Score.CompareTo(this.Score);
        }
    }
}
