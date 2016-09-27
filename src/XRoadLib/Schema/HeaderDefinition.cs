﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using XRoadLib.Extensions;
using XRoadLib.Headers;

namespace XRoadLib.Schema
{
    /// <summary>
    /// Allows to fluently configure headers mandatory elements.
    /// </summary>
    public interface IHeaderDefinitionBuilder<THeader> where THeader : IXRoadHeader
    {
        /// <summary>
        /// Specify mandatory element of the header object.
        /// </summary>
        IHeaderDefinitionBuilder<THeader> WithRequiredHeader<TValue>(Expression<Func<THeader, TValue>> expression);

        /// <summary>
        /// Define namespaces used to define SOAP header elements.
        /// </summary>
        IHeaderDefinitionBuilder<THeader> WithHeaderNamespace(string namespaceName);
    }

    /// <summary>
    /// Configuration options of X-Road header.
    /// </summary>
    public class HeaderDefinition
    {
        private readonly ISet<string> headerNamespaces = new HashSet<string>();

        /// <summary>
        /// Create new instance of header object.
        /// </summary>
        public Func<IXRoadHeader> Initializer { get; private set; }

        /// <summary>
        /// Names of SOAP header elements required by service description.
        /// </summary>
        public ISet<XName> RequiredHeaders { get; } = new SortedSet<XName>(new XNameComparer());

        /// <summary>
        /// Name of WSDL message used to define SOAP header elements.
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// Define custom header type for X-Road messages.
        /// </summary>
        public IHeaderDefinitionBuilder<THeader> Use<THeader>(Func<THeader> initializer) where THeader : IXRoadHeader
        {
            Initializer = () => initializer();

            return new HeaderDefinitionBuilder<THeader>(this);
        }

        /// <summary>
        /// Test if given namespace is defined as SOAP header element namespace.
        /// </summary>
        public bool IsHeaderNamespace(string namespaceName)
        {
            return headerNamespaces.Contains(namespaceName);
        }

        private class HeaderDefinitionBuilder<THeader> : IHeaderDefinitionBuilder<THeader> where THeader : IXRoadHeader
        {
            private readonly HeaderDefinition headerDefinition;

            public HeaderDefinitionBuilder(HeaderDefinition headerDefinition)
            {
                this.headerDefinition = headerDefinition;
                headerDefinition.RequiredHeaders.Clear();
                headerDefinition.headerNamespaces.Clear();
            }

            public IHeaderDefinitionBuilder<THeader> WithRequiredHeader<TValue>(Expression<Func<THeader, TValue>> expression)
            {
                var memberExpression = expression.Body as MemberExpression;
                if (memberExpression == null)
                    throw new ArgumentException($"Only MemberExpression is allowed to use for SOAP header definition, but was {expression.Body.GetType().Name} ({GetType().Name}).", nameof(expression));

                var attribute = memberExpression.Member.GetSingleAttribute<XmlElementAttribute>() ?? memberExpression.Member.DeclaringType.GetElementAttributeFromInterface(memberExpression.Member as PropertyInfo);
                if (string.IsNullOrWhiteSpace(attribute?.ElementName))
                    throw new ArgumentException($"Specified member `{memberExpression.Member.Name}` does not define any XML element.", nameof(expression));

                headerDefinition.RequiredHeaders.Add(XName.Get(attribute.ElementName, attribute.Namespace));

                return this;
            }

            public IHeaderDefinitionBuilder<THeader> WithHeaderNamespace(string namespaceName)
            {
                headerDefinition.headerNamespaces.Add(namespaceName);

                return this;
            }
        }

        private class XNameComparer : IComparer<XName>
        {
            public int Compare(XName x, XName y)
            {
                var ns = string.Compare(x.NamespaceName, y.NamespaceName);
                return ns != 0 ? ns : string.Compare(x.LocalName, y.LocalName);
            }
        }
    }
}