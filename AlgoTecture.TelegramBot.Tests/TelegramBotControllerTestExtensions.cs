using System.Reflection;
using Deployf.Botf;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AlgoTecture.TelegramBot.Tests;

public static class TelegramBotControllerTestExtensions
{
    public static void SetFakeContext(this BotController controller, long chatId,
        long userId,
        string username = "testuser",
        string fullName = "Test User")
    {
        var update = new Update
        {
            Id = 1,
            Message = new Message
            {
                MessageId = 1,
                Date = DateTime.UtcNow,
                Chat = new Chat
                {
                    Id = chatId,
                    Type = ChatType.Private,
                    Username = username,
                    FirstName = fullName
                },
                From = new Telegram.Bot.Types.User
                {
                    Id = userId,
                    Username = username,
                    FirstName = fullName
                }
            }
        };

        var fakeCtx = new UpdateContext(null!, update, null!);
        typeof(BotController)
            .GetProperty("Context", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!
            .SetValue(controller, fakeCtx);
    }
}