﻿using System.Xml;
using XRoadLib.Schema;
using XRoadLib.Serialization.Template;

namespace XRoadLib.Serialization.Mapping
{
    public class DecimalTypeMap : TypeMap
    {
        public DecimalTypeMap(TypeDefinition typeDefinition)
            : base(typeDefinition)
        { }

        public override object Deserialize(XmlReader reader, IXmlTemplateNode templateNode, ContentDefinition content, XRoadMessage message)
        {
            if (reader.IsEmptyElement)
                return MoveNextAndReturn(reader, 0M);

            var value = reader.ReadElementContentAsString();

            return string.IsNullOrEmpty(value) ? 0M : XmlConvert.ToDecimal(value);
        }

        public override void Serialize(XmlWriter writer, IXmlTemplateNode templateNode, object value, ContentDefinition content, XRoadMessage message)
        {
            if (!(content.Particle is RequestDefinition))
                message.Style.WriteExplicitType(writer, Definition.Name);

            writer.WriteValue(value);
        }
    }
}