using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace tictactoe.Model
{
    public class Board
    {
        public char[,] _board;

        public Board()
        {
            _board = new char[,] {{'\0','\0','\0'},
                                  {'\0','\0','\0'},
                                  {'\0','\0','\0'}};
        }

        public bool isWinningMove(char piece) {
            return (horizontalWin(piece)
                    || verticalWin(piece)
                    || diagonalWin(piece));
        }

        public bool horizontalWin(char piece) {
          bool winner = false;
          for(int i = 0; i < _board.GetLength(0) && !winner; i++) {
            int j;
            for(j = 0; j < _board.GetLength(1); j++) {
              if(_board[i,j] != piece)
                break;
            }
            if (j == 3)
              winner = true;
          }
            return winner;
        }

        public bool verticalWin(char piece) {
            bool winner = false;
            for(int i = 0; i < _board.GetLength(1) && !winner; i++) {
              int j;
              for(j = 0; j < _board.GetLength(0); j++) {
                if(_board[j,i] != piece)
                  break;
              }
              if (j == 3)
                winner = true;
            }
            return winner;
        }

        public bool diagonalWin(char piece) {
          return(primaryDiagonalWin(piece) || secondaryDiagonalWin(piece));
        }

        public bool primaryDiagonalWin(char piece) {
          int i;
          for(i = 0; i < _board.GetLength(0); i++) {
            if(_board[i,i] != piece)
              break;
          }
          return (i == 3);
        }

        public bool secondaryDiagonalWin(char piece) {
          int i;
          for(i = 2; i >= _board.GetLength(0); i--) {
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
                    && _board[final.x,final.y] == '\0');
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

        public bool isValidPieceToMove(Position position, char piece) {
            return (_board[position.x,position.y] == piece);
        }

        public bool isOccupiedPosition(Position position) {
            return (_board[position.x, position.y] != '\0');
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
        
        public bool isValidMovement(char piece, Position initial, Position final) {
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
        
        public void putPiece(char piece, Position position) {
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
        
        public void movePiece(char piece, Position initial, Position final) {
            if(isValidMovement(piece,initial,final)) {
                _board[final.x,final.y] = _board[initial.x,initial.y];
                _board[initial.x,initial.y] = '\0';   
            }
        }
        
        public int countPieces(char piece) {
            var count = 0;
            foreach (char item in _board)
            {
                if(item == piece)
                    count++;
            }
            return count;
        }
    }
}
