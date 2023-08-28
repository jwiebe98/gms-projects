using GMS.WTP.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;

namespace GMS.WTP.DataImport
{
    public static class SqlServer
    {
        public static void InsertIntoClaimBordereauTable(this ClaimBordereau claimBordereau, ILogger log)
        {
            using SqlConnection connection = GetConnectionToCIMSDatabase(log);

            string insertQuery =
                @"INSERT INTO [CIMSSystem].[dbo].[WTPClaimsBordereau]
                       ([Period],
                       [Product],
                       [Plan],
                       [Cover Cause],
                       [Claim Number],
                       [Date of Loss],
                       [Date Reported],
                       [Policy Number],
                       [Personal ID],
                       [Departure Date],
                       [Return Date],
                       [Description],
                       [Claimant Age],
                       [Claimant Province],
                       [Claimant],
                       [Date of Birth],
                       [Sex],
                       [Actual Paid Movement],
                       [Actual Received Movement],
                       [Estimated Paid Movement],
                       [Estimated Received Movement],
                       [Current Status],
                       [Loss Location],
                       [Timestamp],
                       [FileName],
                       [RowNumber],
                       [ESBMessageID])
                 VALUES
                       (@Period,
                       @Product,
                       @Plan,
                       @CoverCause,
                       @ClaimNumber,
                       @DateofLoss,
                       @DateReported,
                       @PolicyNumber,
                       @PersonalID,
                       @DepartureDate,
                       @ReturnDate,
                       @Description,
                       @ClaimantAge,
                       @ClaimantProvince,
                       @Claimant,
                       @DateofBirth,
                       @Sex,
                       @ActualPaidMovement,
                       @ActualReceivedMovement,
                       @EstimatedPaidMovement,
                       @EstimatedReceivedMovement,
                       @CurrentStatus,
                       @LossLocation,
                       @Timestamp,
                       @FileName,
                       @RowNumber,
                       @ESBMessageID)";

            log.LogInformation($"Building SQL insert statement");
            using SqlCommand command = new(insertQuery, connection);

            command.Parameters.AddWithValue("@Period", claimBordereau.Period);
            command.Parameters.AddWithValue("@Product", claimBordereau.Product);
            command.Parameters.AddWithValue("@Plan", claimBordereau.Plan);
            command.Parameters.AddWithValue("@CoverCause", claimBordereau.CoverCause);
            command.Parameters.AddWithValue("@ClaimNumber", claimBordereau.ClaimNumber);
            command.Parameters.AddWithValue("@DateofLoss", claimBordereau.DateOfLoss);
            command.Parameters.AddWithValue("@DateReported", claimBordereau.DateReported);
            command.Parameters.AddWithValue("@PolicyNumber", claimBordereau.PolicyNumber);
            command.Parameters.AddWithValue("@PersonalID", claimBordereau.PersonalID);
            command.Parameters.AddWithValue("@DepartureDate", claimBordereau.DepartureDate);
            command.Parameters.AddWithValue("@ReturnDate", claimBordereau.ReturnDate);
            command.Parameters.AddWithValue("@Description", claimBordereau.Description);
            command.Parameters.AddWithValue("@ClaimantAge", claimBordereau.ClaimantAge);
            command.Parameters.AddWithValue("@ClaimantProvince", claimBordereau.ClaimantProvince);
            command.Parameters.AddWithValue("@Claimant", claimBordereau.Claimant);
            command.Parameters.AddWithValue("@DateofBirth", claimBordereau.DateOfBirth);
            command.Parameters.AddWithValue("@Sex", claimBordereau.Sex);
            command.Parameters.AddWithValue("@ActualPaidMovement", claimBordereau.ActualPaidMovement);
            command.Parameters.AddWithValue("@ActualReceivedMovement", claimBordereau.ActualReceivedMovement);
            command.Parameters.AddWithValue("@EstimatedPaidMovement", claimBordereau.EstimatedPaidMovement);
            command.Parameters.AddWithValue("@EstimatedReceivedMovement", claimBordereau.EstimatedReceivedMovement);
            command.Parameters.AddWithValue("@CurrentStatus", claimBordereau.CurrentStatus);
            command.Parameters.AddWithValue("@LossLocation", claimBordereau.LossLocation);
            command.Parameters.AddWithValue("@Timestamp", claimBordereau.Timestamp);
            command.Parameters.AddWithValue("@FileName", claimBordereau.FileName);
            command.Parameters.AddWithValue("@RowNumber", claimBordereau.RowNumber);
            command.Parameters.AddWithValue("@ESBMessageID", claimBordereau.ESBMessageID);

            RunSQLCommand(log, connection, command);
        }

