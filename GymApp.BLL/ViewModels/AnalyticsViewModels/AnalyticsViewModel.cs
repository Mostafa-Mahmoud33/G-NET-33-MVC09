using System;
using System.Collections.Generic;
using System.Text;

namespace GymApp.BLL.ViewModels.AnalyticsViewModels
{
    public class AnalyticsViewModel
    {
        public int TotalMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int ActiveMembers { get; set; }
        public int UpcomingSession { get; set; }
        public int OngoingSession { get; set; }
        public int CompletedSession { get; set; }
    }
}
