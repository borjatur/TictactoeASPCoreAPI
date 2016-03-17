# Web API for play Tictactoe game
## Description

The purpose of the game is that a player who succeeds in placing three of their pieces in a horizontal, vertical, or diagonal row to wins the game.

There are variants of the game. In this implementation the player can move their pieces after placing three of them on the board.

## Getting Started
To test the Web API:
* Type **"dnx web"** in command prompt inside working directory

### API calls
* **Post** call to baseUrl + **/game/play**
  * Body object in JSON format
  ```javascript
  { "name" : <Player name>}
  ```
  * Response
  ```javascript
  { "gameId" : <Random game id>}
  ```
* **Post** call to baseUrl + **/game/put-piece**
  * Body object in JSON format, position represent the place to put a piece in a 3x3 board
    ```javascript
    { "name" : <Player name>,
      "gameId" : <Your game id>,
      "position" : {"x" : <number>, "y" : <number>}
    }
    ```
    * Response object in JSON format equal to GET call response describe below.
* **Post** call to baseUrl + **/game/move-piece**
  * Body object in JSON format
  ```javascript
  { "name" : <Player name>,
    "gameId" : <Your game id>,
    "initialPosition" : {"x" : <number>, "y" : <number>},
    "finalPosition" : {"x" : <number>, "y" : <number>}
  }
  ```
  * Response object in JSON format equal to GET call response describe below.

* **Get** call to baseUrl + **/game/:id**
  * Response a JSON string object like this:
    ```javascript
    {
      "_id": <Game id>,
      "_player1": <First player name>,
      "_player2": <Second player name>,
      "_board": {
        "_board": <Board Array 3x3>
      },
      "_turn": <The current turn of the game>,
      "_active": <Flag for active game>,
      "_winner": <Flag for current player turn win the game>
    }
    ```
* **Get** call to baseUrl + **/game/:id/number-of-pieces/:player**
  * Response the number of pieces in the board of a player.
