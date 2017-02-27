﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App11Athletics.DHCToolkit;
using App11Athletics.ViewModels;
using App11Athletics.Views.Timers;
using ImageCircle.Forms.Plugin.Abstractions;
using Syncfusion.SfCarousel;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class HomeMenuView : ContentPage
    {
        public bool disable = false;

        public HomeMenuView()
        {
            InitializeComponent();
            carouselMain.BindingContext = new CarouselViewModel();
            CarouselMain_OnSelectionChanged(null, null);
            MenuTitle = gridLabel.Width / 8;
            Opacity = 0;

        }




        public double MenuTitle { get; set; }

        public static bool running { get; set; }


        #region Overrides of Page

        protected override async void OnAppearing()
        {
            //            Opacity = 0;
            base.OnAppearing();
            await Task.Delay(100);
            Opacity = 0;
            MenuTitle = gridLabel.Width / 8;
            //            foreach (var view in griddots.Children)
            //            {
            //                // if (!string.Equals(view.StyleId, cI, StringComparison.Ordinal))
            //                if (view.StyleId != carouselMain.SelectedIndex.ToString())
            //                {
            //                    view.FadeTo(0.5);
            //                    view.ScaleTo(1);
            //                }
            //                else
            //                {
            //                    view.FadeTo(1);
            //                    view.ScaleTo(2);
            //                    AnimateDot((Image)view);
            //                }
            //            }

            //            null.ScaleTo(1, 350U, Easing.CubicOut);
            //            this.FadeTo(1, 400U, Easing.CubicOut);
            imageBG.TranslationX = Width / 2;
            imageBG.TranslationY = -Height;
            this.FadeTo(1, 400U, Easing.CubicOut);
            AnimatePages.BgLogoTask(imageBG, Width / 2, Height / 2);

            AnimatePages.AnimatePageIn(gridHomeMenu, null);

            await Task.Delay(400);
        }



        #endregion

        public void HomeMenuView_OnSizeChanged(object sender, EventArgs e)
        {
            MenuTitle = gridLabel.Width / 8;
        }


        //        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        //        {
        //            if (this.disable)
        //                return;
        //            var p = (Image)sender;
        //            var sI = Convert.ToInt32(p.StyleId);
        //            var cI = carouselMain.SelectedIndex.ToString();
        //
        //            if (p.StyleId != cI)
        //            {
        //                carouselMain.SelectedIndex = sI;
        //                CarouselMain_OnSelectionChanged(null, null);
        //                foreach (var view in griddots.Children)
        //                {
        //                    // if (!string.Equals(view.StyleId, cI, StringComparison.Ordinal))
        //                    if (view != p)
        //                    {
        //                        view.FadeTo(0.5);
        //                        view.ScaleTo(1);
        //                    }
        //                }
        //
        //                p.FadeTo(1);
        //                p.ScaleTo(2);
        //                if (running)
        //                {
        //                    running = false;
        //                    var ty = p.TranslationY;
        //                    this.AbortAnimation("AnimationDot");
        //                }
        //
        //                AnimateDot(p);
        //            }
        //            else
        //            {
        //                var index = carouselMain.SelectedIndex;
        //                disable = true;
        //                if (running)
        //                {
        //                    running = false;
        //                    this.AbortAnimation("AnimationDot");
        //                    this.AbortAnimation("AnimationText");
        //                }
        //                if (index == 0)
        //                {
        //                    var nav = new UserProfileView();
        //                    AnimateDotToNav(p, nav);
        //                }
        //                else if (index == 1)
        //                {
        //                    {
        //                        var nav = new StopwatchFeatureView();
        //                        AnimateDotToNav(p, nav);
        //                    }
        //                }
        //                else if (index == 2)
        //                {
        //                    {
        //                        var nav = new WorkoutLogListView();
        //                        AnimateDotToNav(p, nav);
        //                    }
        //                }
        //                else if (index == 3)
        //                {
        //                    {
        //                        var nav = new Discover11AthleticsView();
        //                        AnimateDotToNav(p, nav);
        //                    }
        //                }
        //
        //            }
        //        }



        private void AnimateDotToNav(Image p, Page page)
        {
            Animation parentAnimation = new Animation();
            Animation upAnimation = new Animation(v => p.TranslationY = v, 0, -100, Easing.CubicInOut,
                () => Debug.WriteLine("up finished"));
            parentAnimation.Add(0, 0.6, upAnimation);
            parentAnimation.Commit(this, "AnimationDotToNav", 16, 1200, null, async (v, c) =>
            {
                //                await AnimatePages.AnimatePageOut(gridHomeMenu, null);
                await Task.Delay(250);
                await Navigation.PushAsync(page);
                p.Scale = 1;
                p.TranslationY = 0;
                disable = false;
            });
        }



        private void AnimateDot(Image p)
        {
            running = true;
            Animation parentAnimation = new Animation();

            // Create "up" animation and add to parent. 
            Animation upAnimation = new Animation(v => p.TranslationY = v, 0, -10, Easing.CubicInOut,
                () => Debug.WriteLine("up finished"));
            parentAnimation.Add(0, 0.6, upAnimation);

            // Create "down" animation and add to parent.
            Animation downAnimation = new Animation(v => p.TranslationY = v, -10, 0, Easing.CubicOut,
                () => Debug.WriteLine("down finished"));
            parentAnimation.Insert(0.4, 1, downAnimation);
            parentAnimation.Commit(this, "AnimationDot", 16, 1200, null, (v, c) =>
            {
                p.TranslateTo(0, 0, 600U, Easing.SinOut);
                if (running)
                    AnimateDot(p);
            });
        }

        private async void CarouselMain_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            disable = true;
            imageArrowsLeft.IsVisible = false;
            imageArrowsRight.IsVisible = false;
            var s = carouselMain.SelectedIndex;
            if (!running)
            {
                AnimateText(labelMenuTitle);

                //                this.AbortAnimation("AnimationText");
            }


            await Task.Delay(400);

            if (s == carouselMain.SelectedIndex)
            {
                ChangeTitleText(carouselMain.SelectedIndex, labelMenuTitle, "Profile", "Timers",
                     "Workout Log", "Discover 11");
                await AnimateTextIn(labelMenuTitle);
            }
            if (carouselMain.SelectedIndex <= 0)
            {
                imageArrowsLeft.IsVisible = false;
                imageArrowsRight.IsVisible = true;
            }
            else if (carouselMain.SelectedIndex >= 3)
            {
                imageArrowsLeft.IsVisible = true;
                imageArrowsRight.IsVisible = false;
            }
            else
            {
                imageArrowsLeft.IsVisible = true;
                imageArrowsRight.IsVisible = true;
            }


        }



        private async void AnimateText(Label label)
        {

            running = true;
            Animation parentAnimation = new Animation();


            // Create "up" animation and add to parent. 
            Animation upAnimation = new Animation(v => label.TranslationY = v, 0, -10, Easing.CubicInOut);

            parentAnimation.Add(0, 1, upAnimation);
            Animation fadeOutAnimation = new Animation(v => label.Opacity = v, 1, 0, Easing.CubicInOut,
                () =>
                {


                });
            parentAnimation.Add(0, 1, fadeOutAnimation);

            parentAnimation.Commit(this, "AnimationText", 16, 350U, Easing.CubicInOut, async (v, c) =>
            {
                label.Opacity = 0;
                //                ChangeTitleText(carouselMain.SelectedIndex, labelMenuTitle, "Profile", "Timers", "Workout Log",
                //                    "Discover 11");
            });
        }

        private async Task AnimateTextIn(Label label)
        {
            Animation parent2Animation = new Animation();
            Animation fadeInAnimation = new Animation(v => label.Opacity = v, 0, 1, Easing.CubicInOut);
            parent2Animation.Add(0, 1, fadeInAnimation);

            // Create "down" animation and add to parent.
            Animation downAnimation = new Animation(v => label.TranslationY = v, -10, 0, Easing.CubicOut,
                () => Debug.WriteLine("down finished"));
            parent2Animation.Add(0, 1, downAnimation);
            parent2Animation.Commit(this, "Animation2Text", 16, 350U, Easing.CubicInOut);
            running = false;
            disable = false;
        }

        private async void HoldTextFade(Animation parent2Animation)
        {
            var s = carouselMain.SelectedIndex;
            await Task.Delay(700);
            if (s == carouselMain.SelectedIndex)
                parent2Animation.Commit(this, "Animation2Text", 16, 350U, Easing.CubicInOut);

        }

        public async void ChangeTitleText(int selectedIndex, Label label, string title1 = null,
            string title2 = null, string title3 = null, string title4 = null)
        {

            var s = selectedIndex;

            switch (s)
            {
                case 0:
                    {

                        label.Text = title1;
                        //                        await GetValue(px, py);
                        //                        CurrentTitles.Remove(label.Text);
                        break;
                    }

                case 1:
                    {

                        label.Text = title2;
                        //                        await GetValue(px, py);
                        //                        CurrentTitles.Remove(label.Text);
                        break;
                    }

                case 2:
                    {
                        label.Text = title3;
                        //                        await GetValue(px, py);
                        //                        CurrentTitles.Remove(label.Text);
                        break;
                    }

                case 3:
                    {
                        label.Text = title4;
                        //                        await GetValue(px, py);
                        //                        CurrentTitles.Remove(label.Text);
                        break;
                    }
            }
        }

        private async void TapGestureRecognizerBox_OnTapped(object sender, EventArgs e)
        {
            if (running || disable)
                return;
            disable = true;
            var index = carouselMain.SelectedIndex;
            disable = true;
            if (running)
            {
                running = false;
                this.AbortAnimation("AnimationText");
            }
            //            null.ScaleTo(0, 350U, Easing.CubicOut);
            //            await AnimatePages.AnimatePageOut(gridHomeMenu, null); 

            Opacity = 0;
            switch (index)
            {
                case 0:
                    {
                        var nav = new UserProfileView();
                        await Navigation.PushAsync(nav);
                        disable = false;
                    }
                    break;
                case 1:
                    {

                        var nav = new TimerMenu();
                        await Navigation.PushAsync(nav, true);
                        disable = false;

                    }
                    break;
                case 2:
                    {

                        var nav = new WorkoutLogListView();
                        await Navigation.PushAsync(nav, true);
                        disable = false;
                    }
                    break;
                case 3:
                    {

                        var nav = new Discover11AthleticsView();
                        await Navigation.PushAsync(nav, true);
                        disable = false;
                    }
                    break;
            }

        }
    }
}