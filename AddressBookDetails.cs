namespace AddressBookSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NLog;

    class AddressBookDetails : IAddressBook
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        string nameOfAddressBook;
        const string ADD_CONTACT = "add";
        const string UPDATE_CONTACT = "update";
        const string SEARCH_CONTACT = "search";
        const string REMOVE_CONTACT = "remove";
        const string GET_ALL_CONTACTS = "view";
        public Dictionary<string, AddressBook> addressBookList = new Dictionary<string, AddressBook>();
        public static Dictionary<string, List<ContactDetails>> cityToContactMap = new Dictionary<string, List<ContactDetails>>();
        public static Dictionary<string, List<ContactDetails>> stateToContactMap = new Dictionary<string, List<ContactDetails>>();


        /// <summary>
        /// Gets the address book.
        /// </summary>
        /// <returns></returns>
        private AddressBook GetAddressBook()
        {
            Console.WriteLine("\nEnter name of Address Book to be accessed or to be added");
            nameOfAddressBook = Console.ReadLine();

            // search for address book in dictionary
            if (addressBookList.ContainsKey(nameOfAddressBook))
            {
                Console.WriteLine("\nAddressBook Identified");
                logger.Info("Address book " + nameOfAddressBook + " is accessed by user");
                return addressBookList[nameOfAddressBook];
            } 

            // Offer to create a address book if not found in dictionary
            logger.Warn("AddressBook " + nameOfAddressBook + " not found");
            Console.WriteLine("\nAddress book not found. Type y to create a new address book or E to abort");

            // If user want to create a new address book
            if ((Console.ReadLine().ToLower()) == "y")
            {
                AddressBook addressBook = new AddressBook(nameOfAddressBook);
                addressBookList.Add(nameOfAddressBook, addressBook);
                Console.WriteLine("\nNew AddressBook Created");
                logger.Info("New address book created with name : " + nameOfAddressBook);
                return addressBookList[nameOfAddressBook];
            }

            // If User want to abort the operation 
            else
            {
                Console.WriteLine("\nAction Aborted");
                logger.Info("User aborted the operation to create new Address book with name : " + nameOfAddressBook);
                return null;
            }
        }

        /// <summary>
        /// Searches the in city.
        /// </summary>
        public void SearchInCity()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the city name to search for contact");
            string cityName = Console.ReadLine().ToLower();

            // Get the person name to be searched
            Console.WriteLine("\nEnter the person firstname to be searched");
            string firstName = Console.ReadLine().ToLower();
            Console.WriteLine("\nEnter the person lastname to be searched");
            string lastName = Console.ReadLine().ToLower();

            try
            {
                // Get the list of contacts whose city and name matches with search
                var searchResult = cityToContactMap[cityName].FindAll(contact => contact.firstName.ToLower() == firstName
                                                                      && contact.lastName.ToLower() == lastName);

                // If no contacts exist
                if (searchResult.Count() == 0)
                {
                    Console.WriteLine("\nNo contacts found of given search", nameOfAddressBook);
                    return;
                }
                Console.Write("\nThe contacts found in of given search are :");

                // Display the search results
                foreach (ContactDetails contact in searchResult)
                {
                    AddressBook.ToString(contact);
                }
            }
            catch
            {
                Console.WriteLine("\nNo contacts found of given search");
                return;
            }
        }

        /// <summary>
        /// Views all the contacts of a city.
        /// </summary>
        public void ViewAllByCity()
        {
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the city name to search for contact");
            string cityName = Console.ReadLine().ToLower();

            // If the given city doesnt exist
            if (!(cityToContactMap.ContainsKey(cityName)))
            {
                Console.WriteLine("\nNo contacts exist in the city");
                return;
            }

            foreach (ContactDetails contact in cityToContactMap[cityName])
                AddressBook.ToString(contact);
        }

        /// <summary>
        /// Views all the contacts of a state
        /// </summary>
        public void ViewAllByState()
        {
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the state name to search for contact");
            string stateName = Console.ReadLine().ToLower();

            // If the given city doesnt exist
            if (!(stateToContactMap.ContainsKey(stateName)))
            {
                Console.WriteLine("\nNo contacts exist in the city");
                return;
            }

            foreach (ContactDetails contact in stateToContactMap[stateName])
                AddressBook.ToString(contact);
        }

        /// <summary>
        /// Searches the state of the in.
        /// </summary>
        public void SearchInState()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the state name to search for contact");
            string stateName = Console.ReadLine().ToLower();

            // Get the person name to be searched
            Console.WriteLine("\nEnter the person firstname to be searched");
            string firstName = Console.ReadLine().ToLower();
            Console.WriteLine("\nEnter the person lastname to be searched");
            string lastName = Console.ReadLine().ToLower();

            try
            {
                // Get the list of contacts whose city and name matches with search
                var searchResult = stateToContactMap[stateName].FindAll(contact => contact.firstName.ToLower() == firstName
                                                                      && contact.lastName.ToLower() == lastName);

                // If no contacts exist
                if (searchResult.Count() == 0)
                {
                    Console.WriteLine("\nNo contacts found in addressbook {0} of given search", nameOfAddressBook);
                    return;
                }
                Console.Write("\nThe contacts found in addressbook {0} of given search are :", nameOfAddressBook);

                // Display the search results
                foreach (ContactDetails contact in searchResult)
                {
                    AddressBook.ToString(contact);
                }
            }
            catch
            {
                Console.WriteLine("No contacts found in addressbook {0} of given search", nameOfAddressBook);
                return;
            }

        }

        /// <summary>
        /// Deletes the address book.
        /// </summary>
        public void DeleteAddressBook()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("No record found");
                return;
            }
            Console.WriteLine("\nEnter the name of address book to be deleted :");

            //search for address book with given name
            try
            {
                addressBookList.Remove(Console.ReadLine());
                Console.WriteLine("Address book deleted successfully");
                logger.Info("User deleted the AddressBook " + nameOfAddressBook);
            }
            catch
            {
                Console.WriteLine("Address book not found");
            }
        }

        /// <summary>
        /// Views all address books.
        /// </summary>
        public void ViewAllAddressBooks()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("No record found");
                return;
            }

            // Print the address book names available
            Console.WriteLine("\nThe namesof address books available are :");
            foreach (KeyValuePair<string, AddressBook> keyValuePair in addressBookList)
                Console.WriteLine(keyValuePair.Key);
            logger.Info("User viewd all AddressBook names");
        }

        /// <summary>
        /// Adds the or access address book.
        /// </summary>
        public void AddOrAccessAddressBook()
        {
            // To get the name of the addressbook
            AddressBook addressBook = GetAddressBook();

            // Returns no record found if address book is empty
            if (addressBook == null)
            {
                Console.WriteLine("Action aborted");
                return;
            }

            // select the action to be performed in address book   
            while(true)
            {
                Console.WriteLine("\nSelect from below to work on Address book {0}", addressBook.nameOfAddressBook);
                Console.WriteLine("\nType\n\nAdd - To add a contact" +
                                  "\nUpdate- To update a contact" +
                                  "\nView - To view all contacts" +
                                  "\nRemove - To remove a contact and " +
                                  "\nSearch- To search to get contact deatails\nE - To exit\n ");
                switch (Console.ReadLine().ToLower())
                {
                    case ADD_CONTACT:
                        addressBook.AddContact();
                        break;

                    case UPDATE_CONTACT:
                        addressBook.UpdateContact();
                        break;

                    case SEARCH_CONTACT:
                        addressBook.DisplayContactDetails();
                        break;

                    case REMOVE_CONTACT:
                        addressBook.RemoveContact();
                        break;

                    case GET_ALL_CONTACTS:
                        addressBook.GetAllContacts();
                        break;

                    default:
                        Console.WriteLine("\nInvalid option. Exiting address book");
                        return;
                }

                // Ask the user to continue in same address book or to exit
                Console.WriteLine("\nType y to continue in same address Book or any other key to exit");

                // If not equal to y  then exit
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    logger.Debug("User exited the address book " + nameOfAddressBook);
                    return;
                }
            }
        }

        /// <summary>
        /// Adds to city dictionary.
        /// </summary>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="contact">The contact.</param>
        public static void AddToCityDictionary(string cityName, ContactDetails contact)
        {
            // Check if the map already has city key
            if (!(cityToContactMap.ContainsKey(cityName)))
                cityToContactMap.Add(cityName, new List<ContactDetails>());

            // Add the contact to list of respective city map
            cityToContactMap[cityName].Add(contact);
        }

        /// <summary>
        /// Adds to state dictionary.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="contact">The contact.</param>
        public static void AddToStateDictionary(string stateName, ContactDetails contact)
        {
            // Check if the map already has state key
            if (!stateToContactMap.ContainsKey(stateName))
                stateToContactMap.Add(stateName, new List<ContactDetails>());

            // Add the contact to list of respective city map
            stateToContactMap[stateName].Add(contact);
        }
     }
}
