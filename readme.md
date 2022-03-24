# Blacksmith Blackjack
 **Description**

This is a simple unity implementation of a blackjack game. It was created as a foundation for a level based blackjack game and has some fantasy themes as the full game would be based around being a blacksmith for a fantasy town. The blackjack gameplay is a simple 1v1 against the dealer to reach 21 while staying below it. Each player gets dealt 2 cards out of a standard 52 card deck and one of the Dealers cards will be dealt face down. The Dealer always hits on a 17 and ties will go to the Player.

This is made using Unity 2020.3.15f2 utilizing a 1080x1920 resolution for mobile devices. 

**Class Breakdown**

 - *GameController*
This is what outside scripts that run the project would be able to access and it contains what any script should need to run such as a StartGame method and a OnComplete action. It is abstract as it does not contain any gameplay itself. 

 - *BlackjackController*
Inheriting from *GameController*, this implements all of the logic behind running the blackjack game. It handles checking for win conditions and also the Dealer's decision making. It takes a config that determines game specific settings as well as references to both a *IBlackjackPresenter* and an *IBlackjackModel*. 

 - *BlackjackPresenter*
This handles the presentation and interaction for the user, implementing *IBlackjackPresenter* to allow the the controller to show changes in the state of the game. 

 - *BlackjackModel*
This handles the data of the game by keeping track of the deck, player hands and who has stood. It implements *IBlackjackModel* so that the controller can interact with it.

**Approach**

When working on this project I wanted to ensure that this project would be easily extendible into different variants. This led me to splitting out the Model/Controller/Presenter so that I could use the factory to switch them around as needed. I could potentially set up a rigged deck using a different model and controller while continuing to use the same presenter, or I could completely change the front end of the game. The only dependency on Unity within the *BlackjackController* is from Debug.Log statements, so this could be completely split from Unity if needed. Likewise smaller elements like the *HandController* and *DeckController* are kept basic so that they could be reused in a different type of game.

**Moving Forwards**

Continuing on this project will revolve around a lot of features to turn it into a fully fledged app. Implementing the gold based meta game, allowing level progression, finishing the UI systems and turning *GameInit* into a proper main menu. This will also require more variety in the blackjack game so adding items to the *BlackjackGameConfig* and extending it into multiple game types. Ideally these types and configs will be loaded in via JSON or configured through scriptable objects. 
