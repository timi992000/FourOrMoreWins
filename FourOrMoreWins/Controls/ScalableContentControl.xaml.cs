using System.Windows;
using System.Windows.Controls;

namespace FourOrMoreWins.Client.Controls
{
	/// <summary>
	/// Interaction logic for ScalableContentControl.xaml
	/// </summary>
	public partial class ScalableContentControl : UserControl
	{
		public ScalableContentControl()
		{
			InitializeComponent();
		}

		#region MyContent
		public static object GetMyContent(DependencyObject obj)
		{
			return obj.GetValue(MyContentProperty);
		}

		public static void SetMyContent(DependencyObject obj, object value)
		{
			obj.SetValue(MyContentProperty, value);
		}

		// Using a DependencyProperty as the backing store for MyContent.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MyContentProperty =
				DependencyProperty.RegisterAttached("MyContent", typeof(object), typeof(ScalableContentControl));
		#endregion

		public double ScalingFactor { get; set; }

	}
}
