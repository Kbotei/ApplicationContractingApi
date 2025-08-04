using MobileAppApi.Models;

namespace MobileAppApi.NetworkModels
{
    public class ContractingAccountDataResponse
    {
        public bool DocumentsInFlight { get; set; } = false;

        public List<Applicant> Applicants { get; set; } = [];

        public List<SimpleField> AccountFields { get; set; } = [];
    }
}
