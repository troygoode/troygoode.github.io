--- 
layout: post
title: PagedList can haz HtmlHelperz?
excerpt: My PagedList library now has a new friend. I've just published a new PagedList.Mvc library which gives your ASP.NET MVC project a new HtmlHelper for generating pager controls for your IPagedList instance.
---

Wow, has it already been almost two years since my [last post](/2009/08/15/spearmen-javelin-throwers-and-the-state-pattern-oh-my/)?

Back in March I made some modifications to the [PagedList library](http://github.com/troygoode/pagedlist) I've been working on the last couple of years to (finally) add an HTML Helper that can generate the HTML paging code you frequently will use alongside the PagedList library. Over the last few days I have tweaked everything a bit more and am now quite happy with where the library sits.

## Installation

You can install via Nuget:

{% cmdline <a href="http://nuget.org/packages/PagedList">PM&gt; Install-Package PagedList</a> %}
{% cmdline <a href="http://nuget.org/packages/PagedList.Mvc">PM&gt; Install-Package PagedList.Mvc</a> %}

Alternatively, [download the source](https://github.com/TroyGoode/PagedList) straight from GitHub.

## What does PagedList do again?

The first package contains the core PagedList library, which allows you to do this:

{% gist 1053069 %}

## Okay, so what is PagedList.Mvc then?

The second package contains the new HTML Helper which lets you render pagers that look like this:

{% postimg DefaultPagingControlStyles.png %}

*Note: All elements rendered by the HTML Helper have CSS classes applied to allow you to easily modify styling.*

You call the HTML Helper like so:

{% gist 1053136 %}

## Customizing the Rendering Options

There are several out-of-the-box render configurations as you can see above, and you can also pass ad-hoc render configurations to the render method (also shown above). Sometimes, though, you want to create a render configuration that will be used repeatedly throughout your application. This is easy to do as well:

{% gist 1053159 %}

For more information on the available rendering option settings, check [PagedListRenderingOptions.cs](https://github.com/TroyGoode/PagedList/blob/master/src/PagedList.Mvc/PagedListRenderOptions.cs) on GitHub.
