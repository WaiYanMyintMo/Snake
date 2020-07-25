# Welcome to Snake - your classic Snake game as a console app. 

Written in .NET Core 3.1

## Quick Start Guide
Valid keys are arrow keys ← ↑ → ↓ or WASD. The inputs are queued.

The console will wait until you press one of these.

You win when the entire screen is filled with snake.

When game is over, you will see your snake's length.

You may pass command line parameters / change source code config to control various configurations. 

## Quick Project Explanation
Split into "Core" class library that has the world class.

And then the "Snake" console project, that deals with input + rendering + extra UI logic.

Both have configurable Options class. The rest you may read the code / ask me through issues.

## Features
* Smooth inputs
  * Inputs are queued.
  * WASD and ArrowKey boths are supported
  * Will have a mono-game version available soon.
* Amazing configuration
  * (Almost)Every defaults are changable using command line options or source code, powered by CommandLineParser.
    If there is one you want to change, feel free to send a pull request.
    Examples are - 
      * How many seconds should the game wait between each update / render
      * X and Y size of the world of the game
      * Apple's X and Y coordinate.
      * You can set a seed so that the randomness is reconstructable (assuming your snake is in the same position)
        * You know exactly where the apple will spawn
      * Verboseness so you know which random seed you played
  * Project is split into Core and UI part, meaning I could reuse the Core part in any project I want to port to
    In this case, I can reuse it to make a cross-platform game using monogame.
* Forgiving AI
  * Apple is much more likely to spawn around you.
  

### TODO
* Add color
* Port to MonoGame
* Add more gameplay such as quick apple bonus.

## Known Bugs (features?)
* Only starting snake location can't be controlled as of now using config
* Warping not implemented yet, wall will kill

Thank you for trying this out.
