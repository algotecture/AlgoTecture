
using AlgoTecture.Identity.Contracts.Commands;
using Grpc.Core;
using Identity.Grpc;
using MediatR;

namespace AlgoTecture.IdentityService.Grpc;

public class TelegramAuthGrpcService : TelegramAuth.TelegramAuthBase
{
    private readonly IMediator _mediator;

    public TelegramAuthGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<TelegramLoginResponse> TelegramLogin(
        TelegramLoginRequest request, ServerCallContext context)
    {
        var result = await _mediator.Send(new TelegramLoginCommand(
            request.TelegramUserId,
            request.TelegramUserFullName));

        return new TelegramLoginResponse
        {
            IdentityId = result.IdentityId.ToString(),
            UserId = result.UserId?.ToString() ?? ""
        };
    }
}