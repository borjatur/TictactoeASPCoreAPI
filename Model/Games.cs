using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using tictactoe.Model;

namespace tictactoe.Model
{
    public class Games
    {
        private List<Game> games;
        public Games()
        {
            games = new List<Game>();
        }
        
        private string generatePseudoRandomId() {
            return Guid.NewGuid().ToString();
        }
        
        private string newGame(string player) {
            Game game = new Game(generatePseudoRandomId(),player);
            games.Add(game);
            return game.getId();
        }
        
        private string joinGame(string player) {
            Game game = games.Last();
            return game.setSecondPlayer(player);
        }
        
        public string play(string player) {
            string gameId = "";
            if(games.Any()) {
                Game game = games.Last();
                if(game.isGameActive()) {
                    gameId = newGame(player);
                }
                else {
                    gameId = joinGame(player);
                }
            }
            else {
                gameId = newGame(player);
            }
            return gameId;
        }
        
        public Game searchGame(string gameId) {
             return games.Find(g => g.getId().Equals(gameId));
        }
        
        public void putPiece(string gameId, string player, Position position) {
            Game game = searchGame(gameId);
            if(game != null) {
                game.putPiece(player, position);
            }
            else {
                throw new Exception("Game not found");
            }
        }
        
        public void movePiece(string gameId, string player, Position initial, Position final) {
            Game game = searchGame(gameId);
            if(game != null) {
                game.movePiece(player,initial,final);
            }
            else {
                throw new Exception("Game not found");
            }
        }
    }
}
