--- 
layout: post
title: "MVC: Strongly Typed ViewData Without A Code-Behind"
---

If you've tried to use strongly-typed **ViewData** with a **ViewPage** or **ViewContentPage** without using a code-behind file you may have run into the curious scenario of how to specify a generic in the **&lt;%@ Page %&gt;** element's **Inherits** attribute.

## The Problem

Let's say you wanted to specify that the **ViewData** for this view would be an integer. Normally you would specify this in the code-behind like so:

{% gist 4007445 Example-1.cs %}

Since we don't have a code-behind file though, we have to change the *.aspx* file's **&lt;%@ Page %&gt;** element. Trying to set it to the following will not work:

{% gist 4007445 Example-2.aspx %}

## The Basic Answer

The value of the **Inherits** attribute appears to be delivered straight to the CLR without any language specific parsing, which means we have to specify it the way the CLR wants to interpret it. I browsed around and found ([here](http://forums.asp.net/t/1193721.aspx) and [here](https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=104071&wa=wsignin1.0)) some information that points out the proper way to do this:

{% gist 4007445 Example-3.aspx %}

## WTF?

Eww, huh? The syntax is gnarly, but it gets the job done. Let's break the syntax down to make sure you understand what is going on:

    ParentType`1[ [ChildType,ChildTypeAssembly] ]

* *"ParentType"* is fairly obvious. This is the generic object that you want to create a reference to.
* *"&#x60;1[ ["* is a bit more obscure. The "&#x60;1" specifies the number of generics in this argument, which we'll come back to in a second. The double square brackets are required. A single square bracket will **NOT** work.
* *"ChildType"* is the type of object you wish to have the generic consume (what is the proper terminology here? anyone know?).
* *"ChildTypeAssembly"* is the name of the assembly (**NOT** the namespace) that contains the *ChildType*. All of your common value types will be located in "**mscorlib**".

## Multiple Generic Types

Let's say **ViewPage** took two arguments as part of its generic declaration area (ie: **ViewPage&lt;X,Y&gt;**). It doesn't, by the way, but we'll pretend it does for the sake of argument; you may find this information useful someday.

To have two type parameters, you'll have to change the "&#x60;1" to a "&#x60;2" and add an extra **"[ChildType, ChildTypeAssembly]"** reference, separated by a space. More parameters would continue to follow the same logic.

If you wanted to achieve the equivalent of **ViewPage&lt;int,string&gt;** it would look like this:

    System.Web.Mvc.ViewPage`2[ [System.Int32,mscorlib], [System.String,mscorlib] ]

## A Generic of Generics

A more likely scenario is that you may want to have your **ViewData** be a generic **List&lt;T&gt;**. In this case you'll just nest the declarations. If you wanted to pass **ViewData** of the type **List&lt;string&gt;** to the **ViewPage** it would look like:

    System.Web.Mvc.ViewPage`1[[System.Collections.Generic.List`1[[System.String,mscorlib]], mscorlib]]