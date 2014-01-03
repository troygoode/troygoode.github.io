--- 
layout: post
title: SSL Links/URLs in MVC
---

A couple days ago a reader sent me a question regarding how to use SSL with the MVC framework. Specifically the reader wanted to know the easiest way to make an Ajax call to a HTTPS page from a non HTTPS page. The tricky part here is to do so without hard-coding any URLs (as one of the best practices of the MVC framework is that views and controllers should be divorced from the routes that access them).

Currently an ActionLink like:
  
{% gist 4005985 Example-1.aspx %}

... outputs the following hyperlink:

{% gist 4005985 Example-2.html %}

I've created a few extension methods that enable you to fully-qualify these URLs and change the protocol to HTTPS at run-time. Using the extension method "ToSslLink" like so:

{% gist 4005985 Example-3.aspx %}

... now outputs a hyperlink with protocol, server, port, etc:

{% gist 4005985 Example-4.html %}

If you're using Ajax, however, you'll be more interested in getting to the URL directly rather than building an entire hyperlink. In that case you can use "ToSslUrl":

{% gist 4005985 Example-5.aspx %}

... to output this:

{% gist 4005985 Example-6.txt %}

Here is the code for the extension methods I've created, hopefully some of you will find this useful:

{% gist 4005985 Example-7.cs %}

One important missing element from all is this posting to a form via HTTPS from a non-HTTPS page. You can certainly create your form tags manually and populate the action attribute using Url.Action(...).ToSslUrl(), but you cannot try and combine these extension methods with the Html.Form(...) helper that is most commonly used inside a using statement.

**Note**: The above code was updated on July 7th, 2008 to fix a bug reported by [Mike Hadlow](http://mikehadlow.blogspot.com). Thanks Mike!