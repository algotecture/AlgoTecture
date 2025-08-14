using Newtonsoft.Json;

namespace AlgoTecture.Domain.Models.Dto
{
    public class UserCredentialModel
    {
        [JsonRequired]
        public string EmailLogin { get; set; } = null!;

        [JsonRequired]
        public string UserPassword { get; set; } = null!;
    }
}