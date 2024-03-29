﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using App11Athletics.Helpers;
using App11Athletics.Models;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class WorkoutLogListView : ContentPage
    {
        public WorkoutLogListView(DateTime WorkoutDate)
        {
            WorkoutDateCurrent = App.LogDate(WorkoutDate);
            InitializeComponent();
            Title = WorkoutDateCurrent;
            //            oneRepMaxView.TranslationY = 1500;
            //            viewWeightOptions.TranslationY = -1000;
            //            //            OneRepMaxView_Clicked(null, null);
            //            labelMaxLift.Text = Settings.UserOneRMLift;
            //            labelMaxWeight.Text = Settings.UserOneRMAx + "lbs";
        }



        public string MaxLift { get; set; }
        public string MaxWeight { get; set; }
        public string WorkoutDateCurrent { get; set; }
        public double OneRepMaxFontSize { get; set; }
        public double WOneRepMaxFontSize { get; set; }

        public double labelWeightOptionsFontSize { get; set; }
        public bool MaxUp { get; set; }

        protected override async void OnAppearing()
        {
            var itemSource = await App.Database.GetFilteredItemsAsync(WorkoutDateCurrent);
            listView.ItemsSource = itemSource;
            base.OnAppearing();
            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTodoId = -1;


        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutLogOptionsView(WorkoutDateCurrent) { BindingContext = new TodoItem() });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);
            await Navigation.PushAsync(new WorkoutLogOptionsView(WorkoutDateCurrent) { BindingContext = e.SelectedItem as TodoItem });
        }



        //        private async void TapGestureRecognizerOneRepMax_OnTapped(object sender, EventArgs e)
        //        {
        //            await oneRepMaxView.TranslateTo(0, 0, 350U, Easing.CubicOut);
        //            oneRepMaxView.Clicked += OneRepMaxView_Clicked;
        //            oneRepMaxView.FocusEntry();
        //            MaxUp = true;
        //
        //
        //        }
        //
        //        private async void OneRepMaxView_Clicked(object sender, EventArgs e)
        //        {
        //            labelMaxLift.Text = oneRepMaxView.Lift;
        //            if (!string.IsNullOrEmpty(oneRepMaxView.Lift))
        //                Settings.UserOneRMAx = OneRepMaxCalc(oneRepMaxView.WeightLifted, oneRepMaxView.StepperRepValue);
        //
        //            labelMaxWeight.Text = Settings.UserOneRMAx + " lbs";
        //
        //            await oneRepMaxView.TranslateTo(0, 1500, 350U, Easing.CubicIn);
        //            WorkoutLogListView_OnSizeChanged(null, null);
        //            MaxUp = false;
        //        }
        private string OneRepMaxCalc(string weightlifted, double reps)
        {
            if (!string.IsNullOrEmpty(weightlifted))
            {
                var dweightLifted = Convert.ToDouble(weightlifted);

                var Max = (dweightLifted / (1.0278 - (0.0278 * reps)));
                if (Max < 0)
                    Max = 0;
                return Convert.ToInt32(Max).ToString();
            }
            return string.Empty;
        }

        private void WorkoutLogListView_OnSizeChanged(object sender, EventArgs e)
        {
            //            if (labelMaxLift.Text != null && labelMaxLift.Text.Length > 12)
            //                OneRepMaxFontSize = gridOneRepMax.Width / (labelMaxLift.Text.Length);
            //
            //            else
            //            {
            //                OneRepMaxFontSize = gridOneRepMax.Width / 12;
            //            }
            //            WOneRepMaxFontSize = Width / 12;
            //            labelWeightOptionsFontSize = Width / 12;
            //            labelMaxLift.FontSize = OneRepMaxFontSize;
            //            labelMaxWeight.FontSize = WOneRepMaxFontSize;

        }

        //        private void OneRepMaxView_OnWClicked(object sender, EventArgs e)
        //        {
        //            viewWeightOptions.TranslateTo(0, 0, 350U, Easing.CubicOut);
        //        }

        //        private async void OneRepMaxView_OnWUnfocused(object sender, FocusEventArgs e)
        //        {
        //            viewWeightOptions.TranslateTo(0, -1000, 350U, Easing.CubicIn);
        //
        //            await oneRepMaxView.FadeTo(1, 350U, Easing.CubicIn);
        //            listView.FadeTo(1, 350U, Easing.CubicIn);
        //        }
        //
        //        private void OneRepMaxView_OnWFocused(object sender, FocusEventArgs e)
        //        {
        //            oneRepMaxView.FadeTo(0.2, 350U, Easing.CubicIn);
        //            listView.FadeTo(0, 350U, Easing.CubicIn);
        //        }

        public void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var image = (Image)sender;
            if (image.StyleId == "d")
            {
                image.IsVisible = false;
            }
            else
            {

            }

        }

        #region Overrides of Page

        //        protected override bool OnBackButtonPressed()
        //        {
        //            if (!MaxUp)
        //                return base.OnBackButtonPressed();
        //            oneRepMaxView.TranslateTo(0, 1500, 350U, Easing.CubicIn);
        //            return true;
        //        }

        #endregion

        private async void OneRepMaxView_OnLiftClicked(object sender, EventArgs e)
        {
            //            Device.BeginInvokeOnMainThread(() => oneRepMaxView.FocusEntry());

        }
    }
}