using Newtonsoft.Json;

namespace AlgoTecture.Domain.Models.Dto
{
    public class UserCredentialModel
    {
        [JsonRequired]
        public string EmailLogin { get; set; }
        [JsonRequired]
        public string UserPassword { get; set; }
    }
}