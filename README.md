# ASP.NET MVC Dynamic resources

A simple library allowing you to register dynamic scripts &amp; style sheets for individual views in ASP.NET MVC

## Known issues

The library is aimed mainly at projects with isolated pages and hence resources. A overlap could occur when a normal requests to a controller renders a bundle with one specific resource and later on another asynchronous request to another controller gets another bundle with the same resource inside.
