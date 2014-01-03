--- 
layout: post
title: "MVC: New Membership Starter Kit Release"
sharing: false
---

## The Starter Kit

If you haven't had a chance to read about the MVC Membership Starter Kit I've created, [read this post first](/2008/04/02/mvc-membership-starter-kit/).

## New Release

Since we first created the starter kit a week and a half ago, [Maarten Balliauw](http://blog.maartenballiauw.be/) and I have been hard at work fleshing out the implementation to provide as much functionality as possible. Last night we finished the last stretch of things we had identified for this release and have posted the code as a new release on CodePlex. Keep in mind you can also always download our latest builds from CodePlex as well without waiting for a new release.

## New Features

### OpenID

Mads Kristensen [released a lightweight OpenID consumer](http://blog.madskristensen.dk/post/OpenID-implementation-in-Csharp-and-ASPNET.aspx) earlier this year that I then proceeded to flesh out with [a security patch](/2008/04/11/openid-check_authentication/). The reason I did so was so that I could include OpenID in this release of the Starter Kit.

Out of the box you can create a route to the OpenIDLogin action, which displays the following view:

![IMAGE](/custom/files/OpenID_2.jpg)

Once the user has entered their OpenID url, the starter kit will take care of the rest for you, with one critical exception: you have to map the url to a user in your membership database. To do so, you simple override a virtual method and return a MembershipUser, like so:

{% gist 4006125 Example-1.cs %}

Note that the above implementation maps the OpenID url to a user's UserName, which may or may not be what you want for your application. Adjust accordingly.

### Password Recovery Tools

Maarten did a great job providing users with a way to manage their passwords. While logged in they can change their password:

![IMAGE](/custom/files/ChangePassword_2.jpg)

Or if they are having trouble logging in, they can submit their username...

![IMAGE](/custom/files/ForgotPassword_2.jpg)

...and then answer their password question (if the system is configured to require it)...

![IMAGE](/custom/files/PasswordQuestion_2.jpg)

...and they will then receive their password via email (or a newly generated password -- depending on system configuration).

### Client-Side Validation

All non-administrative forms now include basic client-side validation. The validations even change based upon your Membership settings.

For instance, by default the ASP.Net Membership provider requires passwords to contain at least 1 non-alphanumeric character. If a user entered a password of "password" they would see the following alert:

![IMAGE](/custom/files/ClientSideValidation_2.jpg)

### Components: Login & LoginStatus

Maarten created components that emulate the functionality of the old Login and LoginStatus controls. Now it is easy to have a Login box on every page.

### Major Refactoring

Most of the controller and filter code has been broken out into a separate assembly.

Your FormsAuthentication and FormsAuthenticationAdministration controllers should now inherit from a base version of each. Maarten has created a boat load of virtual method hooks for each action (OnBeforeBlah, OnAfterBlah, OnErrorBlah) that provides you with easy extensibility points without needing to directly modify the starter kit base code.

Hopefully this refactoring will make it easy for you to upgrade to future versions of the starter kit's code as they become available.

## The Future

Currently we've cleared our plate and have no more planned features to attend to. Does this mean that we are done? No. This is what you can expect to see us working on next:

* Preview3 updates, whenever it becomes available.
* Validations on the administrative side.
* Bug fixes, of course. :-)
* If you have suggestions for what you would like to see in the next release, please [drop me an email](mailto:troygoode@gmail.com) and let me know!

You can [download the new release](https://www.codeplex.com/Release/ProjectReleases.aspx?ProjectName=MvcMembership&ReleaseId=12261) from CodePlex. 