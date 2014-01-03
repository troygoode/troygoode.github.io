--- 
layout: post
title: Return of the PagedList
excerpt: "A few days ago, Craig Stuntz reported an interesting observation: when the first page is returned, the class performs a Skip(0). Suprisingly, this is not free. With that in mind, I set out to correct that issue as well as incorporate a few changes I've made over the past year. The result is nearly identical to the last posted version, just a bit more readable. Additionally..."
---

<p class="warning">I recommend grabbing the latest code from NuGet or directly from <a href="https://github.com/troygoode/pagedlist">the project's site on GitHub</a>. The CodePlex project is no longer maintained.</p>

It has been nearly a year since I [posted](/2008/07/08/PagedList-Strikes-Back/) an updated version of the PagedList&lt;T&gt; functionality originally [created by Scott Guthrie and posted by Rob Conery](http://blog.wekeroad.com/blog/aspnet-mvc-pagedlistt). Since then I have used the class in a number of projects and find it indispensable.

A few days ago, Craig Stuntz reported an interesting observation: when the first page is returned, the class performs a Skip(0). Suprisingly, [this is not free](http://blogs.teamb.com/craigstuntz/2009/06/10/38313/). With that in mind, I set out to correct that issue as well as incorporate a few changes I've made over the past year. The result is nearly identical to the last posted version, just a bit more readable. Additionally...

* The source is now available on CodePlex: [http://pagedlist.codeplex.com](http://pagedlist.codeplex.com). This should make finding and downloading the code easier than finding the correct blog entry on some dude's blog.
* I have posted a release-compiled, XML commented, signed assembly on CodePlex. I got tired of having to copy the source into multiple projects and finding a place to put it in that project's taxonomy.
* Further incremental changes can be found in the Change Log on the CodePlex project site.