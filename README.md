# ObservableGroupedList
An observable grouped list that associates a key to a group that allows for sorted grouped data to be presented.
This is achieved using an ObservableSortedList with a collection of ObservableGroups.

Using an IComparer, it is possible to sort both groups and internal items.

## Usage
*A demo app is included in the solution showing most of these principals.*

Creating a ObservableGroupedList of people where indiviual groups are grouped based on the inital letter of their full name, with the items being sorted by their full name.
```C#
Dictionary<string, PersonViewModel> persons = PersonService.FetchPersons().ToDictionary(x => x.FullName, y => y);

ObservableGroupedList<char, string, PersonViewModel> GroupedPersons = new ObservableGroupedList<char, string, PersonViewModel>(x => x.FullName[0], persons);
```

Display it using a CollectionViewSource that binds to the values of the ObservableGroupedList (ItemsPath must also be set to Values).
```XAML
<CollectionViewSource x:Name="PersonsCollectionViewSource" Source="{x:Bind ViewModel.GroupedPersons.Values, Mode=OneWay}" ItemsPath="Values" IsSourceGrouped="True">
```
Displaying groups in a ListView using custom styling.
```XAML
<ListView ItemsSource="{x:Bind PersonsCollectionViewSource.View, Mode=OneWay}">
    <ListView.ItemsPanel>
        <ItemsPanelTemplate>
            <ItemsStackPanel AreStickyGroupHeadersEnabled="True" GroupPadding="0, 15"/>
        </ItemsPanelTemplate>
    </ListView.ItemsPanel>

    <ListView.GroupStyle>
        <GroupStyle>
            <GroupStyle.HeaderContainerStyle>
                <Style TargetType="ListViewHeaderItem">
                    <Setter Property="HorizontalContentAlignment" Value="Stretch"/>
                </Style>
            </GroupStyle.HeaderContainerStyle>

            <GroupStyle.HeaderTemplate>
                <DataTemplate>
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>

                        <TextBlock Grid.Column="0" Text="{Binding Key}" FontWeight="SemiBold"/>
                        <TextBlock Grid.Column="1" Text="{Binding Count, Converter={StaticResource StringFormatConverter}, ConverterParameter='{}{0} items'}" Foreground="Gray" FontSize="14" VerticalAlignment="Bottom"/>
                    </Grid>
                </DataTemplate>
            </GroupStyle.HeaderTemplate>
        </GroupStyle>
    </ListView.GroupStyle>

    <ListView.ItemTemplate>
        <DataTemplate>
            <ListViewItem Margin="0, 5" Padding="15, 10">
                <StackPanel Orientation="Horizontal" Spacing="10">
                    <PersonPicture DisplayName="{Binding FullName, Mode=OneWay}" Height="48"/>

                    <StackPanel VerticalAlignment="Center">
                        <TextBlock Text="{Binding FullName, Mode=OneWay}" FontSize="16" FontWeight="SemiBold"/>
                        <TextBlock Text="{Binding Gender, Mode=OneWay}" Foreground="Gray"/>
                    </StackPanel>
                </StackPanel>
            </ListViewItem>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```
