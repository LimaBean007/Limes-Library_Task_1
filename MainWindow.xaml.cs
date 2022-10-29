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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///  
    //---------CODE ATTRIBUTION---------
    //The following method was from StackOverFlow
    //Authors : dtb
    //Link:"https://www.flaticon.com/free-icons/education" title="education icons">Education icons created by Freepik - Flaticon
    //---------END---------
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //setup
            gridLeaderBoard.Visibility = Visibility.Hidden;
            gridWelcome.Visibility = Visibility.Hidden;
            gridReplaceBook.Visibility = Visibility.Hidden;
            gridSignIn.Visibility = Visibility.Visible;
            
            populateBooks();
           

        }
        Point LB1StartMousePos;
        Point LB2StartMousePos;
        private void checkBookSortingList()
        {
            //creates a list to store the items that user sorted
            List<Book> booklist = new List<Book>();
            foreach (ListBoxItem item in listviewBooksSorted.Items)
            {
                //---------CODE ATTRIBUTION---------
                //The following method was from StackOverFlow
                //Authors : David Anderson
                //Link: https://stackoverflow.com/questions/10476902/how-to-insert-object-type-in-listview#:~:text=A%20ListView%20cannot%20add%20or,and%20set%20its%20Tag%20property.&text=An%20Object%20that%20contains%20data,be%20assigned%20to%20this%20property.
                //---------END---------
                //gets the objects
                Book book = (Book)item.Tag;
                booklist.Add(book);

            }
            //creates a list that is stored
            List<Book> newBooks = ListWorker.books.OrderBy(item => item.BookTopic).ThenBy(item => item.BookNumber).ThenBy(item => item.AuthorSurname).ToList();
            //checks if the lists match and are in order
            if (newBooks.SequenceEqual(booklist))
            {
                //adds points, opdates their points and gets new books
                MessageBox.Show("Well done. You got it correct : )","Limes Library",MessageBoxButton.OK,MessageBoxImage.Information);
                Util.addPoints();
                setUserDetails();
                populateBooks();

            }
            else
            {
                string output = "";
                foreach (Book book in newBooks)
                {
                    output+= book.BookTopic + "." + book.BookNumber + " " + book.AuthorSurname +"\n";
                }
                MessageBox.Show("Incorrect. Please try again : (\n","Limes Library", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBox.Show(output, "Limes Library", MessageBoxButton.OK, MessageBoxImage.Information);
                populateBooks();

            }
        }
       
        private void populateBooks()
        {
            //clears all lists and makes new books
            listviewBooksUnsorted.Items.Clear();
            listviewBooksSorted.Items.Clear();
            ListWorker.books.Clear();
            for (int i = 0; i < 10; i++)
            {
                Book book = new Book();
                //adds the books to the lists and displays it
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Tag = book;
                listBoxItem.Content = book.BookTopic + "." + book.BookNumber + " " + book.AuthorSurname;
                ListWorker.books.Add(book);
                listviewBooksUnsorted.Items.Add(listBoxItem);
            }
        }
     

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //clears all listviews
            listviewBooksUnsorted.Items.Clear();
            listviewBooksSorted.Items.Clear();
            //popluates the listviews
            foreach (Book book in ListWorker.books)
            {
                //adds to the listview
                ListBoxItem item = new ListBoxItem();
                item.Content = book.BookTopic + "." + book.BookNumber + " " + book.AuthorSurname;

                listviewBooksUnsorted.Items.Add(item);

            }

        }

        private void populateLeaderBoard()
        {
            //creates a list to store all users and another for the top 3
            List<User> newUsers = Util.sortUserPoints();
            List<User> top3 = new List<User>();
            //array to check if there is a second and third user
            bool[] Top3 = {false,false};
            if (newUsers != null)
            {
                //gets the first user
                User firstUser = newUsers.FirstOrDefault();
                //---------CODE ATTRIBUTION---------
                //The following method was from Stackoverflow
                //Authors : Andrei Tătar
                //Link: https://stackoverflow.com/questions/38634835/change-listbox-item-background-color-programmatically
                //---------END---------
                //adds the first place user
                listviewLeaderBoard.Items.Add(new ListBoxItem { Content = firstUser.Username + " " + firstUser.UserPoints,FontFamily= new FontFamily("Ink Free"),FontSize=35, FontStyle =FontStyles.Oblique, Foreground = Brushes.Gold });
                //checks if there is a second place user
                if (newUsers.Count >= 2)
                {
                    //if there is a user add to the list
                    listviewLeaderBoard.Items.Add(new ListBoxItem { Content = newUsers[1].Username + " " + newUsers[1].UserPoints, FontFamily = new FontFamily("Ink Free"), FontSize = 30, FontStyle = FontStyles.Oblique, Foreground = Brushes.Silver });
                    Top3[0] = true;
                }
                //checks if there is a third place user
                if (newUsers.Count >= 3)
                {
                    //if there is a user add to the list
                    listviewLeaderBoard.Items.Add(new ListBoxItem { Content = newUsers[2].Username + " " + newUsers[2].UserPoints, FontFamily = new FontFamily("Ink Free"), FontSize = 25, FontStyle = FontStyles.Oblique, Foreground = Brushes.RosyBrown});
                    Top3[1] = true;
                }
                //removes the first user and if there is, the second and third user
                newUsers.RemoveAt(0);
                if (Top3[0] == true)
                {
                    newUsers.RemoveAt(0);
                }
                if (Top3[1] == true)
                {
                    newUsers.RemoveAt(0);
                }
                //displays the users
                foreach (User user in newUsers)
                {

                    listviewLeaderBoard.Items.Add(new ListBoxItem { Content = user.Username + " " + user.UserPoints, FontFamily = new FontFamily("Ink Free") });

                }
            }
        }

        private void listviewBooksUnsorted_Drop(object sender, DragEventArgs e)
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from YouTube
            //Authors : SingletonSean
            //Link:https://www.youtube.com/watch?v=DbSLgHpY_sA
            //---------END---------
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                listviewBooksUnsorted.Items.Add(listItem);
            }
        }

        private void listviewBooksUnsorted_MouseMove(object sender, MouseEventArgs e)
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from YouTube
            //Authors : SingletonSean
            //Link:https://www.youtube.com/watch?v=DbSLgHpY_sA
            //---------END---------
            Point mPos = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    // This gets the selected item
                    ListBoxItem selectedItem = (ListBoxItem)listviewBooksUnsorted.SelectedItem;
                    // You need to remove it before adding it to another listbox.
                    // if  you dont, it throws an error (due to referencing between 2 listboxes)
                    listviewBooksUnsorted.Items.Remove(selectedItem);

                    // The actual dragdrop thingy
                    // DragDropEffects.Copy... i dont think this matters but oh well.
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Copy);

                    // This code will check if the listboxitem is inside a ListBox or not.
                    // This will stop the ListBoxItem you dragged from vanishing if you dont
                    // Drop it inside a listbox (drop it in the titlebar or something lol)

                    // ListBoxItems are objects obviously, and objects are passed and moved by reference.
                    // Any change to an object affects every reference. 'selectedItem' is a reference
                    // To LB2.SelectedItem, and they both will NEVER be different :)

                    if (selectedItem.Parent == null)
                    {
                        listviewBooksUnsorted.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }

        private void listviewBooksUnsorted_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from YouTube
            //Authors : SingletonSean
            //Link:https://www.youtube.com/watch?v=DbSLgHpY_sA
            //---------END---------
            LB1StartMousePos = e.GetPosition(null);
        }

        private void listviewBooksSorted_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from YouTube
            //Authors : SingletonSean
            //Link:https://www.youtube.com/watch?v=DbSLgHpY_sA
            //---------END---------
            LB2StartMousePos = e.GetPosition(null);
        }

        private void listviewBooksSorted_Drop(object sender, DragEventArgs e)
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from YouTube
            //Authors : SingletonSean
            //Link:https://www.youtube.com/watch?v=DbSLgHpY_sA
            //---------END---------
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                listviewBooksSorted.Items.Add(listItem);
            }
        }

        private void listviewBooksSorted_MouseMove(object sender, MouseEventArgs e)
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from YouTube
            //Authors : SingletonSean
            //Link:https://www.youtube.com/watch?v=DbSLgHpY_sA
            //---------END---------
            Point mPos = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    // This gets the selected item
                    ListBoxItem selectedItem = (ListBoxItem)listviewBooksSorted.SelectedItem;
                    // You need to remove it before adding it to another listbox.
                    // if  you dont, it throws an error (due to referencing between 2 listboxes)
                    listviewBooksSorted.Items.Remove(selectedItem);

                    // The actual dragdrop thingy
                    // DragDropEffects.Copy... i dont think this matters but oh well.
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Copy);

                    // This code will check if the listboxitem is inside a ListBox or not.
                    // This will stop the ListBoxItem you dragged from vanishing if you dont
                    // Drop it inside a listbox (drop it in the titlebar or something lol)

                    // ListBoxItems are objects obviously, and objects are passed and moved by reference.
                    // Any change to an object affects every reference. 'selectedItem' is a reference
                    // To LB2.SelectedItem, and they both will NEVER be different :)

                    if (selectedItem.Parent == null)
                    {
                        listviewBooksSorted.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }

        private void btnReplaceBooks_Click_1(object sender, RoutedEventArgs e)
        {
            //goes the replace books page
            gridWelcome.Visibility = Visibility.Hidden;
            gridLeaderBoard.Visibility = Visibility.Hidden;
            gridReplaceBook.Visibility = Visibility.Visible;
            setUserDetails();
            populateBooks();
        }

        private void setUserDetails()
        {
            //sets the GUI components to the users name and points
            lblUsername.Content = currentUser.username;
            lblPoints.Content = currentUser.userPoints;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Limes Library",
                          MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                //Creates a delay of 1.4 seconds
                int delay = 1400;
                //---------CODE ATTRIBUTION---------
                //The following method was tafrom StackOverFlow
                //Author : Carlos Loth
                //Link: https://stackoverflow.com/questions/2902327/c-sharp-winforms-change-cursor-icon-of-mouse
                Mouse.OverrideCursor = Cursors.Wait;
                //---------END---------
                Task.Delay(delay).Wait();
                Environment.Exit(1);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //creates validation object
            Validation newVal = new Validation();

            try
            {
                //creates string and stores user input
                string name = txtUsername.Text;
                //checks if empty and has no symbols
                if (newVal.checkStringInput(name) == true && newVal.checkSpecial(name) ==false)
                {
                    //calls method to either add the user or log them in
                    Util.checkIfUserExists(name);
                    //goes to main menu
                    gridSignIn.Visibility = Visibility.Hidden;
                    gridWelcome.Visibility = Visibility.Visible;
                    setUserDetails();
                }
                else
                {
                    MessageBox.Show("Please enter your Username","Limes Library", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter your Username", "Limes Library", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnCheckSort_Click(object sender, RoutedEventArgs e)
        {
            //calls check method
            checkBookSortingList();
        }
        private void btnFindCallNumbers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming soon : )","Limes Library", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnIdentifyAreas_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming soon : )", "Limes Library", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            //goes to Leaderboard and popluates the list
            gridLeaderBoard.Visibility = Visibility.Visible;
            gridReplaceBook.Visibility = Visibility.Hidden;
            populateLeaderBoard();
        }

        private void btnLeaderBoardBack_Click(object sender, RoutedEventArgs e)
        {
            //goes to the previous screen and clears the leaderboard
            gridLeaderBoard.Visibility = Visibility.Hidden;
            listviewLeaderBoard.Items.Clear();
            gridReplaceBook.Visibility = Visibility.Visible;
            gridWelcome.Visibility = Visibility.Visible;
        }
        private void toMainMenu()
        {
            listviewBooksSorted.Items.Clear();
            listviewBooksUnsorted.Items.Clear();
            listviewLeaderBoard.Items.Clear();

        }
        private void btnReplaceBooks_Back_Click(object sender, RoutedEventArgs e)
        {
            //goes to the welcome page
            gridLeaderBoard.Visibility = Visibility.Hidden;
            listviewLeaderBoard.Items.Clear();
            gridReplaceBook.Visibility = Visibility.Hidden;
            gridWelcome.Visibility = Visibility.Visible;
            toMainMenu();
        }
    }
}
