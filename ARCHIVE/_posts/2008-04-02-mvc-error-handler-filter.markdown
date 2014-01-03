--- 
layout: post
title: "MVC: Action Filter for Handling Errors"
---

A few months ago I posted [an article and some code](/2008/01/04/mvc-authentication-and-errors/) that contained filters for forms authentication and error handling for the Preview 1 (CTP) release of the MVC framework. Unfortunately [the Preview 2 release](http://blogs.msdn.com/brada/archive/2008/03/05/asp-net-mvc-preview-2.aspx) that was made available a few weeks ago changed enough that the code I posted no longer works. 

What the Preview 2 release did provide, however, was a new built-in filter framework. Rob Conery has already gone through the trouble of [creating authentication filters](http://blog.wekeroad.com/blog/aspnet-mvc-securing-your-controller-actions/) that cover most of the functionality I had before, but I have yet to see an implementation of a filter for error handling that I like. I've gone ahead and started from scratch, throwing away my old filters, and created a new filter that I think covers most of the same scenarios as my old ErrorHandler filters while being much simpler to implement and use. Hopefully you'll find it useful. 

First let's take a look at a simple use case scenario: 

{% gist 4006173 Example-1.cs %}

In the code above, we have a simple action that displays a product based upon the ID specified. What do we do when no ID is specified though? The "correct" thing to do seems to be to throw an exception, as we've done, but now the user will see either (a) an ugly 500 error screen [worst case] or (b) be redirected to the generic error page [best case]. Sometimes we'd like a bit more control than that though... 

Let's go ahead and add our error handling filter to this action and tell it that whenever **ArgumentNullException** is thrown, redirect to the "Products" page, where the user can select a product with a valid ID. 

{% gist 4006173 Example-2.cs %}

So we've added a [**RedirectToUrlOnError**] attribute and supplied it with a **Type** property - detailing the exception to catch - and a **Url** property - specifying the **Url** to navigate to upon a matched exception. You'll notice we are making a call to the **GetProduct**(*int*) method to retrieve the product's model so that we can pass it into the view's ViewData. What if this method were to fail? What if we weren't entirely certain what exception it would throw, or maybe we didn't care, we just want to handle any exception except for **ArgumentNullException** (which is already being handled). In this case we'll add another filter, but this time we will not specify the **Type** of exception that it should catch and just tell it that if anything isn't caught by another error handler redirect to the homepage. 

{% gist 4006173 Example-3.cs %}

You can have as many error handler filters attached to an action as you need, but only one may have no Type specified. 

Now let's take a look at the code for the filter itself:

{% gist 4006173 Example-4.cs %}

So the [**RedirectToUrlOnError**] attribute inherits from the [**RedirectOnError**] attribute, which is where most of the hard work is done. We'll take a look at that base class in a bit, but first let's look at the other attribute you can use to trap and respond to errors - the [**RedirectToActionOnError**] attribute. We'll continue with the **Product**(*id*) sample from above, but this time redirect to an action rather than a hardcoded Url:

{% gist 4006173 Example-5.cs %}

You can see that this time instead of providing the Url property we are using providing the type of the controller that contains our target action, and the name of the action as a string. (Unfortunately lambda expressions are not allowed as parameters to an attribute, so I was limited in my options here. If you have a better idea, please let me know!) Also note that the catch-all is still there as a [**RedirectToUrlOnError**] attribute. You may use the [**RedirectToActionOnError**] attribute as a catch-all and you can mix and match the two attribute types, but still only one catch-all attribute total is allowed per action (in other words, you cannot have one of each).

Now let's see the code for this filter:

{% gist 4006173 Example-6.cs %}

Other than some complexity with determining the Url, everything is very similar to the other filter. Again it appears the base class is doing the heavy lifting. Let's finally take a look at that base class:

{% gist 4006173 Example-7.cs %}

That's all there is. Feel free to take it, use it, change it, whatever. I tried my best to document it thoroughly with comments, but if you have any questions just drop a comment below and I'll try to respond quickly. I do ask that if you make any improvements, please leave a comment here letting everyone know what you've changed so that we can all benefit. 

## Here are the filters in a downloadable format:

{% postdownload RedirectOnErrorAttributes.zip %}