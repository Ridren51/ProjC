using Avalonia.Controls;
using AppCore.Model;
using EasySave_WPF.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Avalonia.Data;
using System.Collections.ObjectModel;

namespace EasySave_WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}