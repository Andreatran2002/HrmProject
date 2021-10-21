using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace App.Security.Requirements
{
    public class AppAuthorizationHandler : IAuthorizationHandler
    {
        private readonly ILogger<AppAuthorizationHandler> _logger;
        private readonly UserManager<AppUser> _userManager;
        
        public AppAuthorizationHandler(ILogger<AppAuthorizationHandler> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var requirements = context.PendingRequirements.ToList(); //
            _logger.LogInformation("Context resorse: "+context.Resource?.GetType().Name);
            foreach (var requirement in requirements)
            {
                if (requirement is GenZRequirement)
                {
                    // Code sử lí kt 
                    if(IsGenZ(context.User,(GenZRequirement)requirement ))
                    {
                        context.Succeed(requirement);
                    }
                    
                }
                if (requirement is PostUpdateRequirement)
                {
                    // Code sử lí kt 
                    bool canupdate = CanUpdatePost(context.User,context.Resource,(PostUpdateRequirement)requirement);
                    if (canupdate) context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }

        private bool CanUpdatePost(ClaimsPrincipal user, object resource, PostUpdateRequirement requirement)
        {
            if (user.IsInRole("Admin")) {
                _logger.LogInformation("Admin đực truy cập");
                return true; 
            }
            var Post = resource as Post;
            var datecreated = Post.Created; 
            var datecanupdate = new DateTime(requirement.Year,requirement.Month,requirement.Day);
            if (datecreated <datecanupdate) {
                _logger.LogInformation("Qua han");
                return false;
            } 
            _logger.LogInformation("Được truy cập");
            return true; 
        }

        private bool IsGenZ(ClaimsPrincipal user, GenZRequirement requirement)
        {
            var appUserTask =  _userManager.GetUserAsync(user);
            Task.WaitAll(appUserTask);
            var appUser = appUserTask.Result; 

            if (appUser.BirthDate == null ) {
                _logger.LogInformation($"{appUser.BirthDate} khong co");
                return false;
            } 
            int year = appUser.BirthDate.Value.Year; 
            var success =  (year>=requirement.FromYear && year<=requirement.ToYear);

            if (success){
                _logger.LogInformation($"{appUser.BirthDate} duoc phep truy cap");
            }
            else{
                _logger.LogInformation($"{appUser.BirthDate} khong thanfh cong");
            }

            return success;
        }
    }
}