using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using LibraryManagmentSystem.Data.Model;

namespace LibraryManagmentSystem.Authorization
{
    public class ManagerAuthurizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Member>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Member member)
        {
            if (context.User == null || member == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.ManagerRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
