using Optional;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XRoadLib.Serialization;

namespace MyNamespace
{
    public class SyydistusPunkt : IXRoadXmlSerializable
    {
        public Option<DateTime?> AlgusKP { get; set; }
        public Option<string> KlientsysteemiID { get; set; }
        public Option<IList<KvalifikatsiooniParagrahv>> KvalifikatsiooniParagrahvid { get; set; }
        public Option<DateTime?> LahendiKP { get; set; }
        public Option<long?> LahendKL { get; set; }
        public Option<DateTime?> LoppKP { get; set; }
        public Option<long> ObjektID { get; set; }
        public Option<IList<Sanktsioon>> SeotudKaristused { get; set; }
        public Option<DateTime?> SulgemiseKP { get; set; }
        public Option<string> SyydistusPunktCSV { get; set; }
        public Option<Syyteosyndmus> Syyteosyndmus { get; set; }

        void IXRoadXmlSerializable.ReadXml(XmlReader reader, XRoadMessage message)
        {
        }

        void IXRoadXmlSerializable.WriteXml(XmlWriter writer, XRoadMessage message)
        {
        }
    }
}