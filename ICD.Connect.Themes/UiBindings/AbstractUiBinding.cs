using System;
using System.Collections.Generic;
using ICD.Common.Utils.Extensions;
using ICD.Common.Utils.Services.Logging;
using ICD.Connect.Settings;
using ICD.Connect.Settings.Originators;

namespace ICD.Connect.Themes.UiBindings
{
	public abstract class AbstractUiBinding<TSettings> : AbstractOriginator<TSettings>, IUiBinding
		where TSettings : IUiBindingSettings, new()
	{
		#region Properties

		/// <summary>
		/// Gets the category for this originator type (e.g. Device, Port, etc)
		/// </summary>
		public override string Category { get { return "UI Binding"; } }

		#endregion

		#region Methods

		/// <summary>
		/// Gets the originators that are bound against.
		/// </summary>
		/// <returns></returns>
		public abstract IEnumerable<IOriginator> GetOriginators();

		#endregion
	}

	public abstract class AbstractUiBinding<TOriginator1, TSettings> : AbstractUiBinding<TSettings>
		where TSettings : IUiBindingSettings1Originator, new()
		where TOriginator1 : class, IOriginator
	{
		#region Events

		public event EventHandler OnOriginator1Changed;

		#endregion

		#region Fields

		private TOriginator1 m_Originator1;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the first originator that is bound against.
		/// </summary>
		public TOriginator1 Originator1 { get { return m_Originator1; }
			private set
			{
				if (m_Originator1 == value)
					return;

				UnsubscribeOriginator1(m_Originator1);

				m_Originator1 = value;

				SubscribeOriginator1(m_Originator1);

				OnOriginator1Changed.Raise(this);
			} 
		}

		#endregion

		#region Methods

		public override IEnumerable<IOriginator> GetOriginators()
		{
			yield return Originator1;
		}

		#endregion

		#region Originator1 Callbacks

		/// <summary>
		/// Subscribe to Originator1 events
		/// </summary>
		/// <param name="originator1"></param>
		protected virtual void SubscribeOriginator1(TOriginator1 originator1)
		{
		}

		/// <summary>
		/// Unsubscribe from Originator1 events
		/// </summary>
		/// <param name="originator1"></param>
		protected virtual void UnsubscribeOriginator1(TOriginator1 originator1)
		{
		}

		#endregion

		#region Settings

		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			Originator1 = null;
		}

		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.Originator1Id = Originator1 == null ? (int?)null : Originator1.Id;
		}

		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			base.ApplySettingsFinal(settings, factory);

			TOriginator1 originator = null;

			if (settings.Originator1Id != null)
			{
				try
				{
					originator = factory.GetOriginatorById((int)settings.Originator1Id) as TOriginator1;
				}
				catch (KeyNotFoundException)
				{
					Logger.Log(eSeverity.Error, "No {0} with id {1}", typeof(TOriginator1).Name, settings.Originator1Id);
				}
			}

			Originator1 = originator;
		}

		#endregion
	}

	public abstract class AbstractUiBinding<TOriginator1, TOriginator2, TSettings> : AbstractUiBinding<TOriginator1, TSettings>
		where TSettings : IUiBindingSettings2Originators, new()
		where TOriginator1 : class, IOriginator
		where TOriginator2 : class, IOriginator
	{
		#region Events

		public event EventHandler OnOriginator2Changed;

		#endregion

		#region Fields

		private TOriginator2 m_Originator2;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the first originator that is bound against.
		/// </summary>
		public TOriginator2 Originator2
		{
			get { return m_Originator2; }
			private set
			{
				if (m_Originator2 == value)
					return;

				UnsubscribeOriginator2(m_Originator2);

				m_Originator2 = value;

				SubscribeOriginator2(m_Originator2);

				OnOriginator2Changed.Raise(this);
			}
		}

		#endregion

		#region Methods

		public override IEnumerable<IOriginator> GetOriginators()
		{
			foreach (IOriginator originator in GetBaseOriginstors())
				yield return originator;

			yield return Originator2;
		}

		private IEnumerable<IOriginator> GetBaseOriginstors()
		{
			return base.GetOriginators();
		}

		#endregion

		#region Originator2 Callbacks

		/// <summary>
		/// Subscribe to Originator2 events
		/// </summary>
		/// <param name="originator2"></param>
		protected virtual void SubscribeOriginator2(TOriginator2 originator2)
		{
		}

		/// <summary>
		/// Unsubscribe from Originator2 events
		/// </summary>
		/// <param name="originator2"></param>
		protected virtual void UnsubscribeOriginator2(TOriginator2 originator2)
		{
		}

		#endregion

		#region Settings

		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			Originator2 = null;
		}

		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.Originator2Id = Originator2 == null ? (int?)null : Originator2.Id;
		}

		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			base.ApplySettingsFinal(settings, factory);

			TOriginator2 originator = null;

			if (settings.Originator2Id != null)
			{
				try
				{
					originator = factory.GetOriginatorById((int)settings.Originator2Id) as TOriginator2;
				}
				catch (KeyNotFoundException)
				{
					Logger.Log(eSeverity.Error, "No {0} with id {1}", typeof(TOriginator2).Name, settings.Originator2Id);
				}
			}

			Originator2 = originator;
		}

		#endregion
	}

	public abstract class AbstractUiBinding<TOriginator1, TOriginator2, TOriginator3, TSettings> : AbstractUiBinding<TOriginator1, TOriginator2, TSettings>
		where TSettings : IUiBindingSettings3Originators, new()
		where TOriginator1 : class, IOriginator
		where TOriginator2 : class, IOriginator
		where TOriginator3 : class, IOriginator
	{
		#region Events

		public event EventHandler OnOriginator3Changed;

		#endregion

		#region Fields

		private TOriginator3 m_Originator3;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the first originator that is bound against.
		/// </summary>
		public TOriginator3 Originator3
		{
			get { return m_Originator3; }
			private set
			{
				if (m_Originator3 == value)
					return;

				UnsubscribeOriginator3(m_Originator3);

				m_Originator3 = value;

				SubscribeOriginator3(m_Originator3);

				OnOriginator3Changed.Raise(this);
			}
		}

		#endregion

		#region Methods

		public override IEnumerable<IOriginator> GetOriginators()
		{
			foreach (IOriginator originator in GetBaseOriginstors())
				yield return originator;

			yield return Originator3;
		}

		private IEnumerable<IOriginator> GetBaseOriginstors()
		{
			return base.GetOriginators();
		}

		#endregion

		#region Originator3 Callbacks

		/// <summary>
		/// Subscribe to Originator3 events
		/// </summary>
		/// <param name="originator3"></param>
		protected virtual void SubscribeOriginator3(TOriginator3 originator3)
		{
		}

		/// <summary>
		/// Unsubscribe from Originator3 events
		/// </summary>
		/// <param name="originator3"></param>
		protected virtual void UnsubscribeOriginator3(TOriginator3 originator3)
		{
		}

		#endregion

		#region Settings

		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			Originator3 = null;
		}

		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.Originator3Id = Originator3 == null ? (int?)null : Originator3.Id;
		}

		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			base.ApplySettingsFinal(settings, factory);

			TOriginator3 originator = null;

			if (settings.Originator3Id != null)
			{
				try
				{
					originator = factory.GetOriginatorById((int)settings.Originator3Id) as TOriginator3;
				}
				catch (KeyNotFoundException)
				{
					Logger.Log(eSeverity.Error, "No {0} with id {1}", typeof(TOriginator3).Name, settings.Originator3Id);
				}
			}

			Originator3 = originator;
		}

		#endregion
	}

	public abstract class AbstractUiBinding<TOriginator1, TOriginator2, TOriginator3, TOriginator4, TSettings> : AbstractUiBinding<TOriginator1, TOriginator2, TOriginator3, TSettings>
		where TSettings : IUiBindingSettings4Originators, new()
		where TOriginator1 : class, IOriginator
		where TOriginator2 : class, IOriginator
		where TOriginator3 : class, IOriginator
		where TOriginator4 : class, IOriginator
	{
		#region Events

		public event EventHandler OnOriginator4Changed;

		#endregion

		#region Fields

		private TOriginator4 m_Originator4;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the first originator that is bound against.
		/// </summary>
		public TOriginator4 Originator4
		{
			get { return m_Originator4; }
			private set
			{
				if (m_Originator4 == value)
					return;

				UnsubscribeOriginator4(m_Originator4);

				m_Originator4 = value;

				SubscribeOriginator4(m_Originator4);

				OnOriginator4Changed.Raise(this);
			}
		}

		#endregion

		#region Methods

		public override IEnumerable<IOriginator> GetOriginators()
		{
			foreach (IOriginator originator in GetBaseOriginstors())
				yield return originator;

			yield return Originator4;
		}

		private IEnumerable<IOriginator> GetBaseOriginstors()
		{
			return base.GetOriginators();
		}

		#endregion

		#region Originator4 Callbacks

		/// <summary>
		/// Subscribe to Originator4 events
		/// </summary>
		/// <param name="originator4"></param>
		protected virtual void SubscribeOriginator4(TOriginator4 originator4)
		{
		}

		/// <summary>
		/// Unsubscribe from Originator4 events
		/// </summary>
		/// <param name="originator4"></param>
		protected virtual void UnsubscribeOriginator4(TOriginator4 originator4)
		{
		}

		#endregion

		#region Settings

		protected override void ClearSettingsFinal()
		{
			base.ClearSettingsFinal();

			Originator4 = null;
		}

		protected override void CopySettingsFinal(TSettings settings)
		{
			base.CopySettingsFinal(settings);

			settings.Originator4Id = Originator4 == null ? (int?)null : Originator4.Id;
		}

		protected override void ApplySettingsFinal(TSettings settings, IDeviceFactory factory)
		{
			base.ApplySettingsFinal(settings, factory);

			TOriginator4 originator = null;

			if (settings.Originator4Id != null)
			{
				try
				{
					originator = factory.GetOriginatorById((int)settings.Originator4Id) as TOriginator4;
				}
				catch (KeyNotFoundException)
				{
					Logger.Log(eSeverity.Error, "No {0} with id {1}", typeof(TOriginator4).Name, settings.Originator4Id);
				}
			}

			Originator4 = originator;
		}
		#endregion
	}
}
