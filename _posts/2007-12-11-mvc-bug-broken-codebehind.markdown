--- 
layout: post
title: "MVC Bug: Broken Codebehind"
excerpt: A bug in ASP.NET's Visual Studio template may leave you stumped.
sharing: false
---

<p class="warning">This post is no longer relevant with recent releases of ASP.NET MVC.</p>

**Update (Jan 2):** I have posted [a permanent fix in the form of modified Visual Studio templates in another blog post](/2008/01/02/mvc-template-fix/).

**Update (Dec 11):** ScottGu was kind enough to [post a fix for this particular problem](#comment-152106388). It is unfortunate that you have to repeat the fix for every view you add to your project, but I guess that's why it is called a CTP! ScottGu said... 

<blockquote>
There is a bug in the file template when you create a new page - and the .designer.cs file isn't generated.
	
To fix this, right click on the file and choose the "Convert to Web Application" menu item. This will generate the .designer.cs file that contains your control declarations. From that point on the code-behind will be kept up to date as you make changes.

Hope this helps,

Scott
</blockquote>

**Original Post:**

It may not be a popular choice, but I'm perfectly okay with in-line code in my views as long as it doesn't contain business logic. One of the developers on my project prefers tag-based views (&lt;asp:Blah runat="server" /&gt;) and came to me yesterday with a curious issue. I've spent some of the morning investigating the issue and it does appear that there is a bug in the [newly released ASP.Net MVC Framework](http://weblogs.asp.net/scottgu/archive/2007/12/09/asp-net-3-5-extensions-ctp-preview-released.aspx).

The issue? **Controls declared in an ASPX are not visible to that page's codebehind.**

To test this hypothesis (and make sure we hadn't somehow broken our project) I started a new "ASP.Net MVC Web Application" project (note that the bug also exists for the "ASP.Net MVC Web Application and Test" project). I then opened the Views/Home/About.aspx file and added the following line:

{% gist 3998555 About.aspx %}

Then I opened up the page's codebehind (Views/Home/About.aspx.cs) and added the following:

{% gist 3998555 About.aspx.cs %}

Ctrl+Shift+B to compile and bam, a build error: **"The name 'myLiteral' does not exist in the current context."**

I began to wonder if this was not supported by the MVC framework, but I took a look back at one of ScottGu's most recent articles, [ASP.Net MVC Framework (Part 3): Passing ViewData from Controllers to Views](http://weblogs.asp.net/scottgu/archive/2007/12/06/asp-net-mvc-framework-part-3-passing-viewdata-from-controllers-to-views.aspx), and saw that he references controls from the page's codebehind several times. I also tried using several controls besides the Literal to no avail. 

To test what was going on, I commented out the reference in the Page_Load method, added a string declaration and put a breakpoint on it. When I ran the MVC application in debug mode and loaded the page I was able to see the "myLiteral" control reference. It appears that the reference is available at runtime but Visual Studio just is not able to see it at compile time. Odd. 

![IMAGE](/custom/files/old/DebugMyLiteral.jpg)

For now I've told the developer to just use in-line code, but I'm well aware that many developers are loathe to do so. Thoughts? Suggestions?