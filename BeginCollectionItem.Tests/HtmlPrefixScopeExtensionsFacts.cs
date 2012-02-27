using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using HtmlHelpers.BeginCollectionItem;
using Moq;
using NUnit.Framework;
using Should;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeginCollectionItem.Tests
{
    // ReSharper disable UnusedMember.Global
    public static class HtmlPrefixScopeExtensionsFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestFixture]
        [TestClass]
        public class TheBeginCollectionItemMethod
        {
            [Test]
            [TestMethod]
            public void WritesCollectionIndexHiddenInput_WhenThereIsNothingInRequestData()
            {
                const string collectionName = "CollectionName";
                var httpContext = new Mock<HttpContextBase>();
                var httpContextItems = new Dictionary<string, object>();
                httpContext.Setup(p => p.Items).Returns(httpContextItems);

                var httpRequest = new Mock<HttpRequestBase>();
                httpContext.Setup(p => p.Request).Returns(httpRequest.Object);

                var viewContext = new ViewContext();
                var viewContextWriter = new Mock<TextWriter>();
                var text = string.Empty;
                viewContextWriter.Setup(m => m.WriteLine(It.IsAny<string>())).Callback((string s) => text += s);
                viewContext.Writer = viewContextWriter.Object;

                var html = new HtmlHelper(viewContext, new FakeViewDataContainer());
                viewContext.HttpContext = httpContext.Object;

                using (var result = html.BeginCollectionItem(collectionName))
                {
                    result.ShouldNotBeNull();
                }

                text.ShouldNotBeNull();
                text.ShouldStartWith(string.Format(
                    @"<input type=""hidden"" name=""{0}.index"" autocomplete=""off"" value=""",
                        collectionName));
                text.ShouldContain(@""" />");
            }

            [Test]
            [TestMethod]
            public void WritesExpectedCollectionIndexHiddenInput_WhenThereIsAnIndexInRequestData()
            {
                const string collectionName = "CollectionName";
                var index0 = Guid.NewGuid();
                var index1 = Guid.NewGuid();
                var indexes = string.Format("{0},{1}", index0, index1);
                var httpContext = new Mock<HttpContextBase>();
                var httpContextItems = new Dictionary<string, object>();
                httpContext.Setup(p => p.Items).Returns(httpContextItems);

                var httpRequest = new Mock<HttpRequestBase>();
                httpRequest.Setup(i => i[It.Is<string>(s => s == string.Format("{0}.index", collectionName))])
                    .Returns(indexes);
                httpContext.Setup(p => p.Request).Returns(httpRequest.Object);

                var viewContext = new ViewContext();
                var viewContextWriter = new Mock<TextWriter>();
                var text = string.Empty;
                viewContextWriter.Setup(m => m.WriteLine(It.IsAny<string>())).Callback((string s) => text += s);
                viewContext.Writer = viewContextWriter.Object;

                var html = new HtmlHelper(viewContext, new FakeViewDataContainer());
                viewContext.HttpContext = httpContext.Object;

                using (var result = html.BeginCollectionItem(collectionName))
                {
                    result.ShouldNotBeNull();
                }

                text.ShouldNotBeNull();
                text.ShouldEqual(string.Format(
                    @"<input type=""hidden"" name=""{0}.index"" autocomplete=""off"" value=""{1}"" />",
                        collectionName, index0));
            }
        }

        [TestFixture]
        [TestClass]
        public class TheBeginHtmlFieldPrefixScopeMethod
        {
            [Test]
            [TestMethod]
            public void Returns_IDisposable()
            {
                var viewContext = new ViewContext();
                var html = new HtmlHelper(viewContext, new FakeViewDataContainer());

                using (var result = html.BeginHtmlFieldPrefixScope(string.Empty)
                    as HtmlPrefixScopeExtensions.HtmlFieldPrefixScope)
                {
                    result.ShouldNotBeNull();
                    result.ShouldImplement<IDisposable>();
                }
            }

            [Test]
            [TestMethod]
            public void Wraps_HtmlHelper_ViewData_TemplateInfo()
            {
                var viewContext = new ViewContext();
                var html = new HtmlHelper(viewContext, new FakeViewDataContainer());

                using (var result = html.BeginHtmlFieldPrefixScope(string.Empty)
                    as HtmlPrefixScopeExtensions.HtmlFieldPrefixScope)
                {
                    result.ShouldNotBeNull();
                    // ReSharper disable PossibleNullReferenceException
                    result.TemplateInfo.ShouldNotBeNull();
                    // ReSharper restore PossibleNullReferenceException
                    result.TemplateInfo.ShouldEqual(html.ViewData.TemplateInfo);

                }
            }

            [Test]
            [TestMethod]
            public void Changes_HtmlHelper_ViewData_TemplateInfo_HtmlFieldPrefix_WhenUsed()
            {
                const string nextFieldPrefix = "InnerItems";
                const string prevFieldPrefix = "OuterItems";
                var viewContext = new ViewContext();
                var html = new HtmlHelper(viewContext, new FakeViewDataContainer());
                html.ViewData.TemplateInfo.HtmlFieldPrefix = prevFieldPrefix;

                using (var result = html.BeginHtmlFieldPrefixScope(nextFieldPrefix)
                    as HtmlPrefixScopeExtensions.HtmlFieldPrefixScope)
                {
                    result.ShouldNotBeNull();
                    // ReSharper disable PossibleNullReferenceException
                    result.PreviousHtmlFieldPrefix.ShouldNotBeNull();
                    // ReSharper restore PossibleNullReferenceException
                    result.PreviousHtmlFieldPrefix.ShouldEqual(prevFieldPrefix);
                    html.ViewData.TemplateInfo.HtmlFieldPrefix.ShouldEqual(nextFieldPrefix);
                }
            }

            [Test]
            [TestMethod]
            public void Restores_HtmlHelper_ViewData_TemplateInfo_HtmlFieldPrefix_WhenDisposed()
            {
                const string nextFieldPrefix = "InnerItems";
                const string prevFieldPrefix = "OuterItems";
                var viewContext = new ViewContext();
                var html = new HtmlHelper(viewContext, new FakeViewDataContainer());
                html.ViewData.TemplateInfo.HtmlFieldPrefix = prevFieldPrefix;

                using (var result = html.BeginHtmlFieldPrefixScope(nextFieldPrefix)
                    as HtmlPrefixScopeExtensions.HtmlFieldPrefixScope)
                {
                    result.ShouldNotBeNull();
                }
                html.ViewData.TemplateInfo.HtmlFieldPrefix.ShouldEqual(prevFieldPrefix);
            }
        }

        private class FakeViewDataContainer : IViewDataContainer
        {
            private ViewDataDictionary _viewData = new ViewDataDictionary();
            public ViewDataDictionary ViewData
            {
                get { return _viewData; }
                set { _viewData = value; }
            }
        }
    }
}