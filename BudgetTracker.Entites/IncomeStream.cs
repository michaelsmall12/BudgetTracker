using System.ComponentModel;

namespace BudgetTracker.Entites
{
    public class IncomeStream : INotifyPropertyChanged
    {
        private Guid _id;
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string incomeCategory;
        /// <summary>
        /// Gets or sets the income type
        /// </summary>
        public string IncomeCategory
        {
            get => incomeCategory;
            set
            {
                incomeCategory = value;
                OnPropertyChanged(nameof(IncomeCategory));
            }
        }

        /// <summary>
        /// Gets or sets the property changed event
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Triggers the Property changed event
        /// </summary>
        /// <param name="name">The name of the property to update</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
