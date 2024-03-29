﻿using System;


namespace App11Athletics.ViewModels.Timers
{
    public class TabataFeatureViewModel : RoundCounterFeatureViewModel
    {
        public TabataFeatureViewModel()
        {

            WorkRound = true;
            TotalRoundTimeTimeSpan = TimeSpan.Zero;
            //            TotalTimeOffTimeSpan = TimeSpan.Zero;
            CurrentRound = 1;
            TotalRounds = 1;

        }

        public TimeSpan TotalTimeOffTimeSpan { get; set; }
        public bool WorkRound { get; set; }
        public string WorkTime { get; set; }


        //public string WorkTime
        //        {
        //            get
        //            {
        //                switch (WorkRound)
        //                {
        //                    case true:
        //                        return "Work Time!";
        //                    case false:
        //                        return "Rest Time";
        //                    default:
        //                        return string.Empty;
        //                }
        //            }
        //        }

        public override bool OnTimerTick()
        {
            if (CurrentRound == 1 && WorkRound && WorkTime != "WorkTime")
            {
                WorkTime = "WorkTime";
            }
            if (TimerRunning)
            {
                TimerTimeSpan = ElapsedTimeSpan - DateTime.Now.Subtract(StartDateTime);
            }

            if (WorkRound)
            {
                if (TimerTimeSpan < TotalRoundTimeTimeSpan && TimerTimeSpan > TimeSpan.Zero)
                {
                    return TimerRunning;
                }
                TimerTimeSpan = TimeSpan.Zero;
                ElapsedTimeSpan = TotalTimeOffTimeSpan;
                WorkRound = false;
                if (CurrentRound == TotalRounds)
                {
                    ResetCommandMethod();
                    WorkTime = "Finished!";
                    return Stop;
                }
                WorkTime = "Rest Time!";

                StartDateTime = DateTime.Now;
                return TimerRunning;
            }


            if (TimerTimeSpan > TimeSpan.Zero)
                return TimerRunning;

            TimerTimeSpan = TimeSpan.Zero;
            if (CurrentRound == TotalRounds)
            {
                ResetCommandMethod();
                WorkTime = "Finished!";
                return Stop;
            }
            CurrentRound = UpdateRound(CurrentRound, TotalRounds);
            ElapsedTimeSpan = TotalRoundTimeTimeSpan;
            WorkRound = true;
            WorkTime = "Work Time!";
            StartDateTime = DateTime.Now;
            return TimerRunning;
        }

        #region Overrides of RoundCounterFeatureViewModel

        public override void ResetTimer()
        {
            WorkRound = true;
            WorkTime = null;

            base.ResetTimer();
        }

        #endregion
    }
}