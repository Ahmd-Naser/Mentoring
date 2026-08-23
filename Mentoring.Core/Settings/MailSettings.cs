using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Core.Settings;

public class MailSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}