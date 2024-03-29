﻿using System;
using App11Athletics.Models;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class WorkoutLogOptionsView : ContentPage
    {
        public WorkoutLogOptionsView(string date = null, bool isMax = false)
        {
            WorkoutDateCurrent = date;
            IsMaxBool = isMax;
            IsLogBool = !IsMaxBool;
            InitializeComponent();


        }

        #region Overrides of Page

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CanSaveBool = CanSave();
        }

        #endregion
        public bool IsMaxBool { get; set; }
        public bool IsLogBool { get; set; }
        private string WorkoutDateCurrent { get; }
        public bool CanSaveBool { get; set; }

        private bool CanSave()
        {
            if (IsMaxBool)
                return !(string.IsNullOrEmpty(nameEntry.Text) || nameEntry == null || string.IsNullOrEmpty(RepEntry.Text));

            return !(string.IsNullOrEmpty(nameEntry.Text) || nameEntry == null);
        }
        private string OneRepMaxCalc(string weightlifted = null, string reps = null)
        {
            if (string.IsNullOrEmpty(weightlifted) || string.IsNullOrEmpty(reps))
                return string.Empty;
            var dweightLifted = Convert.ToDouble(weightlifted);
            var dreps = Convert.ToDouble(reps);
            var Max = dweightLifted / (1.0278 - (0.0278 * dreps));
            if (Max < 0)
                Max = 0;
            return Convert.ToInt32(Max).ToString();
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            todoItem.LoggedDate = WorkoutDateCurrent;
            todoItem.IsMaxLift = IsMaxBool;


            if (IsMaxBool && !string.IsNullOrEmpty(WeightEntry.Text) && !string.IsNullOrEmpty(RepEntry.Text))
            {
                todoItem.OneRepMax = OneRepMaxCalc(WeightEntry.Text, RepEntry.Text);
                todoItem.WSets = string.Empty;
            }
            await App.Database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.Database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //        void OnSpeakClicked(object sender, EventArgs e)
        //        {
        //            var todoItem = (TodoItem)BindingContext;
        //            DependencyService.Get<ITextToSpeech>().Speak(todoItem.Name + " " + todoItem.Notes);
        //        }



        private void SetEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //            var numEntry = (Entry)sender;
            //            var numEntryText = numEntry.Text;
            //            var restrictCount = 2;
            //            if (numEntryText != null && numEntryText.Length > restrictCount)
            //            {
            //                // If it is more than your character restriction
            //                numEntryText = numEntryText.Remove(numEntryText.Length - 1); // Remove Last character
            //                numEntry.Text = numEntryText; // Set the Old value
            //            }
            //
            //            if (string.IsNullOrEmpty(numEntry.Text) || numEntry.Text == "0")
            //            {
            //                numEntry.Text = null;
            //            }
        }

        private void RepEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //            var numEntry = (Entry)sender;
            //            var numEntryText = numEntry.Text;
            //            var restrictCount = 2;
            //            if (numEntryText != null && numEntryText.Length > restrictCount)
            //            {
            //                // If it is more than your character restriction
            //                numEntryText = numEntryText.Remove(numEntryText.Length - 1); // Remove Last character
            //                numEntry.Text = numEntryText; // Set the Old value
            //            }
            //
            //            if (string.IsNullOrEmpty(numEntry.Text) || numEntry.Text == "0")
            //            {
            //                numEntry.Text = null;
            //            }
        }

        private void Entry_OnFocused(object sender, FocusEventArgs e)
        {
            //            var t = (Entry)sender;
            //            Device.BeginInvokeOnMainThread(() => { t.Text = null; });
        }

        private void RepEntry_OnFocused(object sender, FocusEventArgs e)
        {
            var t = (Entry)sender;
            Device.BeginInvokeOnMainThread(() => { t.Text = null; });
        }

        private void NameEntry_OnPropertyChanging(object sender, PropertyChangingEventArgs e) { }

        private void OptionUnfocused(object sender, FocusEventArgs e)
        {
            CanSaveBool = CanSave();
        }

        private void WeightEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //            var numEntry = (Entry)sender;
            //            var numEntryText = numEntry.Text;
            //            var restrictCount = 3;
            //            if (numEntryText != null && (numEntryText.Length > restrictCount) || numEntry.Text.Contains("."))
            //            {
            //                // If it is more than your character restriction
            //                if (numEntry.Text.Contains("."))
            //
            //                    numEntryText = numEntryText.Remove(numEntryText.Length - 1); // Remove Last character
            //                numEntry.Text = numEntryText; // Set the Old value
            //            }
            //
            //            if (string.IsNullOrEmpty(numEntry.Text) || numEntry.Text == "0")
            //            {
            //                numEntry.Text = null;
            //            }
        }
    }
}