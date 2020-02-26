using System.Collections.Generic;
using ICD.Connect.Themes.UserInterfaces;

namespace ICD.Connect.Themes.UserInterfaceFactories
{
	public interface IUserInterfaceFactory
	{
		/// <summary>
		/// Disposes the instantiated UIs.
		/// </summary>
		void Clear();

		/// <summary>
		/// Instantiates the user interfaces for the originators in the core.
		/// </summary>
		/// <returns></returns>
		void BuildUserInterfaces();

		/// <summary>
		/// Gets the instantiated user interfaces.
		/// </summary>
		IEnumerable<IUserInterface> GetUserInterfaces();

		/// <summary>
		/// Assigns the rooms to the existing user interfaces.
		/// </summary>
		void ReassignUserInterfaces();

		/// <summary>
		/// Activates this user interface.
		/// </summary>
		void ActivateUserInterfaces();
	}
}
