EXTERNAL playSound(soundName)
EXTERNAL swapBackground(backgroundName)

~ swapBackground("citysquare")

Wait… did you hear that? It wasn’t just the wind, was it? No... it sounded like footsteps—slow, deliberate, and close; too close, actually.

This a second sentence.



* [Go To Test Dialogue] -> TestDialogue
* [Visit a nearby park] -> VisitPark

=== TestDialogue ===

~ swapBackground("citystreets")

# speaker: Joe
Where did he go?


+ [Visit the art gallery] -> ArtGallery

=== ArtGallery ===

~ swapBackground("artgallery")

You enter the art gallery, filled with breathtaking paintings and sculptures. You get lost in the world of art, appreciating the creativity of various artists. When you exit, you notice it's getting late. What's your next move?

+ [Head to a local cafe] -> LocalCafe
+ [Return to the city square] -> ReturnToSquare

=== LocalCafe ===

~ swapBackground("localcafe")

You find a cozy cafe and order a cappuccino. As you sip your coffee, you meet a local poet who shares their latest work with you. The conversation is inspiring, and you leave the cafe with a sense of creativity. What do you want to do next?

+ [Explore the city by night] -> ExploreCityByNight
+ [Return to your hotel] -> ReturnToHotel

=== ExploreCityByNight ===

~ swapBackground("citybynight")

The city comes alive at night. You explore vibrant nightlife, with live music, street performances, and colorful lights. It's an exciting and unforgettable experience. You make your way back to your hotel late at night. As you drift to sleep, you wonder what tomorrow will bring.

-> GoodEnding

=== ReturnToSquare ===

~ swapBackground("citysquare")

You return to the city square, where a street magician captivates a small crowd. You watch the magic show, amazed by the magician's skills. When it's over, you decide to find a good spot to take a photo. As you look at the image on your camera, you realize it captures the magical moment perfectly. After a few hours, you decide to return to your hotel.

+ [Go Back To My Hotel Room] -> ReturnToHotel

=== VisitPark ===

~ swapBackground("park")

You choose to visit a nearby park. It's a peaceful oasis amidst the urban chaos. You take a leisurely stroll, feed the ducks in the pond, and sit on a bench to enjoy a book. As you read, you notice someone playing a melodious tune on a guitar nearby. The park becomes your haven of serenity.

+ [Strike up a conversation with the guitarist] -> ConversationWithGuitarist
+ [Continue reading] -> ContinueReading

=== ConversationWithGuitarist ===

~ swapBackground("park")

You approach the guitarist and start a conversation. They tell you about their passion for music and even play a song for you. It's a beautiful and heartfelt moment in the park. As evening approaches, you decide your day has been perfect.

+ [Go Back To My Hotel Room] -> ReturnToHotel

=== ContinueReading ===
You continue to read your book, immersing yourself in its world. The park offers you a serene escape, and you enjoy every moment of it. After a few hours, you decide to return to your hotel.
You continue to read your book, immersing yourself in its world. The park offers you a serene escape, and you enjoy every moment of it. After a few hours, you decide to return to your hotel.
You continue to read your book, immersing yourself in its world. The park offers you a serene escape, and you enjoy every moment of it. After a few hours, you decide to return to your hotel.
You continue to read your book, immersing yourself in its world. The park offers you a serene escape, and you enjoy every moment of it. After a few hours, you decide to return to your hotel.

+ [Go Back To My Hotel Room] -> ReturnToHotel

=== ReturnToHotel ===

~ swapBackground("hotel")

You head back to your hotel, satisfied with the day's exploration. You relax in your room and reflect on your adventures. Your heart is full of wonderful memories, and you're excited to see what tomorrow has in store.You decide it's time to sleep.

+ [Close your eyes] -> GoodEnding

=== GoodEnding ===

~ swapBackground("theend")

As you lay in bed, you feel a deep sense of contentment. Your day was filled with delightful experiences, and you can't wait to see where your next adventure takes you.


-> DONE
