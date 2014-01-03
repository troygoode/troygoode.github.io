---
layout: post
title: npm install require-directory
excerpt: A node.js npm package to recursively iterates over specified directory, requiring each file, and returning a nested hash structure containing those libraries.
---

A common pattern I have found when building node.js applications
revolves around how you refactor a large file into a directory of
smaller files. For example, in an [Express](http://expressjs.com) app
you might start out defining routes in your *app.js* file, but
eventually migrate those routes into a *routes.js* file that is then
required into *app.js*. What happens when *routes.js* grows too large
though? You chunk it up. You often end up with a *routes/* directory,
giving you a project structure that looks something like this:

* app.js
* routes/
  * auth.js
  * home.js
  * products.js

I also find it common to throw an *index.js* file into that *routes/*
directory, which might look like:

{% gist 4310301 index.js %}

Eventually you might find that *routes/auth.js* has grown too large for
one file as well, and you may refactor your files to look more like so:

* app.js
* routes/
  * auth/
    * login.js
    * logout.js
    * register.js
  * home.js
  * products.js

This changes your *routes/index.js* file into:

{% gist 4310301 index2.js %}

... and so on and so forth. As node.js applications grow larger I see
this pattern repeated over and over, often within the same application.
To that end, I created **require-directory** - an
[npm](http://npmjs.org) package that simplifies that *index.js* file
down into:

{% gist 4310301 index3.js %}

For more information, [check out the package's GitHub page.](https://github.com/troygoode/node-require-directory)
