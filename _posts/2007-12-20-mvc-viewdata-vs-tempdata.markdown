--- 
layout: post
title: "MVC: ViewData vs. TempData"
excerpt: What is ViewData? What is TempData? I cover the difference between these two new concepts in ASP.NET MVC.
sharing: false
---

<p class="warning">This article was written for the December CTP release of the MVC framework. Unfortunately, it does not entirely apply to the Preview 2 release or subsequent releases.</p>

A commenter ([oVan](http://www.superwasp.net/weblog/)) recently [left a comment](/2007/12/10/aspnet-mvc-membership-basics/#comment-152106519) on one of my posts with a suggestion to redirect to a different URL rather than display a different view. He cautioned that if I do so to make certain I use store data in the TempData dictionary rather than ViewData. I had not yet heard of the TempData dictionary, so I set off into [Reflector](http://www.aisto.com/roeder/dotnet/) to find out what exactly it is.

Initially it appeared that TempData was simply a Dictionary&lt;string,object&gt; stored in session. This is close to the truth, but not the whole truth. In fact the internal declaration of TempData's storage device is:

{% gist 3998629 Example-1.cs %}

Why such a complicated declaration? Further digging revealed that the second element of the pair (the HashSet) is used as a kind of key synchronization device. When an item is added to the internal storage, the key is placed in the HashSet. At some point that I have been unable to determine, the key is removed from the HashSet. Then when the TempData object is built again, the discrepancy is noticed and the Dictionary entry that has a key not contained in the HashSet is removed from the Dictionary.  

The end result is that **any data stored in TempData will be around for the life the current request and the next request only**, or until the item is removed explicitly. This is useful when you want to pass data to another view that you will be redirecting to, rather than rendering to.  

{% gist 3998629 Example-2.cs %}

In the code above if you navigate to the Index page, you are automatically redirected to the Test1 action's URL. The Test1 action then executes and is able to access the data stored for it in TempData. If you then navigate to the Test2 action, that action is unable to retrieve the data previously stored in TempData.

To retrieve data from TempData inside of a view, do the following:
  
{% gist 3998629 Example-3.aspx %}

One suggestion for an improvement in the next CTP would be to have all data available in TempData copied over to ViewData at the beginning of a request (after the old TempData has been cleared). This would allow us to simply say:

{% gist 3998629 Example-4.aspx %}

Otherwise the view has to be very aware of the controller and how it plans to pass data into the view. Currently I cannot have a view that supports being passed data both ways without doing the following:  

{% gist 3998629 Example-5.aspx %}