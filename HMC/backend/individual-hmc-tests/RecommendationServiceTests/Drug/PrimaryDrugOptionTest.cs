﻿using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug.DrugRecommendation;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class PrimaryDrugOptionTest
    {
        [TestMethod]
        public void Test_PrimaryDrugOptions_NeedsRH_NoNeedPrescriptionDrugs_NotHasAnyPrescriptionDrugs_NotHasExistingPrescription_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,

                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NeedsRH_NeedPrescriptionDrugs_HasPrescriptionDrugsRarelyOrNever_NotHasExistingPrescription_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = RARELY_OR_NEVER,
                    ExistingPrescription = false

                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NeedsRH_NeedPrescriptionDrugs_HasPrescriptionDrugsRarelyOrNever_HasExistingPrescription_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = RARELY_OR_NEVER,
                    ExistingPrescription = true

                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NeedsRH_NeedPrescriptionDrugs_HasPrescriptionDrugsOneOrTwo_NotHasExistingPrescription_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = ONE_OR_TWO,
                    ExistingPrescription = false

                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NeedsRH_NeedPrescriptionDrugs_HasPrescriptionDrugsOneOrTwo_HasExistingPrescription_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = ONE_OR_TWO,
                    ExistingPrescription = true

                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NeedsRH_NeedPrescriptionDrugs_HasPrescriptionDrugsThreeOrMore_NotHasExistingPrescription_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = THREE_OR_MORE,
                    ExistingPrescription = false

                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]

        public void Test_PrimaryDrugOptions_NeedsRH_NeedPrescriptionDrugs_HasPrescriptionDrugsThreeOrMore_HasExistingPrescription_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = THREE_OR_MORE,
                    ExistingPrescription = true

                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NoNeedRH_NoNeedPrescriptionDrugs_NotHasPrescriptionDrugs_NotHasExistingPrescription_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,

                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NeedRH_NeedPrescriptionDrugs_HasPrescriptionDrugsRarelyOrNever_NotHasExistingPrescription_Returns_BasicDrug()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = RARELY_OR_NEVER,
                    ExistingPrescription = false
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, BASIC_DRUG);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NoNeedRH_NeedPrescriptionDrugs_HasPrescriptionDrugsRarelyOrNever_HasExistingPrescription_Returns_EnhancedDrug()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = RARELY_OR_NEVER,
                    ExistingPrescription = true
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, ENHANCED_DRUG);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NoNeedRH_NeedPrescriptionDrugs_HasPrescriptionDrugsOneOrTwo_NotHasExistingPrescription_Returns_BasicDrug()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = ONE_OR_TWO,
                    ExistingPrescription = false
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, BASIC_DRUG);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NoNeedRH_NeedPrescriptionDrugs_HasPrescriptionDrugsOneOrTwo_HasExistingPrescription_Returns_EnhancedDrug()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = ONE_OR_TWO,
                    ExistingPrescription = true
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, ENHANCED_DRUG);
        }
        [TestMethod]
        public void Test_PrimaryDrugOptions_NoNeedRH_NeedPrescriptionDrugs_HasPrescriptionDrugsThreeOrMore_NotHasExistingPrescription_Returns_BasicDrug()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = THREE_OR_MORE,
                    ExistingPrescription = false
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, BASIC_DRUG);
        }
        [TestMethod]

        public void Test_PrimaryDrugOptions_NoNeedRH_NeedPrescriptionDrugs_HasPrescriptionDrugsThreeOrMore_HasExistingPrescription_Returns_EnhancedDrug()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = THREE_OR_MORE,
                    ExistingPrescription = true
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugOption(quote);

            Assert.AreEqual(result, ENHANCED_DRUG);
        }
    }
}
