using System.Collections.Generic;
using System.Linq;
using ICD.Common.Utils.Services.Logging;
using ICD.Common.Utils.Xml;
using ICD.Connect.Settings;

namespace ICD.Connect.Themes
{
	public abstract class AbstractThemeSettings : AbstractSettings, IThemeSettings
	{
		private const string ELEMENT_UI_BINDINGS = "UiBindings";
		private const string ELEMENT_UI_BINDING = "UiBinding";

		private readonly SettingsCollection m_UiBindingSettings;

		/// <summary>
		/// Gets the settings for the UI Bindings.
		/// </summary>
		public SettingsCollection UiBindingSettings { get { return m_UiBindingSettings; } }

		/// <summary>
		/// Constructor.
		/// </summary>
		protected AbstractThemeSettings()
		{
			m_UiBindingSettings = new SettingsCollection();
		}

		#region Methods

		/// <summary>
		/// Writes the routing settings to xml.
		/// </summary>
		/// <param name="writer"></param>
		protected override void WriteElements(IcdXmlTextWriter writer)
		{
			base.WriteElements(writer);

			m_UiBindingSettings.ToXml(writer, ELEMENT_UI_BINDINGS, ELEMENT_UI_BINDING);
		}

		/// <summary>
		/// Updates the settings from xml.
		/// </summary>
		/// <param name="xml"></param>
		public override void ParseXml(string xml)
		{
			base.ParseXml(xml);

			IEnumerable<ISettings> uiBindings = PluginFactory.GetSettingsFromXml(xml, ELEMENT_UI_BINDINGS);

			AddSettingsLogDuplicates(UiBindingSettings, uiBindings);
		}

		private void AddSettingsLogDuplicates(SettingsCollection collection, IEnumerable<ISettings> settings)
		{
			foreach (ISettings item in settings.Where(item => !collection.Add(item)))
				Logger.AddEntry(eSeverity.Error, "{0} failed to add duplicate {1}", GetType().Name, item);
		}

		#endregion
	}
}
