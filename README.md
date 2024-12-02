# Higher Lower Game
## Playing the Game on the Web
The game is hosted by Unity, playable here:

https://play.unity.com/en/games/e60204c3-43c3-4577-83a2-300eadcb7cfe/higher-or-lower

## Setting up on Your Own Computer
The game was developed using Unity 2022.3.19f1. You can clone the repository to your own computer and then open the project in Unity to edit or play.

## Design Decisions
### Playing Cards
The design of the playing cards are dynamically generated. This was done by following a set of rules to determine the pip (suit image) placement on the cards. A dynamic method was chosen over using static images as it allowed easier customisation of the cards, especially if later on further features were added relating to the cards, such as a shop, allowing the user to purchase cards of different styles.


## Improvement Ideas
### Shop
Ideally, I would have liked to have implemented a shop, where coins could be used towards purchasing new card designs. This would make use of the dynamic card design generation I implemented.

### Wager Buttons
In future I would improve the wager modifier buttons, as currently you can only click to increment or decrement the wager by one. This is not user friendly once the number of coins a user has grows larger, and wagers become bigger. Ideally, a mouse hold to increase quickly, or a manual entry system would be implemented.

### Betting Odds
To make the game more interesting, I wanted to make the amount of money you receive from betting higher or lower to be randomised, but known to the player before betting. This would force the player to make more difficult decisions and have the opportunity to take more risks. For example, if the chance of drawing a lower card was low, but the payout was high, a player may choose to take a risk and bet lower - rather than taking the obvious option of higher.

### UI Polishing
In the future, I would make the UI more responsive. This would include tweening for scene changes, interactions with buttons, or game actions - such as shuffling the deck. This would also include the addition of sound effects within the game, and also having more information presented to the user - such as a pop-up notification for when money is won or lost.
