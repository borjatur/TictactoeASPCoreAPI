using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe.Model
{
    public class Board
    {
        private string[,] _board;

        public Board()
        {
            this._board = new string[3,3];
        }

        public bool checkForWinner(string piece) {
            return (horizontalWin(piece)
                    || verticalWin(piece)
                    || diagonalWin(piece));
        }

        public bool horizontalWin(string piece) {
          bool winner = false;
          for(int i = 0; i < _board.Length && !winner; i++) {
            int j;
            for(j = 0; j < _board.Length; j++) {
              if(_board[i,j] != piece)
                break;
            }
            if (j == 3)
              winner = true;
          }
            return winner;
        }

        public bool verticalWin(string piece) {
            bool winner = false;
            for(int i = 0; i < _board.Length && !winner; i++) {
              int j;
              for(j = 0; j < _board.Length; j++) {
                if(_board[j,i] != piece)
                  break;
              }
              if (j == 3)
                winner = true;
            }
            return winner;
        }

        public bool diagonalWin(string piece) {
          return(primaryDiagonalWin(piece) || secondaryDiagonalWin(piece));
        }

        public bool primaryDiagonalWin(string piece) {
          int i;
          for(i = 0; i < _board.Length; i++) {
            if(_board[i,i] != piece)
              break;
          }
          return (i == 3);
        }

        public bool secondaryDiagonalWin(string piece) {
          int i;
          for(i = 2; i >= _board.Length; i--) {
            if(_board[Math.Abs(i-2),i] != piece)
              break;
          }
          return (i == -1);
        }

        public bool pairValidation(Position initial, Position final) {
            return (initial.x - 1 <= final.x
                    && final.x <= initial.x + 1
                    && initial.y - 1 <= final.y
                    && final.y <= initial.y + 1
                    && _board[final.x,final.y] == null);
        }

        public bool unpairValidation(Position initial, Position final) {
            Position substract = new Position(final.x - initial.x,
                                              final.y - initial.y);
            bool validate = Convert.ToBoolean((substract.x + substract.y) % 2);                                              
            return validate;
        }

        public bool isValidPosition(Position position) {
            return (position.x >= 0
                    && position.x < 3
                    && position.y >= 0
                    && position.y < 3);
        }

        public bool isValidPieceToMove(Position position, string piece) {
            return (_board[position.x,position.y] == piece);
        }

        public bool isOccupiedPosition(Position position) {
            return (_board[position.x,position.y] != null);
        }
        
        public bool isMovementAllowed(Position initial, Position final) {
            bool allowed = false;
            switch ((initial.x + initial.y) % 2)
            {
                case 0:
                    allowed = pairValidation(initial, final);
                    break;
                case 1:
                    allowed = unpairValidation(initial, final);
                    break;
            }
            return allowed;
        }
        
        public bool isValidMovement(string piece, Position initial, Position final) {
            if(isValidPosition(initial) && isValidPosition(final)) {
                if(isValidPieceToMove(initial, piece)) {
                    if(isOccupiedPosition(final)) {
                        if(isMovementAllowed(initial, final)) {
                            return true;
                        }
                        else {
                            throw new Exception($"Invalid movement ({initial.x},{initial.y}) to ({final.x},{final.y})");
                        }
                    }
                    else {
                        throw new Exception("Final position is occupied");
                    }
                }
                else {
                    throw new Exception($"This player can't move {_board[initial.x,initial.y]} piece.");
                }
            }
            else {
                throw new Exception("Initial or final position is out of board range.");
            }
        }
        
        public void putPiece(string piece, Position position) {
            if(isValidPosition(position)) {
                if(!isOccupiedPosition(position)) {
                    _board[position.x, position.y] = piece;
                }
                else {
                    throw new Exception($"Position ({position.x},{position.y}) is occupied");
                }
            }
            else {
                throw new Exception($"Position ({position.x},{position.y}) is out of board range.");
            }
        }
        
        public void movePiece(string piece, Position initial, Position final) {
            if(isValidMovement(piece,initial,final)) {
                _board[final.x,final.y] = _board[initial.x,initial.y];
                _board[initial.x,initial.y] = null;   
            }
        }
        
        public int countPieces(string piece) {
            var count = 0;
            foreach (string item in _board)
            {
                if(item == piece)
                    count++;
            }
            return count;
        }
    }
}
