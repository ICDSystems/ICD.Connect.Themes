using ICD.Common.Properties;
using ICD.Connect.Settings.Core;

namespace ICD.Connect.Themes.Extensions
{
	public static class DeviceFactoryExtensions
	{
		[PublicAPI]
		public static ITheme GetUiFactoryById(this IDeviceFactory factory, int id)
		{
			return factory.GetOriginatorById<ITheme>(id);
		}
	}
}