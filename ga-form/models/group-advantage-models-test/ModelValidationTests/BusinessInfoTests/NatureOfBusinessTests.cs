﻿using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.BusinessInfoTests
{
    [TestClass]
    public class NatureOfBusinessTests
    {

        private const string ERROR_MESSAGE =
            "Please enter one of the allowable values: SERVICES_MEDICAL_SERVICES, SERVICES_IT_SERVICES, SERVICES_SOCIAL_SERVICES_EDUCATION, SERVICES_SOCIAL_ASSISTANCE, SERVICES_PROFESSIONAL_SERVICES, SERVICES_AUTOMOTIVE_MECHANICAL_SERVICES, SERVICES_RECREATIONAL_SERVICES, SERVICES_OTHER_SERVICES, SERVICES_GENERAL_DENTAL_PRACTICE, SERVICES_SPECIAL_DENTAL_PRACTICE, MANUFACTURING, FINANCIAL_INSURANCE_OFFICES, FINANCIAL_INVESTMENT_FIRMS, FINANCIAL_REAL_ESTATE_OFFICES, FINANCIAL_OTHER_FINANCIAL_SERVICES, CONSTRUCTION_GENERAL_CONTRACTORS, CONSTRUCTION_SPECIAL_TRADE_CONTRACTORS, CONSTRUCTION_ROOFING_COMPANIES, CONSTRUCTION_DEMOLITION, PUBLIC_ADMIN, WHOLESALE_TRADE, RETAIL_FOOD_GENERAL_RETAIL, RETAIL_FOOD_RESTAURANTS, RETAIL_FOOD_AUTOMOTIVE_RETAILERS, RETAIL_FOOD_PHARMACIES, RETAIL_EXPLOSIVES_CHEMICALS, RETAIL_MARIJUANA, TRANSPORT_UTILITY_TRUCKING, TRANSPORT_UTILITY_UTILITIES, TRANSPORT_UTILITY_OILFIELD_COMPANIES, TRANSPORT_UTILITY_OTHER, AGRICULTURE_FARMING_OPERATIONS, AGRICULTURE_AGRICULTURAL_SERVICES, AGRICULTURE_VETERINARY_SERVICES, AGRICULTURE_OTHER, '', null.";

        [DataTestMethod]
        [DataRow("SERVICES_MEDICAL_SERVICES")]
        [DataRow("SERVICES_IT_SERVICES")]
        [DataRow("SERVICES_SOCIAL_SERVICES_EDUCATION")]
        [DataRow("SERVICES_SOCIAL_ASSISTANCE")]
        [DataRow("SERVICES_PROFESSIONAL_SERVICES")]
        [DataRow("SERVICES_AUTOMOTIVE_MECHANICAL_SERVICES")]
        [DataRow("SERVICES_RECREATIONAL_SERVICES")]
        [DataRow("SERVICES_OTHER_SERVICES")]
        [DataRow("SERVICES_GENERAL_DENTAL_PRACTICE")]
        [DataRow("SERVICES_SPECIAL_DENTAL_PRACTICE")]
        [DataRow("MANUFACTURING")]
        [DataRow("FINANCIAL_INSURANCE_OFFICES")]
        [DataRow("FINANCIAL_INVESTMENT_FIRMS")]
        [DataRow("FINANCIAL_REAL_ESTATE_OFFICES")]
        [DataRow("FINANCIAL_OTHER_FINANCIAL_SERVICES")]
        [DataRow("CONSTRUCTION_GENERAL_CONTRACTORS")]
        [DataRow("CONSTRUCTION_SPECIAL_TRADE_CONTRACTORS")]
        [DataRow("CONSTRUCTION_ROOFING_COMPANIES")]
        [DataRow("CONSTRUCTION_DEMOLITION")]
        [DataRow("PUBLIC_ADMIN")]
        [DataRow("WHOLESALE_TRADE")]
        [DataRow("RETAIL_FOOD_GENERAL_RETAIL")]
        [DataRow("RETAIL_FOOD_RESTAURANTS")]
        [DataRow("RETAIL_FOOD_AUTOMOTIVE_RETAILERS")]
        [DataRow("RETAIL_FOOD_PHARMACIES")]
        [DataRow("RETAIL_EXPLOSIVES_CHEMICALS")]
        [DataRow("RETAIL_MARIJUANA")]
        [DataRow("TRANSPORT_UTILITY_TRUCKING")]
        [DataRow("TRANSPORT_UTILITY_UTILITIES")]
        [DataRow("TRANSPORT_UTILITY_OILFIELD_COMPANIES")]
        [DataRow("TRANSPORT_UTILITY_OTHER")]
        [DataRow("AGRICULTURE_FARMING_OPERATIONS")]
        [DataRow("AGRICULTURE_AGRICULTURAL_SERVICES")]
        [DataRow("AGRICULTURE_VETERINARY_SERVICES")]
        [DataRow("AGRICULTURE_OTHER")]
        [DataRow("")]
        [DataRow(null)]
        public void Valid_NatureOfBusiness_Passes(string natureOfBusiness)
        {
            ModelValidator.AssertValidatorNoResult(
                new BusinessInfo()
                {
                    natureOfBusiness = natureOfBusiness
                });
        }

        [DataTestMethod]
        [DataRow("asdf")]
        public void Invalid_NatureOfBusiness_Fails(string natureOfBusiness)
        {
            ModelValidator.AssertValidatorHasResult(
                new BusinessInfo()
                {
                    natureOfBusiness = natureOfBusiness
                },
                ERROR_MESSAGE);
        }

    }
}