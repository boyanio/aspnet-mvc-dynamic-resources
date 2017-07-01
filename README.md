# ASP.NET MVC Dynamic resources

A simple library allowing you to register dynamic scripts &amp; style sheets for individual views in ASP.NET MVC. For more info, check this [blog post](https://boyan.in/dynamic-resource-asp-net-mvc/).

## Use cases

* I shall able to register individual resources, scripts and style sheets, from each view and partial view.
* If multiple views require the same resource, it shall appear only once in the output HTML.
* I shall be able to indicate the order of inclusion of the resources.
* When a resource is changed, browsers shall get the newest version.
* AJAX requests resulting in a partial view shall determine the required resources by this view.

## Examples

```csharp
<!-- _Layout.cshtml -->
<html>
<head>
  <title>Hello, World</title>
  @Html.RenderDynamicStyleSheets()
</head>
<body>
  @RenderBody()
  @Html.RenderDynamicScripts()
</body>
</html>
```

```csharp
<!-- View.cshtml -->
@{
  Layout = "_Layout.cshtml";

  Html.RegisterDynamicStyleSheets("home.css");
  Html.RegisterDynamicScripts("common.js", "home.js");
}
```

```csharp
<!-- _PartialView.cshtml -->
@{
  Html.RegisterDynamicStyleSheets("partialView.css");
  Html.RegisterDynamicScripts("common.js", "partialView.js");
}
```

For AJAX requests to work, you need to register a global action filter in MVC.

```csharp
GlobalFilters.Filters.Add(new DynamicResourceAttachmentAttribute());
```

## Known issues

The library is aimed mainly at projects with isolated pages and hence resources. A overlap could occur when a normal requests to a controller renders a bundle with one specific resource and later on another asynchronous request to another controller gets another bundle with the same resource inside.
