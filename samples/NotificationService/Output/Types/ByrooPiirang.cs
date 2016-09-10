using Optional;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XRoadLib.Serialization;

namespace MyNamespace
{
    public class ByrooPiirang : IXRoadXmlSerializable
    {
        public Option<long?> AsjaLiikKL { get; set; }
        public Option<IList<string>> AsjaNR { get; set; }
        public Option<long?> OsaliseAsjadeKuvamineKL { get; set; }

        void IXRoadXmlSerializable.ReadXml(XmlReader reader, XRoadMessage message)
        {
        }

        void IXRoadXmlSerializable.WriteXml(XmlWriter writer, XRoadMessage message)
        {
        }
    }
}