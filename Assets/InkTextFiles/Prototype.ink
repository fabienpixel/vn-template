EXTERNAL playSound(soundName)
EXTERNAL swapBackground(backgroundName)

~ swapBackground("beach")


Once upon a time in a mystical forest, you find yourself at a crossroads. Do you:

+ [Go left] -> LeftCrossroads
+ [Go right] -> RightCrossroads

=== LeftCrossroads ===

~ playSound("donk2")
~ swapBackground("forest")

You walk deeper into the dense woods, guided by the chirping of birds and the rustling leaves. After a while, you stumble upon a hidden treasure chest. You can:

* [Open the chest] -> OpenChest
+ [Leave it and continue your journey] -> ContinueJourney

=== RightCrossroads ===
You follow the path to the right, and soon you encounter a talking squirrel. It looks at you and says, "Hello there, traveler! I can guide you to a secret garden. Will you follow me?"

* [Follow the squirrel] -> FollowSquirrel
+ [Politely decline and continue on your own] -> DeclineSquirrel

=== OpenChest ===
As you open the chest, it reveals a shimmering gemstone that emits a soft, soothing glow. You decide to keep it and return to the crossroads. What's your next move?

+ [Go left] -> LeftCrossroads
+ [Go right] -> RightCrossroads

=== ContinueJourney ===
You leave the chest behind and press on with your journey through the forest, wondering about the adventures that lie ahead. You return to the crossroads. What's your next move?

+ [Go left] -> LeftCrossroads
+ [Go right] -> RightCrossroads

=== FollowSquirrel ===
You follow the squirrel, which leads you to a hidden garden filled with colorful flowers, singing birds, and a magical fountain. You take a moment to relax and savor the beauty. What do you do now?

* [Explore the garden] -> ExploreGarden
* [Drink from the fountain] -> DrinkFountain

=== DeclineSquirrel ===
You thank the squirrel but decide to continue on your path alone. As you walk, you come across a mysterious portal. It's glowing with an eerie light. Will you:

* [Enter the portal] -> EnterPortal
* [Ignore it and keep going] -> IgnorePortal

=== EnterPortal ===

# RESTART

You cross the portal and find yourself in a mystical forest at the exact same point where you stared. You find the same crossroads. Do you:

+ [Go left] -> LeftCrossroads
+ [Go right] -> RightCrossroads

=== ExploreGarden ===
You explore the garden, finding a rare and enchanting flower. You pluck it and keep it as a cherished memento. After a while, you decide to leave the garden and continue your adventure.

* [Go left] -> LeftCrossroads
* [Go right] -> RightCrossroads

=== DrinkFountain ===
You drink from the magical fountain, and it fills you with an incredible sense of vitality. You feel rejuvenated and continue your journey with renewed energy.

* [Go left] -> LeftCrossroads
* [Go right] -> RightCrossroads

=== IgnorePortal ===
You choose to ignore the portal and press on with your journey through the forest, eager to see what else awaits you.

-> DONE
