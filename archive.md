---
layout: default
title: Archives
---

<div class="archives" itemscope itemtype="http://schema.org/Blog">
  <ul>
  {% for post in site.posts reverse %}
    {% if post.layout == 'post' %}
      <li>
        <div>{{ post.date | date_to_string }}</div>
        <div><a href="{{ post.url }}">{{post.title}}</a></div>
      </li>
    {% endif %}
  {% endfor %}
  </ul>
</div>
