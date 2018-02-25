using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TwoCansImageConverter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ImportButton_Click(object sender, RoutedEventArgs e)
		{
			const int redOffset = 2;
			const int greenOffset = 1;
			const int blueOffset = 0;

			// Get image filename.
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Title = "Import Profile Image";
			if (dlg.ShowDialog() == true) {
				// Open image as bitmap.
				string filename = dlg.FileName;
				BitmapImage image = new BitmapImage(new Uri(filename));

				// Get image properties.
				int width = image.PixelWidth;
				int height = image.PixelHeight;
				int bytesPerPixel = (image.Format.BitsPerPixel + 7) / 8;
				int stride = width * bytesPerPixel;

				// Convert image to byte array.
				byte[] imageData = new byte[height * stride];
				image.CopyPixels(imageData, stride, 0);

				// Build image string.
				StringBuilder imageString = new StringBuilder("image := []\n");
				int pixelCount = imageData.Length / bytesPerPixel;
				for (int pixelIdx = 0; pixelIdx < pixelCount; ++pixelIdx) {
					int byteIdx = pixelIdx * bytesPerPixel;
					imageString.Append("image.Push(");
					imageString.Append("[" + imageData[byteIdx + redOffset]);
					imageString.Append("," + imageData[byteIdx + greenOffset]);
					imageString.Append("," + imageData[byteIdx + blueOffset] + "])\n");
				}

				// Set textbox text and copy to clipboard.
				TextBox.Text = imageString.ToString();
				TextBox.SelectAll();
				Clipboard.SetText(TextBox.Text);
				CopiedLabel.Visibility = System.Windows.Visibility.Visible;

				// Hide label after 2 seconds.
				DispatcherTimer timer = new DispatcherTimer();
				timer.Interval = TimeSpan.FromSeconds(2.0);
				timer.Tick += new EventHandler(delegate(object s, EventArgs a) {
					CopiedLabel.Visibility = System.Windows.Visibility.Hidden;
				});
				timer.Start();
			}
		}

		private void ExportButton_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
			dlg.Title = "Export Image Data";
			dlg.FileName = "data.ahk";
			dlg.Filter = "AutoHotkey Script|*.ahk";
			if (dlg.ShowDialog() == true) {
				File.WriteAllText(dlg.FileName, TextBox.Text);
			}
		}
	}
}
