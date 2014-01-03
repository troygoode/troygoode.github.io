--- 
layout: post
title: "MVC: How Do I Prevent Exposing a Public Method on a Controller?"
---

The purists will say that you shouldn't have any public methods on a controller that aren't meant to be exposed via a view or redirect to another action. Assuming that you, like I, have encountered a situation where you need/want to do this, however, here is what you do:
  
{% gist 4006183 %}

The **System.Web.Mvc.NonAction** attribute will prevent your public method from being accessed via any routes.