using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace AddressBook
{
    public class Contact
    {

        public string fname { get; set; }
        public string lname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public long zip { get; set; }
        public long phoneNo { get; set; }
        public string emailId { get; set; }
        public readonly int n;

        public Contact(string fname, string lname, string address, string city, string state, long zip, long phoneNo, string emailId)
        {
            this.fname = fname;
            this.lname = lname;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phoneNo = phoneNo;
            this.emailId = emailId;
        }
        public Contact()
        {

        }
        public override string ToString()
        {
            return "First Name: " + fname + ", " + "Last Name: " + lname + ", " + "Address: " + address + ", " + "City: " + city + ", " + "State: " + state + ", " + "Zip: " + zip + ", Phone Number: " + phoneNo + ", Email-id: " + emailId;
        }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Contact p = (Contact)obj;
                return (fname == p.fname) && (lname == p.lname);
            }
        }
        public override int GetHashCode()
        {
            return n;
        }
        public void SortByCity(List<Contact> contacts)
        {
            contacts.Sort((contact1, contact2) => contact1.city.CompareTo(contact2.city));
        }
        public void SortByState(List<Contact> contacts)
        {
            contacts.Sort((contact1, contact2) => contact1.state.CompareTo(contact2.state));
        }
        public void SortByZip(List<Contact> contacts)
        {
            contacts.Sort((contact1, contact2) => contact1.zip.CompareTo(contact2.zip));
        }
    }
}
