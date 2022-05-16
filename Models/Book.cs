using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuScaleInterview.Models
{
    class Book : IDataErrorInfo, INotifyPropertyChanged
    {
 

        /// <summary>
        /// Initialize a new instance of the Book Class
        /// </summary>
        public Book(int id, String title, String author, int copies)
        {
            BookId = id;
            BookTitle = title;
            BookAuthor = author;
            NumOfCopies = copies;
        }

        private int bookId;
        /// <summary>
        /// Gets or sets the Bookid of the Book.
        /// </summary>
        public int BookId
        {
            get
            {
                return bookId;
            }
            set
            {
                bookId = value;
                OnPropertyChanged("BookId");
            }
        }

        private String _BookTitle;
        /// <summary>
        /// Gets or sets the Title of the Book. 
        /// </summary>
        public String BookTitle {
            get
            {
                return _BookTitle;
            }
            set
            {
                _BookTitle = value;
                OnPropertyChanged("BookTitle");
            }
        }

        private String _BookAuthor;
        /// <summary>
        /// Gets or sets the Author of the Book. 
        /// </summary>
        public String BookAuthor
        {
            get
            {
                return _BookAuthor;
            }
            set
            {
                _BookAuthor = value;
                OnPropertyChanged("BookAuthor");
            }
        }

        private int _NumOfCopies;
        /// <summary>
        /// Gets or sets the number of copies of the book. 
        /// </summary>
        public int NumOfCopies
        {
            get
            {
                return _NumOfCopies;
            }
            set
            {
                _NumOfCopies = value;
                OnPropertyChanged(nameof(NumOfCopies));
            }
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

        #region IDataErrorInfo Members

         public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        #endregion

        #region Validation

        static readonly string[] ValidatedProperties =
        {
            "BookTitle",
            "BookAuthor",
            "NumOfCopies"
        };

        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        string GetValidationError(String propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "BookTitle":
                case "BookAuthor":
                    error = ValidateName(propertyName);
                    break;
                case "NumOfCopies":
                    error = ValidateNum(propertyName);
                    break;

            }

            return error;
        }

        private string ValidateName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                switch (name)
                {
                    case "BookTitle":
                        return "The Title is somewhere on the cover, look again...";
                    case "BookAuthor":
                        return "Someone wrote this, give credit where credit is due.";
                }
            }

            return null;
        }

        private string ValidateNum(string num)
        {
            try
            {
                int numOfCopies = Convert.ToInt32(num);
            }
            catch (OverflowException)
            {
                return num + " is outside the range of the Int32 type. Try again...";
            }
            catch (FormatException)
            {
                return num + " is not in a recognizable format. Try again...";
            }

            return null;
        }
        #endregion
    }
}
