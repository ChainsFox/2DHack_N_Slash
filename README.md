2DHack_N_Slash:
12/05/2024:
Small progress - part 16 - 11:38
-Bugs: when you die you can still see your crosshair.(fixed: disable child game object)

15/05/2024:
part 16 - 30:06
-Bugs: enemy still attack you when you die.

19/06/2024
-Bugs: Enemy still attack you when you die, and when you die you slide till you hit a wall and stop?
->Fixed 2nd part by adding:
"rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;"
... In the is alive bool.
-Bugs: enemy still hit you when you die
part 16 - 30:06

27/06/2024
Bugs:
-Im guessing because i use the flipx instead of flipping the localscale my hit animation is affected because it only showing one side.
-Player attack is not working also because of the flipx im guessing.
part 16-end.

29/06/2024
*)Temporary Fix:@@@
-I split the enemy attack and the player attack into 2 script, the enemy attack script function as normal using localscale, and the player attack script use the normal knockback, which the downside is it only knock back on the x > 0/right side. So i decided to only give it Y knock back.(easy way)
->Logically is to have the knock back based on the pointer, which i dont know how to do yet.
*)Bugs:
-Animation hit is still bug, but is it not really a bug, mainly just bad logic cause i use the flipx on the player.(!!!)
30/01/2024
part 17-7:18

02/07/2024
-enemy got bugged when trying to do the cliff detection zone logic, it keeps attacking the player if detected and if it dont hit then it will stuck there
->It is fixed but i dont know clearly why, i mess around with the animation state a bit
part 17-end.

03/07/2024
part 18-9:01

06/07/2024
part18-done;

07/07/2024
part19-done

08/07/2024
-IDEAS and UPGRADE:@@@
impact effect on bullet 
upgrade to new input system with the shooting script later on
-Bugs fixes:
+)Change bullet layer to BulletHitbox so that it will only interact with the enemy, and the terrain(ground). ->you can no longer damage yourself with the bullet.

09/07/2024
part20, part21 is skippable cause im not gonna implement it into my game.
bugs: 
+)when the player die the enemy still attack.@@@
part22-24:09

10/07/2024
BUGS:
+)When you die in the air you get stuck there(because of the previous fixes/bad logic in the damageable script)
->OnDeath function in flyingeye script, might be a nice way to fix it(it did?)@@@
IDEAS:
+)Figure out a way to communicate with the player that you have i frame when you get hit.
part22-31:32

11/07/2024
part22-done

15/07/2024
1) part23-added the fix at 10:38, before that my game don't use that aerial attack so i am not implementing it yet.
2) part24-done-bug: font doesnt scale with screen size
3) part25-4:25-done/todo(you can add sfx on your own, either use the playOneShotBehaviour script, or add a audio source to the object and play from the local script, maybe even reuse the audio manager script from previous project)->Add sound effect later on/improve
4) bugs: all the audio is so low except the bg music :v

17/07/2024
IMPROVEMENT/FIX:
added attack cooldown for skeleton enemy using behaviour in state attack
added sfx for slash, bullet prefab...
fade remove for player death, added canMove variable and improve logic on function OnJump.->When you die you can no longer move or jump, and will fade away.
BUGS:
enemy still attack you when u die+you can jump when you die->fixed with death fade remove.
part26-13:48

18/07/2024
part26-done
Finished relearn/redoing the crash course, is time to begin freestyling

2DHack_N_Slash(Phase2-Freestyle):
*)Temporary Fix/Compromise:
-I split the enemy attack and the player attack into 2 script, the enemy attack script function as normal using localscale, and the player attack script use the normal knockback, which the downside is it only knock back on the x > 0/right side. So i decided to only give it Y knock back.(easy way)
->Logically is to have the knock back based on the pointer, which i dont know how to do yet.
-If we release crouch button under object we will still be in crouch but after we walk out of the object it will not auto go back to standing mode, we have to input crouch button again to do that.

*)IDEAS and UPGRADE:
impact effect on bullet(V)
upgrade to new input system with the shooting script later on(V)

*)VISON
-add as much 2d mechanic as i can if possible, that fit the game(double jump, dash, wall jump, slide, crouch...)
-build the full map in 1 scene
-enemy variety
-Polished/Game feel:
+)UI(cool downs, i frame effect, jump/dash effect...) 
+)Music/sfx
+)Gameplay

21/07/2024
-updating the shooting script to new input system.
->Problems: the player can no longer hold to auto attack or shoot.

23/07/2024
-I learn a bit about interactions, good knowledge for later use if possible.
-added a delay time to destroy so we no longer need a timer to the shoot/slash script.(Shorter code)
->Fix: Player can now hold to auto attack(shooting script upgrade)

24/07/2024
-bullet impact effect(using animation and adding logic in bullet script)
->Might need to check on the bullet as a whole later.