        public static void InsertIntoPaymentReportTable(this PaymentReport paymentReport, ILogger log)
        {
            using SqlConnection connection = GetConnectionToCIMSDatabase(log);

            string insertQuery =
                @"INSERT INTO [CIMSSystem].[dbo].[WTPPaymentReport]
                       ([Cheque],
                       [Payment ID],
                       [Claim Number],
                       [Personal ID],
                       [Claim Type],
                       [ICD10],
                       [Payee],
                       [Address 1],
                       [Address 2],
                       [City],
                       [Postal Code],
                       [Province],
                       [Country],
                       [Lost Date],
                       [Payment Date],
                       [CAD Amount],
                       [USD Amount],
                       [USD to CAD],
                       [Total],
                       [Timestamp],
                       [FileName],
                       [RowNumber],
                       [ESBMessageID])
                 VALUES
                       (@Cheque,
                       @PaymentID,
                       @ClaimNumber,
                       @PersonalID,
                       @ClaimType,
                       @ICD10,
                       @Payee,
                       @Address1,
                       @Address2,
                       @City,
                       @PostalCode,
                       @Province,
                       @Country,
                       @LostDate,
                       @PaymentDate,
                       @CADAmount,
                       @USDAmount,
                       @USDtoCAD,
                       @Total,
                       @Timestamp,
                       @FileName,
                       @RowNumber,
                       @ESBMessageID)";

            log.LogInformation($"Building SQL insert statement");
            using SqlCommand command = new(insertQuery, connection);

            command.Parameters.AddWithValue("@Cheque", paymentReport.ChequeNumber);
            command.Parameters.AddWithValue("@PaymentID", paymentReport.PaymentID);
            command.Parameters.AddWithValue("@ClaimNumber", paymentReport.ClaimNumber);
            command.Parameters.AddWithValue("@PersonalID", paymentReport.PersonalID);
            command.Parameters.AddWithValue("@ClaimType", paymentReport.ClaimType);
            command.Parameters.AddWithValue("@ICD10", paymentReport.ICD10);
            command.Parameters.AddWithValue("@Payee", paymentReport.Payee);
            command.Parameters.AddWithValue("@Address1", paymentReport.Address1);
            command.Parameters.AddWithValue("@Address2", paymentReport.Address2);
            command.Parameters.AddWithValue("@City", paymentReport.City);
            command.Parameters.AddWithValue("@PostalCode", paymentReport.PostalCode);
            command.Parameters.AddWithValue("@Province", paymentReport.Province);
            command.Parameters.AddWithValue("@Country", paymentReport.Country);
            command.Parameters.AddWithValue("@LostDate", paymentReport.LossDate);
            command.Parameters.AddWithValue("@PaymentDate", paymentReport.PaymentDate);
            command.Parameters.AddWithValue("@CADAmount", paymentReport.CADAmount);
            command.Parameters.AddWithValue("@USDAmount", paymentReport.USDAmount);
            command.Parameters.AddWithValue("@USDtoCAD", paymentReport.USDtoCAD);
            command.Parameters.AddWithValue("@Total", paymentReport.TotalCADAmount);
            command.Parameters.AddWithValue("@Timestamp", paymentReport.Timestamp);
            command.Parameters.AddWithValue("@FileName", paymentReport.FileName);
            command.Parameters.AddWithValue("@RowNumber", paymentReport.RowNumber);
            command.Parameters.AddWithValue("@ESBMessageID", paymentReport.ESBMessageID);

            RunSQLCommand(log, connection, command);
        }

