# Welcome to Snake - your classic Snake game as a console app. 

Written in .NET Core 3.1

## Quick Start Guide
Valid keys are arrow keys ← ↑ → ↓ or WASD.
The console will wait blank until you press one of these.

## Quick Project Explanation
Split into "Core" class library that has the world class.
And then the "Snake" console project, that deals with input + rendering + custom game logic.

## Confused / Concerned Concepts
* Input, first time doing game dev. Game loops are hard, haven't learned to use events and delegates.

## Known Bugs
* Text shaking once every ~1-5 second 
* Apple is glitchy after some time and can't be eaten (in hard-coded Random seed)
* Snake when hitting, will not die, but rather glitch its way out of it (feature?)

Thank you for trying this out. This took me a lot of willpower to try to finish this.

## Future Goals
* remove "input" characters in bottom left corner of console
* start screen instead of blank
* end screen with snake's length as highscore when die / a score somewhere on the screen.
* include color
* port to MonoGame
* fully emulate nokia snake with quick apple bonus gameply
