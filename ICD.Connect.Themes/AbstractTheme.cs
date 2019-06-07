using System;
using System.Collections.Generic;
using ICD.Common.Utils;
using ICD.Common.Utils.Services;
using ICD.Connect.API.Commands;
using ICD.Connect.Partitioning.Rooms;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Cores;
using ICD.Connect.Settings.Originators;
using ICD.Connect.Themes.UserInterfaceFactories;
using ICD.Connect.Themes.UserInterfaces;

namespace ICD.Connect.Themes
{
	public abstract class AbstractTheme<TSettings> : AbstractOriginator<TSettings>, ITheme
		where TSettings : AbstractThemeSettings, new()
	{
		private bool m_CoreSettingsApplied;
		private ICore m_Core;

		/// <summary>
		/// Constructor.
		/// </summary>
		protected AbstractTheme()
		{
			IcdEnvironment.OnProgramInitializationComplete += IcdEnvironmentOnProgramInitializationComplete;
		}

		/// <summary>
		/// Override to release resources.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void DisposeFinal(bool disposing)
		{
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
		public abstract void ClearUserInterfaces();

		/// <summary>
		/// Clears and rebuilds the user interfaces.
		/// </summary>
		public abstract void BuildUserInterfaces();

		#endregion

		/// <summary>
		/// Override to apply settings to the instance.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="factory"></param>
		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			SetCore(ServiceProvider.TryGetService<ICore>());

			base.ApplySettingsFinal(settings, factory);

			BuildUserInterfaces();
		}

		/// <summary>
		/// Called once both the core and the application have finished loading.
		/// Override this method to enable touch panels.
		/// </summary>
		protected virtual void ActivateUserInterfaces()
		{
		}

		#region Program Initialization Callbacks

		private void SetCore(ICore core)
		{
			if (core == m_Core)
				return;

			Unsubscribe(m_Core);

			m_Core = core;
			m_CoreSettingsApplied = false;

			Subscribe(m_Core);
		}

		/// <summary>
		/// Subscribe to the core events.
		/// </summary>
		/// <param name="core"></param>
		private void Subscribe(ICore core)
		{
			if (core == null)
				return;

			core.OnSettingsApplied += CoreOnSettingsApplied;
		}

		/// <summary>
		/// Unsubscribe from the core events.
		/// </summary>
		/// <param name="core"></param>
		private void Unsubscribe(ICore core)
		{
			if (core == null)
				return;

			core.OnSettingsApplied -= CoreOnSettingsApplied;
		}

		/// <summary>
		/// Called when the Core finishes applying settings.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void CoreOnSettingsApplied(object sender, EventArgs eventArgs)
		{
			m_CoreSettingsApplied = true;

			ActivateUserInterfacesIfReady();
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
