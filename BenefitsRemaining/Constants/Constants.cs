namespace GMS.CIMS.BenefitsRemaining
{
    public static class Constants
    {
        public const string STORED_PROCEDURE_TO_GET_CLAIMS = "[dbo].[Customer_Claims_BenefitsRemaining]";
        public const string STORED_PROCEDURE_TO_GET_PLAN_PARAMETERS = "[dbo].[Customer_Plans_BenefitsRemaining]";
        public const string COMBINED = "COMBINED";
        public const string PER_PRACTITIONER = "PER_PRACTITIONER";
        public const string POLICY_HOLDER = "POLICY_HOLDER";
        public const int PAID = 2;
        public const string DOLLAR = "DOLLAR";
        public const string COUNT = "COUNT";
        public const string PER_PERSON = "PER_PERSON";
        public const int LIFETIME = 999;
        public const string SIMPLE = "SIMPLE";
        public const string COMPLETE = "COMPLETE";
        public const string DATE_DISPLAY_FORMAT = "dd-MMM-yyyy";
        public const string POLICY_YEAR_PERIOD_DATE_FORMAT = "MMM dd, yyyy";
    }
}
