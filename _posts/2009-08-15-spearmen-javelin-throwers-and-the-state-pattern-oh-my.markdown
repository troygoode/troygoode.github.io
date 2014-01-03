--- 
layout: post
title: Spearmen, Javelin Throwers, and the State Pattern. Oh My!
excerpt: Jamie Farser & Ayende Rahein recently had a conversation about using the State Pattern for units in a game Jamie is building. I've been following along trying to figure my way through the state pattern as well, and decided to take a stab at my own solution to Jamie's problem.
---

Last week one of Ayende/Oren's posts caught my eye: [Let the fighting commence!](http://ayende.com/Blog/archive/2009/08/05/let-the-fighting-commence.aspx) In it he discussed [a blog post seen on Jamie Farser's blog](http://www.thecodespring.com/2009/08/interface-overload.html), where Jamie discusses some design difficulties he has run into while designing a turn-based game similar to Axis & Allies named [Everland](http://code.google.com/p/everland/). Since then I've been following Jamie's progress ([first try](http://www.thecodespring.com/2009/08/state-pattern.html), [further exploration](http://www.thecodespring.com/2009/08/state-pattern-part-15.html), [second try](http://www.thecodespring.com/2009/08/state-pattern-part-2-of.html), [third try](http://www.thecodespring.com/2009/08/state-pattern-part-3.html), [even further exploration](http://www.thecodespring.com/2009/08/state-pattern-transitions.html)) as he tries to take some of Ayende's advice and implement the state pattern. Like Jamie, I am fairly new to the state pattern - sure I've read about it plenty (starting in [Head First Design Patterns](http://www.amazon.com/First-Design-Patterns-Elisabeth-Freeman/dp/0596007124) - a must have!), but I honestly have just never implemented it "for reals." In my back & forth with Jamie I realized that it was time to put up or shut up - if I'm going to offer criticism I should also offer up an alternative solution.

## The Problem

Let's say we have a game where the board is made up of six-side polygons ("hexes", if you will) and each hex can contain a unit like a spearman or a javelin thrower. Each unit on the board belongs to a player who can take turns moving his units, telling his units to attack other player's units, etc. Some units have different capabilities than other units - a spearman must be within 1 hex of another unit to attack that unit, whereas a javelin thrower can be 2 units away. How do we design the application in such a way that:

* The game engine doesn't need to know specifics about each unit (such as range of attack).
* A unit's capabilities may change during the course of gameplay (such as a spearman being upgraded to a javelin thrower).

## My Solution

Before we look at the code, lets run through what is going to happen in-game.

1. The user selects one of their units, then selects an opposing unit somewhere else on the gameboard, then clicks an "Attack" button.
2. The game engine creates a context (deriving from **ITurnContext**) that describes what the user has asked to do. In our case, an **AttackContext** is created containing references to the attacking unit and the defending unit.
3. That context is then passed into the *ExecuteTurn* method of the attacking **Unit**, which then passes it along into the *Handle* method of its *CurrentState* property (which is an object deriving from **UnitState**). Our attacker's *CurrentState* property is currently **Spearman**, but could just as easily be **JavelinThrower**. Changing a unit from melee to ranged is as simple as changing the state of that unit.
4. Our **Spearman** state derives from **UnitState**, and the **UnitState** base class' *Handle* method uses [double dispatch](http://en.wikipedia.org/wiki/Double_dispatch) to forward the *Handle* request on to the appropriate method of our subclass. In our case that would be the *Handle* (*AttackContext*) method of the *Spearman* class.
5. Our **Spearman** class' *Handle* method then checks to see if the unit is within range to attack the defending unit - if it is, an **AttackCommand** is returned denoting who the attacker and the defender are; if it isn't, a **MovementCommand** is returned denoting which hex to move to.
6. The **Unit** class then calls *Execute* on the returned command, causing the movement or attack operation to be, well, executed.

Let's go through each of the above steps and take a look at the code involved one by one. Skipping the first step (the UI), we'll move on to where the game engine is creating an **AttackContext** and passing it into the attacking **Unit**:

{% gist 3998655 Example-1.cs %}

Okay, easy enough. Moving on we can see the **Unit** class' *ExecuteTurn* method pass the context to its *CurrentState* property's *Handle* method:

{% gist 3998655 Example-2.cs %}

Our attacking unit's *CurrentState* property is currently set to an instance of **Spearman**, but before we take a look at that class we'll take a look at its base class (**UnitState** - which is doing double dispatch to the **Spearman**'s *Handle* method):

{% gist 3998655 Example-3.cs %}

The Spearman class' Handle method is then invoked:

{% gist 3998655 Example-4.cs %}

Notice that the only concern **Spearman** currently has when handling an attack context is ensuring that it gets within range and then attacks the target. We'll see an example of a **JavelinThrower** later. Finally we'll peek at what the **MovementCommand** and **AttackCommand** actually do:

{% gist 3998655 Example-5.cs %}

Before we move on, here is the promised **JavelinThrower** class:

{% gist 3998655 Example-6.cs %}

Obviously **JavelinThrower** & **Spearman** currently only differ in the range at which they can attack (&lt;=2 and &lt;= 1, respectively), so the code in their *Handle* methods could be abstracted out using the [strategy pattern](http://en.wikipedia.org/wiki/Strategy_pattern).

## Prove It!

So now that we've had a chance to see the code involved the question comes down to: does it actually work? Here are our unit tests:

{% gist 3998655 Example-7.cs %}

And the results...

![IMAGE](/custom/files/old/Spearman-JavelinThrower-Tests-Passed.PNG)

Yay!

So that is the road I went down, but what I really want to know is how would *you *solve this problem? If you'd like to take a deeper look at my code, download it and give it a try yourself:

[EverlandStatePattern.zip](/custom/files/old/EverlandStatePattern.zip)