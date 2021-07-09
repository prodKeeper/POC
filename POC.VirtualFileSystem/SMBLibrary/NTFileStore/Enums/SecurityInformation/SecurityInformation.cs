using System;

namespace SMBLibrary
{
    /// <summary>
    /// [MS-DTYP] 2.4.7 - SECURITY_INFORMATION
    /// </summary>
    [Flags]
    public enum SecurityInformation : uint
    {
        OWNER_SECURITY_INFORMATION = 0x00000001,
        GROUP_SECURITY_INFORMATION = 0x00000002,
        DACL_SECURITY_INFORMATION = 0x00000004,
        SACL_SECURITY_INFORMATION = 0x00000008,
        LABEL_SECURITY_INFORMATION = 0x00000010,
        ATTRIBUTE_SECURITY_INFORMATION = 0x00000020,
        SCOPE_SECURITY_INFORMATION = 0x00000040,
        BACKUP_SECURITY_INFORMATION = 0x00010000,
    }
}
