using System;
using ICD.Common.Utils.EventArguments;
using ICD.Connect.Protocol.Crosspoints.Crosspoints;

namespace ICD.Connect.Themes.Crosspoints.UiBindings.XP3.Equipment
{
	public interface IXp3EquipmentUiBinding : IXp3UiBinding
	{
		event EventHandler<GenericEventArgs<IEquipmentCrosspoint>> OnCrosspointChanged;

		IEquipmentCrosspoint Crosspoint { get; }
	}
}