using ICD.Connect.Settings.Originators;

namespace ICD.Connect.Themes.UiBindings
{
	public interface IUiBinding : IOriginator
	{
		/// <summary>
		/// Gets the originator that is bound against.
		/// </summary>
		IOriginator Originator { get; }
	}

	public interface IUiBinding<TOriginator> : IUiBinding
	{
		/// <summary>
		/// Gets the originator that is bound against.
		/// </summary>
		new TOriginator Originator { get; }
	}
}
