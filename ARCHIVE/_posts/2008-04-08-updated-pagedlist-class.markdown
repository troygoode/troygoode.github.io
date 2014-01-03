--- 
layout: post
title: Rob Conery's PagedList Class (Updated)
---

<p class="warning">A new, improved version of this class is now available <a href="/2011/06/28/pagedlist-can-haz-htmlhelperz/">here</a>.</p> 

Robert Muehsig has posted a great user control for the MVC framework that [adds pagination links to the bottom of a paged list](http://code-inside.de/blog-in/2008/04/08/aspnet-mvc-pagination-view-user-control/). In it he used a slightly customized version of [Rob Conery's PagedList](http://blog.wekeroad.com/2007/12/10/aspnet-mvc-pagedlistt/) class that Rob was kind enough to post way back when the first CTP was released. This reminded me that I should probably post the version I have customized, as I think it makes it a bit easier to use and maintain. Check out the code [on GitHub.](https://github.com/troygoode/pagedlist)

## Changes from Rob's version:

* **Added a "TotalPages" property.** If you're going to loop through each of the pages to display page navigation, you'll obviously need this.
* **Changed "IsPreviousPage" to "HasPreviousPage".** It just sounds better.
* **Changed "IsNextPage" to "HasNextPage".** See above.
* **Added a "IsFirstPage" property.** The opposite way of using the above two properties. I prefer this way, but kept the original way for backwards compatibility (except the naming).
* **Added a "IsLastPage" property.** See above.
* **Changed the first constructor to accept IEnumerable&lt;T&gt; rather than IQueryable&lt;T&gt;.** I'm not exactly sure why Rob originally made it IQueryable. I'm aware that by passing an IQueryable (LINQ) object to this constructor you'll avoid retrieving the entire set (only taking the results needed), but since IQueryable inherits from IEnumerable everything should be hunky-dory. He probably had a reason and I'm going to wind up breaking all of my stuff, but IEnumerable is just so much handier. =)
* **Removed the second constructor.** The second constructor took List&lt;T&gt;, which is unnecessary after changing the first constructor to accept IEnumerable.
* **Cleaned up property declarations a bit.** Mainly to make the page a bit shorter, but also to prevent the multiple calculations that could happen in the original. Also the original allowed the changing of certain properties after an instance was created, which would put the instance into an inconsistent state.
* **Added argument checking and handled a few exception scenarios more gracefully.** Trying to make debugging a bit friendlier. </li>
* **Removed the second extension method that didn't specify a pageSize.** I don't really think that baking in an extension method that sets pageSize to 10 is a good idea, I'd prefer pageSize to be explicitly set elsewhere by the calling code.
* **Moved the code to the "System.Collections.Generic" namespace.** I'm sure a lot of you are breaking out in a cold sweat to see me putting something into a System.* namespace, but I kind of feel like this is something that the .Net team just "forgot". =) Move it wherever makes you comfortable.

Please note that I took many of these ideas from the commentary below Rob's original post. I'm sure many of you are using something similar, but I thought it would be useful to get something posted online that is a bit more fleshed out than the original example.

Thanks for the great work Rob & Robert!