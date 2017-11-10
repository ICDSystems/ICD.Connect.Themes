using System.Collections.Generic;
using ICD.Connect.Settings;

namespace ICD.Connect.Themes
{
	public abstract class AbstractThemeSettings : AbstractSettings, IThemeSettings
	{
		private const string THEME_ELEMENT = "Theme";

		/// <summary>
		/// Gets the xml element.
		/// </summary>
		protected override string Element { get { return THEME_ELEMENT; } }
	}
}
