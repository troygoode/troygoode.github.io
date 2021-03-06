--- 
layout: post
title: MVC Template Fix
---

Several weeks ago [I blogged about a bug](/2007/12/11/mvc-bug-broken-codebehind/) I found in the [CTP release of the MVC framework](http://weblogs.asp.net/scottgu/archive/2007/12/09/asp-net-3-5-extensions-ctp-preview-released.aspx). The gist of the bug is that controls declared in the html portion of a page or user control could not be referenced from the code-behind of that page or user control. ScottGu posted a comment clarifying that [this was due to a bug in the templates](/2007/12/11/mvc-bug-broken-codebehind/#comment-152106388) released in the CTP. 

After nearly a month of following Scott's advice on how to fix the bug, I finally grew tired of such a repetitive task and have fixed the templates. You'll find a zip file containing the fix and installation instructions at the bottom of this post. Before I get into that though, let me explain the workaround (suggested by ScottGu) that I've been using until now: 

## The Easy, Repetitive Workaround

The problem is that the template for pages, master pages, and user controls only include the declarative HTML file and it's code-behind. The template is missing the designer file used by Visual Studio to hide the references generated by the server controls declared in your HTML. Here is what a MVC page looks like right after creation: 

![IMAGE](/custom/files/old/SimpleWorkaround-Before_2.jpg) 

You could manually create a designer class to fix this issue, but that would be a pain. Luckily Visual Studio provides us with the handy "Convert to Web Application" feature to generate the designer class for us. Simply right-click on the .ASPX (or .Master or .ASCX) and click the "Convert to Web Application" option on the context menu. 

![IMAGE](/custom/files/old/SimpleWorkaround-During_2.jpg) 

After doing so, you should see this: 

![IMAGE](/custom/files/old/SimpleWorkaround-After_2.jpg) 

*Voil&agrave;*! Your codebehind file for that page/control (and that page/control only) should now work as expected. Its quick and simple, but I'm sure you can imagine already the annoyance at having to do this for each and every page or control you create. That thought leads us to... 

## The Slightly More Complex Permanent Solution

First, an admission of guilt: I only fixed the C# templates. I apologize to any VB.Net'ers out there ahead of time. 

There are four templates that must be fixed: 

* View Master Page - A master page.
* View Page - A normal page.
* View Content Page - A page that uses a master page.
* View User Control - A control.

These four templates are found in two different places: the *item templates* folder and the item *templates cache* folder. 

Those folders are found at: "*Common7IDEItemTemplatesCSharpWeb1033*" and "*Common7IDEItemTemplatesCacheCSharpWeb1033*" respectively (within your VS 9 root folder). 

The item templates folder contains zip files which are at some point unzipped and stored in the item *templates cache* folder. I suppose you could just update the contents of the *item templates cache* folder, but I don't know if those settings would ever be written over by the *item templates* folder, so it is better to be safe and fix it in both places. 

![IMAGE](/custom/files/old/TemplateFix-ZipLocation_2.jpg) 

![IMAGE](/custom/files/old/TemplateFix-CacheLocation_2.jpg) 

The contents of a typical template for pages/user controls are: 

* a *vtemplate* file (which serves as the manifest for the rest of the items)
* an icon file
* an html file (ASPX, ASCX, etc)
* a codebehind file
* a designer file

As you can see from the screenshot below, the templates did not include a designer file. 

![IMAGE](/custom/files/old/TemplateFix-ZipContents_2.jpg) 

In addition, the vtemplate file does not describe a designer file. 

![IMAGE](/custom/files/old/TemplateFix-TemplateFile_2.jpg) 

The fix is to add a line to the vtemplate file referencing a designer file and then add the designer file itself to the template folder/zip. The designer file should look something like: 

![IMAGE](/custom/files/old/TemplateFix-DesignerFile_2.jpg) 

I've gone ahead and done all this for you -- well, actually for me, but you get the point. :-) All you need to do is download the zip file from below and follow the instructions included in the readme.txt file.

[MVC+Template+Fix.zip](/custom/files/old/MVC+Template+Fix.zip)