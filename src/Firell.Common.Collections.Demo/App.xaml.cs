// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Firell.Common.Collections.Demo.Views;

using Microsoft.UI.Xaml;

namespace Firell.Common.Collections.Demo;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    internal MainWindow? MainWindow { get; private set; }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new MainWindow();
        MainWindow.Activate();
    }
}
