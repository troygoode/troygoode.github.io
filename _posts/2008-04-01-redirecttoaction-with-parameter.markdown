--- 
layout: post
title: "MVC: How Do I RedirectToAction While Passing a Parameter?"
---

A common scenario that seems under-documented to me in the latest MVC release, is how does one use RedirectToAction while passing a value to that action?

In the December CTP, you would do this:

{% gist 4006179 Example-1.cs %}

The new syntax is close, but not easily discoverable. Instead of the above, from now on you should use:

{% gist 4006179 Example-2.cs %}