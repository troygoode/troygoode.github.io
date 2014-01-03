--- 
layout: post
title: "MVC: Membership Starter Kit"
---

<p class="warning">A newer version of the Membership Starter Kit is now available. <a href="/2008/04/11/mvc-membership-starter-kit-11/">Click here</a> to see what has changed.</p>

## Introduction

One of my very first blog posts (and most definitely my most popular so far) revolved around [how to integrate ASP.Net membership and forms authentication into the ASP.Net MVC framework](/2007/12/10/aspnet-mvc-membership-basics/) which had just been released in it's December CTP flavor. It has remained popular to this day, but unfortunately the Preview 2 release of the MVC framework has caused much of the code I released in that article to no longer function correctly.

Even before the release of Preview 2, I had been planning to extend the samples I was providing to offer more useful features. I don't know about you, but nearly every website I ever create with ASP.Net requires some kind of security/membership system. My preference is to use the built-in system when possible (except for the horrible Profiles sub-system). This means creating login, logout, & registration functionality every time, as well as creating administrative screens for managing the users that enter your system.

WebForms provides some controls to help with the login and registration process, but user administration has always been delegated to either (a) the built-in tool that runs separately and doesn't work remotely or (b) rolling your own solution. The development of the MVC framework seems to me like a good time to resolve this scenario and provide the community with an array of pre-built tools to help boot-strap projects so that we can stop working on infrastructure and start working on the heart of the individual application.

**With that in mind I have created a CodePlex project: the [ASP.Net MVC Membership Starter Kit](http://www.codeplex.com/MvcMembership).** It currently provides controllers and views for all of the common authentication and user administration needs, including:

* Login/Logout
* Registration
* List of Registered Users
* User Details / Administration
* Role Management

A big thanks goes out to Rob Conery, as I borrowed his recent [Authentication Filters](http://blog.wekeroad.com/blog/aspnet-mvc-securing-your-controller-actions/) and included those, along with my recently released [Error Handling Filters](/2008/04/02/mvc-error-handler-filter/).

Okay, enough wall o'text. Let's take a look at some screenshots:

## Screenshot Tour

Here is the menu when you first log in:

{% postimg Menu-LoggedOut_2.jpg %}

The registration page:

{% postimg Registration_4.jpg %}

The login page (note that the Administrator's credentials are displayed to make it easy to get started, you'll obviously want to change them and remove that note):

{% postimg Login_2.jpg %}

Having logged in, here is what the menu looks like now:

{% postimg Menu-LoggedIn_2.jpg %}

The current options upon clicking the Security tab:

{% postimg Security_2.jpg %}

Clicking "Manage Users" brings you to a list of all users currently registered:

{% postimg ManageUsers_2.jpg %}

Clicking on the user takes you to a form that allows you to view their details and edit a few aspects of their profile...

{% postimg User-Top_2.jpg %}

as well as view/change the roles they are in and help with password issues:

{% postimg User-Bottom_2.jpg %}

From the Security tab you can also go to a list of all the system's roles, from which you can add/delete roles as you need:

{% postimg Roles_2.jpg %}

Clicking on a role from the roles list or the user profile allows you to view/modify the users in that role:

{% postimg UsersInRole_2.jpg %}

And finally, the Security tab offers a link to another registration form, geared toward Administrators:

{% postimg CreateUser_2.jpg %}

## NSFAQ (Not-So Frequently Asked Questions)

**Q:** Does this compete with MvcContrib?<br>
**A:** No. [MvcContrib](http://www.codeplex.com/MVCContrib) is a great project that aims to features like alternate routing and view engines, and IoC integration. This project is simply a starter kit to help get membership-based applications off the ground a little quicker. There is no reason you could not use both projects together.

**Q:** What dependencies does using this starter kit saddle me with?<br>
**A:** Ideally, none, other than the ASP.Net Membership API. Every measure will be taken to avoid using third party libraries, be it JavaScript or .Net. We will strive to separate the code and rely on default settings as much as possible to make it easy for you to customize your installation without anything in the starter kit getting in the way.

**Q:** Who is responsible for this?<br>
**A:** Currently, only [me](https://github.com/troygoode), but I'd like that to change! I'm keenly interested in finding a few other developers that would like to contribute to enhancing the starter kit. Please [email me](mailto:troygoode@gmail.com) if you'd like to help!

## What's Next?

There are a few features missing that I would like to include in the very short term. Primarily these are features for end-users, like Change Password and Forgot My Password (both of which are currently available on the administration side). Beyond that, visual cleanup of the forms (and separation of the style sheets) as well as a bit of AJAX-ification of the forms would be nice. If you use the starter kit and have suggestions or criticisms, please post to the [Discussions forum](http://www.codeplex.com/MvcMembership/Thread/List.aspx) or [Issue Tracker](http://www.codeplex.com/MvcMembership/WorkItem/List.aspx)!

You can download the latest release from [the CodePlex project](http://www.codeplex.com/MvcMembership). I hope you all find it useful!