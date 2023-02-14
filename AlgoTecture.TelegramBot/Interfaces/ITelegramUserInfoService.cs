using System.Threading.Tasks;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.TelegramBot.Interfaces;

public interface ITelegramUserInfoService
{
    Task<TelegramUserInfo> Create(AddTelegramUserInfoModel addTelegramUserInfoModel);
}