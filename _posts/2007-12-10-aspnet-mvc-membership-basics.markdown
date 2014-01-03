--- 
layout: post
title: ASP.Net MVC Membership Basics
excerpt: How to integrate ASP.NET Membership into the recently release ASP.NET MVC framework.
sharing: false
---

<p class="warning">This article was written for the December 2007 CTP release of the MVC framework. It does not entirely apply to the Preview 2 release or subsequent releases.</p>

[The MVC bits have finally arrived](http://asp.net/downloads/3.5-extensions/) and I've spent a while digesting them. I've been waiting for the bits to be released to begin working on a side-project, so the first thing I did after downloading them last night was crank it up and start working on a new ASP.NET MVC Web Application project.

Typically, the first thing I do on a new project is set up the authentication/authorization system. For the project I am working on I want to use the ASP.Net Membership system, but most of the Membership controls do not work with the MVC framework because they require postbacks. I spent some time last night building a Security controller and views for Registration and Login that I thought would be worth sharing.

So far I have implemented basic functionality for Register, Login, and Logout. There are three files we will have to create. We will also have to change the routing table. Let's start with the SecurityController:

{% gist 3998394 SecurityController.cs %}

So we have two basic actions methods that only display views (Register and Login) and three action-only methods that have no views (CreateUser, Authenticate, Logout).

I have chosen to not have a logout page, but to instead redirect to the homepage. Switch out 'Response.Redirect( "/" );' for 'RenderView( "Logout" );' and create a Logout.aspx view if you would like to display a logout message.

Now let's look at the Login view:

{% gist 3998394 LoginView.aspx %}

The toolkit's Html.Checkbox(...) method annoys me. More on why in another post. For now I've instead just written the html out by hand. You'll note I've also linked the label to the checkbox with JavaScript so that clicking the label toggles the checkbox.

Then the Register view:

{% gist 3998394 RegisterView.aspx %}

Another straightforward view. Not much to discuss here.

And finally, let's add the new routes:
  
{% gist 3998394 global.asax.cs %}

I personally like login to be "http://website/login" and register to be "http://website/register", so that is how I have configured it. The other three actions (Logout, Authenticate, and CreateUser) I access via the default route (ex: /Security/Logout).

That's it! You should now have a working registration/login system. I'll leave making it pretty with CSS as an exercise for the reader.

I have included all of the code samples above in the below ZIP file. Just unzip it and place the controller into the Controllers directory, the views into the Views/Security directory (which you will have to create), and copy the code from Routes.txt to the appropriate area of your Global.asax.

[MVCMembership_v1.2.zip](/custom/files/old/MVCMembership_v1.2.zip)

<a name="update-dec-11"></a>
**UPDATE (Dec 11):** Johan and [Steve Harman](http://stevenharman.net/) were kind enough to [point out](#comment-152106495) that I had foolishly set the "remember me" checkbox's label's "for" attribute to point to the password field instead of the checkbox itself. I have fixed the code above and provided a new zip file (1.1) for download. Thanks guys!

<a name="update-dec-19"></a>
**Update (Dec 19):** [oVan](http://www.superwasp.net/weblog/) [pointed out a bug](#comment-152106514) in the routing rules defined in the routes.txt file. I have updated the zip file with the correct code. Thanks oVan!

<a name="update-jan-3"></a>
**Update (Jan 3):** [James Nail asked a very good question via a comment](#comment-152106536): what do you set for the loginUrl and defaultUrl in your web.config? Well James, here is how I've setup my web.config...

Assuming we'll be using forms authentication and securing all pages except login and the homepage, place the following inside the &lt;system.web&gt; element:

{% gist 3998394 webconfig-1.xml %}

Then, somewhere outside the &lt;system.web&gt; element add:

{% gist 3998394 webconfig-2.xml %}

Note that this will grant access to all actions within the Security controller. It is also worth pointing out that, dependent on your setup, your CSS and image files may not load unless you also create a location path for their directory and grant all users access like so:

{% gist 3998394 webconfig-3.xml %}

I have not updated the zip file with these web.config settings; let me know if anyone would prefer that I add it. I hope this helps some of you!