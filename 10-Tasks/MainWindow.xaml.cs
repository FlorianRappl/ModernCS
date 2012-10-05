using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;

/*
 * 
 * (c) Florian Rappl, 2012.
 * 
 * This work is a demonstration for training purposes and may be used freely for private purposes.
 * Usage for business training / workshops is prohibited without explicit permission from the author.
 * 
 */

namespace ModernCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Synchron.Click += new RoutedEventHandler(Synchron_Click);
            Asynchron.Click += new RoutedEventHandler(Asynchron_Click);
        }

        void Asynchron_Click(object sender, RoutedEventArgs e)
        {
            MyLabel.Content = string.Empty;
            //In einen Task verpackt
            Task task = new Task(HeavyWorkDoing);
            task.Start();
            //Werden wir nie sehen
            MyLabel.Content = "Asynchron Finished";
        }

        void Synchron_Click(object sender, RoutedEventArgs e)
        {
            MyLabel.Content = string.Empty;
            //Direkt gestartet
            HeavyWorkDoing();
            //Werden wir nach Verzögerung sehen
            MyLabel.Content = "Synchron Finished";
        }

        delegate void ChangeDelegate(int x);

        void ChangeCountdown(int remaining)
        {
            MyLabel.Content = "Noch " + remaining + " Sekunden";
        }

        void HeavyWorkDoing()
        {
            var countdown = 5;

            while(countdown >= 0)
            {
                //Oops ! Potentielles Problem mit Multithreading...
                //MyLabel.Content = "Noch " + countdown + " Sekunden";

                //Daher brauchen wir einen Delegate
                ChangeDelegate del = new ChangeDelegate(ChangeCountdown);
                MyLabel.Dispatcher.Invoke(del, countdown);

                Thread.Sleep(1000);
                countdown--;
            }
        }
    }
}
