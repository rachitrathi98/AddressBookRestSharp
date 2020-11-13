using AddressBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AddressBookTest
{
    [TestClass]
    public class UnitTest1
    {

        /// <summary>
        /// UC16
        /// </summary>
        [TestMethod]
        public void CompareRetrievedDataFromDB()
        {
            ContactsDB expected = new ContactsDB()
            {
                first_name = "Rachit",
                last_name = "Rathi",
                city = "Juhu",
                phone = "123456789",
                B_Name = "Book1",
                B_Type = "Family"
            };
            
            var actual = AddressBookRepoDB.RetrieveData();

            Assert.AreEqual(expected.first_name, actual.first_name);
            Assert.AreEqual(expected.last_name, actual.last_name);
            Assert.AreEqual(expected.city, actual.city);
            Assert.AreEqual(expected.phone, actual.phone);
            Assert.AreEqual(expected.B_Name, actual.B_Name);
            Assert.AreEqual(expected.B_Type, actual.B_Type);

        }
        /// <summary>
        /// UC17 
        /// </summary>
        [TestMethod]
        public void CompareUpdatedDataFromDB()
        {
            string expected = "Karnataka";

            string actual = AddressBookRepoDB.UpdateDetailsInDB();

            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// UC18 
        /// </summary>
        [TestMethod]
        public void GetDateBetweenRangeNow()
        {
            string expected = "RachitTanmayAkhil";

            string actual = AddressBookRepoDB.GetDateBetweenRange();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddContact()
        {
            bool expected = true;
            ContactsDB contactDetails = new ContactsDB();
            contactDetails.first_name = "Rakesh";
            contactDetails.last_name = "Sharma";
            //contactDetails.address = "Khar";
            //contactDetails.city = "Mumbai";
            //contactDetails.state = "Maharashtra";
            //contactDetails.zip = 400058;
            //contactDetails.phone = "989879876";
            //contactDetails.email = "rakesh@gmail.com";
            //contactDetails.Date = DateTime.Parse("2019-06-10");
            contactDetails.B_Name = "Book4";
            contactDetails.B_Type = "Family";
            contactDetails.B_ID = "BK4";
            AddressBookRepoDB addressBookRepoDB = new AddressBookRepoDB();
            bool actual = addressBookRepoDB.AddContactDetailsInDB(contactDetails);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddMultipleContactUsingThred()
        {
            ContactsDB contactDetails = new ContactsDB();
            contactDetails.first_name = "Rakesh";
            contactDetails.last_name = "Sharma";
            contactDetails.B_Name = "Book4";
            contactDetails.B_Type = "Family";
            contactDetails.B_ID = "BK4";
            ContactsDB contactsDB = new ContactsDB();
            contactsDB.first_name = "Rahul";
            contactsDB.last_name = "Singh";
            contactsDB.B_Name = "Book5";
            contactsDB.B_Type = "Friends";
            contactsDB.B_ID = "BK5";
            List<ContactsDB> contactList = new List<ContactsDB>();
            contactList.Add(contactDetails);
            contactList.Add(contactsDB);
            AddressBookRepoDB addressBookRepoDB = new AddressBookRepoDB();
            addressBookRepoDB.AddContactDetailsInDBusingThread(contactList);
                    
        }

        }
}
