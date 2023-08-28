using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers
{
    public static class ClaimTypeMapper
    {
        public static IFixedLengthTypeMapper<Claim> GetClaimTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new Claim());
            mapper.Property(x => x.SubmittedLineNumber, 6);
            mapper.Property(x => x.PaidLineNumber, 2);
            mapper.Property(x => x.ServiceStatus, 1);
            mapper.Property(x => x.AdjudicationRuleNumberApplied, 5);
            mapper.Property(x => x.FrequencyRuleNumberApplied, 5);
            mapper.Property(x => x.DateOfService, 8);
            mapper.Property(x => x.ServiceCode, 5);
            mapper.Property(x => x.Filler4, 20);
            mapper.Property(x => x.ServiceNameEnglish, 35);
            mapper.Property(x => x.ServiceNameFrench, 35);
            mapper.Property(x => x.ToothCode, 2);
            mapper.Property(x => x.ToothSurface, 5);
            mapper.Property(x => x.ProfessionalFeeClaimed, 6);
            mapper.Property(x => x.PreviouslyPaidAmount, 6);
            mapper.Property(x => x.ProfessionalFeeEligibleAmount, 6);
            mapper.Property(x => x.DeductibleAmountProfessionalFee, 6);
            mapper.Property(x => x.ProfessionalFeeBenefitAmount, 6);
            mapper.Property(x => x.LabProcedureCode, 5);
            mapper.Property(x => x.LabFeeClaimed, 6);
            mapper.Property(x => x.EligibleAmountLab, 6);
            mapper.Property(x => x.LabDeductibleAmount, 6);
            mapper.Property(x => x.LabBenefitAmount, 6);
            mapper.Property(x => x.ExpenseProcedureCode, 5);
            mapper.Property(x => x.ExpenseClaimed, 6);
            mapper.Property(x => x.ExpenseEligibleAmount, 6);
            mapper.Property(x => x.ExpenseDeductibleAmount, 6);
            mapper.Property(x => x.ExpenseBenefitAmount, 6);
            mapper.Property(x => x.ErrorCode, 12);
            mapper.Property(x => x.ESIMessages2, 12);
            mapper.Property(x => x.TotalFeesClaimed, 7);
            mapper.Property(x => x.CoinsuranceAmount, 6);
            mapper.Property(x => x.CoinsurancePercentage, 3);
            mapper.Property(x => x.TotalFeesPaid, 6);
            mapper.Property(x => x.PaidServiceCode1, 5);
            mapper.Property(x => x.PaidServiceCode2, 5);
            mapper.Property(x => x.Filler5, 3);
            mapper.Property(x => x.PlanNumber, 5);
            mapper.Property(x => x.BenefitCode, 5);
            mapper.Property(x => x.CategoryCode, 2);
            mapper.Property(x => x.CategoryLabelEnglish, 40);
            mapper.Property(x => x.CategoryLabelFrench, 40);
            mapper.Property(x => x.CoverageCodeFromEligibility, 2);
            mapper.Property(x => x.CarrierEHCField, 10);
            mapper.Property(x => x.ServiceCodeSource, 4);
            mapper.Property(x => x.MaximumCutbackAmount, 6);
            mapper.Property(x => x.RuleCutbackAmount, 6);
            mapper.Property(x => x.FeeGuideAmount, 6);
            mapper.Property(x => x.Filler6, 50);

            return mapper;
        }
    }
}
