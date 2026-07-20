namespace GymApp.BLL.ViewModels.Sessions
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TrainerName { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public int AvailableSlots { get; set; }

        public TimeSpan Duration => EndDate - StartDate;


        // Started start date > Current Date && start date < end date
        // completed start date > Current end date < Current Date



        public string Status
        {
            get
            {
                var now = DateTime.Now;
                if (StartDate < now && EndDate > now)
                    return "Ongoing";
                else if (EndDate < now)
                    return "Completed";
                else
                    return "Upcoming";
                
            }
        }
    }
}
