﻿using System;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace XRoadLib.Styles
{
    public class DocLiteralStyle : Style
    {
#if !NETSTANDARD2_0
        public override XmlElement CreateSoapHeader(SoapHeaderBinding binding)
        {
            var element = document.CreateElement(PrefixConstants.SOAP, "header", NamespaceConstants.SOAP);

            element.SetAttribute("message", $"{PrefixConstants.TARGET}:{binding.Message.Name}");
            element.SetAttribute("part", binding.Part);
            element.SetAttribute("use", "literal");

            return element;
        }
#endif

        public override void AddItemElementToArrayElement(XmlSchemaElement arrayElement, XmlSchemaElement itemElement, Action<string> addSchemaImport)
        {
            arrayElement.SchemaType = new XmlSchemaComplexType { Particle = new XmlSchemaSequence { Items = { itemElement } } };
        }

        public override SoapBinding CreateSoapBinding()
        {
            return new SoapBinding
            {
                Style = SoapBindingStyle.Document,
                Transport = NamespaceConstants.HTTP
            };
        }

        public override SoapBodyBinding CreateSoapBodyBinding(string targetNamespace)
        {
            return new SoapBodyBinding { Use = SoapBindingUse.Literal };
        }

        public override SoapHeaderBinding CreateSoapHeaderBinding(XName headerName, string messageName, string targetNamespace)
        {
            return new SoapHeaderBinding
            {
                Message = new XmlQualifiedName(messageName, targetNamespace),
                Part = headerName.LocalName,
                Use = SoapBindingUse.Literal
            };
        }

        public override SoapOperationBinding CreateSoapOperationBinding()
        {
            return new SoapOperationBinding { SoapAction = "", Style = SoapBindingStyle.Document };
        }
    }
}