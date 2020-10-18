namespace AddressBookSystem
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface created for abstraction and easy access for user
    /// </summary>
    public interface IAddressBook
    {
        void AddOrAccessAddressBook();
        void ViewAllAddressBooks();
        void DeleteAddressBook();
    }

    public class Program
    {
        public const string TO_ADD_OR_ACCESS = "add";
        public const string TO_VIEW_ALL_ADDRESSBOOKS = "view";
        public const string TO_DELETE_ADDRESS_BOOK = "delete";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Program");

            AddressBookDetails addressBookDetails = new AddressBookDetails();
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\nType to select address book\nAdd - To add or access address book\nview - To view all names of address books\nDelete - To delete Address book\nE - To exit");

                switch (Console.ReadLine().ToLower())
                {
                    // To add or access new Address book
                    case TO_ADD_OR_ACCESS:
                        addressBookDetails.AddOrAccessAddressBook();
                        break;

                    // To view all address book names
                    case TO_VIEW_ALL_ADDRESSBOOKS:
                        addressBookDetails.ViewAllAddressBooks();
                        break;

                    // To delete an address book
                    case TO_DELETE_ADDRESS_BOOK:
                        addressBookDetails.DeleteAddressBook();
                        break;

                    default:
                        Console.WriteLine("User exited application");
                        flag = false;
                        return;
                }
            }
        }
    }
}
