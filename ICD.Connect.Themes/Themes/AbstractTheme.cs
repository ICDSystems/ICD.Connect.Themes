using System;
using System.Collections.Generic;
using System.Linq;
using ICD.Common.Logging.LoggingContexts;
using ICD.Common.Utils;
using ICD.Common.Utils.Extensions;
using ICD.Common.Utils.Services.Logging;
using ICD.Connect.API.Commands;
using ICD.Connect.API.Nodes;
using ICD.Connect.Partitioning.Rooms;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Originators;
using ICD.Connect.Themes.UiBindings;
using ICD.Connect.Themes.UserInterfaceFactories;

namespace ICD.Connect.Themes
{
	public abstract class AbstractTheme<TSettings> : AbstractOriginator<TSettings>, ITheme
		where TSettings : IThemeSettings, new()
	{
		private readonly UiBindingCollection m_UiBindings;

		private bool m_CoreSettingsApplied;

		#region Properties

		/// <summary>
		/// Gets the category for this originator type (e.g. Device, Port, etc)
		/// </summary>
		public override string Category { get { return "Theme"; } }

		/// <summary>
		/// Gets the UI Bindings for the theme.
		/// </summary>
		public UiBindingCollection UiBindings { get { return m_UiBindings; } }

		#endregion

		/// <summary>
		/// Constructor.
		/// </summary>
		protected AbstractTheme()
		{
			m_UiBindings = new UiBindingCollection();

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
		/// Override to clear the instance settings.
		/// </summary>
		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			UiBindings.Clear();
		}

		/// <summary>
		/// Override to apply properties to the settings instance.
		/// </summary>
		/// <param name="settings"></param>
		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.UiBindingSettings.SetRange(UiBindings.Where(b => b.Serialize).Select(b => b.CopySettings()));
		}

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

			IEnumerable<IUiBinding> uiBindings = GetUiBindings(settings, factory);
			UiBindings.SetChildren(uiBindings);

			m_CoreSettingsApplied = false;

			BuildUserInterfaces();
		}

		private IEnumerable<IUiBinding> GetUiBindings(TSettings settings, IDeviceFactory factory)
		{
			return GetOriginatorsSkipExceptions<IUiBinding>(settings.UiBindingSettings, factory);
		}

		private IEnumerable<T> GetOriginatorsSkipExceptions<T>(IEnumerable<ISettings> originatorSettings,
															   IDeviceFactory factory)
			where T : class, IOriginator
		{
			foreach (ISettings settings in originatorSettings)
			{
				T output;

				try
				{
					output = factory.GetOriginatorById<T>(settings.Id);
				}
				catch (Exception e)
				{
					Logger.Log(eSeverity.Error, e, "Failed to instantiate {0} with id {1}", typeof(T).Name, settings.Id);
					continue;
				}

				yield return output;
			}
		}

		#endregion

		#region Console

		/// <summary>
		/// Gets the child console nodes.
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<IConsoleNodeBase> GetConsoleNodes()
		{
			foreach (IConsoleNodeBase node in GetBaseConsoleNodes())
				yield return node;

			foreach (IConsoleNodeBase node in ThemeConsole.GetConsoleNodes(this))
				yield return node;
		}

		/// <summary>
		/// Wrokaround for "unverifiable code" warning.
		/// </summary>
		/// <returns></returns>
		private IEnumerable<IConsoleNodeBase> GetBaseConsoleNodes()
		{
			return base.GetConsoleNodes();
		}

		/// <summary>
		/// Calls the delegate for each console status item.
		/// </summary>
		/// <param name="addRow"></param>
		public override void BuildConsoleStatus(AddStatusRowDelegate addRow)
		{
			base.BuildConsoleStatus(addRow);

			ThemeConsole.BuildConsoleStatus(this, addRow);
		}

		/// <summary>
		/// Gets the child console commands.
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<IConsoleCommand> GetConsoleCommands()
		{
			foreach (IConsoleCommand command in GetBaseConsoleCommands())
				yield return command;

			foreach (IConsoleCommand command in ThemeConsole.GetConsoleCommands(this))
				yield return command;
		}

		/// <summary>
		/// Workaround for "unverifiable code" warning.
		/// </summary>
		/// <returns></returns>
		private IEnumerable<IConsoleCommand> GetBaseConsoleCommands()
		{
			return base.GetConsoleCommands();
		}

		#endregion
	}
}
