// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Reflection;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace Firell.Common.Collections.Demo.Views;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
    }

    private void MainNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        NavigationViewItem? selectedItem = args.SelectedItem as NavigationViewItem;
        if (selectedItem == null)
        {
            return;
        }

        Type? pageType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name.Equals($"{selectedItem.Tag}", StringComparison.OrdinalIgnoreCase));
        if (pageType != null)
        {
            MainFrame.Navigate(pageType, null, new DrillInNavigationTransitionInfo());
        }
    }
}
