using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookSystem
{
    public class ContactDetails
    {
        // Variables
        public string firstName;
        public string lastName;
        public string address;
        public string city;
        public string state;
        public string zip;
        public string phoneNumber;
        public string email;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactDetails"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="address">The address.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="email">The email.</param>
        public ContactDetails(string firstName, string lastName, string address, string city, string state, string zip, string phoneNumber, string email)
        {
            this.firstName = firstName.ToLower();
            this.lastName = lastName.ToLower();
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.
        /// </returns>
        public override bool Equals(Object obj)
        {
            // if the list is null
            if (obj == null)
                return false;

            // if the object passed is not a list
            if (!(obj is List<ContactDetails>))
                return true;

            // Get the contacts from list with same name
            var duplicates = ((List<ContactDetails>)obj).FindAll(contact => ((contact.firstName).ToLower() == (this.firstName).ToLower()
                                                                    && (contact.lastName).ToLower() == (this.lastName).ToLower()));
            
            // Return true if duplicate is found else false
            if (duplicates.Count > 0)
                return true;
            else
                return false;
        }
    }
}
