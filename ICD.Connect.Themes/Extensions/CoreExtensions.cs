using System.Collections.Generic;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Core;

namespace ICD.Connect.Themes.Extensions
{
	public sealed class UserInterfaceFactoryCollection : AbstractOriginatorCollection<ITheme>
	{
		public UserInterfaceFactoryCollection()
		{
		}

		public UserInterfaceFactoryCollection(IEnumerable<ITheme> children) : base(children)
		{
		}
	}

	public static class CoreExtensions
	{
		public static UserInterfaceFactoryCollection GetUiFactories(this ICore core)
		{
			return new UserInterfaceFactoryCollection(core.Originators.GetChildren<ITheme>());
		}
	}
}