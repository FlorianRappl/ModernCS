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
using System.IO;

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
        bool cancel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var directories = Directory.GetDirectories(@"C:\");
            //Ab hier wird ein neuer Thread gestartet und der Rest als Continuation ausgeführt
            //Die Continuation wird jedoch IM AKTUELLEN CONTEXT, d.h. ohne Cross-Threading Probleme, ausgeführt werden
            await SearchDirectories(directories);
            Liste.Items.Add("Suche abgeschlossen!");
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            cancel = true;
        }

        private async void SearchDirectories(string[] directories)
        {
            //Gehen über all Verzeichne - könnten wir auch Rekursiv machen
            foreach (var directory in directories)
            {
                //Größe merken
                var size = 0;
                //Dateien auslesen
                var files = Directory.GetFiles(directory);

                //Über alle Verzeichnisse gehen
                foreach (var file in files)
                {
                    //Größe auslesen (asynchron!) und hinzufügen
                    size += await File.ReadAllBytes(file).Length;

                    //--- Ab hier ist wieder Continuation im UI Thread Kontext---

                    //Soll abgebrochen werden?!
                    if (cancel)
                    {
                        cancel = false;
                        Liste.Items.Add("Suche abgebrochen!");
                        return;
                    }
                }

                //Hier wird die Ausgabe geschrieben
                Liste.Items.Add("Verzeichnis {" + directory + "} mit " + size + " Bytes");
            }
        }
    }
}
