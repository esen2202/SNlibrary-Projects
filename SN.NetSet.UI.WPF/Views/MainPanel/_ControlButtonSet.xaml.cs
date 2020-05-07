﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SN.NetSet.UI.WPF.Views.MainPanel
{
    /// <summary>
    /// Interaction logic for _ControlButtonSet.xaml
    /// </summary>
    public partial class _ControlButtonSet : UserControl
    {
        MainWindow mainWindow;

        public _ControlButtonSet()
        {
            InitializeComponent();
            this.Loaded += _ControlButtonSet_Loaded;
        }

        private void _ControlButtonSet_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            PinnedIconChanger();
            TopMostIconChanger();
        }

        private void BtnAlwaysTop_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.SetPinSideBar(!mainWindow.GetStatusPinSideBar());
            PinnedIconChanger();
        }

        private void BtnShowHideList_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.MainPanel.ListVisibility == Visibility.Collapsed)
            {
                mainWindow.MainPanel.ListVisibility = Visibility.Visible;
                mainWindow.SizeToContent = SizeToContent.Manual;
                mainWindow.Height = SystemParameters.WorkArea.Height;
            }
            else
            {
                mainWindow.SizeToContent = SizeToContent.Height;
                mainWindow.MainPanel.ListVisibility = Visibility.Collapsed;
            }
            ShowHideIconChanger(mainWindow.MainPanel.ListVisibility == Visibility.Visible);
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.SetTopMost(!mainWindow.GetStatusTopMost(),true);
            TopMostIconChanger();
        }

        private void ShowHideIconChanger(bool visible)
        {
            iconShowHide.Kind = visible == false ? MaterialDesignThemes.Wpf.PackIconKind.ArrowExpand : MaterialDesignThemes.Wpf.PackIconKind.ArrowCollapse;
        }

        private void PinnedIconChanger()
        {
            if (mainWindow.GetStatusPinSideBar())
                iconPin.Kind = MaterialDesignThemes.Wpf.PackIconKind.PinOutline;
            else
                iconPin.Kind = MaterialDesignThemes.Wpf.PackIconKind.PinOffOutline;
        }

        private void TopMostIconChanger()
        {
            iconTopMost.Kind = mainWindow.GetStatusTopMost()
                ? MaterialDesignThemes.Wpf.PackIconKind.ArrangeBringToFront
                : MaterialDesignThemes.Wpf.PackIconKind.ArrangeSendToBack;
        }
    }
}