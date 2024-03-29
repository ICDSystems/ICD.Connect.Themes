﻿using ICD.Common.Properties;
using ICD.Connect.Settings;

namespace ICD.Connect.Themes.Extensions
{
	public static class DeviceFactoryExtensions
	{
		[PublicAPI]
		public static ITheme GetThemeById(this IDeviceFactory factory, int id)
		{
			return factory.GetOriginatorById<ITheme>(id);
		}
	}
}
