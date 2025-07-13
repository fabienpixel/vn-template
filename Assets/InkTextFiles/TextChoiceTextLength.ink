You’re about to test choice scaling at 23–27 character length.

-> Test1

=== Test1 ===
# speaker: System
Test 1: Displaying **1 choice**

* [Exactly 23 characters long (23)]
    -> Test2

=== Test2 ===
# speaker: System
Test 2: Displaying **2 choices**

* [This one's twenty-four chars (24)]
    -> Test3
* [Stretching just to twenty-five (25)]
    -> Test3

=== Test3 ===
# speaker: System
Test 3: Displaying **3 choices**

* [Testing the 26-char limit here (26)]
    -> Test4
* [Now reaching the edge at 27... (27)]
    -> Test4
* [Holding firm at twenty-three (23)]
    -> Test4

=== Test4 ===
# speaker: System
Test 4: Displaying **4 choices**

* [A good one: twenty-six chars (26)]
    -> Test5
* [Now we push past with 27 txt (27)]
    -> Test5
* [A clean cut 24-char phrase (24)]
    -> Test5
* [Let’s bring it back to 25! (25)]
    -> Test5

=== Test5 ===
# speaker: System
Test 5: Displaying **5 choices**

* [Another one that’s 23 long (23)]
    -> END
* [Text with 27 characters now (27)]
    -> END
* [Hit again with 26 straight on (26)]
    -> END
* [Solid fit at twenty-four chrs (24)]
    -> END
* [Quick wrap at twenty-five max (25)]
    -> END

=== END ===
# speaker: System
Test complete — you’ve now seen how 23 to 27 characters behave.
-> DONE
