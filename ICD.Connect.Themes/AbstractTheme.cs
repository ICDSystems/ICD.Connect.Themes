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
		private bool m_EnvironmentInitialized;
		private bool m_CoreSettingsApplied;

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
		/// Override to apply settings to the instance.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="factory"></param>
		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			base.ApplySettingsFinal(settings, factory);

			IcdEnvironment.OnProgramInitializationComplete += IcdEnvironmentOnProgramInitializationComplete;
			ServiceProvider.GetService<ICore>().OnSettingsApplied += CoreOnSettingsApplied;

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

		private void ActivateUserInterfacesIfReady()
		{
			if (!m_CoreSettingsApplied || !m_EnvironmentInitialized)
				return;

			ActivateUserInterfaces();
		}

		private void CoreOnSettingsApplied(object sender, EventArgs eventArgs)
		{
			m_CoreSettingsApplied = true;
			ActivateUserInterfacesIfReady();
		}

		private void IcdEnvironmentOnProgramInitializationComplete(object sender, EventArgs eventArgs)
		{
			m_EnvironmentInitialized = true;
			ActivateUserInterfacesIfReady();
		}

	}
}
