using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.Entities.personnel.PayStatementDetails;

public class PayStatementDetailsModel
{
    public int PersonelCode { get; set; }
    public string OrganizationName { get; set; }
    public string NationalCode { get; set; }
    public string? GovermentFormalEmployeeNumber { get; set; } // Changed to nullable int
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public string CertificateNumber { get; set; }
    public string IssuanceVillName { get; set; }
    public string IssuanceAreaName { get; set; }
    public string IssuanceCityName { get; set; }
    public string IssuanceProvinceName { get; set; }
    public string BirthDate { get; set; } // Consider using DateTime if parsing dates
    public int BirthDateYear { get; set; }
    public int BirthDateMonth { get; set; }
    public int BirthDateDay { get; set; }
    public string BirthCityName { get; set; }
    public string DegreeName { get; set; }
    public string MajorName { get; set; }
    public string SpecialExtraName { get; set; }
    public string OrganizationPostNumber { get; set; }
    public string JobRankName { get; set; }
    public string JobFieldName { get; set; }
    public string Category { get; set; }
    public string MarriageStatusName { get; set; }
    public int JobGroup { get; set; }
    public int ServiceDays { get; set; }
    public int ServiceMonths { get; set; }
    public int ServiceYears { get; set; }
    public string FormatedOrganizationUnit { get; set; }
    public string ServiceProvinceName { get; set; }
    public string ServiceCityName { get; set; }
    public string ServiceAreaName { get; set; }
    public string ServiceVillName { get; set; }
    public int GovermentFormalServicePostalCode { get; set; }
    public string PlacePostalCode { get; set; }
    public string SacrificesStatus { get; set; }
    public string? SacrificePercent { get; set; }
    public string? MartyFamily { get; set; }
    public string? SacrificialOther { get; set; }
    public int ChildNumber { get; set; }
    public int YearsIncrementFactor { get; set; }
    public string StatementTypeName { get; set; }
    public string Discription { get; set; }
    public string TotalSalary { get; set; }
    public string ExecuteDate { get; set; } // Consider using DateTime if parsing dates
    public string FormatedExecuteDate { get; set; } // Consider using DateTime if parsing dates
    public string EndDate { get; set; } // Consider using DateTime if parsing dates
    public string FormatedEndDate { get; set; } // Consider using DateTime if parsing dates
    public string IssuanceDate { get; set; } // Consider using DateTime if parsing dates
    public string FormatedIssuanceDate { get; set; } // Consider using DateTime if parsing dates
    public int IssuanceNumber { get; set; }
    public string InsuranceNumber { get; set; }
    public string MilitrayServiceName { get; set; }
    public string WorkLawOrganizationPostName { get; set; }
    public string JobCode { get; set; }
    public string OrganizationLocationPostName { get; set; }
    public string ReportResponsableName { get; set; }
    public string ReportPostOrganization { get; set; }
    public string FarsiTotalSalary { get; set; }
    public string EmploymentTypeName { get; set; }
    public decimal AdjastmentCoefficient { get; set; }
}
