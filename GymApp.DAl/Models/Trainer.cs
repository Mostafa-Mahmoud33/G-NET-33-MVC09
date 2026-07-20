using GymApp.DAl.Models.Enums;

namespace GymApp.DAl.Models
{
    public class Trainer : GymUser
    {
        public Specialties Specialty { get; set; }

        public ICollection<Session> Sessions { get; set; } = [];
    }
}
