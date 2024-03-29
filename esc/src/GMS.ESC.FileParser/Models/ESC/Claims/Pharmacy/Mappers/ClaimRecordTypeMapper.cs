﻿using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers
{
    public static class ClaimRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<ClaimRecord> GetClaimRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new ClaimRecord());
            mapper.Property(x => x.RecordType, 1);
            mapper.Property(x => x.VCSAssignedClaimReferenceNumber, 10);
            mapper.Property(x => x.PharmacyTraceNumber, 6);
            mapper.Property(x => x.DateClaimReceivedatESICanada, 8);
            mapper.Property(x => x.DateProcessedAdjudicated, 8);
            mapper.Property(x => x.ClaimStatus, 1);
            mapper.Property(x => x.CommunicationsFlag, 1);
            mapper.Property(x => x.ClientLanguageFlag, 1);
            mapper.Property(x => x.PharmacyNumber, 10);
            mapper.Property(x => x.PrescriptionNumber, 7);
            mapper.Property(x => x.CurrentRxNumber, 7);
            mapper.Property(x => x.DateOfService, 8);
            mapper.Property(x => x.DINNumber_GPNumber, 8);
            mapper.Property(x => x.DrugDescriptionEnglish, 30);
            mapper.Property(x => x.DrugDescriptionFrench, 30);
            mapper.Property(x => x.NewRefillCodeCPhAV2, 2);
            mapper.Property(x => x.MajorCompoundIngredient, 8);
            mapper.Property(x => x.MetricQuantity, 5);
            mapper.Property(x => x.DaysSupply, 3);
            mapper.Property(x => x.ClientLastName, 15);
            mapper.Property(x => x.ClientFirstName, 12);
            mapper.Property(x => x.PatientLastName, 15);
            mapper.Property(x => x.PatientFirstName, 12);
            mapper.Property(x => x.PatientDateOfBirth, 8);
            mapper.Property(x => x.SexOfPatient, 1);
            mapper.Property(x => x.CustomerIDMajorAccountID, 2);
            mapper.Property(x => x.GSAS, 19);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.PatientCode, 3);
            mapper.Property(x => x.PatientRelationshipCode, 1);
            mapper.Property(x => x.ProductSelectionCode, 1);
            mapper.Property(x => x.SubmittedIngredientCost, 6);
            mapper.Property(x => x.SubmittedCostUpCharge, 6);
            mapper.Property(x => x.SubmittedProvincialTax, 6);
            mapper.Property(x => x.SubmittedGST, 6);
            mapper.Property(x => x.SubmittedProfessionalFee, 6);
            mapper.Property(x => x.SubmittedGenericIncentive, 6);
            mapper.Property(x => x.SubmittedSpecialServicesFee, 6);
            mapper.Property(x => x.SubmittedCompoundingFee, 6);
            mapper.Property(x => x.SubmittedCopay, 6);
            mapper.Property(x => x.SubmittedCoinsurance, 6);
            mapper.Property(x => x.SubmittedTotalAmountClaimed, 6);
            mapper.Property(x => x.PayableIngredientCost, 6);
            mapper.Property(x => x.PayableCostUpCharge, 6);
            mapper.Property(x => x.PayableProvincialSalesTax, 6);
            mapper.Property(x => x.PayableGSTTax, 6);
            mapper.Property(x => x.PayableProfessionalFee, 6);
            mapper.Property(x => x.PayableGenericIncentive, 6);
            mapper.Property(x => x.PayableSpecialServicesFee, 6);
            mapper.Property(x => x.PayableCompoundingFee, 6);
            mapper.Property(x => x.PayableCopayAmount, 6);
            mapper.Property(x => x.PayableCoinsurance, 6);
            mapper.Property(x => x.PayableTotalAmount, 6);
            mapper.Property(x => x.AmountTowardCostPlus, 6);
            mapper.Property(x => x.AmountTowardAnnualDeductibleFamily, 6);
            mapper.Property(x => x.AmountTowardAnnualDeductibleIndividualFamily, 6);
            mapper.Property(x => x.AmountTowardAnnualDeductibleSingle, 6);
            mapper.Property(x => x.AnnualDeductibleAmountSatisfiedFamily, 6);
            mapper.Property(x => x.AnnualDeductibleAmountSatisfiedIndividualFamily, 6);
            mapper.Property(x => x.AnnualDeductibleAmountSatisfiedSingle, 6);
            mapper.Property(x => x.TestIndicator, 1);
            mapper.Property(x => x.ErrorCodes, 12);
            mapper.Property(x => x.CostBasis, 2);
            mapper.Property(x => x.UnitPrice, 9);
            mapper.Property(x => x.MaximumAllowableCost, 9);
            mapper.Property(x => x.CostDifference, 6);
            mapper.Property(x => x.TherapeuticClass, 6);
            mapper.Property(x => x.FederalSchedule, 1);
            mapper.Property(x => x.ProvincialSchedule, 2);
            mapper.Property(x => x.DosageForm, 2);
            mapper.Property(x => x.RouteOfAdministration, 1);
            mapper.Property(x => x.SubmissionMethodCode, 1);
            mapper.Property(x => x.PayeeCode, 1);
            mapper.Property(x => x.PaymentMethod, 1);
            mapper.Property(x => x.AuthorizationCode, 7);
            mapper.Property(x => x.AuthorizationNumber, 6);
            mapper.Property(x => x.EFTNumber, 19);
            mapper.Property(x => x.DeductibleSatisfiedFlag, 1);
            mapper.Property(x => x.NextRolloverDate, 8);
            mapper.Property(x => x.PaymentStatus, 1);
            mapper.Property(x => x.OriginalClaimReferenceNumber, 10);
            mapper.Property(x => x.OriginalClaimTraceNumber, 6);
            mapper.Property(x => x.ClientLocation, 6);
            mapper.Property(x => x.ReimbursementFlag, 1);
            mapper.Property(x => x.PrescriberNumber, 10);
            mapper.Property(x => x.ProviderCode, 2);
            mapper.Property(x => x.ProviderZone, 2);
            mapper.Property(x => x.RefillsAuthorizedCPhAV3, 2);
            mapper.Property(x => x.DINProductName, 10);
            mapper.Property(x => x.RefillRepeatAuthorizations, 2);
            mapper.Property(x => x.ProvincialHealthCareID, 13);
            mapper.Property(x => x.UnlistedCompound, 1);
            mapper.Property(x => x.InterventionCodes, 4);
            mapper.Property(x => x.PreviouslyPaid, 6);
            mapper.Property(x => x.PharmacistID, 6);
            mapper.Property(x => x.CPhAVersionNumber, 2);
            mapper.Property(x => x.ESIEclipseCode, 2);
            mapper.Property(x => x.AQPPCode, 3);
            mapper.Property(x => x.Filler1, 1);
            mapper.Property(x => x.OriginalRxNumberCPhAV3, 9);
            mapper.Property(x => x.CurrentRxNumberCPhAV3, 9);
            mapper.Property(x => x.NewRefillCodeCPhAV3, 1);
            mapper.Property(x => x.MetricQuantityClaimedCPhAV3, 6);
            mapper.Property(x => x.MetricQuantityPaidCPhAV3, 6);
            mapper.Property(x => x.MedicalReasonReference, 1);
            mapper.Property(x => x.MedicalCondition, 6);
            mapper.Property(x => x.ProviderSoftwareID, 3);
            mapper.Property(x => x.POSDeviceID, 8);
            mapper.Property(x => x.PrescriberIDReference, 2);
            mapper.Property(x => x.CPhAResponseCodes, 10);
            mapper.Property(x => x.Dosage, 8);
            mapper.Property(x => x.FormularyDrugIndicator, 1);
            mapper.Property(x => x.AttachmentCode, 3);
            mapper.Property(x => x.DiseaseCode, 6);
            mapper.Property(x => x.COBRuleNumber, 1);
            mapper.Property(x => x.GeneralCode, 1);
            mapper.Property(x => x.GenProdIndicator, 1);
            mapper.Property(x => x.PrescriberID, 15);
            mapper.Property(x => x.PrescriberReferenceCode, 2);
            mapper.Property(x => x.DeductPaid, 6);
            mapper.Property(x => x.AccumID, 5);
            mapper.Property(x => x.Deferred, 1);
            mapper.Property(x => x.AlternateIdentification, 16);
            mapper.Property(x => x.LineOfBusiness, 3);
            mapper.Property(x => x.Filler2, 3);
            return mapper;
        }
    }
}