25/07/2024
+)HP show negative number
example: -5/100 ->small detail m

ight fix later.
-dash mechanic build started

26/07/2024
-try to implement dash with new input system(didnt work)

30/07/2024
-dash only work on y axis.(need to fix)
-bugs: player can press up and down and the character will move

31/07/2024
-dash implemented
-down and up input bug

01/08/2024
-fix down and up input bug(running in place)
-cant implement look up because if i hold w and press/hold a or d at the same time it will run in place again. -> this is because if we hold a or d the reference variable OnMove will be true no matter what.

04/08/2024
-learning and researching about crouch mechanic.
-can't follow video step by step, follow the idea, but implement it your own way because the coding is different, with new input system and etc. Also this is kinda related to the interaction logic with hold input.
12:09 

05/08/2024
goals:
+)hold crouch switch/disable collider, or look into the vector 2 height stuff...
-6 min crouch guide learn(the game smith)
-need to learn 7 min crouch guide next(pix and dev)->size change from the bottom not center of the collider/sprite

07/08/2024
BUGS:
-just found out that the player hitbox is off because of my flipx script(might want to look into that later)
LEANRED:
-learned how to change pivot through sprite(use the sprite editor->slice->choose pivot) - i cant figure out a way to use this with the crouch mechanic tho, very janky.
-applied offset logic so that the coll looks like is lower down, but in reality we just change the position of the collider so that it seems that way. -> Player dont clip through ground.

08/08/2024
note:
-onwallcheck don't work because of the flipx logic(reminder)
-20 min crouch guide - 19:59(animations)
progress:
-if player is under a platform when release the crouch button will stay in the crouch coll, but if walk out it will stay in that collider until we input and release the crouch button again(bugged)

10/08/2024
-if we jump we are not crouching
-still working on the crouch mechanic, putting it in the isgrounded if statement seems to be better because it will not glitch up and down. But i still dont know how to auto release crouch after we run out of an overhead platform.

13/08/2024
-COMPROMISE: last attempt at auto release crouch, i will temporary accept this inconvenience and move on to the next step

16/08/2024
-animation started
-bugs:
+)if you release crouch button while still under object and then go out, it will kinda get stuck in the animation, and it seems that only event will impact it, because i try !isgrounded and it didnt work.

19/08/2024
-added more transition for crouching
-input bug: if you in put downward(s) + left/right(a or d), you will move slower, but if you are crouching and still under an object, you will stay crouched but you don't have to input sa or sd to move, which mean you can press a or d and it will be faster.(Look at the move direction x and y to notice)
-added double jump: very simple double jump, the double jump got the same force as the normal jump. No advanced jump added yet(optional)

22/08/2024
-double jump mechanic now cause infinite wall jumping(FIXED it by using old code - i added a new code cause i forgot the old script change something in the unity editor and i forgot about it, i must have thought that the script was bugged and use a new one, but in reality it still works lol)
-Ultimate ability start

28/08/2024
-ability research(copied ability system from TIMBER)
-route 1: if i don't want to use the abilty system, i could make a prefab for the waterball, add a script to it, and then reference it in the playercontroller script. Then figure out a way to give it cooldown and activate it in the playercontroller script.
-route 2: try to learn how to use the abilty system script

31/08/2024
-need to change 180 rotation on y and position on x
->I change the 180 rotaion on y, but i didn't figure out how to do the postion on x yet.

06/09/2024
-added animation trigger for ability waterball
-flip logic is change, now using rotate(0f,180f,0f) to flip the player. Need to double check and retest all the other mechanic/functions related to this.
-The behaviour lockVelocity and canMove is messing with the directions?

07/09/2024
-locked movement when using waterball
-added some sfx for waterball
-bugs: when you near the wall and the enemy hit you it kinda lag the sprite making it spin around

09/09/2024
-ability cooldown started
(Making A MOBA Character in 2023 - #3: ABILITY COOLDOWNS (Unity 2023 Tutorial) - 6:37
->need to learn how it works, also need to learn how to make it reuseable and not just duplicate code(so that i don't have to repeat code for each ability)

14/09/2024
-i understand how the logic of ability cooldown works
-failed on trying to implement reusable cooldown logic
-ideas(2 more ability then move on to the next part):
+)fire breathing 
+)thunder slash

21/09/2024
-research for 3rd ability(hold attack/ability)
+)we can use a bool hold parameter on animation to figure out when are we holding attack, and setting the animation

23/09/2024
-ability 3 start(basic component complete)
+)need to figure out damage over time
+)hold ability/quick release
+)visual indication

17/10/2024
-starting to fine tune how the actual firebreath ability would work(ability 3)
-player cannot move when holding the ability, letting go will release it.
-cons:
+)the flame still need to be remove on quick release and after a certain amount of time

