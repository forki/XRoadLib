#if NETSTANDARD2_0

using System.Xml;

namespace System.Web.Services.Description
{
    public abstract class ServiceDescriptionFormatExtension
    {
        internal abstract void Write(XmlWriter writer);
    }
}

#endif
