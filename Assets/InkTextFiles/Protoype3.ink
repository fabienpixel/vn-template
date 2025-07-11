// Declare a variable
VAR trust = 0

== intro ==
// Start of story
# speaker: Lisa
# emotion: worried
Lisa: It's getting dark... I don't like this forest.

What do you want to do?

* Stay calm
    # speaker: You
    # emotion: calm
    We’ll be fine. Just stick close to me.
    ~ trust = trust + 1
    -> path_decision
* Panic a little
    # speaker: You
    # emotion: nervous
    You're right... this place gives me the creeps.
    ~ trust = trust - 1
    -> path_decision

== path_decision ==
# speaker: Lisa
# emotion: thoughtful
Lisa: There's a path that splits ahead.

Which way do we go?

* Left, toward the river
    -> river_path
* Right, toward the cabin
    -> cabin_path

== river_path ==
# speaker: Lisa
# emotion: relieved
Lisa: I can hear the water. This might be a good spot to rest.

-> the_end

== cabin_path ==
# speaker: Lisa
# emotion: tense
Lisa: That cabin looks abandoned... are you sure?

{trust > 0:
    # speaker: You
    # emotion: confident
    I think we'll be okay. Let's check it out.
}

{trust <= 0:
    # speaker: You
    # emotion: unsure
    I’m not sure… but we can’t stay outside either.
}

-> the_end

== the_end ==
# speaker: Lisa
# emotion: neutral
Lisa: Whatever happens, I’m glad you’re here.

THE END

-> DONE
