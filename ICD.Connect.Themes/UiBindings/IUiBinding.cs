using System.Collections.Generic;
using ICD.Connect.Settings.Originators;

namespace ICD.Connect.Themes.UiBindings
{
	public interface IUiBinding : IOriginator
	{
		/// <summary>
		/// Gets the originators that are bound against.
		/// </summary>
		/// <returns></returns>
		IEnumerable<IOriginator> GetOriginators();
	}
}
