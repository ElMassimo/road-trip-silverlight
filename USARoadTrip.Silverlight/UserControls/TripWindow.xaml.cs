﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using USARoadTrip.Silverlight.WCFServices;
using USARoadTrip.Silverlight.Services;
using USARoadTrip.Silverlight.Utility;

namespace USARoadTrip.Silverlight.UserControls
{
    public partial class TripWindow : ChildWindow
    {
        private Trip _trip = new Trip();

        public Trip MyTrip
        {
            get { return _trip; }
            set 
            {
                _trip = value;
                if(_trip != null)
                    LayoutRoot.DataContext = MyTrip;
            }
        }
        
        public TripWindow()
        {
            InitializeComponent();
            LayoutRoot.DataContext = MyTrip;
            SaveTripButton.Content = "Create trip";
            SaveTripButton.Click += CreateTripButton_Click;
        }

        public TripWindow(Trip trip)
        {
            InitializeComponent();
            MyTrip = trip;
            SaveTripButton.Content = "Edit trip";
            SaveTripButton.Click += EditTripButton_Click;
            NameTextBox.IsEnabled = false;
        }

        private void CreateTripButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(_trip.Name) || String.IsNullOrWhiteSpace(_trip.Description))
                MessageBox.Show("Please enter the trip name and description", "Create trip", MessageBoxButton.OK);
            else
            {
                BusyIndicator.IsBusy = true;
                _trip.UserNick = RoadTripGlobals.UserId;
                RoadTripServices.CreateTripService(CreateTrip_Completed).CreateTripAsync(_trip);
            }
        }

        private void CreateTrip_Completed(object sender, CreateTripCompletedEventArgs e)
        {
            BusyIndicator.IsBusy = false;

            if (e.Error != null)
                GuiUtils.ShowConnectionErrorMessage();
            else if (!e.Result)
                MessageBox.Show("You have already created a trip with the same name. Please choose a different one.", "Create trip", MessageBoxButton.OK);
            else
            {
                MessageBox.Show("Trip created succesfully!", "Create trip", MessageBoxButton.OK);            
                this.DialogResult = true;
            }
        }

        private void EditTripButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(_trip.Name) || String.IsNullOrWhiteSpace(_trip.Description))
                MessageBox.Show("Please enter the trip description", "Edit trip", MessageBoxButton.OK);
            else
            {
                BusyIndicator.IsBusy = true;
                _trip.UserNick = RoadTripGlobals.UserId;
                RoadTripServices.UpdateTripService(EditTrip_Completed).UpdateTripAsync(_trip);
            }
        }

        private void EditTrip_Completed(object sender, UpdateTripCompletedEventArgs e)
        {
            BusyIndicator.IsBusy = false;

            if (e.Error != null)
                GuiUtils.ShowConnectionErrorMessage();
            else if (!e.Result)
                MessageBox.Show("There was an error while updating the trips's description.", "Edit trip", MessageBoxButton.OK);
            else
            {
                MessageBox.Show("Trip updated succesfully!", "Edit trip", MessageBoxButton.OK);
                this.DialogResult = true;
            }
        }
    }
}
