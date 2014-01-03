--- 
layout: post
title: MVC Authentication and Errors
---

<p class="warning">This article was written for the December CTP release of the MVC framework. Unfortunately, it does not entirely apply to the Preview 2 release or subsequent releases.</p>

I love working with the [recent CTP release](http://weblogs.asp.net/scottgu/archive/2007/12/09/asp-net-3-5-extensions-ctp-preview-released.aspx) of the ASP.Net MVC framework, but it is definitely an early release and is lacking many of the developer friendly features that we have grown to rely upon in WebForms. One such feature is WebForm's easy to understand authentication model.

In the WebForms world URLs can be referenced in a web.config file and then have authentication rules applied to them. A rule that says that you must be logged in to view a secure page may look something like:

{% gist 4007528 Example-1.xml %}

A rule that says only users in the Administrators group may view it might look like this:

{% gist 4007528 Example-2.xml %}

It didn't take long for me to run into the lack of any central authentication scheme in the new MVC framework. I searched around and found some [older information from prior to the CTP release posted by Fredrik Normen](http://weblogs.asp.net/fredriknormen/archive/2007/11/25/asp-net-mvc-framework-security.aspx) that seemed to address my issues, but unfortunately one of the features his solution requires did not make its way into the CTP release: attribute based exception handling.

Looking through the code samples on the page you see how he uses the .Net frameworks built in [**PrincipalPermission**](http://msdn2.microsoft.com/en-us/library/system.security.permissions.principalpermission.aspx) attribute (from **System.Security.Permissions**) to classify an action as demanding the user be in a specific role. If the user is not in that role the .Net framework will throw a **SecurityException**. What good does that do? Well take a look at line 3 in the below code:

{% gist 4007528 Example-3.cs %}

The **ExceptionHandler** attribute appears to take two values:

* The view to render in the event of an error.
* The type of error to match against.

So based upon this code the **PrincipalPermission** will interrogate the user's roles when the action is requested and if the user is not in the "Admin" role it will throw a **SecurityException**. At that point the **ExceptionHandler** will wake up and say "hey I can handle that" and render the view named "Error". Neat huh? Too bad **ExceptionHandler** doesn't exist...

Personally I liked most of the concepts that were introduced in Frederik's post, so I went ahead and began to implement the **ExceptionHandler** attribute. Along the way I realized that what was really needed was a way to apply filters to a controller. I've seen [Ivan Carrero's controller filter implementation](http://flanders.co.nz/blog/archive/2007/12/17/implementing-filters-in-asp.net-mvc.aspx), but I wanted filters that hooked straight into the MVC Controller's three major lifecycle events: **OnPreAction**, **OnPostAction**, and **OnError**. By doing so I felt I would minimize the difference between code in a filter and code in a controller. Thus was born the **FilterController**.

## Filter Controller

The **FilterController** is an abstract class deriving from the **System.Web.Mvc.Controller**. It's primary purpose is to interrogate itself via reflection when it is created and to then load any attributes that implement the **IControllerFilter** interface:

{% gist 4007528 Example-4.cs %}

The filters are then called for each of the three integration events: **OnPreAction**, **OnPostAction**, and **OnError**. Here is what the **OnError** event does:

{% gist 4007528 Example-5.cs %}

The **OnPreAction** and **OnPostAction** events look almost exactly like the **OnError** event.

To fulfill my initial goal of obtaining functionality similar to that described in Frederik's post, I have created two filters:

* **SecurityFilter**
* **ErrorHandlerFilter**

## Security Filter

While the **PrincipalPermission** attribute used in Frederik's post handles many security scenarios well, it wasn't as flexible or keyboard friendly as I would prefer. I created the **SecurityFilter** and an arrangement of sub-filters to create what I think is an easier solution.

To use the security filter in your controller you must first inherit from **FilterController** and apply the **[SecurityFilter]** attribute.

{% gist 4007528 Example-6.cs %}

This alone does nothing, but you are now able to add one or more of the **SecurityFilter**'s sub-filters to this controller or it's actions. The sub-filters I have created are:

* **RequireLogin** Validates that the user is logged in.
* **RequireAnonymous** Validates that the user is NOT logged in.
* **RequireRole** Validates that the user is in the specified role.
* **RequireAnyRole** Validates that the user is in at least one of the specified roles.
* **RequireEachRole** Validates that the user is in every one of the specified roles.

Let's imagine a controller for a simple bulletin board system. In order to post to this forum you must be logged in, if you want to delete a post you must be in either the "Administrators" role or the "Moderators" role, and if you want to undelete a post you must be in the "Administrators" group. That controller would look something like:

{% gist 4007528 Example-7.cs %}

By applying the [**RequireLogin**] attribute to the class you have applied that filter to all of the actions as well, which means you must be logged in to call the **Post** method. The other two methods use the appropriate version of the role requirement filters to achieve their goal.

What happens if the filter validations fail? In the case of an anonymous user attempting to access a restricted resource an **AnonymousAccessException** (which derives from **SecurityException**) is thrown while all other scenarios throw a **SecurityException**. What you do with those exceptions leads us to...

## Error Handler Filter

Using the above **ForumController**, let's add the **ErrorHandler** filter:

{% gist 4007528 Example-8.cs %}

Like with the last filter, this filter by itself does nothing but allow us to use the **ErrorHandler** sub-filter. Let's go ahead and add two sub-filters: one to handle security exceptions and one to handle all other exceptions.

In the event of a security exception we'll render the "AccessDenied" view while all other exceptions will render the "SystemError" view:

{% gist 4007528 Example-9.cs %}

First notice that we are now providing the **ErrorHandlerFilter** attribute with an option that says **ErrorHandlerMode.Render**. This is because in the event of an error we want the controller to render the view with the name passed in. Later on we'll look at the other mode: **ErrorHandlerMode.Redirect**.

Next notice that we are providing three values to each of the two **ErrorHandler** sub-filters:

* The order in which the sub-filter should be processed. This is important because the order the attribute is returned by reflection is unknown.
* The name of the view to render. Just like calling **RenderView**() from an action, this view name must be accessible to the controller (either in the controller's view directory or in the **Shared** directory).
* The type of the exception to match against.

Keep in mind that these sub-filters could be applied at either the class level or the method level and that method-level sub-filters are processed before class-level sub-filters. We'll stick with class-level throughout this article.

An "Access Denied" page popping up whenever we try to go somewhere we aren't allowed to without logging in isn't the best user experience. Let's improve it by sending anonymous users to the login page instead. This time however, we don't want just render the login view, we want to actually redirect to the **SecurityController**'s **Login** action. While we're at it, I'll show you an example of handling multiple exceptions with one sub-filter:

{% gist 4007528 Example-10.cs %}

Here in line 1 we specify not the name of the view to render, but the name of the action and controller to redirect to (in the format "action,controller"). The handler knows to process this as a redirect because we've changed the mode for this one sub-filter to **ErrorHandlerMode.Redirect**. Lines 7, 8, & 9 illustrate the capability for one sub-filter to match against many exceptions.

## Download The Code

I hope you find these filters useful. If they don't happen to match your particular problem, then feel free to write your own filters. To do so you only have to implement the **IControllerFilter** interface. I've attempted to make it even easier to do so by providing a base class named **ControllerFilter** that already implements the interface and has several hooks for you to take advantage of.

You are free to use or modify this code for anything including commercial purposes. The only restriction I ask is that you do not take credit for this work yourself, but I do not require any specific attribution.

I have packaged the code into four different releases:

<p class="warning">These projects were built on Vista x64. If you are running a 32-bit version of Windows you may initially have trouble building the source versions below. To fix this you'll need to re-add the System.Web.Extensions reference. <a href="#comment-152106318">See my comment below for more details.</a></p>

**Full Source Release
**The entire solution zipped up.
{% postdownload MVC+Controller+Filters+(Source+and+Example+Website).zip %}

**Example Site & Binaries Release**
**(Recommended)** Just the binaries zipped up with an example site.
{% postdownload MVC+Controller+Filters+(Example+Website).zip %}

**Filter Source Release**
The source code for the FilterController and filters. No example site.
{% postdownload MVC+Controller+Filters+(Source).zip %}

**Filter Binaries Release**
The binaries for the FilterController and filters only. No source.
{% postdownload MVC+Controller+Filters+(Binaries).zip %}