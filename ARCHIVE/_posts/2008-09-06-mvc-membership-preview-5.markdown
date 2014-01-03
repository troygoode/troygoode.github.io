--- 
layout: post
title: MVC Membership - Preview 5
sharing: false
excerpt: A new version of the MVC Membership Starter Kit is now available, bringing support to ASP.NET MVC Preview 5.
---

<p class="warning">This post is out of date. Please reference this project's <a href="https://github.com/troygoode/membershipstarterkit">GitHub page</a> for the latest code.</p>

Last weekend I posted a release of the MVC Membership Starter Kit that targets [Preview 5 of the ASP.Net MVC framework](http://haacked.com/archive/2008/08/29/asp.net-mvc-codeplex-preview-5-released.aspx). There was no packaged release targeting [Preview 4](http://weblogs.asp.net/scottgu/archive/2008/07/14/asp-net-mvc-preview-4-release-part-1.aspx) (though if you [downloaded the latest source](http://www.codeplex.com/MvcMembership/SourceControl/ListDownloadableCommits.aspx), it worked), so this release essentially packages the changes from both previews.

## Why does the MVC Membership Starter Kit still exist?

The biggest change affecting the MVC Membership Starter Kit is that the default ASP.Net MVC Web Application project includes [login/logout, register, and change password functionality as of Preview 4](http://weblogs.asp.net/scottgu/archive/2008/07/14/asp-net-mvc-preview-4-release-part-1.aspx). This is great and I'm glad to see them do it. It is my hope they will eventually include membership administration functionality as well. For now, however, they haven't included it. This leaves three main features that are now driving the existence of this project:

* **Membership Administration:** What good is registration, login, and logout if a site's owner cannot change a user's roles or manage their user base? While Visual Studio's [Web Site Administration Tool](http://msdn.microsoft.com/en-us/library/yy40ytx0.aspx) covers most of your bases during development, it isn't a real option for use by web site administrators in a production application.
* **OpenID Integration:** Preview 4's built-in login functionality only covers the use of standard username/password authentication. It seems likely that this will continue, as I am not aware of any plans for official [OpenID](http://openid.net/) support within the .Net framework. We are using [Andrew Arnott & co's DotNetOpenId](http://code.google.com/p/dotnetopenid/) project to help you let your users log in using OpenID.
* **WindowsLive LiveID Integration:** [Maarten](http://blog.maartenballiauw.be/)'s [LiveID](http://dev.live.com/liveid/) integration has unfortunately not made this release. I anticipate that it will be available again for the next release, which will target [the first beta release of ASP.Net MVC](http://haacked.com/archive/2008/09/05/mvcfutures-and-asp.net-mvc-beta.aspx).

## What has changed?

* **No more login/logout/password retrieval.** Because the AccountController and its views are now included by default in new projects, the need for this functionality has gone away.
* **Less assemblies to reference.** Rather than the MembershipAdministrationController and other code being compiled into a separate assembly that you must include, you now drop the controllers directly into your web app. This allows you to easily change the code as your project evolves. The starter kit's implementation is really just a starting point that you can build off of.
* **Controllers are split.** In previous releases the OpenID and WindowsLive login functionality was included in the MembershipAuthenticationController. The OpenID functionality has since been moved into a separate controller with separate views. This was done because (a) the MembershipAuthenticationController no longer exists and (b) moving forward more of the pieces of this kit will be separated from each other so that you can include them a la carte.

## Download

You can download the Preview 5 release of the MVC Membership Starter Kit from CodePlex:

[http://www.codeplex.com/MvcMembership/Release/ProjectReleases.aspx?ReleaseId=16809#ReleaseFiles](http://www.codeplex.com/MvcMembership/Release/ProjectReleases.aspx?ReleaseId=16809#ReleaseFiles)