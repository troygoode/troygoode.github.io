--- 
layout: post
title: PagedList Strikes Back
sharing: false
excerpt: Yet another update to my PagedList library.
---

<p class="warning">There is an updated version of the PagedList&lt;T&gt; code available <a href="https://github.com/troygoode/pagedlist">on GitHub</a>.</p>

A few months ago I posted about [my changes](/2008/04/08/updated-pagedlist-class/) to [Rob Conery's PagedList](http://blog.wekeroad.com/2007/12/10/aspnet-mvc-pagedlistt/) class. Since writing that article many comments have been left about how to further improve the design, which I have since incorporated into a new, further improved PagedList class. For those who aren't familiar, the PagedList class allows scenarios such as the following:

{% gist 4001462 Example-1.cs %}

So in the above scenario (an example MVC action), if you had a list of 30 products with IDs 1-30 and passed in a pageIndex of 2 you would pass the products with IDs 21-30 into your view. Best of all it uses IQueryable, so if you pass a LINQ expression or IQueryable result into ToPagedList(...) it will do the filtering on the database side! Take a look at the new improved PagedList [on GitHub](https://github.com/troygoode/pagedlist).

**Changes Since Previous Version:**

* **Changed "TotalPages" property to "PageCount".** This was done to more clearly illustrate the purpose of this property (more in line with the standard List&lt;T&gt; .Count property).
* **Changed "TotalCount" property to "TotalItemCount".** This was done to differentiate between Pages and Items more clearly.
* **Switched "PageIndex" back to being a zero-based index, rather than one-based.** The PageIndex was set to be one-based because the most common use-case that I could think of, outputting a pager (i.e.: Previous - 1 - 2 - 3 - Next), was one-based and it would be nice to not have to do "obj.PageIndex + 1" everywhere. This was probably a poor decision. Some people found it confusing and [others found that it didn't work with the standard .Net DataPager](/2008/04/08/updated-pagedlist-class/#comment-152106122).
* **Added "PageNumber" property.** As suggested [via comment](/2008/04/08/updated-pagedlist-class/#comment-152106104) on my previous post, PageNumber is just PageIndex + 1.
* **Treat all data as IQueryable for the duration of class initialization.** Tgmdbm, a frequent contributer on the [ASP.Net MVC forums](http://forums.asp.net/1146.aspx), pointed out [some good reasons](/2008/04/08/updated-pagedlist-class/#comment-152106101) why everything should be treated as IQueryable before the .Skip(x).Take(y). I'm not 100% sold that treating it as IEnumerable would also work for IQueryable objects (since IQueryable supports IEnumerable), but to be safe I have defaulted everything to using IQueryable. You can still pass in a strict IEnumerable, but it will be converted to IQueryable for the duration of the PagedList class initialization process.
* **Tested with [xUnit.net](http://www.codeplex.com/xunit).** While the PagedList class isn't exactly rocket science, I want to strive to improve the quality of the code that I'm presenting to the community. I also enjoyed having an excuse to try out xUnit's data-driven testing using the [Theory] attribute (similar to MbUnit's [RowTest]) for the first time. Not much has been written about it, but I was able to find [some examples from Ben Hall](http://blog.benhall.me.uk/2008/01/introduction-to-xunitnet-extensions.html) which got me up and testing quickly.

A big thanks to everyone who has thrown their two cents in to help improve this small but useful class!