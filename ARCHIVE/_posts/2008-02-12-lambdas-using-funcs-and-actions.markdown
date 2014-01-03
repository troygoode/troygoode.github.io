--- 
layout: post
title: Lambdas Using Funcs and Actions
---

Many of you are probably familiar with [lambda expressions](http://weblogs.asp.net/scottgu/archive/2007/04/08/new-orcas-language-feature-lambda-expressions.aspx) by now, as they have been extensively covered since the release of .Net 3.5. Most articles tend to focus on how to pass a lambda expression into a method, but how do you create a method that accepts a lambda expression? Enter the **[Func](http://msdn2.microsoft.com/en-us/library/bb549151.aspx)** and **[Action](http://msdn2.microsoft.com/en-us/library/system.action.aspx)** types...

## Funcs vs. Actions

**Funcs** and **Actions** are extremely similar; both give you first class support for passing an anonymous method into another method. The difference is in the return value: **Funcs** have one, **Actions** do not.

## A Simple Action Consumer

There are several variants of the **Action** type, each taking a varying number of generic parameters. The simplest generic **Action** takes one generic parameters: the type of the object to be passed into the **Action**.

Suppose you had a list of articles and wanted to iterate through them and modify each of them in some way, but only the ones that were created before 2008. In this case you should probably use a **foreach** loop, but instead I'll show you how to do it by encapsulating that **foreach** loop in another method that then calls your lambda:

{% gist 4006198 Example-1.cs %}

I'll be the first to admit it isn't the finest example of coding practices, but I believe it illustrates how you can use an **Action** to pass processing in as a parameter.

## A Simple Func Consumer

What if you want to pass in your processing and retrieve a return value? Use the **Func** type. Let's keep the same example as above, but this time we'll place variable processing in the lambda to manipulate the article's title differently based upon the year the article was published:

{% gist 4006198 Example-2.cs %}

In the above example you'll see that we have changed **Action** to **Func** in the **IterateArticles** method signature and that we have added a string to the list of types in the **Func** definition. *When using a **Func**, the last generic parameter is always the return type.* You should also note that we can now return a value from our anonymous method.

## A Two-Parameter Func

Finally let's take a look at an example of a multi-parameter **Func**. Once again we'll use the same example, except this time rather than passing the **Article** object to the anonymous method we'll pass only the data it needs: the date created and the title:

{% gist 4006198 Example-3.cs %}

Here we have specified three generic parameters for the **Func**; two to be passed in, one to return. In our lambda expression we have also had to wrap the "x,y" in parentheses to prevent the compiler from throwing an error because it thinks the comma is intended to separate parameters to the **IterateArticles** method rather than the **Func.**

## A Zero-Parameter Func

Occasionally you may need to use a **Func** that will not require any parameters. Trying to pass this into the lambda consumer using only the "{...}" notation of anonymous methods will not work, you will need to use "()=&gt;{...}" as seen below:

{% gist 4006198 Example-4.cs %}

I hope this helps get you started creating your own lambda consuming methods. Keep in mind that **Funcs** and **Actions** are not limited to just one or two parameters as shown in this article; both can accept up to four input parameters (not including the **Func's** extra return parameter). As you become more comfortable creating lambda expressions and lambda consumers I am certain you will find many places in our your code that can be simplified and/or made more flexible by using them. 