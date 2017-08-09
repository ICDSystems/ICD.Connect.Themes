using ICD.Connect.Settings;

namespace ICD.Connect.Themes
{
	public abstract class AbstractTheme<TSettings> : AbstractOriginator<TSettings>, ITheme
		where TSettings : AbstractThemeSettings, new()
	{
		/// <summary>
		/// Clears the instantiated user interfaces.
		/// </summary>
		public abstract void ClearUserInterfaces();

		/// <summary>
		/// Clears and rebuilds the user interfaces.
		/// </summary>
		public abstract void BuildUserInterfaces();
	}
}
