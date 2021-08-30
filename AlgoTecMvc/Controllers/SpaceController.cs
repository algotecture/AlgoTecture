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

        public async Task<ActionResult<Space>> AddSpace(AddSpaceModel addSpaceModel)
        {
            addSpaceModel.TypeOfSpaceId = 1;
            var targetUser = await _unitOfWork.Users.GetByEmail(addSpaceModel.UserEmail);
            var targetUserId = default(long);
            if (targetUser == null)
            {
                var newUser = new User
                {
                    Email = addSpaceModel.UserEmail
                };
               targetUser = await _unitOfWork.Users.Add(newUser);
               await _unitOfWork.CompleteAsync();
               
            }
            targetUserId = targetUser.Id;

            var isLatitude = double.TryParse(addSpaceModel.Latitude, out var latitude);
            var isLongitude = double.TryParse(addSpaceModel.Latitude, out var longitude);
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
                OwnerId = targetUserId
            };

            targetSpaceToUpdate.SpaceProperty = JsonConvert.SerializeObject(spaceProperty);
            
            await _unitOfWork.CompleteAsync();
            TempData["Success"] = "Added Successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}