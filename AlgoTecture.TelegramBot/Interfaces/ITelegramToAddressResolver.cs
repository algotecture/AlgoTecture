using System.Collections.Generic;
using AlgoTecture.TelegramBot.Models;

namespace AlgoTecture.TelegramBot.Interfaces;

public interface ITelegramToAddressResolver
{
    void TryAddCurrentAddressList(long chatId, List<TelegramToAddressModel> telegramToAddressModels);

    List<TelegramToAddressModel> TryGetAddressListByChatId(long chatId);

    void RemoveAddressListByChatId(long chatId);

}