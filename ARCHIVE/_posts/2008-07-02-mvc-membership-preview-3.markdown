--- 
layout: post
title: MVC Membership - Preview 3
---

Tonight we posted a [new release of the MVC Membership Starter Kit](http://www.codeplex.com/MvcMembership/Release/ProjectReleases.aspx?ReleaseId=14919). This release is an update to migrate the starter kit to [the new Preview 3 release of the MVC framework](http://weblogs.asp.net/scottgu/archive/2008/05/27/asp-net-mvc-preview-3-release.aspx). While several bugs have been squashed, no major new functionality has been added.

If you couldn't wait and downloaded the source prior to the official Preview 3 release, you'll still want to download tonight's official release as several important issues have been addressed.

## Bug Fixes in 1.3:

* The System.Web.Abstraction, System.Web.Mvc, and System.Web.Routing DLLs being used prior to tonight were from an earlier Preview 3 release and were not signed by Microsoft.
* Routing ambiguities caused a 404 error to occur when a user enters an incorrect username/password combination on the login page.
* Several errors were occurring on various password recovery screens which created a bad user experience.
* Errors on the administration section's Create User and Display User pages were preventing that functionality from working at all.

Thanks for all of the interest everyone has shown in this project and thanks to [Maarten](http://blog.maartenballiauw.be/) and [Greg](http://gregorybeamer.spaces.live.com/blog/) for their contributions; especially to Maarten as he did most of the grunt work in upgrading our code base to Preview 3. Please send any feedback you have our way, we'd love to hear it!