using ICD.Connect.Settings;
using ICD.Connect.Settings.Originators;

namespace ICD.Connect.Themes
{
	public interface ITheme : IOriginator
	{
		/// <summary>
		/// Clears the instantiated user interfaces.
		/// </summary>
		void ClearUserInterfaces();

		/// <summary>
		/// Clears and rebuilds the user interfaces.
		/// </summary>
		void BuildUserInterfaces();
	}
}
