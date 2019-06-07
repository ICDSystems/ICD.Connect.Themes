using System;
using ICD.Connect.Partitioning.Rooms;

namespace ICD.Connect.Themes.UserInterfaces
{
	public interface IUserInterface : IDisposable
	{
		#region Properties

		/// <summary>
		/// Gets the room attached to this UI.
		/// </summary>
		IRoom Room { get; }

		/// <summary>
		/// Gets the target instance attached to this UI (i.e. the Panel, KeyPad, etc).
		/// </summary>
		object Target { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Updates the UI to represent the given room.
		/// </summary>
		/// <param name="room"></param>
		void SetRoom(IRoom room);

		/// <summary>
		/// Tells the UI that it should be considered ready to use.
		/// For example updating the online join on a panel or starting a long-running process that should be delayed.
		/// </summary>
		void Activate();

		#endregion
	}
}
