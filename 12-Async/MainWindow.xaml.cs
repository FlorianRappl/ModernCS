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
        bool cancel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonClick(object sender, RoutedEventArgs e)
        {
            Liste.Items.Clear();
            var directories = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            //Ab hier wird ein neuer Thread gestartet und der Rest als Continuation ausgeführt
            //Die Continuation wird jedoch IM AKTUELLEN CONTEXT, d.h. ohne Cross-Threading Probleme, ausgeführt werden
            await SearchDirectories(directories);

            Liste.Items.Add("Suche abgeschlossen!");
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            cancel = true;
        }

        private async Task<int> ReadFileLengthAsync(string file)
        {
            //Better be safe than sorry - könnten hier auch einige Abfragen machen,
            //aber dies ist quick and dirty
            try
            {
                var length = await Task.Run(() => File.ReadAllBytes(file).Length);
                return length;
            }
            catch
            {
                return 0;
            }
        }

        private async Task<bool> SearchDirectories(string[] directories)
        {
            //Gehen über all Verzeichnisse
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
                    size += await ReadFileLengthAsync(file);

                    //--- Ab hier ist wieder Continuation im UI Thread Kontext---

                    //Soll abgebrochen werden?!
                    if (cancel)
                    {
                        cancel = false;
                        Liste.Items.Add("Suche abgebrochen!");
                        return true;
                    }
                }

                //Hier wird die Ausgabe geschrieben
                Liste.Items.Add("Verzeichnis {" + directory + "} mit " + size + " Bytes");

                //Anschließend in die Unterverzeichnisse rein
                var subdirectories = Directory.GetDirectories(directory);
                //Wurde das Cancel ausgelesen?! Wir setzen es anschließend zurück, daher brauchen
                //wir einen zweiten Indikator (Rückgabewert)
                var cancelled = await SearchDirectories(subdirectories);

                if (cancelled)
                    return true;
            }

            //Sind wir soweit gekommen, wurde anscheinend nicht gecancelled
            return false;
        }
    }
}
