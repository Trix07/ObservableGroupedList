// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Firell.Common.Collections.Demo.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Firell.Common.Collections.Demo.Views;

public sealed partial class ObservableSortedListPage : Page
{
    public ObservableSortedListPage()
    {
        InitializeComponent();
        ViewModel = new ObservableSortedListViewModel();
    }

    public ObservableSortedListViewModel ViewModel { get; set; }
}
