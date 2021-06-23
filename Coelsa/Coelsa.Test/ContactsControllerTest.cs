using Coelsa.Common.Common;
using Coelsa.Common.Interfaces;
using Coelsa.Common.Models;
using Coelsa.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Coelsa.Test
{
    [TestClass]
    public class ContactsControllerTest
    {
        private ContactsController target;
        private Mock<IContactRepository> contactMock;

        [TestInitialize]
        public void Initialize()
        {
            contactMock = new Mock<IContactRepository>();

            target = new ContactsController(contactMock.Object);
        }


        [TestMethod]
        public void GetOK()
        {
            Contact contact = new Contact();
            contact.IdContact = 1;
            contact.FirstName = "FirstPrueba";
            contact.LastName = "LastPrueba";
            contact.Company = "CompanyPrueba";
            contact.Email = "prueba@test.com";
            Contact contact2 = new Contact();
            contact2.IdContact = 2;
            contact2.FirstName = "FirstPrueba";
            contact2.LastName = "LastPrueba";
            contact2.Company = "CompanyPrueba";
            contact2.Email = "prueba@test.com";

            List<Contact> listaContacts = new List<Contact>();
            listaContacts.Add(contact); listaContacts.Add(contact2);

            PaginationGeneric<Contact> paginator = new PaginationGeneric<Contact>();
            paginator.Result = listaContacts;

            contactMock.Setup(s => s.GetByCompany("CompanyPrueba", 1, 10)).ReturnsAsync(paginator);
            
            var result = target.GetByCompany("CompanyPrueba").Result as ObjectResult;
            var paginatorResult = result.Value as PaginationGeneric<Contact>;

            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(((List<Contact>)paginatorResult.Result).Count, 2);
            Assert.IsNotNull(paginatorResult);
        }

        [TestMethod]
        public void GetException()
        {
            contactMock.Setup(s => s.GetByCompany("CompanyPrueba", 1, 10)).Throws(new Exception());

            var result = target.GetByCompany("CompanyPrueba");
            
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Status.ToString(), "Faulted");            
        }
    }
}
