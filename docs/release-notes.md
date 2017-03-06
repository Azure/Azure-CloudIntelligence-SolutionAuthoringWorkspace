---
layout: default
title: Release notes
navigation_weight: 3
---
# Release notes
  
{% for post in site.posts %}
   {% if post.excerpt != post.content %}
## [{{ post.date | date: "%m/%d/%Y, %r (%Z)" }}]({{ post.url }})
   {% else %}
## {{ post.date | date: "%m/%d/%Y, %r (%Z)" }}
   {% endif %}
   {{ post.excerpt }}
{% endfor %}
