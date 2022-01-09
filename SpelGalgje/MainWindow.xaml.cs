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

namespace SpelGalgje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string geheimWoord;
        int levensBegin = 10;
        string juist;
        string fout;
        //private bool isJuist = false;
        public MainWindow()
        {
            InitializeComponent();
            StringBuilder stringbuilder = new StringBuilder();
            stringbuilder.AppendLine("Geef een woord in");
          
            lblMain.Content = stringbuilder.ToString();
           
        }

        private void btnStartSpel_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringbuilder = new StringBuilder();
            btnStartSpel.Visibility = Visibility.Hidden;
            btnRaadWoord.Visibility = Visibility.Visible;
           
            if(String.IsNullOrEmpty(txbGeheimWoord.Text))
            {
                MessageBox.Show("Geen woord opgeslagen");
                return;
            }
            else
            {
                geheimWoord = txbGeheimWoord.Text;
            }
            
            txbGeheimWoord.Text = "";

            stringbuilder.AppendLine("Juiste letters: ").AppendLine("Foute letters: ");
            lblMain.Content = stringbuilder.ToString();
            lblLevens.Content = levensBegin.ToString();
            //het woord en start nieuw spel
          

        }
       
        private void btnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            levensBegin = 10;
            lblMain.Content = "Geef een woord in";
            btnStartSpel.Visibility= Visibility.Visible;
            btnRaadWoord.Visibility= Visibility.Hidden;
            txbGeheimWoord.Text = "";
           
        }

        private void btnRaadWoord_Click(object sender, RoutedEventArgs e)
        {
            LetterCheck();
            
        }

        
        private void LetterCheck()
        {
            StringBuilder stringbuilder = new StringBuilder();
         
           
            string letters = txbGeheimWoord.Text;
            string winBericht = "";
            if (geheimWoord.Equals(letters))
            {
                winBericht = "😎 Proficiaat je wint het";
            }
            else if (levensBegin == 0)
            {
                winBericht = "😔 Probeer opnieuw";
            }
            lblMain.Content = stringbuilder.ToString();

            if (geheimWoord.Contains(letters))
                {
                    juist += letters;
                }
                else
                {
                    levensBegin--;
                    fout += letters;
                }
            
                stringbuilder.AppendLine("Juiste letters: " + juist).AppendLine("Foute letters: " + fout);

            lblMain.Content = stringbuilder.ToString();
            lblMain.Content = winBericht;

        }
        

    }
}
