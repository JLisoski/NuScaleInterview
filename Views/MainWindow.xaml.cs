using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NuScaleInterview.ViewModels;
using NuScaleInterview.Models;

namespace NuScaleInterview.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Global Book Id Counter
        int bookId = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add Book to Inventory Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Add to Inventory
            string title;
            string author;

            //Clear StatusMessage Box 
            StatusMessageBox.Clear();

            //If Title is empty, default to Title
            if (!String.IsNullOrEmpty(BookTitleBox.Text))
            {
                title = BookTitleBox.Text;
            }
            else
            {
                StatusMessageBox.Text += "Blank title present, deafaulting to Title for the book\r\n";
                title = "Title";
            }

            //If Author is empty, default to Author
            if (!String.IsNullOrEmpty(BookAuthorBox.Text))
            {
                author = BookAuthorBox.Text;
            }
            else
            {
                StatusMessageBox.Text += "Blank author name present, deafult to Author for the book\r\n";
                author = "Author";
            }
 
            int copies;

            //Try-catch converting the number entered
            try
            {
                copies = Convert.ToInt32(NumOfCopiesBox.Text);
            }
            catch (Exception){
                StatusMessageBox.Text += "Invalid Number of Copies! Defaulting to 1";
                copies = 1;
            }

            //Declare new book object, needs to be changed to BookViewModel
            BookViewModel currentBook = new BookViewModel(bookId, title, author, copies);

            //Increase bookId
            bookId++;

            //Add book to DataGrid
            DataGrid.Items.Add(currentBook);

            //Clear the text boxes
            BookAuthorBox.Clear();
            BookTitleBox.Clear();
            NumOfCopiesBox.Clear();
        }

        /// <summary>
        /// Remove book from Inventory Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Remove from Inventory

            //Clear Status Message Box
            StatusMessageBox.Clear();

            //If Datagrid count is zero
            if(DataGrid.Items.Count == 0)
            {
                BookIdBox.Clear();
                StatusMessageBox.Text = "DataGrid is empty, there is nothing to remove";
                return;
            }

            //Pull BookId from BookID Text Box next to Remove Button
            int bookIdToRemove;
            try
            {
                bookIdToRemove = Convert.ToInt32(BookIdBox.Text);
            }
            catch (Exception)
            {
                //Exception thrown, try again
                BookIdBox.Clear();
                StatusMessageBox.Text = "Invalid BookId, Try Again!";
                return;
            }

            //Search DataGrid for bookId to remove, and return index position
            int removeIndex = 0;
            int iterationsMade = 0;

            for(int i=0; i<DataGrid.Items.Count; i++)
            {
                //Grab current BookViewModel object
                BookViewModel tempBook = (BookViewModel)DataGrid.Items.GetItemAt(i); 

                if(tempBook.Book.BookId == bookIdToRemove)
                {
                    removeIndex = i;
                    iterationsMade = i;
                    break;
                }

                //Increase iteration couunter
                iterationsMade++;
            }

            //If bookId not found, output error message
            if (removeIndex == 0 && iterationsMade != 0)
            {
                BookIdBox.Clear();
                StatusMessageBox.Text = "BookId Not Found, Try Again!";
            }
            else
            {
                BookIdBox.Clear();

                //Remove desired BookId from the Data Grid
                DataGrid.Items.RemoveAt(removeIndex);
            }

        }

        /// <summary>
        /// Update existing book in Inventory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Update the Inventory

            //Clear Status Message Box
            StatusMessageBox.Clear();

            //If Datagrid count is zero
            if (DataGrid.Items.Count == 0)
            {
                BookIdToUpdateBox.Clear();
                StatusMessageBox.Text = "DataGrid is empty, there is nothing to update";
                return;
            }

            //Pull BookId from BookIdToUpdateText Box next to update button
            int bookIdToUpdate = 0;
            try
            {
                bookIdToUpdate = Convert.ToInt32(BookIdToUpdateBox.Text);
            }
            catch (Exception)
            {
                BookIdToUpdateBox.Clear();
                StatusMessageBox.Text = "Invalid BookId, Try Again!";
                return;
            }

            //Search DataGrid for bookId to update, and return index position
            int updateIndex = 0;
            int iterations = 0;

            for(int i=0; i<DataGrid.Items.Count; i++)
            {
                BookViewModel tempBook = (BookViewModel)DataGrid.Items.GetItemAt(i);

                if(tempBook.Book.BookId == bookIdToUpdate)
                {
                    updateIndex = i;
                    break;
                }

                iterations++;
            }

            //Update Book Fields
            BookViewModel oldBook = (BookViewModel)DataGrid.Items.GetItemAt(updateIndex);
            string title;
            string author;
            int newBookCopies;

            //Grab updated info

            //If book title box empty, use old book title 
            if (!String.IsNullOrEmpty(BookTitleBox.Text))
            {
                title = BookTitleBox.Text;
            }
            else
            {
                title = oldBook.Book.BookTitle;
            }

            //If book author box empty, use old book author 
            if (!String.IsNullOrEmpty(BookAuthorBox.Text))
            {
                author = BookAuthorBox.Text;
            }
            else
            {
                author = oldBook.Book.BookAuthor;
            }

            //If num of copies box empty, use old book copies 
            if (!String.IsNullOrEmpty(NumOfCopiesBox.Text))
            {
                newBookCopies = Convert.ToInt32(NumOfCopiesBox.Text);
            }
            else
            {
                newBookCopies = oldBook.Book.NumOfCopies;
            }

            BookViewModel newBook = new BookViewModel(oldBook.Book.BookId, title, author, newBookCopies);


            if(updateIndex == 0 && iterations != 0)
            {
                BookIdToUpdateBox.Clear();
                StatusMessageBox.Text = "BookId Not Found, Try Again!";
            }
            else
            {
                BookIdToUpdateBox.Clear();
                //Insert into DataGrid at specified index
                DataGrid.Items.Insert(updateIndex, newBook);
                DataGrid.Items.Remove(oldBook);
            }

            //Clear the text boxes
            BookAuthorBox.Clear();
            BookTitleBox.Clear();
            NumOfCopiesBox.Clear();
            BookIdToUpdateBox.Clear();


        }

        /// <summary>
        /// Not sure what this is for...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selecting a row in the data grid
        }
    }
}
