using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MonsterManagement
{
	/// <summary>
	/// Interaction logic for Spell.xaml
	/// </summary>
	public partial class Spell : Page
	{
		private short Level = 0;
		private short fp = 0;

		public Spell()
		{
			InitializeComponent();
		}

		private void Valider_Click(object sender, RoutedEventArgs e)
		{
			Stats stats = new Stats(Level, fp);
			NavigationService.Navigate(stats);
		}

		private void Level_Checked(object sender, RoutedEventArgs e)
		{
			if (sender.Equals(Trois))
				Level = 3;
			if (sender.Equals(Cinq))
				Level = 5;
			if (sender.Equals(Sept))
				Level = 7;
			if (sender.Equals(Neuf))
				Level = 9;
		}

		private void FP_Checked(object sender, RoutedEventArgs e)
		{
			if (sender.Equals(UnQuart))
				fp = 1;
			if (sender.Equals(UnDemi))
				fp = 2;
			if (sender.Equals(Un))
				fp = 3;
			if (sender.Equals(Deux))
				fp = 4;
		}
	}
}
