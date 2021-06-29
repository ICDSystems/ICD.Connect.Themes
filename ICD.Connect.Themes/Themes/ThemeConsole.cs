using System;
using System.Collections.Generic;
using ICD.Common.Utils;
using ICD.Connect.API.Commands;
using ICD.Connect.API.Nodes;
using ICD.Connect.Partitioning.Rooms;
using ICD.Connect.Settings.Originators;
using ICD.Connect.Themes.UiBindings;
using ICD.Connect.Themes.UserInterfaceFactories;
using ICD.Connect.Themes.UserInterfaces;

namespace ICD.Connect.Themes
{
	public static class ThemeConsole
	{
		/// <summary>
		/// Gets the child console nodes.
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static IEnumerable<IConsoleNodeBase> GetConsoleNodes(ITheme instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			yield break;
		}

		/// <summary>
		/// Calls the delegate for each console status item.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="addRow"></param>
		public static void BuildConsoleStatus(ITheme instance, AddStatusRowDelegate addRow)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");
		}

		/// <summary>
		/// Gets the child console commands.
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static IEnumerable<IConsoleCommand> GetConsoleCommands(ITheme instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			yield return new ConsoleCommand("PrintUis", "Prints information about the current UIs", () => PrintUis(instance));
			yield return new ConsoleCommand("PrintUiBindings", "Prints information about the configured UI bindings", () => PrintUiBindings(instance));
		}

		private static string PrintUiBindings(ITheme instance)
		{
			TableBuilder builder = new TableBuilder("UI Binding", "Originator");

			foreach (IUiBinding binding in instance.UiBindings)
			{
				foreach (IOriginator originator in binding.GetOriginators())
				{
					builder.AddRow(binding, originator);
				}
			}

			return builder.ToString();
		}

		private static string PrintUis(ITheme instance)
		{
			TableBuilder builder = new TableBuilder("Type", "Room", "Target");

			foreach (IUserInterfaceFactory factory in instance.GetUiFactories())
			{
				foreach (IUserInterface ui in factory.GetUserInterfaces())
				{
					Type type = ui.GetType();
					IRoom room = ui.Room;
					object target = ui.Target;

					builder.AddRow(type, room, target);
				}
			}

			return builder.ToString();
		}
	}
}
