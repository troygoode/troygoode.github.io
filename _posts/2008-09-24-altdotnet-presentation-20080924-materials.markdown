--- 
layout: post
title: Introduction to ASP.Net MVC for Alt.Net DC - Presentation Materials
sharing: false
excerpt: Whether you attended tonight's Alt.Net DC presentation or not, you may want to check out the example Blog application I prepared for it.
---

I just got back from my ASP.Net MVC Presentation for tonight's DC Alt.Net meeting and I had a great time. I hope everyone got a good feel for what it takes to develop an application using the MVC framework. Please don't hesitate to [email me](mailto:troygoode@gmail.com) if you have any questions. Also I owe a big thanks to [Matthew Podwysocki](http://codebetter.com/blogs/matthew.podwysocki/default.aspx) for inviting me to speak. Thanks Matt, I really enjoyed the opportunity.

I promised that I would post the project that we created tonight (a basic blog engine), which you can now find linked below. There are three projects in the solution:

* **MvcTutorials.Blog.Domain:** This project has the Comment & Post entities, as well as the repositories used to populate them.
* **MvcTutorials.Blog.Domain.Bootstrap:** This project adds random data to the blog database for testing.
* **MvcTutorials.Blog.ReferenceWebsite:** This is the MVC website I created prior to the meeting to decide what I was going to present and how long it would take to do it. This is where you'll find the code we wrote tonight.

If you want to run the project, make sure you have SQL Server Express installed and create a database named "MvcTutorial-Blog". Afterward you can run the "MvcTutorials.Blog.Domain.Bootstrap" project to insert test data and run the website project.

Be sure to let me know if there are any questions or comments about tonight's presentation or this post's code!

[MvcTutorial-Blog.zip](/custom/files/old/MvcTutorial-Blog.zip)