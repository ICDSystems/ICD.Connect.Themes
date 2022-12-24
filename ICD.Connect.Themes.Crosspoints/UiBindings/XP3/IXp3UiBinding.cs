using ICD.Connect.Protocol.Crosspoints;
using ICD.Connect.Themes.UiBindings;

namespace ICD.Connect.Themes.Crosspoints.UiBindings.XP3
{
	public interface IXp3UiBinding : IUiBinding
	{
		/// <summary>
		/// The Xp3 control.
		/// </summary>
		Xp3 Xp3 { get; }
	}
}
