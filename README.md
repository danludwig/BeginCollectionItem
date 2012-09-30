# BeginCollectionItem Html Helper
This is a handy HtmlHelper was [originally authored by Steve Sanderson](http://blog.stevensanderson.com/2010/01/28/editing-a-variable-length-list-aspnet-mvc-2-style/). It's based on the [MVC2 style of converting HTTP POST data into ViewModel collection properties using the out of the box DefaultModelBinder](http://haacked.com/archive/2008/10/23/model-binding-to-a-list.aspx), and is quite helpful when your HTML forms contain collections.

## License
This software is subject to the terms of the [Microsoft Public License (Ms-PL)](http://www.opensource.org/licenses/MS-PL).

## Usage
This class is in the `HtmlHelpers.BeginCollectionItem` namespace. To use it in a view you would either put a `@using HtmlHelpers.BeginCollectionItem` directive at the top of the view, or to make it available to multiple views, add an entry in the `system.web.webPages.razor/pages/namespaces` section of your Views web.config(s). When installing this into an MVC3 or higher project, the second option is done automatically for your root Views/Web.config file:

    <system.web.webPages.razor>
        ...
        <pages>
            <namespaces>
                <add namespace="System.Web.Helpers" />
                <add namespace="System.Web.Mvc" />
                <add namespace="System.Web.Mvc.Ajax" />
                <add namespace="System.Web.Mvc.Html" />
                <add namespace="System.Web.Routing" />
                <add namespace="System.Web.WebPages" />
                <add namespace="HtmlHelpers.BeginCollectionItem" /> <!-- this will be added when you install the package -->
            </namespaces>
        </pages>
        ...
    </system.web.webPages.razor>

Note that if you are using the second approach with views files in an area, that area's Views/web.config file will need the namespace reference. Areas do not pick up the reference form the root web.config, and installing this package will not affect web.config files in your Area Views folders.

More general usage of this helper is [well documented on the web](https://www.google.com/search?q=%22begincollectionitem%22).

## Changelog

Release 1.2
 - Install a `Collection.cshtml` editor template for use with the helper.
 - Update demo project to use `Collection.cshtml` editor template.
 - Optimize `TextWriter.WriteLine` to use its own string formatting overloads.

Release 1.1
 - Moved the default web.config transform from the root web.config to the Views web.config.
 - Corrected namespace declaration and documentation to indicate the correct namespace, HtmlHelpers.BeginItemCollection.
 - Added .gitignore to Solution Items folder.