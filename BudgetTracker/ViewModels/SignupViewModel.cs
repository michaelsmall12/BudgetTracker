using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.ViewModels
{
    public class SignupViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Constructor for the signup view model
        /// </summary>
        public SignupViewModel() { }

        /// <summary>
        /// Gets or sets if the navigation view model is the target
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        /// <returns>bool indicating if the navigation was the intended target</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// Triggered when navigating away from the View Model
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        /// <summary>
        /// Triggered when navigating to the View Model
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
