using Microsoft.VisualBasic;
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
using System.Windows.Threading;

namespace SpelGalgje
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatchertimer = new DispatcherTimer();
        
        private string geheimWoord;

        string spelerNaam;

        private int levensBegin = 10;
        
        private int imgIndex = 0;
        
        private int timerIndex = 0;

        /// <summary>
        /// Ik probeer de foto's te laten te draaien met string list en bitmap image maar de foto's worden in de eerste instantie niet getoond.
        /// </summary>
        private string[] GalgImg = new string[] 
        {
            "/Images/galg1.png", "/Images/galg2.png", "/Images/galg3.png", "/Images/galg4.png",
            "/Images/galg5.png", "/Images/galg6.png", "/Images/galg7.png",
        "/Images/galg8.png", "/Images/galg9.png", "/Images/galg10.png",
        };
     
        //private BitmapImage[] GalgImg = new BitmapImage[10]
        //     {
        //       new BitmapImage(new Uri(@"/Images/galg1.png", UriKind.Relative)),
        //        new BitmapImage(new Uri(@"/Images/galg2.png", UriKind.Relative)),
        //        new BitmapImage(new Uri(@"/Images/galg3.png", UriKind.Relative)),
        //        new BitmapImage(new Uri(@"/Images/galg4.png", UriKind.Relative)),
        //        new BitmapImage(new Uri(@"/Images/galg5.png", UriKind.Relative)),
        //        new BitmapImage(new Uri(@"/Images/galg6.png", UriKind.Relative)),
        //        new BitmapImage(new Uri(@"/Images/galg7.png", UriKind.Relative)),
        //        new BitmapImage(new Uri(@"/Images/galg8.png", UriKind.Relative)),
        //        new BitmapImage(new Uri(@"/Images/galg9.png", UriKind.Relative)),
        //        new BitmapImage(new Uri(@"/Images/galg10.png", UriKind.Relative)),
        //    };
        
        private string[] galgjeWoorden = new string[]
      {
             "grafeem","tjiftjaf", "maquette","kitsch","pochet","convocaat","jakkeren", "collaps","zuivel","cesium","voyant","spitten","pancake","gietlepel","karwats",
            "dehydreren","viswijf","flater","cretonne","sennhut","tichel","wijten","cadeau","trotyl","chopper","pielen","vigeren","vrijuit","dimorf","kolchoz","janhen",
             "plexus","borium","ontweien","quiche","ijverig","mecenaat","falset","telexen","hieruit","femelaar","cohesie","exogeen","plebejer","opbouw","zodiak","volder",
            "vrezen","convex","verzenden","ijstijd","fetisj","gerekt","necrose","conclaaf","clipper","poppetjes","looikuip","hinten","inbreng", "arbitraal","dewijl",
            "kapzaag", "welletjes",  "bissen", "catgut", "oxymoron","heerschaar","ureter", "kijkbuis","dryade","grofweg", "laudanum","excitatie", "revolte", "heugel",
            "geroerd", "hierbij","glazig","pussen", "liquide", "aquarium", "formol","kwelder","zwager", "vuldop", "halfaap", "hansop", "windvaan", "bewogen", "vulstuk",
            "efemeer", "decisief","omslag","prairie","schuit","weivlies", "ontzeggen","schijn","sousafoon"
      };
        public MainWindow()
        {
            InitializeComponent();
            StringBuilder stringbuilder = new StringBuilder();
            stringbuilder.AppendLine("");
          
            lblMain.Content = stringbuilder.ToString();

           spelerNaam = Interaction.InputBox("Geef je naam in", "Speler", "Naam", 100, 100);
            while (string.IsNullOrEmpty(spelerNaam)) // Cancel, Sluiten of lege string
            {
                MessageBox.Show("Geef je naam in", " Foutieve invoer"); //Hier als de vakje naam leeg is, wordt deze message box getoond 
                spelerNaam = Interaction.InputBox("Geef je naam in", "Invoer", "Naam", 100, 100);
              
                
            };
            
        }
        private void AddSpelerNamen()
        {
            List<string> listSpelerNamen = new List<string>();
            for(int i = 1; i < listSpelerNamen.Count; i++)
            {
                listSpelerNamen.Add(spelerNaam);
            }
        }
        private void btnStartSpel_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringbuilder = new StringBuilder();
            btnStartSpel.Visibility = Visibility.Hidden; // button start spel verdwijnt
            btnRaadWoord.Visibility = Visibility.Visible; // button raad woord wordt zichtbaar
            WoordSelector(galgjeWoorden); // functie om een random woord in te kiezen  van de lijst galgjeWoorden
            
            txbGeheimWoord.Text = ""; 

            stringbuilder.AppendLine("Juiste letters: ").AppendLine("Foute letters: ");
            lblMain.Content = stringbuilder.ToString();
            lblLevens.Content = levensBegin.ToString();
       
            Timer();
            NieuweTijdStip();

        }
       
        private void btnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            levensBegin = 10; 
            lblMain.Content = "Geef een woord in";
            btnStartSpel.Visibility= Visibility.Visible;
            btnRaadWoord.Visibility= Visibility.Hidden;
            txbGeheimWoord.Text = "";
            dispatchertimer.Stop(); //dispatcher wordt stop gezet want die moet starten bij starten van het spel
            lblTimer.Content = " ";

           
        }
        private void btnRaadWoord_Click(object sender, RoutedEventArgs e)
        {
            LetterCheck();
            txbGeheimWoord.Text = "";
                           
        }
        /// <summary>
        /// In deze funcite wordt gecheckt of het geheimewoord de geraden letter bevat.
        /// Bovendien als de speler de letters juist hebt, wordt het eind (gewonnen) bericht getoond en als niet dan eind (verloren) bericht
        /// wordt getoond.
        /// </summary>
        private void LetterCheck()
        {
            StringBuilder stringbuilder = new StringBuilder();
            string letters = txbGeheimWoord.Text;
            string juist = " ";
            string fout = " ";
            if (geheimWoord.Contains(letters))
            {
                    juist += letters;

                    timerIndex = 0;

                if (geheimWoord == letters)
                {
                    lblMain.Content = "😎 Proficiaat je wint het spel";
                    dispatchertimer.Stop();
                    return;
                }
            }
            else
            {
                levensBegin--; 
                
                fout += letters;

                //ImageBoxGalg.Source = new BitmapImage(GalgImg[imgIndex]);

                imgIndex++;

                ImageBoxGalg.Source = new BitmapImage(new Uri(GalgImg[imgIndex], UriKind.Relative));
          
            } 
            if (levensBegin == 0)
            {
                lblMain.Content = "😔 Probeer opnieuw";

            }
            stringbuilder.AppendLine("Juiste letters: " + juist).AppendLine("Foute letters: " + fout);
        }
        private void Timer()
        {
            dispatchertimer.Interval = TimeSpan.FromSeconds(1);
            dispatchertimer.Tick += Dispatchertimer_Tick;
            dispatchertimer.Start();      
        }

        private void Dispatchertimer_Tick(object sender, EventArgs e)
        {
            timerIndex++;
            lblTimer.Content = timerIndex.ToString();
            if(timerIndex == 10)
            {
                timerIndex = 0;

                levensBegin--;
                
                imgIndex++;

                dispatchertimer.Stop();

                MessageBox.Show("10 seconden zijn verstrekken");

                dispatchertimer.Start();
            }

            lblLevens.Content = levensBegin;
            if(levensBegin < 3)
            {
                grid.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                grid.Background = new SolidColorBrush(Colors.MediumPurple);
            }
        }
        /// <summary>
        /// In deze funtie wordt een random woord geselecteerd
        /// </summary>
        /// <param name="lijst"></param>
        /// <returns> geeft als een woord string terug</returns>
        private string WoordSelector(string[] lijst)
        {
            Random random = new Random();
            for(int i = 0; i < lijst.Length; i++)
            {
                int index = random.Next(0,lijst.Length);
                geheimWoord = lijst[index];

            }
            return geheimWoord;
        }

        //Alle event voor de buttons en menuitems
        private void btnNieuwSpel_MouseEnter(object sender, MouseEventArgs e)
        {
            grid.Background = new SolidColorBrush(Colors.Chocolate);
        }

        private void btnNieuwSpel_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Background = new SolidColorBrush(Colors.MediumPurple);
        }

        private void btnStartSpel_MouseEnter(object sender, MouseEventArgs e)
        {
           
            btnStartSpel.Background = Brushes.Orange;
        }

        private void btnStartSpel_MouseLeave(object sender, MouseEventArgs e)
        {
            btnStartSpel.Background = Brushes.Yellow;
        }

        private void MIAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MIHint_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            char hintLetter = ' ';
            for (int i = 0; i < 2; i++)
            {
                if (geheimWoord != chars)
                {
                    int index = random.Next(0, chars.Length);
                    hintLetter = chars[index];
                }

            }
            MessageBox.Show(hintLetter.ToString(), "Hint");

        }

        private void MenuItemHighScore_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Player Naam:" + spelerNaam).AppendLine("Stip: " + timer2.ToString()).AppendLine("Levens: " + levensBegin);
            MessageBox.Show(sb.ToString(), "Speler score");

        }

        private void MenuItemTimerInstellen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(timer2.ToString());
        }

        //Tijdstip voor om huidige tijd te bekeken  
        DispatcherTimer timer2 = new DispatcherTimer();

        private void NieuweTijdStip()
        {
            timer2.Interval = new TimeSpan(00,00,00);
            timer2.Tick += Timer_Tick;
            timer2.Start();
        }
       
        private void Timer_Tick(object sender, EventArgs e)
        {
         
            lblImgGalg.Content = $"{DateTime.Now.ToLongTimeString()}";

        }

       
    }
}
