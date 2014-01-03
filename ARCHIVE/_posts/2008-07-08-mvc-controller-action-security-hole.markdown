--- 
layout: post
title: MVC Controller Action Security Hole
sharing: false
excerpt: I love the automagic ASP.NET MVC gives you out of the box, but it looks like some of that magic comes at a steep price for your application's security.
---

The latest of [Stephen Walther](http://weblogs.asp.net/stephenwalther/)'s invaluable [ASP.Net MVC Tip series](http://weblogs.asp.net/stephenwalther/archive/tags/Tips/default.aspx) points out a MVC scenario that was previously unknown to me: [passing cookies and server variables into controllers as action parameters](http://weblogs.asp.net/stephenwalther/archive/2008/07/08/asp-net-mvc-tip-15-pass-browser-cookies-and-server-variables-as-action-parameters.aspx). While the idea is neat, [a comment left by Francois Ward](http://weblogs.asp.net/stephenwalther/archive/2008/07/08/asp-net-mvc-tip-15-pass-browser-cookies-and-server-variables-as-action-parameters.aspx#6377484) echoed my immediate skepticism over whether this could be safe. After playing around I believe I have confirmed my suspicions that making use of this capability is a Very Bad Idea.

I'll let Francois' comment explain the problem (emphasis mine):

{% blockquote %}
Tuesday, July 08, 2008 4:16 PM by Francois Ward

Hmm, I didn't look into it much, but is that -safe-? I mean, if the variables in the index function are filled up automatically... it would be ok if it was only one type (all cookies, or all server variables), but since you can mix and match, **whats to present me from forging a request with a cookie with the same name as the server variables**?

I mean, it is already possible to forge anything client-related, for obvious reason... but **forging info that potentially come from the server**? That seems...awkward...

(again, keep in mind this is just my first reaction, maybe if I think it through I'll realize what I just said is totally stupid :) )
{% endblockquote %}

Like Francois said, cookies are easily forged client-side anyway, but most developers tend to rely on the [truthiness](http://en.wikipedia.org/wiki/Truthiness) of our server variables, specifically those that don't come over in the HTTP request. Let me demonstrate how the issue plays out.

For our example I have created a method on the HomeController named Test that takes four parameters that match server variable names. Below are the descriptions of each from MSDN's [IIS Server Variables](http://msdn.microsoft.com/en-us/library/ms524602.aspx) article:

* **REMOTE_ADDR** MSDN Says, "The IP address of the remote host that is making the request."
* **LOGON_USER** MSDN Says, "The Windows account that the user is impersonating while connected to your Web server. Use REMOTE_USER, UNMAPPED_REMOTE_USER, or AUTH_USER to view the raw user name that is contained in the request header. The only time LOGON_USER holds a different value than these other variables is if you have an authentication filter installed."
* **REQUEST_METHOD** MSDN Says, "The method used to make the request. For HTTP, this can be **GET**, **HEAD**, **POST**, and so on."
* **SERVER_NAME** The server's host name, DNS alias, or IP address as it would appear in self-referencing URLs.

My biggest concern is **LOGON_USER**. As described by MSDN this variable normally stores whatever the value of REMOTE_USER, UNMAPPED_REMOTE_USER, or AUTH_USER is, except when you have a third party authentication filter installed. The purpose of these authentication filters is to map the value of one of the above three request variables to a different local name. For example, you may be using a filter to map "DOMAINTroyGoode" to "DOMAIN-DMZStandardUser" and to map "DOMAINScottGuthrie" to "DOMAIN-DMZAdministrator". If you are using such a filter and then add LOGON_USER as a parameter to one of your actions, you are suddenly opening up the ability for anyone to circumvent that authentication filter.

Here is the action I have created:

{% gist 4001511 Example-1.cs %}

And here is what this action will output without any fiddling:

{% postimg Unhacked_2.jpg %}

Now I'll tweak the Url a bit:

{% blockquote %}
First Url:

http://localhost:63260/Home/Test

Second Url:

http://localhost:63260/Home/Test?REMOTE_ADDR=Any.IP.I.Want&LOGON_USER=YourDomainAdministrator&REQUEST_METHOD=POST&SERVER_NAME=microsoft.com
{% endblockquote %}

And voil&agrave;, mischief achieved:

{% postimg TotallyHaxxored_2.jpg %}

Now I'm no Kevin Mitnick, but I can assure you that if I can come up with something like this in an hour or so, a dedicated hacker that is probably far smarter than I will likely give you a lot of heartache if you make use of this feature. Have any "developer mode checks" that check for 127.0.0.1 or localhost? Have any filters to try and prevent GETs on certain actions? Relying on server variables passed in to your actions would make those scenarios (and many others) unwise. Just say no.

In my opinion this feature (the server variable portion) should just be removed from the MVC framework entirely or something should be put in place to prevent overwriting parameters named the same as a server variable. 