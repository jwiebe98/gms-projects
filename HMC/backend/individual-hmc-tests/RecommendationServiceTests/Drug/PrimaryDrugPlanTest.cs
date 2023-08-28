using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug.DrugRecommendation;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class PrimaryDrugPlanTest
    {
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_NotHasExistingPresription_HasRarelyOrNeverDrugPresriptionReturns_Choice()
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
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, CHOICE);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_HasExistingPrescription_Returns_Choice()
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
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, CHOICE);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_HasNoExistingPrescription_Returns_Choice()
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
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, CHOICE);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_HasExistingPrescription_HasOneOrTwoDrugs_Returns_Choice()
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
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, CHOICE);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_NotHasExistingPrescription_HasThreeOrMoreDrugs_Returns_Premier()
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
                    NumberOfDrugPrescriptions = THREE_PLUS,
                    ExistingPrescription = false
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, PREMIER);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_HasExistingPrescription_HasThreeOrMoreDrugs_Returns_Premier()
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
                    NumberOfDrugPrescriptions = THREE_PLUS,
                    ExistingPrescription = true
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, PREMIER);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NoNeedsRH_NoNeedsPrescriptionDrugs_NotHasExistingPrescription_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsRarelyorNever_NotHasExistingPrescription_Returns_Basic()
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
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, BASIC);
        }

        [TestMethod]
        public void Test_PrimaryDrugPlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsRarelyorNever_HasExistingPrescription_Returns_Basic()
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
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsOneOrTwo_NotHasExistingPrescription_Returns_Basic()
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
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsOneOrTwo_HasExistingPrescription_Returns_Basic()
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
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsThreeOrMore_NotHasExistingPrescription_Returns_Basic()
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
                    NumberOfDrugPrescriptions = THREE_PLUS,
                    ExistingPrescription = false
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsThreeOrMore_HasExistingPrescription_Returns_Basic()
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
                    NumberOfDrugPrescriptions = THREE_PLUS,
                    ExistingPrescription = true
                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NoNeedPrescriptionDrugs_NotHasAnyPrescriptionDrugs_NotHasExistingPrescription_Returns_Essential()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,

                }
            };
            var recommendation = new DrugRecommendation();
            var result = recommendation.GetPrimaryDrugPlan(quote);

            Assert.AreEqual(result, ESSENTIAL);
        }
    }
}
