--- 
layout: post
title: MVC Membership Starter Kit Released
excerpt: Almost six months after the official release of Asp.Net MVC 1.0 and nearly a year after the last release of the starter kit, I've finally rewritten and released the Asp.Net MVC Membership Starter Kit.
sharing: false
---

<p class="warning">These instructions are out of date, and a newer version of the Membership Starter Kit is now available with support for ASP.NET MVC 4 and installation via NuGet. For more information, <a href="https://github.com/troygoode/membershipstarterkit">look the project up on GitHub</a>.</p>

Almost six months after the [official release of Asp.Net MVC 1.0](http://haacked.com/archive/2009/03/18/aspnet-mvc-rtw.aspx) and nearly a year after [the last release of the starter kit](/2008/09/06/mvc-membership-preview-5/), I've finally rewritten and released the Asp.Net MVC Membership Starter Kit. If you're already familiar with what it is and want to grab it, you can find the release on the [GitHub project site](https://github.com/troygoode/membershipstarterkit).

## What is the Asp.Net MVC Membership Starter Kit?

The starter kit currently consists of two things:

* A sample website containing the controllers, models, and views needed to administer users & roles.
* A library that provides testable interfaces for administering users & roles and concrete implementations of those interfaces that wrap the built-in Asp.Net Membership & Roles providers.

## How do I use it?

In Asp.Net MVC 1 there isn't a great story for packaging & sharing controllers, views, and other resources so we'll need to follow a few manual steps:

* After getting the source code build it using your preferred IDE or using the included **Build.Debug.bat** or **Build.Release.bat** batch files.
* Grab the **MvcMembership.dll** assembly and place it wherever you're including external libraries in your project. Add a reference to the assembly to your Asp.Net MVC application.
* Copy the **UserAdministrationController.cs** file from the *SampleWebsite*'s *Controllers* directory to your app's *Controllers* directory.
* Copy the **ISmtpClient.cs** file, **SmtpClientProxy.cs** file, and **UserAdministration** folder from the *SampleWebsite*'s *Models *folder to your app's *Models* folder.
* Copy the **UserAdministration** folder from the *SampleWebsite*'s *Views* folder to your app's *Views* folder.
* Make sure you've configured your **web.config** properly for Membership and Roles. If you aren't sure of how to do this, take a look at the first two articles in [this series by Scott Mitchell at 4GuysFromRolla](http://www.4guysfromrolla.com/articles/120705-1.aspx).
* Finally, add the following code to your **global.asax** to keep the membership system updated with each user's last activity date:

{% gist 3998701 %}

## What is new since the last release?

Well, the last release was for [Preview 5](http://mvcmembership.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=16809), so at the very least the project has been updated for Beta and finally Release. Moreover, the project has been completely rewritten from scratch - a major undertaking that was the primary cause of the long delay between releases. Why the rewrite? Two reasons:

* The [first release of the Starter Kit](http://mvcmembership.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=12215) was for Preview 2 of the MVC framework. A lot changed between Preview 2 and Release - A LOT. A lot of the features of the first starter kit were rolled into the OTOB experience (such as login and registration), so I shifted the scope of the project more squarely into the realm of user & role administration. Unfortunately all of these major changes took a toll on the source - I was no longer happy working in the source as it was written for many reasons and thus wanted a rewrite. One of those reasons was...
* Previous releases had no (as in zero, less than one, nada) unit tests. This became increasingly unacceptable to me and trying to add unit tests after-the-fact was a nightmare. Instead I rewrote the project using TDD.

Alright, so that was basically the long-winded spiel to prepare you for the bad news: the project regressed from a functionality perspective. During the course of the rewrite things some things didn't make it in - chief among them is the OpenID integration. I encourage everyone to take a look at the Maarten Balliauw (an MvcMembership contributor) blog post on [authenticating via RPX in MVC](http://blog.maartenballiauw.be/post/2009/07/27/Authenticating-users-with-RPXNow-%28in-ASPNET-MVC%29.aspx).

## What comes next?

The primary motivator for me getting off my but after nearly a year and finishing up this release is my desire to convert it to an ["area" for use in MVC 2](http://weblogs.asp.net/scottgu/archive/2009/07/31/asp-net-mvc-v2-preview-1-released.aspx). Packaging reusable components like this has been a sore spot for the current MVC framework and I'm glad to see the blue badges are going to provide a common solution. Along with that I'll likely try to add RPX authentication ala Maarten's post.