using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Domain.Models;
using Algotecture.Domain.Models.RepositoryModels;
using Algotecture.Libraries.GeoAdminSearch;
using Algotecture.Libraries.Spaces.Interfaces;
using Algotecture.TelegramBot.Interfaces;
using Algotecture.TelegramBot.Models;
using Deployf.Botf;
using Newtonsoft.Json;

namespace Algotecture.TelegramBot.Controllers;

public class TelegramBotTestController : BotController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGeoAdminSearcher _geoAdminSearcher;
    private readonly ITelegramToAddressResolver _telegramToAddressResolver;
    private readonly ISpaceGetter _spaceGetter;

    public TelegramBotTestController(IUnitOfWork unitOfWork, IGeoAdminSearcher geoAdminSearcher, ITelegramToAddressResolver telegramToAddressResolver, ISpaceGetter spaceGetter)
    {
        _unitOfWork = unitOfWork;
        _geoAdminSearcher = geoAdminSearcher;
        _telegramToAddressResolver = telegramToAddressResolver;
        _spaceGetter = spaceGetter;
    }

    [Action]
    private async Task PressGetSubSpacePropertiesButton(long spaceId)
    {
        var targetSpace = await _unitOfWork.Spaces.GetById(spaceId);
        if (targetSpace == null) return;
        
        Button("Update"!);
        Button("Upload photo"!);
        Button("Remove"!);
        await Send($"{targetSpace.SpaceAddress}{Environment.NewLine}Area: {Environment.NewLine}Contract: none ");
    }
    
    [Action]
    private async Task PressTryToFindButton()
    {
        PushL("Enter the address or part of the address");
        await Send();
        var term = await AwaitText();

        var labels = await _geoAdminSearcher.GetAddress(term);
        foreach (var label in labels)
        {
            RowButton(label.label, Q(PressAddressButton));
        }

        await Send("You won");
    }
    
    [Action]
    private async Task PressToRentButton1()
    {
        PushL("Enter the address or part of the address");
        await Send();

        var term = await AwaitText();
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var telegramToAddressList = new List<TelegramToAddressModel>();


        var labels = await _geoAdminSearcher.GetAddress(term);
        var attrsEnumerable = labels.ToList();
        foreach (var label in attrsEnumerable)
        {
            var telegramToAddressModel = new TelegramToAddressModel
            {
                FeatureId = label.featureId,
                latitude = label.lat,
                longitude = label.lon,
                Address = label.label
            };
            telegramToAddressList.Add(telegramToAddressModel);
            RowButton(label.label, Q(PressAddressToRentButton, label.featureId));
        }

        if (!attrsEnumerable.Any())
        {
            RowButton("Try again"!);
            await Send("Nothing found");
        }
        else
        {
            _telegramToAddressResolver.TryAddCurrentAddressList(chatId.Value, telegramToAddressList);
            await Send("Choose the right address");
        }
    }
    
     [Action]
    private async Task PressAddressToRentButton(string geoAdminFeatureId)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var targetAddress = _telegramToAddressResolver.TryGetAddressListByChatId(chatId.Value)?.FirstOrDefault(x => x.FeatureId == geoAdminFeatureId);

        var user = await _unitOfWork.Users.GetByTelegramChatId(chatId.Value);
        var targetSpace = await _spaceGetter.GetByCoordinates(targetAddress!.latitude, targetAddress.longitude);
        //only for parking
        if (targetSpace == null)
        {
            var newSpace = new Space
            {
                UtilizationTypeId = 1,
                Latitude = targetAddress.latitude,
                Longitude = targetAddress.longitude,
                SpaceAddress = targetAddress.Address!
            };
            var spaceEntity = await _unitOfWork.Spaces.Add(newSpace);
            // await _unitOfWork.CompleteAsync();
            var newSubSpaceId = Guid.NewGuid();
            var newSpaceProperty = new SpaceProperty
            {
                SpacePropertyId = Guid.NewGuid(),
                SubSpaces = new List<SubSpace>()
                {
                    new SubSpace
                    {
                        OwnerId = user.Id,
                        SubSpaceId = newSubSpaceId,
                        UtilizationTypeId = 11,
                    }
                }
            };
            newSpace.SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty);
            await _unitOfWork.CompleteAsync();
            _telegramToAddressResolver.RemoveAddressListByChatId(chatId.Value);
            await PressGetSubSpacePropertiesButton(spaceEntity.Id);
            //await Send(targetAddress.Address);
        }
        else
        {
            var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);
            var userSubSpaces = (targetSpaceProperty?.SubSpaces!).Where(x => x.OwnerId == user.Id).ToList();
            if (userSubSpaces.Any())
            {
                var counter = 1;
                foreach (var userSubSpace in userSubSpaces)
                {
                    Button($"({counter})", Q(PressGetSubSpacePropertiesButton, targetSpace.Id));
                    counter++;
                }
            }

            _telegramToAddressResolver.RemoveAddressListByChatId(chatId.Value);

            await Send("Your parking spaces at this address");
        }
    }


    [Action]
    private async Task PressAddressButton()
    {
        PushL("Have a good day");
        await Send();
    } 
}