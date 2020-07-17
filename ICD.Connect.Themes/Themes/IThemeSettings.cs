using ICD.Connect.Settings;

namespace ICD.Connect.Themes
{
	public interface IThemeSettings : ISettings
	{
		/// <summary>
		/// Gets the settings for the UI Bindings.
		/// </summary>
		SettingsCollection UiBindingSettings { get; }
	}
}
