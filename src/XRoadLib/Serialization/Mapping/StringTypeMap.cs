﻿using System;
using System.Xml;
using System.Xml.Linq;
using XRoadLib.Serialization.Template;

namespace XRoadLib.Serialization.Mapping
{
    public class StringTypeMap : TypeMap<string>
    {
        public static ITypeMap Instance { get; } = new StringTypeMap();

        public override XName QualifiedName { get; }

        public StringTypeMap(XName qualifiedName = null)
        {
            QualifiedName = qualifiedName ?? XName.Get("string", NamespaceConstants.XSD);
        }

        public override object Deserialize(XmlReader reader, IXmlTemplateNode templateNode, SerializationContext context)
        {
            if (reader.IsEmptyElement)
                return null;

            var value = reader.ReadString();

            return string.IsNullOrEmpty(value) ? null : value;
        }

        public override void Serialize(XmlWriter writer, IXmlTemplateNode templateNode, object value, Type expectedType, SerializationContext context)
        {
            context.Protocol.Style.WriteExplicitType(writer, QualifiedName);

            var valueString = value.ToString();

            if (valueString.Contains("&") || valueString.Contains("<") || valueString.Contains(">"))
                writer.WriteCData(valueString);
            else writer.WriteValue(valueString);
        }
    }
}