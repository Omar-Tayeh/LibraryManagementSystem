using LibraryManagmentSystem.Data.Model;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagmentSystem.Authorization
{
    public class StaffAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.StaffRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
