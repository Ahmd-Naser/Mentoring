using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Group;

public record GroupResponse(
    int Id,
    string Name,
    string Description,
    string OwnerId,
    string OwnerName,
    int SubscribersCount,
    int ProblemsCount
);
