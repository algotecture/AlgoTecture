using System;
using Newtonsoft.Json;

namespace AlgoTecture.Models.Dto
{
    public class UserCredentialModel
    {
        [JsonRequired]
        public string EmailLogin { get; set; }
        [JsonRequired]
        public string UserPassword { get; set; }

        public bool IsRegistration { get; set; }
    }
}