﻿using System.Windows;
using Filmator.ViewModel;
//using Filmator.Model.Entities;
using Filmator.Model.Provider;
using System;

namespace Filmator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}