#if NETSTANDARD1_5

using System.Collections.Generic;

namespace System.Web.Services.Description
{
    public class Message : NamedItem
    {
        public IList<MessagePart> Parts { get; } = new List<MessagePart>();
    }
}

#endif