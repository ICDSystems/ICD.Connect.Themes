using System;
using ICD.Common.Properties;
using ICD.Common.Utils.EventArguments;

namespace ICD.Connect.Themes.Components.Helpers
{
	public interface IEnhancedEmbeddedVideoView
	{
		#region Events

		/// <summary>
		/// Raised when the user presses/releases the video window
		/// </summary>
		[PublicAPI]
		event EventHandler<BoolEventArgs> OnEmbeddedVideoPressedChanged;

		/// <summary>
		/// Raised when video starts/stops playing
		/// </summary>
		[PublicAPI]
		event EventHandler<BoolEventArgs> OnEmbeddedVideoPlayingChanged;

		/// <summary>
		/// Raised when the snapshot shows/hides
		/// </summary>
		[PublicAPI]
		event EventHandler<BoolEventArgs> OnEmbeddedVideoSnapshotShowingChanged;

		#endregion

		#region Properties
		/// <summary>
		/// Maximum number of sources the control supports
		/// </summary>
		int EmbeddedVideoMaxSources { get; }

		/// <summary>
		/// If the video window is pressed or not
		/// </summary>
		[PublicAPI]
		bool IsEmbeddedVideoPressed { get; }

		/// <summary>
		/// If the video is playing or now
		/// </summary>
		[PublicAPI]
		bool IsEmbeddedVideoPlaying { get; }

		/// <summary>
		/// If the snapshot is showing or not
		/// </summary>
		[PublicAPI]
		bool IsEmbeddedVideoSnapshotShowing { get; }

		#endregion

		/// <summary>
		/// Sets the On/Off state of the control
		/// </summary>
		/// <param name="state"></param>
		[PublicAPI]
		void EmbeddedVideoSetOnOff(bool state);

		/// <summary>
		/// Sets the source for the control
		/// </summary>
		/// <param name="source"></param>
		[PublicAPI]
		void EmbeddedVideoSetSource(int source);

		/// <summary>
		/// Sets the source type for the source at the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="sourceType"></param>
		[PublicAPI]
		void EmbeddedVideoSetSourceType(int index, int sourceType);

		/// <summary>
		/// Sets the streaming URL for the source at the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="url"></param>
		[PublicAPI]
		void EmbeddedVideoSetUrl(int index, [CanBeNull] string url);

		/// <summary>
		/// Sets the snapshot refresh interval for the source at the specified index
		/// Snapshot time is in seconds, 0 indicates no snapshot refresh
		/// </summary>
		/// <param name="index"></param>
		/// <param name="snapshotRefreshTime"></param>
		[PublicAPI]
		void EmbeddedVideoSetSnapshotRefreshTime(int index, ushort snapshotRefreshTime);

		/// <summary>
		/// Sets the snapshot URL for the source at the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="url"></param>
		[PublicAPI]
		void EmbeddedVideoSetSnapshotUrl(int index, [CanBeNull] string url);
	}
}