using System.Collections.Generic;
using ICD.Connect.Partitioning.Rooms;
using ICD.Connect.Themes.UserInterfaces;

namespace ICD.Connect.Themes.UserInterfaceFactories
{
	public abstract class AbstractUserInterfaceFactory : IUserInterfaceFactory
	{
		/// <summary>
		/// Disposes the instantiated UIs.
		/// </summary>
		public abstract void Clear();

		/// <summary>
		/// Instantiates the user interfaces for the originators in the core.
		/// </summary>
		/// <returns></returns>
		public abstract void BuildUserInterfaces();

		/// <summary>
		/// Gets the instantiated user interfaces.
		/// </summary>
		public abstract IEnumerable<IUserInterface> GetUserInterfaces();

		/// <summary>
		/// Assigns the rooms to the existing user interfaces.
		/// </summary>
		public abstract void ReassignUserInterfaces();

		/// <summary>
		/// Assigns the rooms to the existing user interfaces.
		/// </summary>
		public abstract void AssignUserInterfaces(IEnumerable<IRoom> rooms);

		/// <summary>
		/// Activates this user interface.
		/// </summary>
		public virtual void ActivateUserInterfaces()
		{
			foreach (IUserInterface ui in GetUserInterfaces())
				ui.Activate();
		}
	}
}
