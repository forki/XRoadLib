using Optional;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XRoadLib.Serialization;

namespace MyNamespace
{
    public class JoustaKohtulahendRequest : IXRoadXmlSerializable
    {
        public Kohtutoiming Kohtutoiming { get; set; }
        public Olek Staatus { get; set; }
        public ToiminguOsaline Menetleja { get; set; }

        void IXRoadXmlSerializable.ReadXml(XmlReader reader, XRoadMessage message)
        {
        }

        void IXRoadXmlSerializable.WriteXml(XmlWriter writer, XRoadMessage message)
        {
        }
    }
}