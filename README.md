# Welcome to Snake - your classic Snake game as a console app. 

Written in .NET Core 3.1

## Quick Start Guide
Valid keys are arrow keys ← ↑ → ↓ or WASD.
The console will wait blank until you press one of these.
You win when the entire screen is filled with snake.

## Quick Project Explanation
Split into "Core" class library that has the world class.
And then the "Snake" console project, that deals with input + rendering + custom game logic.

## Confused / Concerned Concepts
* Any insights are welcome.

## Known Bugs
* Game will hang if all area around snake head is invlaid
* Game is hard-code random seed for apple
* warping not implemented yet, wall will kill

Thank you for trying this out. This took me a lot of willpower to try to finish this.

## Future Goals
* include color
* port to MonoGame
* add quick apple bonus gameply
