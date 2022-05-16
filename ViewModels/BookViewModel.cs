using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NuScaleInterview.Models;
using System.ComponentModel;

namespace NuScaleInterview.ViewModels
{
    internal class BookViewModel :INotifyPropertyChanged
    {
        /// <summary>
        /// Initialize a new instance of the BookViewModel class. 
        /// </summary>
        public BookViewModel(int id, string title, string author, int copies)
        {
            //Call Books Constructor with passed values 
            Book = new Book(id, title, author, copies);
        }

        /// <summary>
        /// Grabs the book instance
        /// </summary>
        public Book Book
        {
            get;
            set;
        }

        /// <summary>
        /// Saves changes made to the Book Instance. 
        /// </summary>
        public void SaveChanges()
        {
            //Debug.Assert(false, String.Format("{0} by {1} current has {2} copies.", Book.BookTitle, Book.BookAuthor, Book.NumOfCopies));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
