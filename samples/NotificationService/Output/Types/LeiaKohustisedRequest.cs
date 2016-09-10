using Optional;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XRoadLib.Serialization;

namespace MyNamespace
{
    public class LeiaKohustisedRequest : IXRoadXmlSerializable
    {
        public Option<KohustisOtsing> Kohustis { get; set; }
        public Option<MenetlusOtsing> Menetlus { get; set; }
        public Option<ToimingOtsing> Toiming { get; set; }
        public Option<IList<IsikOtsing>> Isikud { get; set; }
        public Isik Kasutaja { get; set; }

        void IXRoadXmlSerializable.ReadXml(XmlReader reader, XRoadMessage message)
        {
        }

        void IXRoadXmlSerializable.WriteXml(XmlWriter writer, XRoadMessage message)
        {
        }
    }
}