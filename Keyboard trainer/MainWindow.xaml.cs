﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Resources;
using System.Windows.Threading;
using System.Diagnostics;
using System.Collections.ObjectModel;

using Keyboard_trainer.Models;
using Keyboard_trainer.ViewModels;

namespace Keyboard_trainer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _capsLock = false;

        private Stopwatch stopwatch;
        public ButtonViewModel ButtonViewModel { get; set; }
        public TextViewModel TextViewModel { get; set; }
        public StatisticViewModel StatisticViewModel { get; set; }
        

        public MainWindow()
        {
            TextViewModel = new TextViewModel();

            InitializeComponent();

            ButtonViewModel = new ButtonViewModel(GridKeyboard);
            stopwatch = new Stopwatch();
            StatisticViewModel = new StatisticViewModel();

            DataContext = this;
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(TextViewModel.DifficultModel != null && StartButton.IsEnabled == true)
                TextViewModel.DifficultModel.Difficulty = (int)Slider.Value;
        }

        private void SwitchEnabled(object toFalse, object toTrue)
        {
            (toTrue as Button).IsEnabled = true;
            (toFalse as Button).IsEnabled = false;
        }

        private void End()
        {
            if (TextViewModel.IsEnd())
            {
                MessageBox.Show("End");
                stopwatch.Stop();
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (ButtonViewModel.IsCapitalOrShift(e.Key)) // CapsLock and Shift
            {
                _capsLock = !_capsLock;
                ButtonViewModel.ButtonContentInit(_capsLock);
            }

            try
            {
                if (!TextViewModel.IsCorrect(ButtonViewModel.GiveKeyCharSpaceSymbol(e.Key)))
                    StatisticViewModel.StatisticModel.Fails++;
            }
            catch { }

            TextViewModel.ChangeInputText(ButtonViewModel.GiveKeyCharSpaceSymbol(e.Key));

            End();
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                _capsLock = !_capsLock;
                ButtonViewModel.ButtonContentInit(_capsLock);
            }
        }

        private void CalculateSpeed()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();

            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += (s, args) => StatisticViewModel.StatisticModel.Speed = InputText.Text.Length / ((int)stopwatch.Elapsed.TotalMinutes + 1);
            dispatcherTimer.Start();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            TextViewModel.GenerateText();
            StatisticViewModel.StatisticModel.Fails = 0;

            stopwatch.Restart();
            CalculateSpeed();

            SwitchEnabled(StartButton, StopButton);
            Slider.IsEnabled = false;

            PreviewKeyDown += Window_PreviewKeyDown;
            PreviewKeyUp += Window_PreviewKeyUp;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchEnabled(StopButton, StartButton);
            stopwatch.Stop();
            Slider.IsEnabled = true;

            PreviewKeyDown -= Window_PreviewKeyDown;
            PreviewKeyUp -= Window_PreviewKeyUp;
        }    

    }
}
