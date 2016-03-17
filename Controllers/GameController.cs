using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using tictactoe.Model;
using tictactoe.Data;
using Newtonsoft.Json;

namespace tictactoe.Controllers
{
    [Route("[controller]")]
    public class GameController : Controller
    {   
        
        // GET game/:id
        [HttpGet("{id}")]
        public JsonResult GetGame(string id)
        {
            if(!String.IsNullOrEmpty(id)) {
                Game game = DataWarehouse.games.searchGame(id);
                if(game != null) {
                    return Json(game);
                }
                else {
                    Response.StatusCode = 404;
                    return Json("Game not found");
                }
            } 
            else {
                Response.StatusCode = 400;
                return Json("You have to send the game id to view the board");
            }
        }
        
        // GET game/:id/number-of-pieces/:player
        [HttpGet("{id}/number-of-pieces/{playerName}")]
        public string GetNumberOfPiecesOfPlayer(string id, string playerName)
        {
            if(!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(playerName)) {
                Game game = DataWarehouse.games.searchGame(id);
                if(game != null) {
                    try {
                        return game.getNumberOfPiecesOfPlayer(playerName).ToString();
                    }
                    catch(Exception e) {
                        return e.Message;
                    }
                }
                else {
                    Response.StatusCode = 404;
                    return "Game not found";
                }
            }
            else {
                Response.StatusCode = 400;
                return "You have to send the game id and the player name.";
            }
        }

        // POST game/play
        [HttpPost("play")]
        public JsonResult Play([FromBody]Player player)
        {
            if(player == null || String.IsNullOrEmpty(player.name)) {
                Response.StatusCode = 400;
                return null;
            }
            else {
                try {
                    string gameId = DataWarehouse.games.play(player.name);
                    Response.StatusCode = 201;
                    return Json(new {gameId = gameId});    
                }
                catch(Exception e) {
                    Response.StatusCode = 400;
                    return Json(e.Message);
                }
            }
        }
        
        // POST game/put-piece
        [HttpPost("put-piece")]
        public JsonResult PutPiece([FromBody]PieceInsertion pieceInsertion)
        {
            if(pieceInsertion != null && pieceInsertion.isValidPieceInsertion()) {
                try {
                    DataWarehouse.games.putPiece(pieceInsertion.gameId,
                                                 pieceInsertion.name,
                                                 pieceInsertion.position);
                                                 
                    Game game = DataWarehouse.games.searchGame(pieceInsertion.gameId);                                                 
                    Response.StatusCode = 201;
                    return Json(game);                                                
                }
                catch(Exception e) {
                    Response.StatusCode = 400;
                    return Json(e.Message);
                }
            }
            else {
                Response.StatusCode = 400;
                return null;   
            }
        }
        
        // POST game/move-piece
        [HttpPost("move-piece")]
        public JsonResult MovePiece([FromBody]PieceMovement pieceMovement)
        {
            if(pieceMovement != null && pieceMovement.isValidPieceMovement()) {
                try {
                    DataWarehouse.games.movePiece(pieceMovement.gameId,
                                                  pieceMovement.name,
                                                  pieceMovement.initialPosition,
                                                  pieceMovement.finalPosition);
                                                  
                    Game game = DataWarehouse.games.searchGame(pieceMovement.gameId);                                                 
                    Response.StatusCode = 201;
                    return Json(game);                                                  
                }
                catch(Exception e) {
                    Response.StatusCode = 400;
                    return Json(e.Message);
                }
            }
            else {
                Response.StatusCode = 400;
                return null;  
            }
        }
    }
}
