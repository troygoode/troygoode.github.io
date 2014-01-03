--- 
layout: post
title: OpenID Check_Authentication In C#
---

Earlier this year Mads Krisensen (of [BlogEngine.net](http://www.dotnetblogengine.net/) fame) posted a [lightweight implementation of OpenID](http://blog.madskristensen.dk/post/OpenID-implementation-in-Csharp-and-ASPNET.aspx) using C#. In the comments on Mads' post, [Andrew Arnott](http://blog.nerdbank.net/) ([a developer of the DotNetOpenId library](http://blog.nerdbank.net/2008/04/dotnetopenid-2.html)) mentioned that the example Mads had posted could "be hacked with a single change of a word in the URL." This is what is technically referred to as a **Very Bad Thing**. Andrew and another poster named "neil" went on to elaborate that implementing OpenID's "check_authentication" algorithm would close this security hole. Unfortunately as of the writing of this article neither Mads nor any of the commentors have provided an implementation of check_authentication that works with the class Mads posted (bear in mind that Andrew only brought up this issue less than a week ago, so Mads may very well be working on it).

Fast forward to yesterday when I was researching my options for implementing OpenID for the next release of the [ASP.Net MVC Membership Starter Kit](http://www.codeplex.com/MvcMembership). I liked Mads solution more than the other OpenID libraries that are current available because of its brevity and how easy it is to include it in a project without introducing an extra assembly dependency, so I decided to go ahead and add the check_authentication functionality. A quick read of [that portion of the OpenID spec](http://openid.net/specs/openid-authentication-1_1.html#mode_check_authentication) and a couple hours of coding/testing and I think I'm about finished.

Here is the method you need to add to Mads' class:

{% gist 4006111 Example-1.cs %}

Now you need to make sure that method is called from somewhere. I chose to make the method private and just call it from inside the Authenticate method. Within the Authenticate method replace:

{% gist 4006111 Example-2.cs %}

... with...

{% gist 4006111 Example-3.cs %}

And that's it!

If anyone with more experience than I in OpenID waters sees a problem with my implementation, let me know and I will try to get it fixed quickly.

**NOTE:** I am aware that Mads' implementation and the use of 'check_authentication' is considered an overly chatty use of OpenID. It seems to me that the extra complexity required to implement OpenID 2.0 protocol is just not worthwhile for most OpenID consumers. Feel free to let me know why this is a stupid position to take.