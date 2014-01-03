--- 
layout: post
title: "CssHandler: CSS + Variables"
excerpt: Hacking together Rory Blyth's CSS code from 2004 alongside a few changes from Gabe Moothart and myself to create a basic CSS preprocessor in .NET.
sharing: false
---

<p class="warning">Please don't consider using this project anymore - it is old and out of date. <a href="http://www.hanselman.com/blog/CoffeeScriptSassAndLESSSupportForVisualStudioAndASPNETWithTheMindscapeWebWorkbench.aspx">Use something like SASS or LESS instead.</a></p>

It's been more than three and a half years since [Rory Blyth](http://www.neopoleon.com) said ["screw standards - let's add variable support to CSS right this minute."](http://www.neopoleon.com/home/blogs/neo/archive/2004/03/06/8705.aspx) In retrospect, adding variables to CSS seems obvious; [everyone is asking about it](http://www.google.com/search?q=css+variables). There have even been [one](http://www.codeproject.com/useritems/CSSVariables.asp) or [two](http://aspnetresources.com/articles/variables_in_css.aspx) other .Net solutions developed, though Rory's solution has remained my favorite. With CSS2 barely working and [CSS3 not addressing the issue](http://www.w3.org/Style/CSS/current-work), those of us who are tired of repeating ourselves throughout our CSS files have no choice but to take matters into our own hands.

With that in mind I set out to update Rory's code. Not that there was anything wrong with it in the first place, of course (this is where I try to tactfully avoid being drawn as a jerkasaurus in one of Rory's comics), but Rory did whip the code up for a [PADNUG](http://www.padnug.org/) meeting and that *was* about 3.5 million years ago (give or take many orders of magnitude). It was time to bring the CssHandler into 2007! Conveniently right before 2008.

Lo and behold, someone else had beat me to it. [Gabe Moothart](http://codingpatterns.blogspot.com/) had already [uploaded the CssHandler to CodePlex](http://www.codeplex.com/CssHandler/), and even improved upon it. While Rory's code originally only allowed variable declarations & references and stripped comments, Gabe's version also resolved application-relative paths (e.g.: "~/blah/blah.gif") into browser friendly paths. I had further goals in mind though, and Gabe was kind enough to add me onto the project.

With my updates complete, I am now proud to present...

## CssHandler 1.0

*Features*:

* Define variables for later reference.
* Resolve application relative paths.
* Only link to one CSS file from your HTML page. Let the CssHandler combine additional CSS files at runtime to limit HTTP connections and share variable definitions across files.
* All comments are stripped before render.
* Most white-space is stripped before render.
* HttpHandler can be mapped to '*.css' or can be referenced as CssHandler.axd and passed a CSS file in the query string.

*Bugs fixed:*

* The @define{...} block is no longer sent down to the client.
* Similarly named variables no longer present a problem.

*Example:*

The HTML references "styles.css", which looks like:

{% gist 3998144 styles.css %}

As seen above, "styles.css" references "styles2.css" via the @import statement, which looks like:

{% gist 3998144 styles2.css %}

The CssHandler then:

1. Intercepts the browser's request for "styles.css".
2. Resolves the two URLs that are using application relative paths.
3. Replaces the @import directive with the text from "styles2.css".
4. Strips all comments from the CSS.</li>
5. Replaces all referenced variables with their defined values.
6. Compresses most of the CSS's white-space.
7. Renders the following to the browser:

{% gist 3998144 styles_rendered.css %}

I hope you find it as useful as I have. Many thanks to Rory Blyth and Gabe Moothart. Hopefully CSS4 will add variables and we'll never have to use this again!

[Visit the project's CodePlex site to download](http://www.codeplex.com/CssHandler/).

**Update (Dec. 2, 2007):** Rory has posted and [given his blessing](http://www.neopoleon.com/home/blogs/neo/archive/2007/11/23/28201.aspx), so to speak. Thanks Rory!