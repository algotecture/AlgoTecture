using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoTecMvc.Core.Interfaces;
using AlgoTecMvc.Models;
using AlgoTecMvc.Models.Dto;
using AlgoTecMvc.Models.RepositoryModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlgoTecMvc.Controllers
{
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

        public async Task<ActionResult<Space>> AddSpaceDeclaration(AddSpaceModel addSpaceModel)
        {
            addSpaceModel.TypeOfSpaceId = 1;
            addSpaceModel.DateStart = "12.02.2021";
            addSpaceModel.DateStop = "12.03.2021";
            addSpaceModel.SubSpace = new SubSpace {Area = 12, TypeOfSpaceId = 2};
            var targetUser = await _unitOfWork.Users.GetByEmail(addSpaceModel.UserEmail);
            if (targetUser == null)
            {
                var newUser = new User
                {
                    Email = addSpaceModel.UserEmail
                };
               targetUser = await _unitOfWork.Users.Add(newUser);
               await _unitOfWork.CompleteAsync();
            }
            var targetUserId = targetUser.Id;

            var isLatitude = double.TryParse(addSpaceModel.Latitude, out var latitude);
            var isLongitude = double.TryParse(addSpaceModel.Longitude, out var longitude);
            
            var newSpace = new Space
            {
                TypeOfSpaceId = addSpaceModel.TypeOfSpaceId,
                SpaceAddress = addSpaceModel.Address,
                Latitude = isLatitude ? latitude : default,
                Longitude = isLongitude ? longitude : default
            };
            var createdSpace = await _unitOfWork.Spaces.Add(newSpace); 
            await _unitOfWork.CompleteAsync();
            var createdSpaceId = createdSpace.Id;

            var targetSpaceToUpdate = await _unitOfWork.Spaces.GetById(createdSpaceId);
            
            var spaceProperty = new SpaceProperty
            {
                SpacePropertyId = Guid.NewGuid(),
                SpaceId = createdSpaceId,
                TypeOfSpace = addSpaceModel.TypeOfSpaceId,
                OwnerId = targetUserId,
                SubSpaces = new List<SubSpace>()
            };
            if (addSpaceModel.SubSpace != null)
            {
                spaceProperty.SubSpaces.Add(new SubSpace
                {
                    SpaceId = createdSpaceId, SubSpaceId = Guid.NewGuid(), Area = addSpaceModel.SubSpace.Area, OwnerId = targetUserId,
                    TypeOfSpaceId = addSpaceModel.SubSpace.TypeOfSpaceId
                });
            }

            targetSpaceToUpdate.SpaceProperty = JsonConvert.SerializeObject(spaceProperty);
            
            await _unitOfWork.CompleteAsync();

            var isDateStart = DateTime.TryParse(addSpaceModel.DateStart, out var dateStart);
            var isDateStop = DateTime.TryParse(addSpaceModel.DateStop, out var dateStop);
            
            var newContract = new Contract
            {
                OwnerUserId = targetUserId,
                SpaceId = createdSpaceId,
                SpacePropertyId = spaceProperty.SpacePropertyId,
                ContractDateStart = isDateStart ? dateStart : default,
                ContractDateStop = isDateStop ? dateStop : default
            };
            
            var createdContract = await _unitOfWork.Contracts.Add(newContract);
            await _unitOfWork.CompleteAsync();

            if (addSpaceModel.RestApi)
            {
                return targetSpaceToUpdate;
            }
            
            TempData["Success"] = "Added Successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}