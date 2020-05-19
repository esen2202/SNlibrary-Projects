using Microsoft.Win32;
using SN.NetSet.UI.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SN.NetSet.UI.WPF.Views.ConfigPanel
{
    /// <summary>
    /// Interaction logic for _ImportExportControl.xaml
    /// </summary>
    public partial class _ImportExportControl : UserControl
    {


        public _ImportExportControl()
        {
            InitializeComponent();
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "NetSet Config File (*.nsc)|*.nsc";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                (_ConfigPanel.ConfigPanelInstance.DataContext as _ConfigPanelViewModel).ImportConfigDb(File.ReadAllText(openFileDialog.FileName));
            }
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "NetSet Config File (*.nsc)|*.nsc";
            if (saveFileDialog.ShowDialog() == true)
            {
                (_ConfigPanel.ConfigPanelInstance.DataContext as _ConfigPanelViewModel).ExportConfigDb(saveFileDialog.FileName);
            }

        }
    }
}
