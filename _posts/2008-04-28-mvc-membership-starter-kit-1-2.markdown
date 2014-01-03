--- 
layout: post
title: MVC Membership Starter Kit - 1.2
---

This weekend I posted a [new release of the MVC Membership Starter Kit](https://www.codeplex.com/Release/ProjectReleases.aspx?ProjectName=MvcMembership&ReleaseId=12667). This release is an update to migrate the starter kit to [the new interim release of the MVC framework](http://weblogs.asp.net/scottgu/archive/2008/04/16/asp-net-mvc-source-refresh-preview.aspx). If you do not feel comfortable using the interim release, please continue using the 1.1 release and wait for Microsoft to release Preview 3; we will update the Starter Kit soon thereafter.

## Changes in 1.2:

* WindowsLive is now a supported authentication scenario ([read Maarten's blog post on this](http://blog.maartenballiauw.be/post/2008/04/ASPNet-MVC-Membership-Starter-Kit-alternative-authentication.aspx)).
* Per [Andrew Arnott's](http://blog.nerdbank.net/) suggestion, the starter kit now uses the [DotNetOpenId](http://code.google.com/p/dotnetopenid/) library rather than the code previously used (which was created by Mads Kristensen). This gives us a more robust and secure implementation that will develop and improve independently of this project.
* All actions that previously expected a username in the route now expect the user's ProviderUserKey (a Guid) instead. This was done because users with OpenID urls as their username could not previously be accessed.
* "Whitelist support" has been added to the OpenID implementation, allowing you to setup regular expressions that dictate which OpenID providers are allowed to be used when logging into your site. By default there is no whitelist, so all providers are allowed.
* The starter kit now offers greater control over which authentication scenarios your site supports and which is the default. Out-of-the-box only FormsAuthentication is enabled and is obviously the default.