using System.Collections.Generic;
using Algotecture.TelegramBot.Models;

namespace Algotecture.TelegramBot.Interfaces;

public interface ITelegramToAddressResolver
{
    void TryAddCurrentAddressList(long chatId, List<TelegramToAddressModel> telegramToAddressModels);

    List<TelegramToAddressModel>? TryGetAddressListByChatId(long chatId);

    void RemoveAddressListByChatId(long chatId);

}