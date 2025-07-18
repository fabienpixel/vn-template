You step into the alley, unsure of what you'll find.

# speaker: Lisa
"What's that?" my master asked.
*	"I am somewhat tired[."]," I repeated.
	"Really," he responded. "How deleterious."
*	"Nothing, Monsieur!"[] I replied.
	"Very good, then."
*  "I said, this journey is appalling[."] and I want no more of it."
	"Ah," he replied, not unkindly. "I see you are feeling frustrated. Tomorrow, things will improve."

What will you do?

* [Keep walking forward]
    # speaker: Player
    Let’s keep going. We’re almost there.
    
    -> ContinueScene

* [Stop and listen]
    # speaker: Player
    Wait. I hear something...

    -> ListenScene

* [Go back]
    # speaker: Lisa
    Fine. Let’s go back. I don’t like this place anyway.

    -> DONE

== ContinueScene ==
# speaker: Lisa
Alright. But stay close.

-> DONE

== ListenScene ==
# speaker: Player
There—did you hear that?

# speaker: Lisa
Footsteps. They're coming this way.

-> DONE
