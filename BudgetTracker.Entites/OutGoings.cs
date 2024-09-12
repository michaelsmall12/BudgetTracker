using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Entites
{
    public class OutGoings : INotifyPropertyChanged
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

        private string name;
        /// <summary>
        /// Gets or sets the name of the outgoing
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private double price;
        /// <summary>
        /// Gets or sets the price
        /// </summary>
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }

        }

        private OutgoingStream outgoingStream;
        /// <summary>
        /// Gets or sets the outgoing stream
        /// </summary>
        public OutgoingStream OutgoingStream
        {
            get => outgoingStream;
            set
            {
                outgoingStream = value;
                OnPropertyChanged(nameof(OutgoingStream));
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
