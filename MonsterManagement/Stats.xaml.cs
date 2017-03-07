using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MonsterManagement
{
	/// <summary>
	/// Interaction logic for Stats.xaml
	/// </summary>
	public partial class Stats : Page
	{
		private int NombreInvoc = 0;
		private int BaseInvocByFP;
		private int MultiplicateurLevel;
		private bool avantage = false;

		public Stats(short level, short fp)
		{

			if (fp == 1) BaseInvocByFP = 8;
			else if (fp == 2) BaseInvocByFP = 4;
			else if (fp == 3) BaseInvocByFP = 2;
			else if (fp == 4) BaseInvocByFP = 1;

			if (level == 3) MultiplicateurLevel = 1;
			else if (level == 5) MultiplicateurLevel = 2;
			else if (level == 7) MultiplicateurLevel = 3;
			else if (level == 9) MultiplicateurLevel = 4;

			NombreInvoc = BaseInvocByFP * MultiplicateurLevel;

			InitializeComponent();
		}

		private void Valider_Click(object sender, RoutedEventArgs e)
		{
			// Stats.
			short force; short.TryParse(Force.Text, out force);
			short dexterite; short.TryParse(Dexterite.Text, out dexterite);
			short constitution; short.TryParse(Constitution.Text, out constitution);
			short intelligence; short.TryParse(Intelligence.Text, out intelligence);
			short sagesse; short.TryParse(Sagesse.Text, out sagesse);
			short charisme; short.TryParse(Charisme.Text, out charisme);
			short[] Caracs = { force, dexterite, constitution, intelligence, sagesse, charisme };

			// Jds.
			short jdsFor; short.TryParse(JDSFor.Text, out jdsFor);
			short jdsDex; short.TryParse(JDSDex.Text, out jdsDex);
			short jdsCon; short.TryParse(JDSCon.Text, out jdsCon);
			short jdsInt; short.TryParse(JDSInt.Text, out jdsInt);
			short jdsSag; short.TryParse(JDSSag.Text, out jdsSag);
			short jdsCha; short.TryParse(JDSCha.Text, out jdsCha);
			short[] JDS = { jdsFor, jdsDex, jdsCon, jdsInt, jdsSag, jdsCha };
			// Attaque un.
			short bonusToucherUn; short.TryParse(ToucherUn.Text, out bonusToucherUn);
			short deAttaqueUn; short.TryParse(NombreDeUn.Text, out deAttaqueUn);
			short typeDeUn; short.TryParse(TypeDeUn.Text, out typeDeUn);
			short degatUn; short.TryParse(DegatUn.Text, out degatUn);
			short[] attaqueUn = { bonusToucherUn, deAttaqueUn, typeDeUn, degatUn };
			// Attaque deux.
			short bonusToucherDeux; short.TryParse(ToucherDeux.Text, out bonusToucherDeux);
			short deAttaqueDeux; short.TryParse(NombreDeDeux.Text, out deAttaqueDeux);
			short typeDeDeux; short.TryParse(TypeDeDeux.Text, out typeDeDeux);
			short degatDeux; short.TryParse(DegatDeux.Text, out degatDeux);
			short[] attaqueDeux = { bonusToucherDeux, deAttaqueDeux, typeDeDeux, degatDeux };
			// Attaque trois.
			short bonusToucherTrois; short.TryParse(ToucherTrois.Text, out bonusToucherTrois);
			short deAttaqueTrois; short.TryParse(NombreDeTrois.Text, out deAttaqueTrois);
			short typeDeTrois; short.TryParse(TypeDeTrois.Text, out typeDeTrois);
			short degatTrois; short.TryParse(DegatTrois.Text, out degatTrois);
			short[] attaqueTrois = { bonusToucherTrois, deAttaqueTrois, typeDeTrois, degatTrois };

			JetsCombat jetsCombat = new JetsCombat(NombreInvoc, Caracs, JDS, attaqueUn, attaqueDeux, attaqueTrois, avantage);

			NavigationService.Navigate(jetsCombat);
		}

		private void Avantage_Checked(object sender, RoutedEventArgs e)
		{
			avantage = true;
		}

		private void Avantage_Unchecked(object sender, RoutedEventArgs e)
		{
			avantage = false;
		}
	}
}
