﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using USARoadTrip.Silverlight.WCFServices;

namespace USARoadTrip.Silverlight.Services
{
    public class RoadTripServices
    {
        public static RoadTripServicesClient GetLoginService(EventHandler<LoginCompletedEventArgs> completedHandler)
        {
            RoadTripServicesClient client = new RoadTripServicesClient();
            client.LoginCompleted += completedHandler;
            return client;
        }
    }
}