﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App11Athletics.Views.Controls
{
    public partial class OneRepMax : ContentView
    {
        public bool wdisabled;

        public OneRepMax()
        {
            InitializeComponent();
            if (myEntry.Text == string.Empty)
                labelentry.Text = myEntry.Placeholder;

        }

        public double TitleFontSize { get; set; }
        public double LiftFontSize { get; set; }
        public double RepFontSize { get; set; }
        public double StepperRepValue { get; set; }
        public string Lift { get; set; }
        public string WeightLifted { get; set; }



        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            myEntry.Focus();
        }

        public event EventHandler Clicked
        {
            add { buttonSaveOneRepMax.Clicked += value; }
            remove { buttonSaveOneRepMax.Clicked -= value; }
        }

        private void OneRepMax_OnSizeChanged(object sender, EventArgs e)
        {
            TitleFontSize = Width / 10;
            LiftFontSize = Width / 6;
            RepFontSize = Width / 10;
        }

        private void MyEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (myEntry.Text.Length > 6)
                LiftFontSize = Width / Convert.ToDouble(myEntry.Text.Length);
            else
            {
                LiftFontSize = Width / 6;
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (wdisabled)
                return;


            wdisabled = true;

            myEntryWeight.Focus();
        }

        private void MyEntryWeight_OnUnfocused(object sender, FocusEventArgs e)
        {
            wdisabled = false;

        }

        private void ButtonSaveOneRepMax_OnClicked(object sender, EventArgs e)
        {
            StepperRepValue = stepperReps.Value;
            Lift = labelentry.Text;
            WeightLifted = myEntryWeight.Text;

        }

        public event EventHandler WClicked
        {
            add { buttonWeight.Clicked += value; }
            remove { buttonWeight.Clicked -= value; }
        }

        public event EventHandler<FocusEventArgs> WUnfocused
        {
            add { myEntryWeight.Unfocused += value; }
            remove { myEntryWeight.Unfocused -= value; }
        }
        public event EventHandler<FocusEventArgs> WFocused
        {
            add { myEntryWeight.Focused += value; }
            remove { myEntryWeight.Focused -= value; }
        }

        private void MyEntryWeight_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (myEntryWeight.Text.Length > 0)
                WeightLifted = myEntryWeight.Text + " lbs";

            else
            {
                WeightLifted = "...";
            }
        }

        public void FocusEntry()
        {
            myEntry.Focus();
        }
    }
}