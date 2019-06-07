using ICD.Common.Utils;
using ICD.Connect.Partitioning.Rooms;

namespace ICD.Connect.Themes.UserInterfaces
{
	public abstract class AbstractUserInterface : IUserInterface
	{
		/// <summary>
		/// Gets the room attached to this UI.
		/// </summary>
		public abstract IRoom Room { get; }

		/// <summary>
		/// Gets the target instance attached to this UI.
		/// </summary>
		public abstract object Target { get; }

		/// <summary>
		/// Release resources.
		/// </summary>
		public abstract void Dispose();

		/// <summary>
		/// Updates the UI to represent the given room.
		/// </summary>
		/// <param name="room"></param>
		public abstract void SetRoom(IRoom room);

		/// <summary>
		/// Tells the UI that it should be considered ready to use.
		/// For example updating the online join on a panel or starting a long-running process that should be delayed.
		/// </summary>
		public abstract void Activate();

		/// <summary>
		/// Gets the string representation for this instance.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			ReprBuilder builder = new ReprBuilder(this);
			builder.AppendProperty("Room", Room);
			builder.AppendProperty("Target", Target);
			return builder.ToString();
		}
	}
}
