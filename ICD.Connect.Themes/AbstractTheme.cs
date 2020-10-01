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
		where TSettings : IThemeSettings, new()
	{
		private bool m_CoreSettingsStarted;
		private ICore m_Core;

		/// <summary>
		/// Gets the Core instance.
		/// </summary>
		public ICore Core { get { return m_Core ?? (m_Core = ServiceProvider.TryGetService<ICore>()); } }

		/// <summary>
		/// Constructor.
		/// </summary>
		protected AbstractTheme()
		{
			IcdEnvironment.OnProgramInitializationComplete += IcdEnvironmentOnProgramInitializationComplete;
			Core.OnLifecycleStateChanged += CoreOnLifecycleStateChanged;
		}

		/// <summary>
		/// Override to release resources.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void DisposeFinal(bool disposing)
		{
			IcdEnvironment.OnProgramInitializationComplete -= IcdEnvironmentOnProgramInitializationComplete;
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
			base.ApplySettingsFinal(settings, factory);

			BuildUserInterfaces();
		}

		/// <summary>
		/// Called once both the core and the application have finished loading.
		/// Override this method to enable touch panels.
		/// </summary>
		protected virtual void ActivateUserInterfaces()
		{
			foreach (IUserInterfaceFactory factory in GetUiFactories())
				factory.ActivateUserInterfaces();
		}

		#region Program Initialization Callbacks

		private void CoreOnLifecycleStateChanged(object sender, LifecycleStateEventArgs args)
		{
			if (args.Data == eLifecycleState.Started)
			{
				m_CoreSettingsStarted = true;
				ActivateUserInterfacesIfReady();
			}
			else
				m_CoreSettingsStarted = false;
			
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
			if (m_CoreSettingsStarted && IcdEnvironment.ProgramIsInitialized)
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
