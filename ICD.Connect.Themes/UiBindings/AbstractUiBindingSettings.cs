using ICD.Common.Utils.Xml;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Attributes.SettingsProperties;
using ICD.Connect.Settings.Originators;

namespace ICD.Connect.Themes.UiBindings
{
	public abstract class AbstractUiBindingSettings : AbstractSettings, IUiBindingSettings
	{
		private const string ELEMENT_ORIGINATOR = "Originator";

		[OriginatorIdSettingsProperty(typeof(IOriginator))]
		public int? OriginatorId { get; set; }

		/// <summary>
		/// Writes property elements to xml.
		/// </summary>
		/// <param name="writer"></param>
		protected override void WriteElements(IcdXmlTextWriter writer)
		{
			base.WriteElements(writer);

			writer.WriteElementString(ELEMENT_ORIGINATOR, IcdXmlConvert.ToString(OriginatorId));
		}

		/// <summary>
		/// Updates the settings from xml.
		/// </summary>
		/// <param name="xml"></param>
		public override void ParseXml(string xml)
		{
			base.ParseXml(xml);

			OriginatorId = XmlUtils.TryReadChildElementContentAsInt(xml, ELEMENT_ORIGINATOR);
		}
	}
}