18/10/2024
-fire breath(ability 3) visual complete-> can quick release, and after a certain amount of time will automatically goes back to normal state.
+)Need to learn how to do damage over time
+)Ui visual 

20/10/2024
-dot research(video)
-you might want to mess around with the hitbox, see if there a route there

21/10/2024
-emulate burning effect by turning off and on the collider along with the animation. ->Success(Easy and simple route)
-Trying to do the start up animation into the hold animation
-When hold fire breath to the end and press a move button, it will not move until you let go of the fire breath button.

22/10/2024
-start up animation done: By using 2 phase animation, add a little bit of delay in the first phase and then transition into the 2nd phase(simple and effective-no code require)
-Bug: if you tap the fire ability fast it will release the fire and it will stuck there.(Check script logic, and look into the interactions again)
-Bug?: When using ability you can still slash and shoot
-Add in the ui bar for fire ability

23/10/2024
-same problem, need to figure this out with ability 3(fire breath)

25/10/2024
-Tapping ability 3 will not spawn the flame, the player will have to hold it(cause i put the logic in the update now).
-Bug: When holding ability 3 and simultaneously pressing left or right button, if you don't let go of the ability 3 button, the player will stuck there

27/10/2024
-Bug still exist, i haven't found a good way to fix it yet

01/11/2024
-freeze and unfreeze function now use gravity drag logic, i prioritize the flame function so that it will get to use the rb freeze logic.
-need to work on background next, along with fire breath ui

1/11/2024
-working on the flame slider ui

2/11/2024
-flame breath logic and ui done(finally)
-double jump is bugged(when doing a late edge jump, it will count double jump as false - if we jump early then its fine) - if u run jump too fast, you will be in the air, thats why it count as a double jump -> This is the coyote jump method 
-can draw background(time consuming)

08/11/2024
-parralax bg, added some more props

09/11/2024
-added sfx for dashing and double jump, added dust effect
+)need to tune dust effect
->Game Feel elemtents+
-Research and decide if:
I want to add a running slide or not
UI for dashing and/or slidng
-Enemy variety last

10/11/2024
-trying to implement running dust
12/11/2024
-dust effect for jump and double jump added.
-)No longer use running dust
+)Should think about if i want to add in the slide, and if i can, i can add in the ui element

13/11/2024
-need to figure out a better way for sliding

15/11/2024
-slide mechanic goals:
1 button function
Can not change direction when sliding
Its a slower but longer dash(basically)
Might want to cancel slide when sliding off on the edge(maybe)
=>Temporary complete 
-)Animation is not running
-)Can't transition smoothly into jump
-)Slide into crouch is bug if we dont place the code correctly 

18/11/2024
+)I change the position for some line of code in the dash couroutine, and change the exit time from the player_slide to the exit = 0.05.
->Doing this will undo the slide into crouch/crouchwalk bug, and make the slide into walk/run look much more faster.
-)Can no longer think about animation if i do it like this(compromise?)

19/11/2024
-)Using slide with other abilites is buggy(fixed?)
-)Optimization need to be checked
+)Can transition smoothly into jump now(added more counter measure code into the jump call back function)

22/11/2024
-fix bg/knockback issue on death
-fix ui position error when changing screen size

//next step is enemy variety, and building the wholemap:
For enemy:
+)A long range arrow type enemy that is on the ground
+)A enemy that with rush at you when you are in range
+)Machine that follow you and shoot you when you are in range(360 or 180 degrees)

13/12/2024
-range enemy started
-basically the same movement as skeleton enemy, but it is bugging rn
07/01/2024
Range enemy :
- enemy when make contact with the player keep flipping sprite
- need to figure out a way for projectile to spawn correctly
08/01/2025
Range enemy :
- enemy when make contact with the player keep flipping sprite(Fixed)
+) added destroy on impact to StraightBullet script
+) Change the audio source on waterball from attribute to animation
+) Enemy can now shoot with correct side
- the range/detected logic is working but not as intended
- no animation with projectile spawn
- when the enemy attack it switch the firepoint?
- if we put attack cooldown on zero, it will attack endlessly but the movement is bugged

12/01/2025
+)added destroy on impact to StraightBullet script(Reverted) -> If i do this it will mess with the fire breath ability
-)new bug: if i die it will cause an error(collider can not be null) in the projectile attack script. 
(Fixed: by putting the damageable object of the player in the range attack parameter in unity)

13/01/2025
+)enemy seems to be spawning project in sync with the animation.
-)Enemy still have the bug where he moves and switches the fire point half a second then switch back

14/01/2025
+)Minium requirement for ending the project is done(option)
->The range enemy is complete

=>Overview: This project is me learning and trying to implement more complex idea and mechanics into my 2d game. Can i implement the things that i think of, into the game, and with all this time and effort, i think i absolutely can. But, there is a big downside, this project is not optimize at all, so that is one of the many things i need to improve on.
