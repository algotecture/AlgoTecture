using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlgoTecMvc.Core.Interfaces;
using AlgoTecMvc.Models;
using AlgoTecMvc.Models.Dto;
using AlgoTecMvc.Models.RepositoryModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlgoTecMvc.Controllers
{
    [Route("[controller]")]
    public class SpaceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpaceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<string>> SearchSpace()
        {
            throw new NotImplementedException();
        }
        [HttpPost("AddSpaceDeclaration")]
        public async Task<ActionResult<Contract>> AddSpaceDeclaration([FromBody]AddContractModel addContractModel)
        {
            //add test user
            var targetUser = await _unitOfWork.Users.GetByEmail(addContractModel.UserEmail);
            if (targetUser == null)
            {
                var newUser = new User
                {
                    Email = addContractModel.UserEmail
                };
                targetUser = await _unitOfWork.Users.Add(newUser);
                await _unitOfWork.CompleteAsync();
            }

            var targetUserId = targetUser.Id;

            var targetSpace = await _unitOfWork.Spaces.GetById(addContractModel.SpaceId);
            var spaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);

            if (addContractModel.SpacePropertyId != spaceProperty.SpacePropertyId)
            {
               //recursive 
            }
          

            
            // var isLatitude = double.TryParse(addSpaceModel.Latitude, out var latitude);
            // var isLongitude = double.TryParse(addSpaceModel.Longitude, out var longitude);

            //var targetSpace = await _unitOfWork.Spaces.GetByCoordinates(latitude, longitude);
            
            //create fake space
            // var newSpace = new Space
            // {
            //     TypeOfSpaceId = addSpaceModel.TypeOfSpaceId,
            //     SpaceAddress = addSpaceModel.Address,
            //     Latitude = isLatitude ? latitude : default,
            //     Longitude = isLongitude ? longitude : default
            // };
            // var createdSpace = await _unitOfWork.Spaces.Add(newSpace);
            // await _unitOfWork.CompleteAsync();
            // var createdSpaceId = createdSpace.Id;
            //
            // var targetSpaceToUpdate = await _unitOfWork.Spaces.GetById(createdSpaceId);
            //
            // var spaceProperty = new SpaceProperty
            // {
            //     SpacePropertyId = Guid.NewGuid(),
            //     SpaceId = createdSpaceId,
            //     TypeOfSpace = addSpaceModel.TypeOfSpaceId,
            //     OwnerId = targetUserId,
            //     SubSpaces = new List<SubSpace>()
            // };
            // if (addSpaceModel.SubSpaces != null && addSpaceModel.SubSpaces.Any())
            // {
            //     foreach (var subSpace in addSpaceModel.SubSpaces)
            //     {
            //         spaceProperty.SubSpaces.Add(new SubSpace
            //         {
            //             SpaceId = createdSpaceId, SubSpaceId = Guid.NewGuid(), Area = subSpace.Area, OwnerId = targetUserId,
            //             TypeOfSpaceId = subSpace.TypeOfSpaceId
            //         });  
            //     }
            // }
            //
            // targetSpaceToUpdate.SpaceProperty = JsonConvert.SerializeObject(spaceProperty);
            //
            // await _unitOfWork.CompleteAsync();
            //create contract
            var isDateStart = DateTime.TryParse(addContractModel.DateStart, out var dateStart);
            var isDateStop = DateTime.TryParse(addContractModel.DateStop, out var dateStop);

            var newContract = new Contract
            {
                OwnerUserId = targetUserId,
                TenantUserId = null,
                SpaceId = addContractModel.SpaceId,
                SpacePropertyId = spaceProperty.SpacePropertyId,
                ContractDateStart = isDateStart ? dateStart : default,
                ContractDateStop = isDateStop ? dateStop : default
            };

            var createdContract = await _unitOfWork.Contracts.Add(newContract);
            await _unitOfWork.CompleteAsync();

            if (addContractModel.RestApi)
            {
                return createdContract;
            }

            TempData["Success"] = "Added Successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}