using System;
using ICD.Common.Properties;
using ICD.Common.Utils.EventArguments;
using ICD.Connect.Panels.CrestronPro.Controls.Streaming.Dge;
using ICD.Connect.Routing;

namespace ICD.Connect.Themes.Components.Helpers
{
	public sealed class StreamSwitcherEmbeddedVideoPresenterHelper : IDisposable
	{
		private int? m_SelectedInput;

		private IEnhancedEmbeddedVideoView m_View;
		private IDgeX00StreamSwitcherControl m_DeviceControl;

		public StreamSwitcherEmbeddedVideoPresenterHelper([NotNull] IEnhancedEmbeddedVideoView view,
		                                           [NotNull] IDgeX00StreamSwitcherControl deviceControl)
		{
			SetView(view);
			SetDeviceControl(deviceControl);
		}

		public void Dispose()
		{
			SetView(null);
			SetDeviceControl(null);
		}

		#region PanelControl Callbacks

		private void SetView(IEnhancedEmbeddedVideoView view)
		{
			if (m_View == view)
				return;

			Unsubscribe(m_View);
			m_View = view;
			Subscribe(m_View);
		}

		private void Subscribe(IEnhancedEmbeddedVideoView panelControl)
		{
			if (panelControl == null)
				return;

			panelControl.OnEmbeddedVideoPlayingChanged += PanelControlOnEmbeddedVideoPlayingChanged;
		}

		private void Unsubscribe(IEnhancedEmbeddedVideoView panelControl)
		{
			if (panelControl == null)
				return;

			panelControl.OnEmbeddedVideoPlayingChanged -= PanelControlOnEmbeddedVideoPlayingChanged;
		}

		private void PanelControlOnEmbeddedVideoPlayingChanged(object sender, BoolEventArgs args)
		{
			m_DeviceControl.SetInputActive(args.Data ? m_SelectedInput : null);
		}

		#endregion

		#region DeviceControl Callbacks

		private void SetDeviceControl(IDgeX00StreamSwitcherControl control)
		{
			if (m_DeviceControl == control)
				return;

			Unsubscribe(m_DeviceControl);
			m_DeviceControl = control;
			Subscribe(m_DeviceControl);

		}

		private void Subscribe(IDgeX00StreamSwitcherControl control)
		{
			if (control == null)
				return;

			control.UiRoute = UiRoute;
			control.UiClearOutput = UiClearOutput;
			control.UiSetStream = UiSetStream;
		}

		private void Unsubscribe(IDgeX00StreamSwitcherControl control)
		{
			if (control == null)
				return;

			control.UiRoute = null;
			control.UiClearOutput = null;
			control.UiSetStream = null;
		}

		private bool UiRoute(RouteOperation info)
		{
			int input = info.LocalInput;

			if (m_View.EmbeddedVideoMaxSources <= input)
				return false;

			m_View.EmbeddedVideoSetSource((ushort)input);
			m_View.EmbeddedVideoSetOnOff(true);

			m_SelectedInput = input;

			//Set active input if panel is showing
			if (m_View.IsEmbeddedVideoPlaying)
				m_DeviceControl.SetInputActive(input);

			return true;
		}

		private bool UiClearOutput()
		{
			m_View.EmbeddedVideoSetOnOff(false);

			m_SelectedInput = null;

			return true;
		}

		private bool UiSetStream(int input, Uri stream)
		{
			string url = stream == null ? string.Empty : stream.ToString();
			m_View.EmbeddedVideoSetUrl(input, url);
			return true;
		}

		#endregion
	}
}