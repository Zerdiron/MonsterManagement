using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MonsterManagement
{
	/// <summary>
	/// Interaction logic for Menu.xaml
	/// </summary>
	public partial class Menu : Page
	{
		public Menu()
		{
			InitializeComponent();
		}

		private void Choix_Click(object sender, RoutedEventArgs e)
		{
			if (sender.Equals(Spell))
			{
				Spell spell = new Spell();
				NavigationService.Navigate(spell);
			}
			else if (sender.Equals(Perso))
			{
				Perso perso = new Perso();
				NavigationService.Navigate(perso);
			}
		}
	}
}
