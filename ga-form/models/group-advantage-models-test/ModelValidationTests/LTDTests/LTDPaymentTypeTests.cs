﻿using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.LTDTests
{
    [TestClass]
    public class LTDPaymentTypeTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: TAXABLE, NON_TAXABLE, '', null.";

        private const string TAXABLE = "TAXABLE";
        private const string NON_TAXABLE = "NON_TAXABLE";

        [TestMethod]
        public void Invalid_PaymentType_Fails()
        {
            string PAYMENT_TYPE = "asdf";

            ModelValidator.AssertValidatorHasResult(new LongTermDisability()
            {
                paymentType = PAYMENT_TYPE
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(TAXABLE)]
        [DataRow(NON_TAXABLE)]
        [DataRow("")]
        public void Valid_PaymentType_Passes(string paymentType)
        {
            ModelValidator.AssertValidatorNoResult(new LongTermDisability()
            {
                paymentType = paymentType,
            });
        }
    }
}




