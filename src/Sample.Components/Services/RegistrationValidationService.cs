namespace Sample.Components.Services;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Sample.Components.Contracts;

public class RegistrationValidationService :
    IRegistrationValidationService
{
    readonly IPublishEndpoint _publishEndpoint;
    private readonly RegistrationDbContext _dbContext;

    public RegistrationValidationService(IPublishEndpoint publishEndpoint, RegistrationDbContext dbContext)
    {
        _publishEndpoint = publishEndpoint;
        _dbContext = dbContext;
    }

    public async Task ValidateRegistration(string eventId, string memberId, Guid registrationId)
    {
        await _dbContext.Set<Registration>().AddAsync(new Registration
        {
            RegistrationId = NewId.NextGuid(),
            RegistrationDate = DateTime.UtcNow,
            MemberId = "MemberId",
            EventId = "EventId",
            Payment = 10000    
        }, CancellationToken.None);

        await _publishEndpoint.Publish(new RegistrationSubmitted { RegistrationId = registrationId });
    }
}