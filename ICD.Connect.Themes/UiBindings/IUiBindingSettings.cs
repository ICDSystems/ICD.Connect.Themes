using ICD.Connect.Settings;

namespace ICD.Connect.Themes.UiBindings
{
	public interface IUiBindingSettings : ISettings
	{
		/// <summary>
		/// Gets/sets the ID for the originator to bind against.
		/// </summary>
		int? OriginatorId { get; set; }
	}
}