--- 
layout: post
title: Add/Update/Delete with LINQPad
excerpt: A quick tutorial on using LINQPad - a query tool that understands LINQ.
---

[Danny Douglass](http://www.dannydouglass.com) recently [posted](http://www.dannydouglass.com/post/2007/12/LINQ-Editor-and-Quiz.aspx) about [LINQPad](http://www.linqpad.net), a query tool that understands LINQ. I spent some time today using it and am very impressed. It did take a while, however, to look through the numerous sample scripts and figure out exactly how to insert/update/delete data, so I thought I would share what I've found. The biggest changes between working in LINQ-To-SQL in a Visual Studio 2008 project and writing a LINQ query in LINQPad are the lack of a **DataContext, **the difference between **C# Expressions** and **C# Statements**, and the addition of a **Dump** command. 

**No Data Context:** When you need to interact with a database via LINQ in a Visual Studio 2008 project, you do so by creating a LINQ-To-SQL DBML file that generates a data context for you. This data context is in charge of maintaining your database connection and is what you use to submit changes to the database. Because there is no data context readily available to you (and no .dbml file) in LINQPad, the way you go about this is slightly different. There is a globally-scoped subroutine, "SubmitChanges()," that should be called whenever you wish to commit an action to the database. 

**C# Expressions vs. C# Statements:** By default LINQPad opens in "C# Expression" mode. In this mode you can type a simple query like "from r in Regions select r" and run it to see the results. As far as I can tell there is no way to insert/update/delete data in this mode. By switching to "C# Statement(s)" mode you are able to declare variables, control flow statements, and reference objects; this is the mode you need to be in to insert/update/delete data. To enter "C# Statement(s)" mode, select it from the "Type" drop-down box at the top of the window as show below: 

![IMAGE](/custom/files/old/LINQPad_2.jpg)

**object.Dump():** When you write a query in "C# Expression" mode the result of that query is automatically rendered to the Results frame (shown below). Because "C# Statement(s)" mode gives you the capability to run many queries in one execute, displaying the results of those queries must be manually invoked. To do so simply call the ".Dump()" method on the resultset of the query you want to display. ".Dump()" is implemented as an extension method available on all objects, so whether you are retrieving a single object, a list of objects, or an anonymous type the Dump method will be available to display your data. 

![IMAGE](/custom/files/old/LINQPad-Results_2.jpg)

Below I have included examples of several ways to query data, as well as an example each for inserting, updating, and deleting data. 

{% gist 3998228 %}