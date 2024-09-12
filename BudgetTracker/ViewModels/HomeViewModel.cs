using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Entites;

namespace BudgetTracker.ViewModels
{
    public class HomeViewModel : BindableBase, INavigationAware
    {
        public ISeries[] IncomeSeries { get; set; } = new ISeries[]
{
    new PieSeries<double>
                {
                    Values = new double[] { 40 },
                    Name = "Slice 1",
                    Pushout = 0, // Prevent the "pop-out" effect
                   InnerRadius = 20,// Show the tag (name) on the chart
                },
                new PieSeries<double>
                {
                    Values = new double[] { 30 },
                    Name = "Slice 2",
                    Pushout = 0, // No pop-out,
                    InnerRadius = 20 // Display tag
                },
                new PieSeries<double>
                {
                    Values = new double[] { 20 },
                    Name = "Slice 3",
                    Pushout = 0,
                     InnerRadius = 20
                },
                new PieSeries<double>
                {
                    Values = new double[] { 10 },
                    Name = "Slice 4",
                    Pushout = 0,
                     InnerRadius = 20
                }
            };

        public ISeries[] OutgoingSeries { get; set; } = new ISeries[]
{
    new PieSeries<double>
                {
                    Values = new double[] { 40 },
                    Name = "Slice 1",
                    Pushout = 0, // Prevent the "pop-out" effect
                   InnerRadius = 20,// Show the tag (name) on the chart
                },
                new PieSeries<double>
                {
                    Values = new double[] { 30 },
                    Name = "Slice 2",
                    Pushout = 0, // No pop-out,
                    InnerRadius = 20 // Display tag
                },
                new PieSeries<double>
                {
                    Values = new double[] { 20 },
                    Name = "Slice 3",
                    Pushout = 0,
                     InnerRadius = 20
                },
                new PieSeries<double>
                {
                    Values = new double[] { 10 },
                    Name = "Slice 4",
                    Pushout = 0,
                     InnerRadius = 20
                }
           };

        public ICoreService CoreService { get; set; }

        public IRegionManager RegionManager { get; set; }
        public HomeViewModel(ICoreService coreService, IRegionManager regionManager)
        {
            CoreService = coreService;
            RegionManager = regionManager;
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
        return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public  void OnNavigatedTo(NavigationContext navigationContext)
        {
            CoreService.ActiverUser.Outgoings = new List<OutGoings>();
            CoreService.ActiverUser.Outgoings.Add(new Entites.OutGoings() { Name = "Item1", Price = 10, OutgoingStream = new Entites.OutgoingStream() { Category = "Cat1" } });
            CoreService.ActiverUser.Outgoings.Add(new Entites.OutGoings() { Name = "Item2", Price = 20, OutgoingStream = new Entites.OutgoingStream() { Category = "Cat2" } });
            CoreService.ActiverUser.Outgoings.Add(new Entites.OutGoings() { Name = "Item3", Price = 30, OutgoingStream = new Entites.OutgoingStream() { Category = "Cat3" } });
            CoreService.ActiverUser.Outgoings.Add(new Entites.OutGoings() { Name = "Item4", Price = 40, OutgoingStream = new Entites.OutgoingStream() { Category = "Cat4" } });
            if (CoreService.ActiverUser.Outgoings.Any())
            {
                for(int i = 0; i < CoreService.ActiverUser.Outgoings.Count ; i++)
                {
                    OutgoingSeries[i] = new PieSeries<double>()
                    {
                        Values = new double[] { CoreService.ActiverUser.Outgoings.ElementAt(i).Price },
                        Name = CoreService.ActiverUser.Outgoings.ElementAt(i).Name,
                        Pushout = 0,
                        InnerRadius = 40
                    };
                }

            }

            RegionManager.RequestNavigate("HomeRegion", "OutgoingView");
        }
    }
}
