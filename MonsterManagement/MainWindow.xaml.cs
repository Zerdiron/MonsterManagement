using System.Windows;
using System.Windows.Navigation;

namespace MonsterManagement
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : NavigationWindow
	{
		private void Invoc_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			MessageBoxResult boxResult = MessageBox.Show("Voulez-vous vraiment quitter l'application ?", "Confirmation de fermeture", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (boxResult == MessageBoxResult.No)
				e.Cancel = true;
		}
		
	}
}
