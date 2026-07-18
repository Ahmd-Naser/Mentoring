using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Group;

public record TraineeDataResponse(
    string Id,
    string Name,
    string Email
);