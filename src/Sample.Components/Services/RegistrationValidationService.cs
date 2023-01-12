namespace Sample.Components.Services;
using MassTransit;
using Sample.Components.Contracts;

public class RegistrationValidationService :
    IRegistrationValidationService
{
    readonly IPublishEndpoint _publishEndpoint;

    public RegistrationValidationService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task ValidateRegistration(string eventId, string memberId, Guid registrationId)
    {
        await _publishEndpoint.Publish(new RegistrationSubmitted { RegistrationId = registrationId });
    }
}