        public static void InsertIntoSavingsReportDetailTable(this SavingsReportDetail savingsReportDetail, ILogger log)
        {
            using SqlConnection connection = GetConnectionToCIMSDatabase(log);

            string insertQuery =
                @"INSERT INTO [CIMSSystem].[dbo].[WTPSavingsReportDetail]
                       ([Claim Number],
                       [Gender],
                       [Considered Charges],
                       [Contracted Savings],
                       [Scrubbed Savings],
                       [Paid Amount],
                       [Gross Savings Perc],
                       [PPO Fees],
                       [Total Paid Amount],
                       [Net Savings],
                       [Net Savings Perc],
                       [Gross Savings YTD],
                       [Gross Savings YTD Perc],
                       [Net Savings YTD],
                       [Net Savings YTD Perc],
                       [Recoveries YTD],
                       [Timestamp],
                       [FileName],
                       [RowNumber],
                       [ESBMessageID])
                 VALUES
                       (@ClaimNumber,
                       @Gender,
                       @ConsideredCharges,
                       @ContractedSavings,
                       @ScrubbedSavings,
                       @PaidAmount,
                       @GrossSavingsPerc,
                       @PPOFees,
                       @TotalPaidAmount,
                       @NetSavings,
                       @NetSavingsPerc,
                       @GrossSavingsYTD,
                       @GrossSavingsYTDPerc,
                       @NetSavingsYTD,
                       @NetSavingsYTDPerc,
                       @RecoveriesYTD,
                       @Timestamp,
                       @FileName,
                       @RowNumber,
                       @ESBMessageID)";

            log.LogInformation($"Building SQL insert statement");
            using SqlCommand command = new(insertQuery, connection);

            command.Parameters.AddWithValue("@ClaimNumber", savingsReportDetail.ClaimNumber);
            command.Parameters.AddWithValue("@Gender", savingsReportDetail.Gender);
            command.Parameters.AddWithValue("@ConsideredCharges", savingsReportDetail.ConsideredCharges);
            command.Parameters.AddWithValue("@ContractedSavings", savingsReportDetail.ContractedSavings);
            command.Parameters.AddWithValue("@ScrubbedSavings", savingsReportDetail.ScrubbedSavings);
            command.Parameters.AddWithValue("@PaidAmount", savingsReportDetail.PaidAmount);
            command.Parameters.AddWithValue("@GrossSavingsPerc", savingsReportDetail.GrossSavingsPercent);
            command.Parameters.AddWithValue("@PPOFees", savingsReportDetail.PPOFees);
            command.Parameters.AddWithValue("@TotalPaidAmount", savingsReportDetail.TotalPaidAmount);
            command.Parameters.AddWithValue("@NetSavings", savingsReportDetail.NetSavings);
            command.Parameters.AddWithValue("@NetSavingsPerc", savingsReportDetail.NetSavingsPercent);
            command.Parameters.AddWithValue("@GrossSavingsYTD", savingsReportDetail.GrossSavingsYTD);
            command.Parameters.AddWithValue("@GrossSavingsYTDPerc", savingsReportDetail.GrossSavingsPercentYTD);
            command.Parameters.AddWithValue("@NetSavingsYTD", savingsReportDetail.NetSavingsYTD);
            command.Parameters.AddWithValue("@NetSavingsYTDPerc", savingsReportDetail.NetSavingsPercentYTD);
            command.Parameters.AddWithValue("@RecoveriesYTD", savingsReportDetail.RecoveriesYTD);
            command.Parameters.AddWithValue("@Timestamp", savingsReportDetail.Timestamp);
            command.Parameters.AddWithValue("@FileName", savingsReportDetail.FileName);
            command.Parameters.AddWithValue("@RowNumber", savingsReportDetail.RowNumber);
            command.Parameters.AddWithValue("@ESBMessageID", savingsReportDetail.ESBMessageID);

            RunSQLCommand(log, connection, command);
        }

