# BeginCollectionItem Html Helper
This is a handy HtmlHelper was [originally authored by Steve Sanderson](http://blog.stevensanderson.com/2010/01/28/editing-a-variable-length-list-aspnet-mvc-2-style/). It's based on the [MVC2 style of converting HTTP POST data into ViewModel collection properties using the out of the box DefaultModelBinder](http://haacked.com/archive/2008/10/23/model-binding-to-a-list.aspx), and is quite helpful when your HTML forms contain collections.

## License
This software is subject to the terms of the [Microsoft Public License (Ms-PL)](http://www.opensource.org/licenses/MS-PL).

## Usage
This class is in the `HtmlHelpers` namespace. To use it in a view, either put a `@using HtmlHelpers` directive at the top of the view, or to make it available to multiple views, add an entry in the system.web/pages/namespaces section of web.config like so:

    <system.web>
        ...
        <pages>
            <namespaces>
                <add namespace="System.Web.Helpers" />
                <add namespace="System.Web.Mvc" />
                <add namespace="System.Web.Mvc.Ajax" />
                <add namespace="System.Web.Mvc.Html" />
                <add namespace="System.Web.Routing" />
                <add namespace="System.Web.WebPages" />
                <add namespace="HtmlHelpers" />
            </namespaces>
        </pages>
        ...
    </system.web>

More general usage of this helper is [well documented on the web](https://www.google.com/search?q=%22begincollectionitem%22).
