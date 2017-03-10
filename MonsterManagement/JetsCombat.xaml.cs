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
		// Attributs.
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
		/// Tableau des Labels pour l'affichage.
		/// </summary>
		private Label[] TabInvocLabel;

		/// <summary>
		/// Si l'invocation joue avec avantage ou non à ses jets.
		/// </summary>
		private bool _Avantage;

		/// <summary>
		/// Si l'invocation joue avec désavantage ou non à ses jets.
		/// </summary>
		private bool _Desavantage;

		/// <summary>
		/// LE Randomizer !
		/// </summary>
		static readonly Random Randomizer = new Random();

		// Constructeur.
		/// <summary>
		/// Constructeur qui construit la page en fonction du nombre de créatures.
		/// </summary>
		/// <param name="NombreInvoc">Le nombre de créatures.</param>
		/// <param name="Caracs">Le tableau des stats des créatures.</param>
		/// <param name="JDS">Le tableau des Jets De Sauvegarde des créatures.</param>
		/// <param name="AttaqueUn">Le tableau des stats de la première attaque.</param>
		/// <param name="AttaqueDeux">Le tableau des stats de la seconde attaque.</param>
		/// <param name="AttaqueTrois">Le tableau des stats de la troisième attaque.</param>
		public JetsCombat(int NombreInvoc, short[] Caracs, short[] JDS, short[] AttaqueUn, short[] AttaqueDeux, short[] AttaqueTrois)
		{
			// On récupère les variables transmises au constructeur.
			_Caracs = Caracs; _JDS = JDS;
			_AttaqueUn = AttaqueUn; _AttaqueDeux = AttaqueDeux; _AttaqueTrois = AttaqueTrois;
			_nombreInvoc = NombreInvoc;

			InitializeComponent();

			TabInvocLabel = new Label[NombreInvoc];

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
				// Ajout de lignes.
				RowDefinition row = new RowDefinition();
				row.Height = new GridLength(26);
				JetCombat.RowDefinitions.Add(row);

				// Définition des labels.
				Label label = new Label();
				if (i % 2 == 0) label.Background = Brushes.Coral;
				else label.Background = Brushes.LightSteelBlue;
				label.Foreground = Brushes.Black;
				Grid.SetRow(label, i + k); Grid.SetColumn(label, j);
				JetCombat.Children.Add(label);
				TabInvocLabel[i - 1] = label;

				if (i > 1 && i % 16 == 0) { j++; k -= 16; }
			}

			// Affiche le "nombre de créature" invoquées.
			InitialisationAffichage();

		}

		// Evènements.
		/// <summary>
		/// Actualise les données en fonction du bouton qui est appuyé.
		/// </summary>
		/// <param name="sender">Le bouton qui déclenche l'évènement.</param>
		/// <param name="e">L'évènement en question.</param>
		private void Refresh_Click(object sender, RoutedEventArgs e)
		{
			if (sender.Equals(JetAttaque))
			{
				InitialisationAffichage();
				if (_AttaqueUn[2] > 0)
					LancerAttaque(_AttaqueUn);

				if (_AttaqueDeux[2] > 0)
					LancerAttaque(_AttaqueDeux);

				if (_AttaqueTrois[2] > 0)
					LancerAttaque(_AttaqueTrois);
			}
			else if (sender.Equals(JetSauvegarde))
			{
				InitialisationAffichage();
				for (short i = 0; i < _nombreInvoc; i++)
				{
					for (short indiceCarac = 0; indiceCarac < 6; indiceCarac++)
					{
						int d20 = Randomizer.Next(1, 21);
						int Save = d20;

						if (_Avantage)
						{
							int d20_2 = Randomizer.Next(1, 21);
							if (d20_2 > d20)
								Save = d20_2;
							else
								Save = d20;
						}
						else if (_Desavantage)
						{
							int d20_2 = Randomizer.Next(1, 21);
							if (d20_2 < d20)
								Save = d20_2;
							else
								Save = d20;
						}

						string info = "";

						int Bonus = _JDS[indiceCarac];
						string Final = (Save + Bonus).ToString();
						if (Save + Bonus < 10)
							Final = 0 + Final;

						string Carac = "";
						if (indiceCarac == 0) Carac = "For";
						else if (indiceCarac == 1) Carac = "Dex";
						else if (indiceCarac == 2) Carac = "Con";
						else if (indiceCarac == 3) Carac = "Int";
						else if (indiceCarac == 4) Carac = "Sag";
						else if (indiceCarac == 5) Carac = "Cha";

						TabInvocLabel[i].Content += string.Format("{0}:{1}{2} ", Carac, Final, info);
					}
				}
			}
		}

		/// <summary>
		/// Quand la case avantage est coché tourne le booléen en true.
		/// </summary>
		private void Avantage_Checked(object sender, RoutedEventArgs e)
		{
			_Avantage = true;
			Desavantage.IsChecked = false;
		}

		/// <summary>
		/// Quand la case avantage est décoché tourne le booléen en false.
		/// </summary>
		private void Avantage_Unchecked(object sender, RoutedEventArgs e)
		{
			_Avantage = false;
		}

		/// <summary>
		/// Quand la case avantage est coché tourne le booléen en true.
		/// </summary>
		private void Desavantage_Checked(object sender, RoutedEventArgs e)
		{
			_Desavantage = true;
			Avantage.IsChecked = false;
		}

		/// <summary>
		/// Quand la case désavantage est décoché tourne le booléen en false.
		/// </summary>
		private void Desavantage_Unchecked(object sender, RoutedEventArgs e)
		{
			_Desavantage = false;
		}

		// Fonctions.
		/// <summary>
		/// Initialise l'affichage.
		/// </summary>
		private void InitialisationAffichage()
		{
			for (short indiceInvoc = 0; indiceInvoc < _nombreInvoc; indiceInvoc++)
			{
				string numero = (indiceInvoc + 1).ToString();
				if (indiceInvoc + 1 < 10)
					numero = 0 + numero;
				TabInvocLabel[indiceInvoc].Content = string.Format("Invoc {0}: ", numero);
			}
		}

		/// <summary>
		/// Permet de lancer les attaques de la créature.
		/// </summary>
		/// <param name="tableauAttaque">Le tableau de stats de l'attaque en question.</param>
		/// <param name="indiceInvoc">L'indice de l'invocation qui fait l'attaque.</param>
		private void LancerAttaque(short[] tableauAttaque)
		{
			for (short indiceInvoc = 0; indiceInvoc < _nombreInvoc; indiceInvoc++)
			{

				int d20 = Randomizer.Next(1, 21);
				int jetAttaqueNaturel = d20;

				if (_Avantage)
				{
					int d20_2 = Randomizer.Next(1, 21);
					if (d20_2 > d20)
						jetAttaqueNaturel = d20_2;
					else
						jetAttaqueNaturel = d20;
				}
				else if (_Desavantage)
				{
					int d20_2 = Randomizer.Next(1, 21);
					if (d20_2 < d20)
						jetAttaqueNaturel = d20_2;
					else
						jetAttaqueNaturel = d20;
				}

				int nombreDe = tableauAttaque[1];
				string crit = "       ";

				if (jetAttaqueNaturel == 20)
				{
					nombreDe = nombreDe * 2;
					crit = "(crit)";
				}
				else if (jetAttaqueNaturel == 1)
					crit = "(fail)";

				string Attaque = (jetAttaqueNaturel + tableauAttaque[0]).ToString();
				if (jetAttaqueNaturel + tableauAttaque[0] < 10)
					Attaque = 0 + Attaque;

				int totalDegat = 0;

				for (short j = 0; j < nombreDe; j++)
					totalDegat += Randomizer.Next(1, tableauAttaque[2] + 1);

				totalDegat += tableauAttaque[3];

				string Degat = totalDegat.ToString();

				if (totalDegat < 10)
					Degat = 0 + Degat;

				TabInvocLabel[indiceInvoc].Content += string.Format(" {0}|{1}{2}", Attaque, Degat, crit);
			}

		}
	}
}
