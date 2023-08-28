using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;


namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class SecondaryTravelOptionTest
    {
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedsTravel_ProvinceNotGiven_NotNeedTravelDuration_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationLessThanOneWeek_Returns_UptoFifteenDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_15_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoWeek_Returns_UptoFifteenDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_15_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoWeek_Returns_UptoThirtyDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_30_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoMonths_Returns_UptoFortyEightDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_48_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoPlusMonths_Returns_UptoFortyEightDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_48_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoPlusMonths_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoWeek_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoToFourWeek_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoMonths_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoMonthsPlus_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NotNeedTravel_ProvinceNotGiven_NoNeedTravelDuration_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceNotSK_NeedTravelDurationLessThanOneWeek_Returns_UptoFifteenDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_15_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoWeeks_Returns_UptoFifteenDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_15_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoWeeks_Returns_UptoThirtyDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_30_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoMonths_Returns_UptoFortyEightDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_48_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceNotSK_NeedTravelDurationTwoPlusMonths_Returns_UptoFortyEightDays()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, TRAVEL_UP_TO_48_DAYS);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoPlusMonths_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoWeek_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoToFourWeek_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoMonths_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryTravelOption_NotNeedRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoMonthsPlus_Returns_None()
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
            var result = recommendation.GetSecondaryTravelOption(quote);

            Assert.AreEqual(result, NONE);
        }
    }
}