        public static void InsertIntoSavingsReportSummaryTable(this SavingsReportSummary savingsReportSummary, ILogger log)
        {
            using SqlConnection connection = GetConnectionToCIMSDatabase(log);

            string insertQuery =
                @"INSERT INTO [CIMSSystem].[dbo].[WTPSavingsReportSummary]
                       ([Cost Agent],
                       [Considered Charges],
                       [Contracted Savings],
                       [Scrubbed Savings],
                       [Paid Amount],
                       [Gross Savings Perc],
                       [PPO Fees],
                       [Total Paid Amount],
                       [Net Savings],
                       [Net Savings Perc],
                       [Gross Savings YTD],
                       [Gross Savings YTD Perc],
                       [Net Savings YTD],
                       [Net Savings YTD Perc],
                       [Recoveries YTD],
                       [Timestamp],
                       [FileName],
                       [RowNumber],
                       [ESBMessageID])
                 VALUES
                       (@CostAgent,
                       @ConsideredCharges,
                       @ContractedSavings,
                       @ScrubbedSavings,
                       @PaidAmount,
                       @GrossSavingsPerc,
                       @PPOFees,
                       @TotalPaidAmount,
                       @NetSavings,
                       @NetSavingsPerc,
                       @GrossSavingsYTD,
                       @GrossSavingsYTDPerc,
                       @NetSavingsYTD,
                       @NetSavingsYTDPerc,
                       @RecoveriesYTD,
                       @Timestamp,
                       @FileName,
                       @RowNumber,
                       @ESBMessageID)";

            log.LogInformation($"Building SQL insert statement");
            using SqlCommand command = new(insertQuery, connection);

            command.Parameters.AddWithValue("@CostAgent", savingsReportSummary.CostAgent);
            command.Parameters.AddWithValue("@ConsideredCharges", savingsReportSummary.ConsideredCharges);
            command.Parameters.AddWithValue("@ContractedSavings", savingsReportSummary.ContractedSavings);
            command.Parameters.AddWithValue("@ScrubbedSavings", savingsReportSummary.ScrubbedSavings);
            command.Parameters.AddWithValue("@PaidAmount", savingsReportSummary.PaidAmount);
            command.Parameters.AddWithValue("@GrossSavingsPerc", savingsReportSummary.GrossSavingsPercent);
            command.Parameters.AddWithValue("@PPOFees", savingsReportSummary.PPOFees);
            command.Parameters.AddWithValue("@TotalPaidAmount", savingsReportSummary.TotalPaidAmount);
            command.Parameters.AddWithValue("@NetSavings", savingsReportSummary.NetSavings);
            command.Parameters.AddWithValue("@NetSavingsPerc", savingsReportSummary.NetSavingsPercent);
            command.Parameters.AddWithValue("@GrossSavingsYTD", savingsReportSummary.GrossSavingsYTD);
            command.Parameters.AddWithValue("@GrossSavingsYTDPerc", savingsReportSummary.GrossSavingsPercentYTD);
            command.Parameters.AddWithValue("@NetSavingsYTD", savingsReportSummary.NetSavingsYTD);
            command.Parameters.AddWithValue("@NetSavingsYTDPerc", savingsReportSummary.NetSavingsPercentYTD);
            command.Parameters.AddWithValue("@RecoveriesYTD", savingsReportSummary.RecoveriesYTD);
            command.Parameters.AddWithValue("@Timestamp", savingsReportSummary.Timestamp);
            command.Parameters.AddWithValue("@FileName", savingsReportSummary.FileName);
            command.Parameters.AddWithValue("@RowNumber", savingsReportSummary.RowNumber);
            command.Parameters.AddWithValue("@ESBMessageID", savingsReportSummary.ESBMessageID);

            RunSQLCommand(log, connection, command);
        }

        private static SqlConnection GetConnectionToCIMSDatabase(ILogger log)
        {
            log.LogInformation($"Getting Database Connection");
            SqlConnection connection = new(EnvironmentVariables.DB_CIMS);

            log.LogInformation($"Opening connection to CIMS database");
            connection.Open();
            return connection;
        }

        private static void RunSQLCommand(ILogger log, SqlConnection connection, SqlCommand command)
        {
            try
            {
                log.LogInformation($"Executing SQL insert statement");
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
            }


            log.LogInformation($"Closing connection to CIMS database");
            connection.Close();
        }
    }
}
