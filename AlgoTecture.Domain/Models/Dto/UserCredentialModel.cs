using Newtonsoft.Json;

namespace Algotecture.Domain.Models.Dto
{
    public class UserCredentialModel
    {
        [JsonRequired]
        public string EmailLogin { get; set; } = null!;

        [JsonRequired]
        public string UserPassword { get; set; } = null!;
    }
}