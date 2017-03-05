---
layout: default
title: Release notes
navigation_weight: 3
---

{% for post in site.posts %}
## {{ post.date | date: "%m/%d/%Y, %r (%Z)" }}
   {{ post.content }}
{% endfor %}
