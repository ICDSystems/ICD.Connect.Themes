using ICD.Connect.Settings;
using ICD.Connect.Settings.Attributes.SettingsProperties;
using ICD.Connect.Settings.Originators;

namespace ICD.Connect.Themes.UiBindings
{
	public interface IUiBindingSettings : ISettings
	{
	}

	public interface IUiBindingSettings1Originator : IUiBindingSettings
	{
		[OriginatorIdSettingsProperty(typeof(IOriginator))]
		int? Originator1Id { get; set; }
	}

	public interface IUiBindingSettings2Originators : IUiBindingSettings1Originator
	{
		[OriginatorIdSettingsProperty(typeof(IOriginator))]
		int? Originator2Id { get; set; }
	}

	public interface IUiBindingSettings3Originators : IUiBindingSettings2Originators
	{
		[OriginatorIdSettingsProperty(typeof(IOriginator))]
		int? Originator3Id { get; set; }
	}

	public interface IUiBindingSettings4Originators : IUiBindingSettings3Originators
	{
		[OriginatorIdSettingsProperty(typeof(IOriginator))]
		int? Originator4Id { get; set; }
	}
}