using System;
using System.Collections.Generic;
using System.Linq;
using ICD.Common.Properties;
using ICD.Common.Utils;
using ICD.Common.Utils.Collections;
using ICD.Common.Utils.Extensions;
using ICD.Connect.Partitioning.Extensions;
using ICD.Connect.Partitioning.Rooms;
using ICD.Connect.Themes.UserInterfaces;

namespace ICD.Connect.Themes.UserInterfaceFactories
{
	public abstract class AbstractUserInterfaceFactory<TUserInterface> : IUserInterfaceFactory
		where TUserInterface : IUserInterface
	{
		private readonly ITheme m_Theme;

		private readonly IcdHashSet<TUserInterface> m_UserInterfaces;
		private readonly SafeCriticalSection m_UserInterfacesSection;

		/// <summary>
		/// Gets the theme.
		/// </summary>
		protected ITheme Theme { get { return m_Theme; } }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="theme"></param>
		protected AbstractUserInterfaceFactory(ITheme theme)
		{
			m_Theme = theme;

			m_UserInterfaces = new IcdHashSet<TUserInterface>();
			m_UserInterfacesSection = new SafeCriticalSection();
		}

		#region Methods

		/// <summary>
		/// Disposes the instantiated UIs.
		/// </summary>
		public void Clear()
		{
			m_UserInterfacesSection.Enter();

			try
			{
				m_UserInterfaces.ForEach(ui => ui.Dispose());
				m_UserInterfaces.Clear();
			}
			finally
			{
				m_UserInterfacesSection.Leave();
			}
		}

		/// <summary>
		/// Gets the instantiated user interfaces.
		/// </summary>
		public IEnumerable<IUserInterface> GetUserInterfaces()
		{
			return m_UserInterfacesSection.Execute(() => m_UserInterfaces.Cast<IUserInterface>().ToArray());
		}

		/// <summary>
		/// Instantiates the user interfaces for the originators in the core.
		/// </summary>
		/// <returns></returns>
		public void BuildUserInterfaces()
		{
			m_UserInterfacesSection.Enter();

			try
			{
				Clear();

				IEnumerable<TUserInterface> uis = GetRooms().SelectMany(r => CreateUserInterfaces(r));

				m_UserInterfaces.AddRange(uis);
			}
			finally
			{
				m_UserInterfacesSection.Leave();
			}

			ReassignUserInterfaces();
		}

		/// <summary>
		/// Assigns the rooms to the existing user interfaces.
		/// </summary>
		public void ReassignUserInterfaces()
		{
			AssignUserInterfaces(GetRooms());
		}

		/// <summary>
		/// Activates this user interface.
		/// </summary>
		public virtual void ActivateUserInterfaces()
		{
			foreach (IUserInterface ui in GetUserInterfaces())
				ui.Activate();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Assigns the rooms to the existing user interfaces.
		/// </summary>
		private void AssignUserInterfaces([NotNull] IEnumerable<IRoom> rooms)
		{
			if (rooms == null)
				throw new ArgumentNullException("rooms");

			IcdHashSet<IRoom> roomSet = rooms.ToIcdHashSet();

			m_UserInterfacesSection.Enter();

			try
			{
				foreach (TUserInterface ui in m_UserInterfaces)
				{
					TUserInterface uiClosure = ui;
					IRoom roomForUi = roomSet.FirstOrDefault(r => RoomContainsTarget(r, uiClosure));
					ui.SetRoom(roomForUi);
				}
			}
			finally
			{
				m_UserInterfacesSection.Leave();
			}
		}

		/// <summary>
		/// Creates the user interfaces for the given room.
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
		protected abstract IEnumerable<TUserInterface> CreateUserInterfaces(IRoom room);

		/// <summary>
		/// Returns true if the room contains the originator in the given ui.
		/// </summary>
		/// <param name="room"></param>
		/// <param name="ui"></param>
		/// <returns></returns>
		protected abstract bool RoomContainsTarget(IRoom room, TUserInterface ui);

		/// <summary>
		/// Gets the rooms for the user interfaces.
		/// </summary>
		/// <returns></returns>
		private IEnumerable<IRoom> GetRooms()
		{
			return m_Theme.Core.GetPartitionManager().GetTopLevelRooms();
		}

		#endregion
	}
}
