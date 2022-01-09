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
       
       private string geheimWoord;
       private int levensBegin = 10;
       private string juist;
       private string fout;
       private int imgIndex = 0;
        private string[] GalgImg = new string[]
       
        {
            "/Images/galg1.png", "/Images/galg2.png", "/Images/galg3.png", "/Images/galg4.png",
            "/Images/galg5.png", "/Images/galg6.png", "/Images/galg7.png",
        "/Images/galg8.png", "/Images/galg9.png", "/Images/galg10.png",
        };
        private DispatcherTimer dispatchertimer = new DispatcherTimer();
        private int timerIndex = 0;
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
            Timer();

        }
       
        private void btnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            levensBegin = 10;
            lblMain.Content = "Geef een woord in";
            btnStartSpel.Visibility= Visibility.Visible;
            btnRaadWoord.Visibility= Visibility.Hidden;
            txbGeheimWoord.Text = "";
            dispatchertimer.Stop();
            lblTimer.Content = " ";

           
        }

        private void btnRaadWoord_Click(object sender, RoutedEventArgs e)
        {
            LetterCheck();
            txbGeheimWoord.Text = "";
                           
        }

        private void LetterCheck()
        {
            StringBuilder stringbuilder = new StringBuilder();

        

            string letters = txbGeheimWoord.Text;
           
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

        }

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
    }
}
