# DuelingStarships
This game is a port of DuelingStarships from Dragon Ruby:  
http://docs.dragonruby.org/#----arcade---dueling-starships---main.rb  
The reason this was done is to prove that Unity does not necessary yield more lines of code for the same result.
My source code currently sits at 99 lines of C#, the original Ruby version stood at 367 lines of Ruby.  

The inspiration comes from the Dragon Ruby Discord, @amirrajan:  
"Try creating Dueling Starships, Basic Gorilla, Box Collision 3, heck even the Top Down RPG sample app, in any of the engines you mentioned. I’ll eat my hat if an identical implementation yields less code then DR.
I’ve paid a dev to do Dueling Starships in Unity in fact already. Even with everything that was available at their disposal, their solution was larger (with hacky stuff for particles).
Trust me the claim about “oh I can do this using X with so little work” kept me up at night. Every single time I test that claim DR wins.
Always open to someone giving it a shot though to see what their result it.
Prove me wrong ^_^"    
"What I’m asking is: Do you think dueling starships can be done in another engine with fewer lines of code using all its baked in features?"  
"If they can create an identical implementation to DRGTK. I’ll give that person $500"  
"Try it :man_shrugging: Surely this is an easy half grand to win"  
"It should be a cake walk to do it in the other engines. $500 bounty if you prove me wrong ^_^"  
"check all the boxes you'd like  
I only care about code written  
checking a box and dragon entities to save loc is totally kosher  
even use assets if you think it'll help, as long as it's identical from a game play standpoint"

With these comments in mind, I wanted to prove it could be done, not to mention the prize!   
Due to the nature of this challenge, I have often sacrificied efficiency, clarity, and style in the name of saving lines.  
For example, using GetComponent over and over on the same component allows me to avoid serializing it, saving 1 line of code at the cost of performance. 
You should definitely not use any of this code in a real project, fair warning.
A cleaner more sensible version of this would be more lines of code, but it should still be entirely possible without going anywhere near 300 lines. In this case however, my only goal was minimizing lines of code.
