using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Group;

public record CreateGroupRequest(
    string Name,
    string Description
);