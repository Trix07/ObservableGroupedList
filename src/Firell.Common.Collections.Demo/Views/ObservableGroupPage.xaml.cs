// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Firell.Common.Collections.Demo.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Firell.Common.Collections.Demo.Views;

public sealed partial class ObservableGroupPage : Page
{
    public ObservableGroupPage()
    {
        InitializeComponent();
        ViewModel = new ObservableGroupViewModel();
    }

    public ObservableGroupViewModel ViewModel { get; set; }
}
