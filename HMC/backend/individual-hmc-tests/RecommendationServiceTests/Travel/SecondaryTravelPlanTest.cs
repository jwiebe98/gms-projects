using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class SecondaryTravelPlanTest
    {
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NoNeedTravelProvinceNotGiven_Returns_Basic()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);
            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationLessThanOneWeek_Returns_Basic()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoWeek_Returns_Basic()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoToFourWeek_Returns_Basic()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoMonths_Returns_Basic()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoPlusMonths_Returns_Basic()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationLessThanOneWeek_Returns_ExtendaPlanSKOptionTwo()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoWeek_Returns_ExtendaPlanSKOptionTwo()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoToFourWeek_Returns_ExtendaPlanSKOptionTwo()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoMonths_Returns_ExtendaPlanSKPlus()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_PLUS);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoPlusMonths_Returns_ExtendaPlanSKPlus()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN_SK_PLUS);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NotNeedTravel_NoProvinceGiven_NoNeedTravelDuration_Returns_Basic()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationLessThanOneWeek_Returns_ExtendaPlan()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);


            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationOneToTwoWeek_Returns_ExtendaPlan()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationTwoToFourWeek_Returns_ExtendaPlan()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationOneToTwoMonths_Returns_ExtendaPlan()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationTwoPlusMonths_Returns_ExtendaPlan()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationLessThanOneWeek_Returns_ExtendaPlanSKOptionTwo()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationOneToTwoWeek_Returns_ExtendaPlanSKOptionTwo()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationTwoToFourWeek_Returns_ExtendaPlanSKOptionTwo()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationOneToTwoMonths_Returns_ExtendaPlanSKPlus()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_PLUS);
        }
        [TestMethod]
        public void Test_SecondaryTravelPlan_NotNeedsRH_NeedTravel_NotProvinceSK_NeedTravelDurationTwoPlusMonths_Returns_ExtendaPlanSKPlus()
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
            var result = recommendation.GetSecondaryTravelPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_PLUS);
        }


    }
}
