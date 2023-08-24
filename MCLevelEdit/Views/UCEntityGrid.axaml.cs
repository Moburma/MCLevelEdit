using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using System;
using MCLevelEdit.Domain;

namespace MCLevelEdit.Views;

public partial class UCEntityGrid : UserControl
{
    public ObservableCollection<Entity> Entities { get; }

    public UCEntityGrid()
    {
        InitializeComponent();
    }
}