using System;
using ICD.Common.Utils;
using ICD.Common.Utils.Services;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Core;

namespace ICD.Connect.Themes
{
	public abstract class AbstractTheme<TSettings> : AbstractOriginator<TSettings>, ITheme
		where TSettings : AbstractThemeSettings, new()
	{
		private bool m_CoreSettingsApplied;
		private ICore m_Core;

		/// <summary>
		/// Clears the instantiated user interfaces.
		/// </summary>
		public abstract void ClearUserInterfaces();

		/// <summary>
		/// Clears and rebuilds the user interfaces.
		/// </summary>
		public abstract void BuildUserInterfaces();

		/// <summary>
		/// Override to clear the instance settings.
		/// </summary>
		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			ClearUserInterfaces();
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		protected AbstractTheme()
		{
			IcdEnvironment.OnProgramInitializationComplete += IcdEnvironmentOnProgramInitializationComplete;
		}

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
		/// Override to release resources.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void DisposeFinal(bool disposing)
		{
			base.DisposeFinal(disposing);

			ClearUserInterfaces();
		}

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
	}
}
