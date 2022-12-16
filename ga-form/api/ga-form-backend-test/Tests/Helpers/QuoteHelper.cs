using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.TestModels
{
    public class QuoteHelper
    {
        public static Quote SetupBasicQuote()
        {
            var quote = new Quote();
            quote.qualify = SetupQualifyInfo("SK");
            return quote;
        }

        public static Qualify SetupQualifyInfo(string province)
        {
            var qualify = new Qualify();
            qualify.businessInfo = SetupCompanyInfo(province);
            return qualify;
        }

        public static BusinessInfo SetupCompanyInfo(string province)
        {
            var businessInfo = new BusinessInfo();
            businessInfo.province = province;
            return businessInfo;
        }

        public static Employee SetupEmployee(string employeeType, long salary, List<string> benefitsWaived)
        {
            var employee = new Employee();
            employee.type = employeeType;
            employee.benefitsWaived = benefitsWaived;
            employee.salary = salary;

            return employee;
        }

        public static List<Employee> CreateListOfEmployees(int numberOfEmployees, string employeeType, long salary, List<string> waivedBenefits)
        {
            var employees = new List<Employee>();
            for (int i = 0; i < numberOfEmployees; i++) employees.Add(SetupEmployee(employeeType, salary, waivedBenefits));
            return employees;
        }
    }
}
