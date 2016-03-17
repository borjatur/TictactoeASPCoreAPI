using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe.Model
{
    public class PieceMovement
    {
        public string name { get; set; }
        public string gameId { get; set; }
        public Position initialPosition { get; set; }
        public Position finalPosition { get; set; }
        
        public bool isValidPieceMovement() {
            return (!String.IsNullOrEmpty(name)
                    && !String.IsNullOrEmpty(gameId)
                    && initialPosition.isValidPosition()
                    && finalPosition.isValidPosition());
        }
    }
}