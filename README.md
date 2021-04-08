# BattleshipGame

This application will allow you to play one-sided battleship game in your console.

App is written using .NET 5 and you can easily run it on your machine using
```
./BattleshipGame/dotnet run
```
command  

By default 10x10 grid will be created, and 1xBattleship and 2xDestroyers will be placed on board randomly.  
You can change these parameters in gameSettings.json file, however grid size more than 10 is not allowed.

Player will have to select coordinates of form "B3", where 'B' is a row and '3' is a column  
to specify a cell to target. Shots result in hits, misses or sinks. The game ends when all ships are sunk.

