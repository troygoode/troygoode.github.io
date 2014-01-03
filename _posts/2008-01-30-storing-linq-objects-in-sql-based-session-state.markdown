--- 
layout: post
title: Storing LINQ Objects in SQL-Based Session State
---

Scott Hanselman [recently posted about various options you have for session storage](http://www.hanselman.com/blog/TroubleshootingExpiredASPNETSessionStateAndYourOptions.aspx) while using ASP.Net. In the comments of his post I brought up an issue I recently encountered at work (where we use SQL Server session state):

**LINQ-To-Sql generated objects are not marked [Serializable] and cannot be stored in out-of-process session storage.**

To get around this I have whipped up the following helper class which will serialize LINQ-To-Sql objects if you set the DataContext's Serializable property to "Unidirectional".

**Note: The following class does not current work for storing List&lt;X&gt; where X is a LINQ-To-Sql object. I'll be working to resolve that sometime later this week.**
 
{% gist 4006217 %}