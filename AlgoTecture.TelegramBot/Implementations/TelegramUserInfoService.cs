using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.TelegramBot.Interfaces;

namespace AlgoTecture.TelegramBot.Implementations;

public class TelegramUserInfoService : ITelegramUserInfoService
{
    private readonly IUnitOfWork _unitOfWork;

    public TelegramUserInfoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<TelegramUserInfo> AddOrUpdate(AddOrUpdateTelegramUserInfoModel addOrUpdateTelegramUserInfoModel)
    {
        if (addOrUpdateTelegramUserInfoModel == null) throw new ArgumentNullException(nameof(addOrUpdateTelegramUserInfoModel));
        if (addOrUpdateTelegramUserInfoModel.TelegramChatId == null) throw new ArgumentNullException(nameof(addOrUpdateTelegramUserInfoModel.TelegramChatId));

        var telegramUserInfoEntity = new TelegramUserInfo
        {
            TelegramUserId = addOrUpdateTelegramUserInfoModel.TelegramUserId,
            TelegramChatId = addOrUpdateTelegramUserInfoModel.TelegramChatId,
            TelegramUserName = addOrUpdateTelegramUserInfoModel.TelegramUserName,
            TelegramUserFullName = addOrUpdateTelegramUserInfoModel.TelegramUserFullName
        };
        //To single transaction
        var telegramUserInfo = await _unitOfWork.TelegramUserInfos.Upsert(telegramUserInfoEntity);

        await _unitOfWork.CompleteAsync();

        if (telegramUserInfo == null)
            throw new ArgumentNullException(nameof(telegramUserInfo));
        
        var userEntity = new User
        {
            CreateDateTimeUtc = DateTime.UtcNow,
            TelegramUserInfoId = telegramUserInfo.Id
        };

        _ = await _unitOfWork.Users.Upsert(userEntity);
        await _unitOfWork.CompleteAsync();

        return telegramUserInfo;
    }
}