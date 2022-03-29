using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Persistence.Core.Interfaces;
using AlgoTecture.TelegramBot.Interfaces;

namespace AlgoTecture.TelegramBot.Implementations;

public class TelegramUserInfoService : ITelegramUserInfoService
{
    private readonly IUnitOfWork _unitOfWork;

    public TelegramUserInfoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<TelegramUserInfo> Create(AddTelegramUserInfoModel addTelegramUserInfoModel)
    {
        if (addTelegramUserInfoModel == null) throw new ArgumentNullException(nameof(addTelegramUserInfoModel));
        if (addTelegramUserInfoModel.TelegramChatId == null) throw new ArgumentNullException(nameof(addTelegramUserInfoModel.TelegramChatId));

        var targetTelegramUserInfo = await _unitOfWork.TelegramUserInfos.GetByTelegramChatId(addTelegramUserInfoModel.TelegramChatId.Value);
        if (targetTelegramUserInfo != null)
        {
            var targetUser = await _unitOfWork.Users.GetById(targetTelegramUserInfo.Id);
            if (targetUser != null)
                return targetTelegramUserInfo;
        }
        
        var telegramUserInfoEntity = new TelegramUserInfo
        {
            TelegramUserId = addTelegramUserInfoModel.TelegramUserId,
            TelegramChatId = addTelegramUserInfoModel.TelegramChatId,
            TelegramUserName = addTelegramUserInfoModel.TelegramUserName,
            TelegramUserFullName = addTelegramUserInfoModel.TelegramUserFullName
        };

        var telegramUserInfo = await _unitOfWork.TelegramUserInfos.Upsert(telegramUserInfoEntity);

        await _unitOfWork.CompleteAsync();

        if (telegramUserInfo == null)
            throw new ArgumentNullException(nameof(telegramUserInfo));

        var userEntity = new User
        {
            CreateDateTime = DateTime.UtcNow,
            TelegramUserInfoId = telegramUserInfo.Id
        };

        _ = await _unitOfWork.Users.Upsert(userEntity);
        await _unitOfWork.CompleteAsync();

        return telegramUserInfo;
    }
}