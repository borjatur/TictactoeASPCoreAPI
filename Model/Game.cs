using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tictactoe.Model;

namespace tictactoe.Model
{
    public class Game
    {
        public string _id;
        public string _player1;
        public string _player2;
        public Board _board;
        public int _turn;
        public bool _active;
        public bool _winner;
        
        public Game(string id, string player1)
        {
            _id = id;
            _player1 = player1;
            _player2 = null;
            _board = new Board();
            _turn = 1;
            _active = false;
            _winner = false;
        }
        
        public string setSecondPlayer(string player) {
            if (_player1 != player) {
                _player2 = player;
                _active = true;
                return _id;
            }
            else {
                throw new Exception($"Player {player} already exists in this game.");
            }
        }
        
        public bool isGameActive() {
            return _active;
        }
        
        public string getId() {
            return _id;
        }
        
        public char getPieceOfPlayer(string player) {
            char piece;
            if(player == _player1) {
                piece = 'O';
            }
            else if(player == _player2) {
                piece = 'X';
            }
            else {
                throw new Exception($"Player {player} doesn't exists in this game");
            }
            return piece;
        }
        
        public int getNumberOfPiecesOfPlayer(string player) {
            char piece = getPieceOfPlayer(player);
            return _board.countPieces(piece);
        }
        
        public string playerToPlay() {
            string player = "";
            switch (_turn % 2)
            {
                case 0:
                    player = _player2;
                    break;
                case 1:
                    player = _player1;
                    break;
            }
            return player;
        }
        
        public void nextTurn() {
            _turn++;
        }
        
        public char getNextPieceToMove() {
            char piece = ' ';
            switch (_turn % 2)
            {
                case 0:
                    piece = 'X';
                    break;
                case 1:
                    piece = 'O';
                    break;
            }
            return piece;
        }
        
        public void putPiece(string player, Position position) {
            if(_turn < 7) {
                if(player == playerToPlay()) {
                    char piece = getNextPieceToMove();
                    _board.putPiece(piece,position);
                    if(_board.isWinningMove(piece)) {
                        _winner = true;
                    }
                    else {
                        nextTurn();
                    }
                }
                else {
                    throw new Exception($"It's not the {player} turn.");
                }
            }
            else {
                throw new Exception("Player's can't put more pieces in the board it's turn to move.");
            }
        }
        
        public void movePiece(string player, Position initial, Position final) {
            if(_turn > 6) {
                if(player == playerToPlay()) {
                    char piece = getNextPieceToMove();
                    _board.movePiece(piece, initial, final);
                    if(_board.isWinningMove(piece)) {
                        _winner = true;
                    }
                    else {
                        nextTurn();
                    }
                }
                else {
                    throw new Exception($"It's not the player {player} turn.");
                }
            }
            else {
                throw new Exception("Players can't move pieces yet.");
            }
        }
    }
}
