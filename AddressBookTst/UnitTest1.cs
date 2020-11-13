using AddressBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace AddressBookTst
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;
        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:3000");
        }
        /// <summary>
        /// UC 22: Retrieves the data from json server.
        /// </summary>
        [TestMethod]
        public void RetrieveDataFromJSONServer()
        {
            RestRequest request = new RestRequest("/contacts", Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Contact> contacts = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            foreach (Contact c in contacts)
            {
                Console.WriteLine(c);
            }
            Assert.AreEqual(4, contacts.Count);
        }
        /// <summary>
        /// UC23: Add Contacts To JSONServer
        /// </summary>
        [TestMethod]
        public void AddContactsToJSONServer()
        {
            RestRequest request = new RestRequest("/contacts", Method.POST);

            JObject jObject = new JObject();
            jObject.Add("fname", "Rakesh");
            jObject.Add("lname", "Sharma");
            jObject.Add("address", "Mumbai");
            jObject.Add("city", "Worli");
            jObject.Add("state", "Maharashtra");
            jObject.Add("zip", 400004);
            jObject.Add("phoneNo", 106098752);
            jObject.Add("emailId", "rakesh@gmail.com");

            request.AddParameter("application/json", jObject, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);

            Contact contacts = JsonConvert.DeserializeObject<Contact>(response.Content);

            Assert.AreEqual("Rakesh", contacts.fname);
            Assert.AreEqual("Sharma", contacts.lname);
            Assert.AreEqual("Mumbai", contacts.address);
            Assert.AreEqual("Worli", contacts.city);
            Assert.AreEqual("Maharashtra", contacts.state);
            Assert.AreEqual(400004, contacts.zip);
            Assert.AreEqual(106098752, contacts.phoneNo);
            Assert.AreEqual("rakesh@gmail.com", contacts.emailId);
            Console.WriteLine(response.Content);
        }
        /// <summary>
        /// UC24: Update Contacts From JsonServer
        /// </summary>
        [TestMethod]
        public void UpdateContactsFromJsonServer()
        {
            RestRequest request = new RestRequest("/contacts/5", Method.PUT);

            JObject jObject = new JObject();
            jObject.Add("fname", "Rakesh");
            jObject.Add("lname", "Sharma");
            jObject.Add("address", "Baner");
            jObject.Add("city", "Pune");
            jObject.Add("state", "Maharashtra");
            jObject.Add("zip", 400004);
            jObject.Add("phoneNo", 106098752);
            jObject.Add("emailId", "rakesh@gmail.com");
            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Contact contacts = JsonConvert.DeserializeObject<Contact>(response.Content);
            Assert.AreEqual("Baner", contacts.address);
            Assert.AreEqual("Pune", contacts.city);
            Console.WriteLine(response.Content);

        }
        /// <summary>
        /// UC 25 DeleteContacts
        /// </summary>
        [TestMethod]
        public void DeleteContacts()
        {
            //Arrange
            RestRequest request = new RestRequest("/contacts/5", Method.DELETE);
            //Act
            IRestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

        }

    }
}