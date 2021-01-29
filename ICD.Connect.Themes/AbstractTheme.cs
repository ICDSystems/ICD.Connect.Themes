using System;
using System.Collections.Generic;
using ICD.Common.Utils;
using ICD.Common.Utils.Extensions;
using ICD.Connect.API.Commands;
using ICD.Connect.Partitioning.Rooms;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Originators;
using ICD.Connect.Themes.UserInterfaceFactories;
using ICD.Connect.Themes.UserInterfaces;

namespace ICD.Connect.Themes
{
	public abstract class AbstractTheme<TSettings> : AbstractOriginator<TSettings>, ITheme
		where TSettings : IThemeSettings, new()
	{
		private bool m_CoreSettingsApplied;

		#region Properties

		/// <summary>
		/// Gets the category for this originator type (e.g. Device, Port, etc)
		/// </summary>
		public override string Category { get { return "Theme"; } }

		#endregion

		/// <summary>
		/// Constructor.
		/// </summary>
		protected AbstractTheme()
		{
			IcdEnvironment.OnProgramInitializationComplete += IcdEnvironmentOnProgramInitializationComplete;
			Core.Originators.OnCollectionChanged += OriginatorsOnCollectionChanged;
			Core.OnLifecycleStateChanged += CoreOnLifecycleStateChanged;
		}

		/// <summary>
		/// Override to release resources.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void DisposeFinal(bool disposing)
		{
			IcdEnvironment.OnProgramInitializationComplete -= IcdEnvironmentOnProgramInitializationComplete;
			Core.Originators.OnCollectionChanged -= OriginatorsOnCollectionChanged;
			Core.OnLifecycleStateChanged -= CoreOnLifecycleStateChanged;

			base.DisposeFinal(disposing);

			ClearUserInterfaces();
		}

		#region Methods

		/// <summary>
		/// Gets the UI Factories.
		/// </summary>
		public abstract IEnumerable<IUserInterfaceFactory> GetUiFactories();

		/// <summary>
		/// Clears the instantiated user interfaces.
		/// </summary>
		public void ClearUserInterfaces()
		{
			GetUiFactories().ForEach(f => f.Clear());
		}

		/// <summary>
		/// Clears and rebuilds the user interfaces.
		/// </summary>
		public void BuildUserInterfaces()
		{
			GetUiFactories().ForEach(f => f.BuildUserInterfaces());
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Called once both the core and the application have finished loading.
		/// Override this method to enable touch panels.
		/// </summary>
		private void ActivateUserInterfaces()
		{
			foreach (IUserInterfaceFactory factory in GetUiFactories())
				factory.ActivateUserInterfaces();
		}

		/// <summary>
		/// Reassigns rooms to the existing user interfaces.
		/// </summary>
		private void ReassignRooms()
		{
			GetUiFactories().ForEach(f => f.ReassignUserInterfaces());
		}

		#endregion

		#region Program Initialization Callbacks

		private void CoreOnLifecycleStateChanged(object sender, LifecycleStateEventArgs args)
		{
			if (args.Data == eLifecycleState.Started)
			{
				m_CoreSettingsApplied = true;
				ActivateUserInterfacesIfReady();
			}
			else
				m_CoreSettingsApplied = false;
		}

		/// <summary>
		/// Reassign the UI rooms when originators are added/removed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void OriginatorsOnCollectionChanged(object sender, EventArgs eventArgs)
		{
			ReassignRooms();
		}

		/// <summary>
		/// Called when the IcdEnvironment initialization state changes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void IcdEnvironmentOnProgramInitializationComplete(object sender, EventArgs eventArgs)
		{
			ActivateUserInterfacesIfReady();
		}

		private void ActivateUserInterfacesIfReady()
		{
			if (m_CoreSettingsApplied && IcdEnvironment.ProgramIsInitialized)
				ActivateUserInterfaces();
		}

		#endregion

		#region Settings

		/// <summary>
		/// Override to apply settings to the instance.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="factory"></param>
		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			// Ensure the rooms are loaded
			factory.LoadOriginators<IRoom>();

			base.ApplySettingsFinal(settings, factory);

			m_CoreSettingsApplied = false;

			BuildUserInterfaces();
		}

		#endregion

		#region Console

		/// <summary>
		/// Gets the child console commands.
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<IConsoleCommand> GetConsoleCommands()
		{
			foreach (IConsoleCommand command in GetBaseConsoleCommands())
				yield return command;

			yield return new ConsoleCommand("PrintUIs", "Prints information about the current UIs", () => ConsolePrintUis());
		}

		/// <summary>
		/// Workaround for "unverifiable code" warning.
		/// </summary>
		/// <returns></returns>
		private IEnumerable<IConsoleCommand> GetBaseConsoleCommands()
		{
			return base.GetConsoleCommands();
		}

		private string ConsolePrintUis()
		{
			TableBuilder builder = new TableBuilder("Type", "Room", "Target");

			foreach (IUserInterfaceFactory factory in GetUiFactories())
			{
				foreach (IUserInterface ui in factory.GetUserInterfaces())
				{
					Type type = ui.GetType();
					IRoom room = ui.Room;
					object target = ui.Target;

					builder.AddRow(type, room, target);
				}
			}

			return builder.ToString();
		}

		#endregion
	}
}
