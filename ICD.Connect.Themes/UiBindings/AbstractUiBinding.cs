using System.Collections.Generic;
using ICD.Common.Utils.Services.Logging;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Originators;

namespace ICD.Connect.Themes.UiBindings
{
	public abstract class AbstractUiBinding<TOriginator, TSettings> : AbstractOriginator<TSettings>, IUiBinding<TOriginator>
		where TOriginator : class, IOriginator
		where TSettings : IUiBindingSettings, new()
	{
		#region Properties

		/// <summary>
		/// Gets the category for this originator type (e.g. Device, Port, etc)
		/// </summary>
		public override string Category { get { return "UI Binding"; } }

		/// <summary>
		/// Gets the originator that is bound against.
		/// </summary>
		IOriginator IUiBinding.Originator { get { return Originator; } }

		/// <summary>
		/// Gets the originator that is bound against.
		/// </summary>
		public TOriginator Originator { get; private set; }

		#endregion

		#region Settings

		/// <summary>
		/// Override to clear the instance settings.
		/// </summary>
		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			Originator = null;
		}

		/// <summary>
		/// Override to apply properties to the settings instance.
		/// </summary>
		/// <param name="settings"></param>
		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.OriginatorId = Originator == null ? (int?)null : Originator.Id;
		}

		/// <summary>
		/// Override to apply settings to the instance.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="factory"></param>
		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			base.ApplySettingsFinal(settings, factory);

			TOriginator originator = null;

			if (settings.OriginatorId != null)
			{
				try
				{
					originator = factory.GetOriginatorById((int)settings.OriginatorId) as TOriginator;
				}
				catch (KeyNotFoundException)
				{
					Logger.Log(eSeverity.Error, "No {0} with id {1}", typeof(TOriginator).Name, settings.OriginatorId);
				}
			}

			Originator = originator;
		}

		#endregion
	}
}
