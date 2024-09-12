using BudgetTracker.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Services.Interfaces
{
    public interface ICoreService
    {
        User ActiverUser { get; set; }
    }
}
