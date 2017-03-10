using System;
using System.Collections.Generic;
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

namespace MonsterManagement
{
	/// <summary>
	/// Interaction logic for Perso.xaml
	/// </summary>
	public partial class Perso : Page
	{
		public Perso()
		{
			InitializeComponent();
		}

		private void Valider_Click(object sender, RoutedEventArgs e)
		{
			if (sender.Equals(Valider))
			{
				short nombreCreature;
				short.TryParse(NombreCreature.Text, out nombreCreature);
				Stats stats = new Stats(nombreCreature);

				NavigationService.Navigate(stats);
			}
		}
	}
}
