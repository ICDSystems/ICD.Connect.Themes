﻿using ICD.Connect.Protocol.Crosspoints;
using ICD.Connect.Protocol.Crosspoints.Services;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Originators;
using ICD.Connect.Themes.UiBindings;

namespace ICD.Connect.Themes.Crosspoints.UiBindings.XP3
{
    public abstract class AbstractXp3UiBinding<TOriginator1, TSettings> : AbstractUiBinding<TOriginator1, TSettings>, IXp3UiBinding
		where TOriginator1 : class, IOriginator
        where TSettings : IXp3UiBindingSettings1Originator, new()
    {
	    public int SystemId { get; private set; }

	    public Xp3 Xp3 { get { return Xp3Service.Xp3; } }

	    protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			Xp3.RemoveSystem(SystemId);

			SystemId = 0;
		}

		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.Xp3SystemId = SystemId;
		}

		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			SystemId = settings.Xp3SystemId != null ? settings.Xp3SystemId.Value : 0;

			base.ApplySettingsFinal(settings, factory);
		}
    }

    public abstract class AbstractXp3UiBinding<TOriginator1, TOriginator2, TSettings> : AbstractUiBinding<TOriginator1, TOriginator2, TSettings>, IXp3UiBinding
        where TOriginator1 : class, IOriginator
        where TOriginator2 : class, IOriginator
	    where TSettings : IXp3UiBindingSettings2Originators, new()
    {
		public int SystemId { get; private set; }

		public Xp3 Xp3 { get { return Xp3Service.Xp3; } }

		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			Xp3.RemoveSystem(SystemId);

			SystemId = 0;
		}

		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.Xp3SystemId = SystemId;
		}

		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			SystemId = settings.Xp3SystemId != null ? settings.Xp3SystemId.Value : 0;

			base.ApplySettingsFinal(settings, factory);
		}
	}

    public abstract class AbstractXp3UiBinding<TOriginator1, TOriginator2, TOriginator3, TSettings> : AbstractUiBinding<TOriginator1, TOriginator2, TOriginator3, TSettings>, IXp3UiBinding
        where TOriginator1 : class, IOriginator
	    where TOriginator2 : class, IOriginator
        where TOriginator3 : class, IOriginator
	    where TSettings : IXp3UiBindingSettings3Originators, new()
    {
		public int SystemId { get; private set; }

		public Xp3 Xp3 { get { return Xp3Service.Xp3; } }

		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			Xp3.RemoveSystem(SystemId);

			SystemId = 0;
		}

		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.Xp3SystemId = SystemId;
		}

		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			SystemId = settings.Xp3SystemId != null ? settings.Xp3SystemId.Value : 0;

			base.ApplySettingsFinal(settings, factory);
		}
	}

    public abstract class AbstractXp3UiBinding<TOriginator1, TOriginator2, TOriginator3, TOriginator4, TSettings> : AbstractUiBinding<TOriginator1, TOriginator2, TOriginator3, TOriginator4, TSettings>, IXp3UiBinding
        where TOriginator1 : class, IOriginator
	    where TOriginator2 : class, IOriginator
	    where TOriginator3 : class, IOriginator
        where TOriginator4 : class, IOriginator
	    where TSettings : IXp3UiBindingSettings4Originators, new()
    {
		public int SystemId { get; private set; }

		public Xp3 Xp3 { get { return Xp3Service.Xp3; } }

		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			Xp3.RemoveSystem(SystemId);

			SystemId = 0;
		}

		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.Xp3SystemId = SystemId;
		}

		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			SystemId = settings.Xp3SystemId != null ? settings.Xp3SystemId.Value : 0;

			base.ApplySettingsFinal(settings, factory);
		}
	}
}
