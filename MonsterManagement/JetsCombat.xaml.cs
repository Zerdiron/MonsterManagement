using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MonsterManagement
{
	/// <summary>
	/// Interaction logic for JetsCombat.xaml
	/// </summary>
	public partial class JetsCombat : Page
	{

		/// <summary>
		/// Le tableau de stats de l'invocation.
		/// </summary>
		private short[] _Caracs;

		/// <summary>
		/// Le tableau des jets de sauvegardes de l'invocation.
		/// </summary>
		private short[] _JDS;

		/// <summary>
		/// Le tableau des stats de la première attaque de l'invocation.
		/// </summary>
		private short[] _AttaqueUn;

		/// <summary>
		/// Le tableau des stats de la seconde attaque de l'invocation.
		/// </summary>
		private short[] _AttaqueDeux;

		/// <summary>
		/// Le tableau des stats de la troisième attaque de l'invocation.
		/// </summary>
		private short[] _AttaqueTrois;

		/// <summary>
		/// Nombre d'invocation invoqué déterminé par le FP et le Level du sort.
		/// </summary>
		private int _nombreInvoc;

		/// <summary>
		/// 
		/// </summary>
		private Label[] TabInvoc;

		/// <summary>
		/// Si l'invocation joue avec avantage ou non à ses jets.
		/// </summary>
		private bool _avantage;

		/// <summary>
		/// LE Randomizer !
		/// </summary>
		static readonly Random Randomizer = new Random();

		/// <summary>
		/// Constructeur qui construit la page en fonction du nombre de créatures.
		/// </summary>
		/// <param name="NombreInvoc">Le nombre de créatures.</param>
		/// <param name="Caracs">Le tableau des stats des créatures.</param>
		/// <param name="JDS">Le tableau des Jets De Sauvegarde des créatures.</param>
		/// <param name="AttaqueUn">Le tableau des stats de la première attaque.</param>
		/// <param name="AttaqueDeux">Le tableau des stats de la seconde attaque.</param>
		/// <param name="AttaqueTrois">Le tableau des stats de la troisième attaque.</param>
		public JetsCombat(int NombreInvoc, short[] Caracs, short[] JDS, short[] AttaqueUn, short[] AttaqueDeux, short[] AttaqueTrois, bool avantage)
		{
			// On récupère les variables transmises au constructeur.
			_Caracs = Caracs; _JDS = JDS;
			_AttaqueUn = AttaqueUn; _AttaqueDeux = AttaqueDeux; _AttaqueTrois = AttaqueTrois;
			_avantage = avantage;
			_nombreInvoc = NombreInvoc;

			InitializeComponent();

			TabInvoc = new Label[NombreInvoc];

			// On définit la taille des colonnes.
			ColumnDefinition colonne_un = new ColumnDefinition();
			GridLength colonne_un_width = new GridLength(600);
			// Si il y a plus de 16 invocations on réduit la taille des colonnes pour en afficher deux.
			if (NombreInvoc > 16)
			{
				colonne_un_width = new GridLength(300);
				ColumnDefinition colonne_deux = new ColumnDefinition();
				GridLength colonne_deux_width = new GridLength(300);
				colonne_deux.Width = colonne_deux_width;
				JetCombat.ColumnDefinitions.Add(colonne_deux);
			}
			colonne_un.Width = colonne_un_width;
			JetCombat.ColumnDefinitions.Add(colonne_un);

			// Variable d'initialisation pour la boucle.
			short j = 0; // Pour l'indice des colonnes de la Grid.
			short k = -1; // Sert pour insérer les invocations dans la deuxième colonne.

			for (int i = 1; i <= NombreInvoc; i++)
			{
				RowDefinition row = new RowDefinition();
				row.Height = new GridLength(26);
				JetCombat.RowDefinitions.Add(row);

				Label label = new Label();
				if (i % 2 == 0) label.Background = Brushes.Coral;
				else label.Background = Brushes.LightSteelBlue;
				label.Foreground = Brushes.Black;
				Grid.SetRow(label, i + k); Grid.SetColumn(label, j);
				JetCombat.Children.Add(label);
				TabInvoc[i - 1] = label;

				if (i > 1 && i % 16 == 0) { j++; k -= 16; }
			}

			Button JetAttaque = new Button();
			JetAttaque.Name = "JetAttaque";
			JetAttaque.HorizontalAlignment = HorizontalAlignment.Center;
			JetAttaque.VerticalAlignment = VerticalAlignment.Center;
			JetAttaque.Margin = new Thickness(200, 0, 0, 0);
			JetAttaque.Width = 150;
			JetAttaque.Click += Refresh_Click;
			JetAttaque.Content = "Jets d'attaque";
			Grid.SetColumn(JetAttaque, 0);
			Grid.SetRow(JetAttaque, 1);
			Button.Children.Add(JetAttaque);

			Button JetSauvegarde = new Button();
			JetSauvegarde.Name = "JetSauvegarde";
			JetSauvegarde.HorizontalAlignment = HorizontalAlignment.Center;
			JetSauvegarde.VerticalAlignment = VerticalAlignment.Center;
			JetSauvegarde.Margin = new Thickness(0, 0, 200, 0);
			JetSauvegarde.Width = 150;
			JetSauvegarde.Click += Refresh_Click;
			JetSauvegarde.Content = "Jets de Sauvegarde";
			Grid.SetColumn(JetSauvegarde, 0);
			Grid.SetRow(JetSauvegarde, 1);
			Button.Children.Add(JetSauvegarde);
		}

		private void Refresh_Click(object sender, RoutedEventArgs e)
		{
			if (sender.Equals(JetAttaque))
			{

				if (_AttaqueUn[2] > 0)
					for (short i = 0; i < _nombreInvoc; i++)
					{
						int d20 = Randomizer.Next(1, 21);
						int jetAttaqueNaturel = d20;

						if (_avantage)
						{
							int d20_2 = Randomizer.Next(1, 21);
							if (d20_2 > d20)
								jetAttaqueNaturel = d20_2;
							else
								jetAttaqueNaturel = d20;
						}

						int nombreDe = _AttaqueUn[1];
						string crit = "";

						if (jetAttaqueNaturel == 20)
						{
							nombreDe = nombreDe * 2;
							crit = "(crit)";
						}

						int totalDegat = 0;

						for (short j = 0; j < nombreDe; j++)
							totalDegat += Randomizer.Next(1, _AttaqueUn[2] + 1);

						totalDegat += _AttaqueUn[3];

						int jetAttaque = jetAttaqueNaturel + _AttaqueUn[0];

						TabInvoc[i].Content = string.Format("Invoc {0}: {1}|{2}{3}", i + 1, jetAttaque, totalDegat, crit);
					}

				if (_AttaqueDeux[2] > 0)
					for (short i = 0; i < _nombreInvoc; i++)
					{
						int d20 = Randomizer.Next(1, 21);
						int jetAttaqueNaturel = d20;

						if (_avantage)
						{
							int d20_2 = Randomizer.Next(1, 21);
							if (d20_2 > d20)
								jetAttaqueNaturel = d20_2;
							else
								jetAttaqueNaturel = d20;
						}

						int nombreDe = _AttaqueUn[1];
						string crit = "";

						if (jetAttaqueNaturel == 20)
						{
							nombreDe = nombreDe * 2;
							crit = "(crit)";
						}

						int totalDegat = 0;

						for (short j = 0; j < nombreDe; j++)
							totalDegat += Randomizer.Next(1, _AttaqueUn[2] + 1);

						totalDegat += _AttaqueUn[3];

						int jetAttaque = jetAttaqueNaturel + _AttaqueUn[0];

						TabInvoc[i].Content += string.Format(", {1}|{2}{3}", i + 1, jetAttaque, totalDegat, crit);
					}

				if (_AttaqueTrois[2] > 0)
					for (short i = 0; i < _nombreInvoc; i++)
					{
						int d20 = Randomizer.Next(1, 21);
						int jetAttaqueNaturel = d20;

						if (_avantage)
						{
							int d20_2 = Randomizer.Next(1, 21);
							if (d20_2 > d20)
								jetAttaqueNaturel = d20_2;
							else
								jetAttaqueNaturel = d20;
						}

						int nombreDe = _AttaqueUn[1];
						string crit = "";

						if (jetAttaqueNaturel == 20)
						{
							nombreDe = nombreDe * 2;
							crit = "(crit)";
						}

						int totalDegat = 0;

						for (short j = 0; j < nombreDe; j++)
							totalDegat += Randomizer.Next(1, _AttaqueUn[2] + 1);

						totalDegat += _AttaqueUn[3];

						int jetAttaque = jetAttaqueNaturel + _AttaqueUn[0];

						TabInvoc[i].Content += string.Format(", {1}|{2}{3}", i + 1, jetAttaque, totalDegat, crit);
					}
			}
		}
	}
}
