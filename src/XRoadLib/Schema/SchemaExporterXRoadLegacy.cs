using System.Reflection;
using System.Web.Services.Description;

#if !NETSTANDARD2_0
using System.Xml;
#endif

namespace XRoadLib.Schema
{
    /// <inheritdoc />
    public abstract class SchemaExporterXRoadLegacy : AbstractSchemaExporter
    {
#if !NETSTANDARD2_0
        private readonly XmlDocument document = new XmlDocument();
#endif

        private readonly Assembly contractAssembly;

        /// <summary>
        /// X-Road producer name for legacy protocols.
        /// </summary>
        protected readonly string producerName;

        /// <inheritdoc />
        protected SchemaExporterXRoadLegacy(string producerName, Assembly contractAssembly, string producerNamespace)
            : base(producerNamespace)
        {
            this.contractAssembly = contractAssembly;
            this.producerName = producerName;
        }

        /// <inheritdoc />
        public override void ExportResponseDefinition(ResponseDefinition responseDefinition)
        {
            base.ExportResponseDefinition(responseDefinition);

            responseDefinition.ContainsNonTechnicalFault = true;
        }

        /// <inheritdoc />
        public override void ExportServiceDescription(ServiceDescription serviceDescription)
        {
            base.ExportServiceDescription(serviceDescription);

            serviceDescription.Namespaces.Add(XRoadPrefix, XRoadNamespace);

#if NETSTANDARD2_0
            var address = new XRoadAddressBinding(XRoadPrefix, XRoadNamespace) { Producer = producerName };
#else
            var address = document.CreateElement(XRoadPrefix, "address", XRoadNamespace);
            address.SetAttribute("producer", producerName);
#endif

            var servicePort = serviceDescription.Services[0].Ports[0];
            servicePort.Extensions.Add(address);

            var soapAddressBinding = (SoapAddressBinding)servicePort.Extensions[0];
            soapAddressBinding.Location = "http://TURVASERVER/cgi-bin/consumer_proxy";

            AddXRoadTitles(servicePort);
        }

        /// <summary>
        /// Define X-Road titles of the service port.
        /// </summary>
        protected virtual void AddXRoadTitles(Port servicePort)
        { }

        /// <summary>
        /// Adds X-Road title element to service port.
        /// </summary>
        protected void AddXRoadTitle(Port servicePort, string language, string title)
        {
#if NETSTANDARD2_0
            var titleBinding = new XRoadTitleBinding(XRoadPrefix, XRoadNamespace)
            {
                Language = language,
                Text = title
            };
#else
            var titleBinding = document.CreateElement(XRoadPrefix, "title", XRoadNamespace);
            titleBinding.InnerText = title;

            if (!string.IsNullOrWhiteSpace(language))
            {
                var attribute = document.CreateAttribute("xml", "lang", NamespaceConstants.XML);
                attribute.Value = language;
                titleBinding.Attributes.Append(attribute);
            }
#endif
            servicePort.Extensions.Add(titleBinding);
        }

        /// <summary>
        /// Configure protocol global settings.
        /// </summary>
        public override void ExportProtocolDefinition(ProtocolDefinition protocolDefinition)
        {
            base.ExportProtocolDefinition(protocolDefinition);

            protocolDefinition.ContractAssembly = contractAssembly;
            protocolDefinition.TechNotesElementName = "technotes";
        }
    }
}