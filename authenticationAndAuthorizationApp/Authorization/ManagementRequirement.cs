using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace authenticationAndAuthorizationApp.Authorization
{
    public class ManagementRequirement :IAuthorizationRequirement
    {
        

        public ManagementRequirement(int probationTime)
        {
            ProbationTime = probationTime;
        }

        public int ProbationTime { get; }
    }

    public class RequirementHandler : AuthorizationHandler<ManagementRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManagementRequirement requirement)
        {
            if(!context.User.HasClaim(x => x.Type == "EmploymentDate"))
                return Task.CompletedTask;
            var empDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "EmploymentDate").Value);
            var period = DateTime.Now - empDate;
            if (period.Days > 30 * requirement.ProbationTime)
            {
                context.Succeed(requirement);

            }
            return Task.CompletedTask;  
        }
    }
}
