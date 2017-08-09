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

		/// <summary>
		/// Returns the collection of ids that the settings will depend on.
		/// For example, to instantiate an IR Port from settings, the device the physical port
		/// belongs to will need to be instantiated first.
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<int> GetDeviceDependencies()
		{
			yield break;
		}
	}
}
