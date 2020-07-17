using System.Collections.Generic;
using ICD.Connect.Settings.Originators;
using ICD.Connect.Themes.UiBindings;
using ICD.Connect.Themes.UserInterfaceFactories;

namespace ICD.Connect.Themes
{
	public interface ITheme : IOriginator
	{
		/// <summary>
		/// Gets the UI Bindings for the theme.
		/// </summary>
		UiBindingCollection UiBindings { get; }

		/// <summary>
		/// Gets the UI Factories.
		/// </summary>
		IEnumerable<IUserInterfaceFactory> GetUiFactories();

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
