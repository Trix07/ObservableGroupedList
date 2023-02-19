// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Firell.Common.Collections.Demo.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Firell.Common.Collections.Demo.Views;

public sealed partial class ObservableGroupedListPage : Page
{
    public ObservableGroupedListPage()
    {
        InitializeComponent();
        ViewModel = new ObservableGroupedListViewModel();
    }

    public ObservableGroupedListViewModel ViewModel { get; set; }
}
