using GymApp.DAl.Context;
using GymApp.DAl.Contracts;
using GymApp.DAl.Models;
using Microsoft.EntityFrameworkCore;

namespace GymApp.DAl.Repositories;



public class PlansRepository : GenericRepository<Plan>, IGenericRepository<Plan>
{

    public PlansRepository(GymDbContext context) : base(context)
    {
    }




}