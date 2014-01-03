--- 
layout: post
title: "MVC Complaint: Checkboxes"
excerpt: The new ASP.NET MVC framework's checkbox control has a major "gotcha" to be wary of.
sharing: false
---

<p class="warning">This article was written for the December CTP release of the MVC framework. Unfortunately, it does not entirely apply to the Preview 2 release or subsequent releases.</p>

I mentioned in my previous post on [building login & registration forms](/2007/12/10/aspnet-mvc-membership-basics/) in the [ASP.NET MVC framework](http://weblogs.asp.net/scottgu/archive/2007/12/09/asp-net-3-5-extensions-ctp-preview-released.aspx) that I was not a fan of the way checkboxes are handled in the first CTP release's [toolkit](http://blog.wekeroad.com/2007/12/05/aspnet-mvc-preview-using-the-mvc-ui-helpers/). To help explain why I don't care for it, consider the following... what would you expect the following helper function to output?

{% gist 3998609 Example-1.aspx %}

I would imagine a single checkbox and a label with the text value, which is incorrect. Instead, this is what is generated:

{% gist 3998609 Example-2.html %}

It looks fairly reasonable to most and really isn't the end of the world, but is rather unusable in my eyes. The text is just output inline directly following the checkbox element with no element wrapping it. This small omission makes it incredibly difficult to style the output effectively using CSS. Simply wrap the text in a &lt;label&gt; element and everything is gravy. What I would prefer to see output is:

{% gist 3998609 Example-3.html %}

I realize this seems like a nitpick; I'm perfectly capable of writing my own HTML checkbox and label (usually). My frustrations do not end there, though; let's look at how you retrieve data from a checkbox within a controller action that you have posted to. Taking the example of a simple login form with two text fields ("userName" and "password") and one checkbox ("rememberMe") you might expect to have the following controller action:

{% gist 3998609 Example-4.cs %}

Given the context of this article, you can probably guess that the above doesn't work. Unfortunately the automagic code that maps fields found in the HTTP request to parameters in a controller action does not support mapping to a boolean. This is partly the fault of the HTTP spec (submitting a checked checkbox causes the form to post with $NAME=$VALUE where $NAME is the name of the checkbox element and $VALUE is the value of the "value" attribute; submitting an unchecked checkbox causes the form to post without any reference to the checkbox -- i.e.: a null value), but could easily be resolved with the following rule:

**When a boolean is declared in a method marked with [ControllerAction], pass false into the parameter unless a text value is found with a field name that correlates to the name of the parameter (in which case pass true).**

In other words, if you can't find a mention of "rememberMe" in the form's POST data, pass false to the parameter. If the form's POST data contains *any *value for a field named "rememberMe," pass true to the parameter. For the time being you should declare the checkbox parameter as a string and test it against null, like so:

{% gist 3998609 Example-5.cs %}

Sure, it isn't rocket science the way it is setup right now, but a little extra work could make for a much more fluid learning curve for new MVC developers. Obviously this is just a CTP release and not the full deal; I fully expect a lot of these quirks to be worked out over the next several months. This does make me wonder, will the toolkit be posted to [CodePlex](http://www.codeplex.com) so that it can benefit from community involvement?