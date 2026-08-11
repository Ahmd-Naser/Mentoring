using Mentoring.Application.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Interfaces;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
    Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
}
