using Avalonia.Controls;
using AppCore.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Avalonia.Data;
using System.Collections.ObjectModel;
using Avalonia.Interactivity;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Model.Backup;
using AppCore;
using EasySave_CLI.ViewModel;
using Avalonia.Animation;
using System;
using Avalonia.Input;
using AppCore.ViewModel;

namespace EasySave_WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        LogsGrid();
    }

    WPFTCPAdapter Adapt = new WPFTCPAdapter();
    private void AddBackupJobs(object sender, RoutedEventArgs e)
    {
        Adapt.AddBackupJob(TxtName.Text, FileSourceTextBox.Text, FileTargetTextBox.Text, (BackupEnum)Type.SelectedIndex);
        this.Principal.Items = Adapt.GetBackupInfos();
    }

    private void PrincipalGrid(object sender, RoutedEventArgs e)
    {
        this.Principal.Items = "";
        this.Principal.Items = Adapt.GetBackupInfos();
    }
   
    private void LogsGrid()
    {
        if (File.Exists("C:\\Users\\cleme\\OneDrive\\Documents\\History.json"))
        {
            this.Central.Items = System.Text.Json.JsonSerializer.Deserialize<List<BackupJob>>(File.ReadAllText("C:\\Users\\cleme\\OneDrive\\Documents\\History.json"));
        }
    }
  
    
    private List<BackupInfos> CellClick()
    {
        List<BackupInfos> dataObjects = Principal.Items.OfType<BackupInfos>().ToList();
        return dataObjects;
    }



    private void PlayBackup(object sender, RoutedEventArgs e)
    {
        var backupJobs = CellClick();
        for (int i = 0; i < backupJobs.Count; i++)
        {
            Adapt.RunSpecificBackup(i);
        }
    }

    private void PauseBackup(object sender, RoutedEventArgs e)
    {
        var backupJobs = CellClick();
        if (Principal.SelectedItem != null)
        {
            Adapt.PauseBackup(Principal.SelectedIndex);
        }
        this.Principal.Items = Adapt.GetBackupInfos();
    }

    private void ResumeBackup(object sender, RoutedEventArgs e)
    {
        var backupJobs = CellClick();
        if (Principal.SelectedItem != null)
        {
            Adapt.ResumeBackup(Principal.SelectedIndex);
        }
        this.Principal.Items = Adapt.GetBackupInfos();
    }

    private void DeleteBackup(object sender, RoutedEventArgs e)
    {
        var backupJobs = CellClick();
        if (Principal.SelectedItem != null)
        {
            Adapt.RemoveBackupJob(Principal.SelectedIndex);
        }
        this.Principal.Items = Adapt.GetBackupInfos();
    }

    private void RefreshGrid(object sender, RoutedEventArgs e)
    {
        this.Principal.Items = Adapt.GetBackupInfos();
    }



    private void Crypto(object sender, RoutedEventArgs e)
    {
    }

 




    private async void FolderButton(object sender, RoutedEventArgs e)
    {
        string filePath = await GetFolder();
        var button = (Button)sender;
        var parent = (Panel)button.Parent;
        var textBox = (TextBox)parent.Children[0];
        textBox.Text = filePath;
    }

    private async Task<string> GetFolder()
    {
        string folderPath = "";
        OpenFolderDialog openFolderDialog = new OpenFolderDialog();
        openFolderDialog.DefaultDirectory = "D:\\"; 
        var result = await openFolderDialog.ShowAsync(this);
        if (result != null)
        {
            folderPath = result;
        }
        return folderPath;
    }
}
