namespace MobileAppApi.Models;

public class Applicant
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public ApplicantType applicantType { get; set; }
}
