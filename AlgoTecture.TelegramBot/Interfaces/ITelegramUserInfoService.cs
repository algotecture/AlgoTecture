using System.Threading.Tasks;
using Algotecture.Domain.Models.Dto;
using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.TelegramBot.Interfaces;

public interface ITelegramUserInfoService
{
    Task<TelegramUserInfo> AddOrUpdate(AddOrUpdateTelegramUserInfoModel addOrUpdateTelegramUserInfoModel);
}