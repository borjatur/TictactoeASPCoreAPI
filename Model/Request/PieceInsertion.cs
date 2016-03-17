using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe.Model
{
    public class PieceInsertion
    {
        public string name { get; set; }
        public string gameId { get; set; }
        public Position position { get; set; }
        
        public bool isValidPieceInsertion() {
            return (!String.IsNullOrEmpty(name)
                    && !String.IsNullOrEmpty(gameId)
                    && position.isValidPosition());
        }
    }
}
