using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass]
    public class RelatedResourcesTest
    {
        private Details GetDetails()
        {
            Details detail = new Details();
            detail.tax = "15";
            detail.fee = "2";
            detail.shipping = "10";
            detail.subtotal = "75";
            return detail;
        }

        private Amount GetAmount()
        {
            Amount amnt = new Amount();
            amnt.currency = "USD";
            amnt.details = GetDetails();
            amnt.total = "100";
            return amnt;
        }

        private List<Links> GetLinksList()
        {
            Links lnk = new Links();
            lnk.href = "http://www.paypal.com";
            lnk.method = "POST";
            lnk.rel = "authorize";
            List<Links> lnks = new List<Links>();
            lnks.Add(lnk);
            return lnks;
        }

        private Capture GetCapture()
        {
            Capture cap = new Capture();
            cap.amount = GetAmount();
            cap.create_time = "2013-01-15T15:10:05.123Z";
            cap.state = "Authorized";
            cap.parent_payment = "1000";
            cap.links = GetLinksList();
            cap.id = "001";
            return cap;
        }

        private Authorization GetAuthorization()
        {
            Authorization author = new Authorization();
            author.amount = GetAmount();
            author.create_time = "2013-01-15T15:10:05.123Z";
            author.id = "007";
            author.parent_payment = "1000";
            author.state = "Authorized";
            author.links = GetLinksList();
            return author;
        }

        private Links CreateLinks()
        {
            Links link = new Links();
            link.href = "http://paypal.com/";
            link.method = "GET";
            link.rel = "authorize";
            return link;
        }

        public Refund CreateRefund()
        {
            List<Links> links = new List<Links>();
            links.Add(CreateLinks());
            Refund refund = new Refund();
            refund.capture_id = "101";
            refund.id = "102";
            refund.parent_payment = "103";
            refund.sale_id = "104";
            refund.state = "Approved";
            refund.amount = GetAmount();
            refund.create_time = "2013-01-17T18:12:02.347Z";
            refund.links = links;
            return refund;
        }

        public Sale CreateSale()
        {
            List<Links> links = new List<Links>();
            links.Add(CreateLinks());
            Sale sale = new Sale();
            sale.amount = GetAmount();
            sale.id = "102";
            sale.parent_payment = "103";
            sale.state = "Approved";
            sale.create_time = "2013-01-17T18:12:02.347Z";
            sale.links = links;
            return sale;
        }

        private RelatedResources CreateRelatedResources()
        {
		    RelatedResources resources = new RelatedResources();
            resources.authorization = GetAuthorization();
            resources.capture = GetCapture();
            resources.refund = CreateRefund();
            resources.sale = CreateSale();
            return resources;
	    }

	    [TestMethod()]
        public void TestRelatedResources() 
        {
            RelatedResources resources = CreateRelatedResources();
            Assert.AreEqual(resources.authorization.id, GetAuthorization().id);
            Assert.AreEqual(resources.sale.id, CreateSale().id);
            Assert.AreEqual(resources.refund.id, CreateRefund().id);
            Assert.AreEqual(resources.capture.id, GetCapture().id);
	    }

	    [TestMethod()]
        public void ConvertToJsonTest() 
        {
		    RelatedResources resources = CreateRelatedResources();
            Assert.IsFalse(resources.ConvertToJson().Length == 0);
	    }

        [TestMethod()]
        public void ToStringTest() 
        {
            RelatedResources resources = CreateRelatedResources();
            Assert.IsFalse(resources.ToString().Length == 0);
	    }    
    }
}
