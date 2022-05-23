using ICD.Common.Utils.Xml;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Attributes.SettingsProperties;
using ICD.Connect.Settings.Originators;

namespace ICD.Connect.Themes.UiBindings
{
	public abstract class AbstractUiBindingSettings : AbstractSettings, IUiBindingSettings
	{
	}

	public abstract class AbstractUiBindingSettings1Originator : AbstractUiBindingSettings, IUiBindingSettings1Originator
	{
		protected virtual string Originator1Element { get { return "Originator1"; } }

		[OriginatorIdSettingsProperty(typeof(IOriginator))]
		public int? Originator1Id { get; set; }

		/// <summary>
		/// Writes property elements to xml.
		/// </summary>
		/// <param name="writer"></param>
		protected override void WriteElements(IcdXmlTextWriter writer)
		{
			base.WriteElements(writer);

			writer.WriteElementString(Originator1Element, IcdXmlConvert.ToString(Originator1Id));
		}

		/// <summary>
		/// Updates the settings from xml.
		/// </summary>
		/// <param name="xml"></param>
		public override void ParseXml(string xml)
		{
			base.ParseXml(xml);

			Originator1Id = XmlUtils.TryReadChildElementContentAsInt(xml, Originator1Element);
		}
	}

	public abstract class AbstractUiBindingSettings2Originators : AbstractUiBindingSettings1Originator, IUiBindingSettings2Originators
	{
		protected virtual string Originator2Element{get { return "Originator2"; }}

		[OriginatorIdSettingsProperty(typeof(IOriginator))]
		public int? Originator2Id { get; set; }

		/// <summary>
		/// Writes property elements to xml.
		/// </summary>
		/// <param name="writer"></param>
		protected override void WriteElements(IcdXmlTextWriter writer)
		{
			base.WriteElements(writer);

			writer.WriteElementString(Originator2Element, IcdXmlConvert.ToString(Originator2Id));
		}

		/// <summary>
		/// Updates the settings from xml.
		/// </summary>
		/// <param name="xml"></param>
		public override void ParseXml(string xml)
		{
			base.ParseXml(xml);

			Originator2Id = XmlUtils.TryReadChildElementContentAsInt(xml, Originator2Element);
		}
	}

	public abstract class AbstractUiBindingSettings3Originators : AbstractUiBindingSettings2Originators, IUiBindingSettings3Originators
	{
		protected virtual string Originator3Element{get { return "Originator3"; }}

		[OriginatorIdSettingsProperty(typeof(IOriginator))]
		public int? Originator3Id { get; set; }

		/// <summary>
		/// Writes property elements to xml.
		/// </summary>
		/// <param name="writer"></param>
		protected override void WriteElements(IcdXmlTextWriter writer)
		{
			base.WriteElements(writer);

			writer.WriteElementString(Originator3Element, IcdXmlConvert.ToString(Originator3Id));
		}

		/// <summary>
		/// Updates the settings from xml.
		/// </summary>
		/// <param name="xml"></param>
		public override void ParseXml(string xml)
		{
			base.ParseXml(xml);

			Originator3Id = XmlUtils.TryReadChildElementContentAsInt(xml, Originator3Element);
		}
	}

	public abstract class AbstractUiBindingSettings4Originators : AbstractUiBindingSettings3Originators, IUiBindingSettings4Originators
	{
		protected virtual string Originator4Element { get { return "Originator4"; } }

		[OriginatorIdSettingsProperty(typeof(IOriginator))]
		public int? Originator4Id { get; set; }

		/// <summary>
		/// Writes property elements to xml.
		/// </summary>
		/// <param name="writer"></param>
		protected override void WriteElements(IcdXmlTextWriter writer)
		{
			base.WriteElements(writer);

			writer.WriteElementString(Originator4Element, IcdXmlConvert.ToString(Originator4Id));
		}

		/// <summary>
		/// Updates the settings from xml.
		/// </summary>
		/// <param name="xml"></param>
		public override void ParseXml(string xml)
		{
			base.ParseXml(xml);

			Originator4Id = XmlUtils.TryReadChildElementContentAsInt(xml, Originator4Element);
		}
	}
}