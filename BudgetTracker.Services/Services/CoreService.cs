using BudgetTracker.Entites;
using BudgetTracker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Services.Services
{
    public class CoreService : ICoreService
    {
        public User ActiverUser { get ; set ; }
    }
}
