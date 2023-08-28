using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class PrimaryTravelPlanTest
    {
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NoNeedTravel_Returns_Essential()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,

                },
                Applicant = new()
                {
                    Province = "foo"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);

            Assert.AreEqual(result, ESSENTIAL);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationLessThanOneWeek_Returns_Choice()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = LESS_THAN_ONE_WEEK

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, CHOICE);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoWeek_Returns_Premier()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_WEEKS

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, PREMIER);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoToFourWeek_Returns_RHBasic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_TO_FOUR_WEEKS

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, RH_BASIC);
        }
        [TestMethod]

        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoMonths_Returns_RHBasic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_MONTHS

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, RH_BASIC);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoPlusMonths_Returns_RHBasic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_PLUS_MONTHS

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, RH_BASIC);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationLessThanOneWeek_Returns_Choice()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = LESS_THAN_ONE_WEEK

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, CHOICE);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoWeeks_Returns_Premier()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_WEEKS

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, PREMIER);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoToFourWeeks_Returns_ExtendaPlanSKOption2()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_TO_FOUR_WEEKS

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoMonths_Returns_ExtendaPlanSKPlus()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_MONTHS

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN_SK_PLUS);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoPlusMonths_Returns_ExtendaPlanSKPlus()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_PLUS_MONTHS

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_PLUS);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NoNeedTravel_ProvinceNotGiven_NoNeedTravelDurationTwoPlusMonths_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                }, 
                Applicant = new()
                {
                    Province = "foo"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationLessThanOneWeek_Returns_ExtendaPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = LESS_THAN_ONE_WEEK
                },
                Applicant = new()
                {
                    Province = "ON"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoWeeks_Returns_ExtendaPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_WEEKS
                },
                Applicant = new()
                {
                    Province = "ON"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoToFourWeeks_Returns_ExtendaPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_TO_FOUR_WEEKS
                },
                Applicant = new()
                {
                    Province = "ON"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoMonths_Returns_ExtendaPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_MONTHS
                },
                Applicant = new()
                {
                    Province = "ON"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoPlusMonths_Returns_ExtendaPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_PLUS_MONTHS
                },
                Applicant = new()
                {
                    Province = "ON"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationLessThanOneWeek_Returns_ExtendaPlanSKOption2()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = LESS_THAN_ONE_WEEK
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoWeek_Returns_ExtendaPlanSKOption2()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_WEEKS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoToFourWeek_Returns_ExtendaPlanSKOption2()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_TO_FOUR_WEEKS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoMonths_Returns_ExtendaPlanSKPlus()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_MONTHS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN_SK_PLUS);
        }
        [TestMethod]
        public void Test_PrimaryTravelPlan_NoNeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoPlusMonths_Returns_ExtendaPlanSKPlus()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_PLUS_MONTHS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new TravelRecommendation();
            var result = recommendation.GetPrimaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN_SK_PLUS);
        }












    }
}
