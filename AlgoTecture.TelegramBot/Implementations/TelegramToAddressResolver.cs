using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AlgoTecture.TelegramBot.Interfaces;
using AlgoTecture.TelegramBot.Models;

namespace AlgoTecture.TelegramBot.Implementations;

public class TelegramToAddressResolver : ITelegramToAddressResolver
{
    private static readonly IDictionary<long, List<TelegramToAddressModel>> TelegramToAddressMap =
        new ConcurrentDictionary<long, List<TelegramToAddressModel>>();

    public void TryAddCurrentAddressList(long chatId, List<TelegramToAddressModel> telegramToAddressModels)
    {
        if (!telegramToAddressModels.Any()) return;

        var isSuccess = TelegramToAddressMap.TryAdd(chatId, telegramToAddressModels);
        if (isSuccess) return;
    
        var isContains = TelegramToAddressMap.ContainsKey(chatId);
        if (isContains) TelegramToAddressMap[chatId] = telegramToAddressModels;
    }

    public List<TelegramToAddressModel>? TryGetAddressListByChatId(long chatId)
    {
        var isSuccess = TelegramToAddressMap.TryGetValue(chatId, out var telegramToAddressModels);
        return isSuccess ? telegramToAddressModels : new List<TelegramToAddressModel>();
    } 
    
    public void RemoveAddressListByChatId(long chatId)
    {
        var isContains = TelegramToAddressMap.ContainsKey(chatId);
        if (isContains) TelegramToAddressMap.Remove(chatId);
    }
}