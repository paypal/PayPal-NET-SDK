#region USINGS
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
#endregion USINGS

namespace JCS
{
    /// <summary>
    /// Provides detailed information about the host operating system.
    /// </summary>
    public static class OSVersionInfo
    {
        #region ENUMS
        /// <summary>
        /// Software Architectures
        /// </summary>
        public enum SoftwareArchitecture
        {
            /// <summary>
            /// unknown
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// 32 bit
            /// </summary>
            Bit32 = 1,
            /// <summary>
            /// 64 bit
            /// </summary>
            Bit64 = 2
        }

        /// <summary>
        /// Processor Architectures
        /// </summary>
        public enum ProcessorArchitecture
        {
            /// <summary>
            /// unknown
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// 32 bit
            /// </summary>
            Bit32 = 1,
            /// <summary>
            /// 64 bit
            /// </summary>
            Bit64 = 2,
            /// <summary>
            /// itanium 64
            /// </summary>
            Itanium64 = 3
        }
        #endregion ENUMS

        #region DELEGATE DECLARATION
        /// <summary>
        /// Is Wow64 Process Delegate
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="isWow64Process">if set to <c>true</c> [is wow64 process].</param>
        /// <returns></returns>
        private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, [Out] out bool isWow64Process);
        #endregion DELEGATE DECLARATION

        #region BITS
        /// <summary>
        /// Determines if the current application is 32 or 64-bit.
        /// </summary>
        static public SoftwareArchitecture ProgramBits
        {
            get
            {
                SoftwareArchitecture pbits = SoftwareArchitecture.Unknown;

                System.Collections.IDictionary test = Environment.GetEnvironmentVariables();

                switch (IntPtr.Size * 8)
                {
                    case 64:
                        pbits = SoftwareArchitecture.Bit64;
                        break;

                    case 32:
                        pbits = SoftwareArchitecture.Bit32;
                        break;

                    default:
                        pbits = SoftwareArchitecture.Unknown;
                        break;
                }

                return pbits;
                // int getOSArchitecture()
                //{
                //    string pa = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                //    return ((String.IsNullOrEmpty(pa) || String.Compare(pa, 0, "x86", 0, 3, true) == 0) ? 32 : 64);
                //}



                //ProcessorArchitecture pbits = ProcessorArchitecture.Unknown;

                //try
                //{
                //    SYSTEM_INFO l_System_Info = new SYSTEM_INFO();
                //    GetSystemInfo(ref l_System_Info);

                //    switch (l_System_Info.uProcessorInfo.wProcessorArchitecture)
                //    {
                //        case 9: // PROCESSOR_ARCHITECTURE_AMD64
                //            pbits = ProcessorArchitecture.Bit64;
                //            break;
                //        case 6: // PROCESSOR_ARCHITECTURE_IA64
                //            pbits = ProcessorArchitecture.Itanium64;
                //            break;
                //        case 0: // PROCESSOR_ARCHITECTURE_INTEL
                //            pbits = ProcessorArchitecture.Bit32;
                //            break;
                //        default: // PROCESSOR_ARCHITECTURE_UNKNOWN
                //            pbits = ProcessorArchitecture.Unknown;
                //            break;
                //    }
                //}
                //catch
                //{
                //     Ignore        
                //}

                //return pbits;
            }
        }

        /// <summary>
        /// Gets the OS bits.
        /// </summary>
        /// <value>
        /// The OS bits.
        /// </value>
        static public SoftwareArchitecture OSBits
        {
            get
            {
                SoftwareArchitecture osbits = SoftwareArchitecture.Unknown;

                switch (IntPtr.Size * 8)
                {
                    case 64:
                        osbits = SoftwareArchitecture.Bit64;
                        break;

                    case 32:
                        if (Is32BitProcessOn64BitProcessor())
                            osbits = SoftwareArchitecture.Bit64;
                        else
                            osbits = SoftwareArchitecture.Bit32;
                        break;

                    default:
                        osbits = SoftwareArchitecture.Unknown;
                        break;
                }

                return osbits;
            }
        }

        /// <summary>
        /// Determines if the current processor is 32 or 64-bit.
        /// </summary>
        /// <value>
        /// The processor bits (32 or 64-bit).
        /// </value>
        static public ProcessorArchitecture ProcessorBits
        {
            get
            {
                ProcessorArchitecture pbits = ProcessorArchitecture.Unknown;

                try
                {
                    SYSTEM_INFO l_System_Info = new SYSTEM_INFO();
                    GetNativeSystemInfo(ref l_System_Info);

                    switch (l_System_Info.uProcessorInfo.wProcessorArchitecture)
                    {
                        case 9: // PROCESSOR_ARCHITECTURE_AMD64
                            pbits = ProcessorArchitecture.Bit64;
                            break;
                        case 6: // PROCESSOR_ARCHITECTURE_IA64
                            pbits = ProcessorArchitecture.Itanium64;
                            break;
                        case 0: // PROCESSOR_ARCHITECTURE_INTEL
                            pbits = ProcessorArchitecture.Bit32;
                            break;
                        default: // PROCESSOR_ARCHITECTURE_UNKNOWN
                            pbits = ProcessorArchitecture.Unknown;
                            break;
                    }
                }
                catch
                {
                    // Ignore        
                }

                return pbits;
            }
        }
        #endregion BITS

        #region EDITION
        /// <summary>
        /// The edition
        /// </summary>
        static private string s_Edition;
        /// <summary>
        /// Gets the edition of the operating system running on this computer.
        /// </summary>
        /// <value>
        /// The edition of the operating system running on this computer.
        /// </value>
        static public string Edition
        {
            get
            {
                if (s_Edition != null)
                    return s_Edition;  //***** RETURN *****//

                string edition = String.Empty;

                OperatingSystem osVersion = Environment.OSVersion;
                OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

                if (GetVersionEx(ref osVersionInfo))
                {
                    int majorVersion = osVersion.Version.Major;
                    int minorVersion = osVersion.Version.Minor;
                    byte productType = osVersionInfo.wProductType;
                    short suiteMask = osVersionInfo.wSuiteMask;

                    #region VERSION 4
                    if (majorVersion == 4)
                    {
                        if (productType == VER_NT_WORKSTATION)
                        {
                            // Windows NT 4.0 Workstation
                            edition = "Workstation";
                        }
                        else if (productType == VER_NT_SERVER)
                        {
                            if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                            {
                                // Windows NT 4.0 Server Enterprise
                                edition = "Enterprise Server";
                            }
                            else
                            {
                                // Windows NT 4.0 Server
                                edition = "Standard Server";
                            }
                        }
                    }
                    #endregion VERSION 4

                    #region VERSION 5
                    else if (majorVersion == 5)
                    {
                        if (productType == VER_NT_WORKSTATION)
                        {
                            if ((suiteMask & VER_SUITE_PERSONAL) != 0)
                            {
                                edition = "Home";
                            }
                            else
                            {
                                if (GetSystemMetrics(86) == 0) // 86 == SM_TABLETPC
                                    edition = "Professional";
                                else
                                    edition = "Tablet Edition";
                            }
                        }
                        else if (productType == VER_NT_SERVER)
                        {
                            if (minorVersion == 0)
                            {
                                if ((suiteMask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows 2000 Datacenter Server
                                    edition = "Datacenter Server";
                                }
                                else if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows 2000 Advanced Server
                                    edition = "Advanced Server";
                                }
                                else
                                {
                                    // Windows 2000 Server
                                    edition = "Server";
                                }
                            }
                            else
                            {
                                if ((suiteMask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows Server 2003 Datacenter Edition
                                    edition = "Datacenter";
                                }
                                else if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows Server 2003 Enterprise Edition
                                    edition = "Enterprise";
                                }
                                else if ((suiteMask & VER_SUITE_BLADE) != 0)
                                {
                                    // Windows Server 2003 Web Edition
                                    edition = "Web Edition";
                                }
                                else
                                {
                                    // Windows Server 2003 Standard Edition
                                    edition = "Standard";
                                }
                            }
                        }
                    }
                    #endregion VERSION 5

                    #region VERSION 6
                    else if (majorVersion == 6)
                    {
                        int ed;
                        if (GetProductInfo(majorVersion, minorVersion,
                            osVersionInfo.wServicePackMajor, osVersionInfo.wServicePackMinor,
                            out ed))
                        {
                            switch (ed)
                            {
                                case PRODUCT_BUSINESS:
                                    edition = "Business";
                                    break;
                                case PRODUCT_BUSINESS_N:
                                    edition = "Business N";
                                    break;
                                case PRODUCT_CLUSTER_SERVER:
                                    edition = "HPC Edition";
                                    break;
                                case PRODUCT_CLUSTER_SERVER_V:
                                    edition = "HPC Edition without Hyper-V";
                                    break;
                                case PRODUCT_DATACENTER_SERVER:
                                    edition = "Datacenter Server";
                                    break;
                                case PRODUCT_DATACENTER_SERVER_CORE:
                                    edition = "Datacenter Server (core installation)";
                                    break;
                                case PRODUCT_DATACENTER_SERVER_V:
                                    edition = "Datacenter Server without Hyper-V";
                                    break;
                                case PRODUCT_DATACENTER_SERVER_CORE_V:
                                    edition = "Datacenter Server without Hyper-V (core installation)";
                                    break;
                                case PRODUCT_EMBEDDED:
                                    edition = "Embedded";
                                    break;
                                case PRODUCT_ENTERPRISE:
                                    edition = "Enterprise";
                                    break;
                                case PRODUCT_ENTERPRISE_N:
                                    edition = "Enterprise N";
                                    break;
                                case PRODUCT_ENTERPRISE_E:
                                    edition = "Enterprise E";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER:
                                    edition = "Enterprise Server";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_CORE:
                                    edition = "Enterprise Server (core installation)";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_CORE_V:
                                    edition = "Enterprise Server without Hyper-V (core installation)";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_IA64:
                                    edition = "Enterprise Server for Itanium-based Systems";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_V:
                                    edition = "Enterprise Server without Hyper-V";
                                    break;
                                case PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT:
                                    edition = "Essential Business Server MGMT";
                                    break;
                                case PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL:
                                    edition = "Essential Business Server ADDL";
                                    break;
                                case PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC:
                                    edition = "Essential Business Server MGMTSVC";
                                    break;
                                case PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC:
                                    edition = "Essential Business Server ADDLSVC";
                                    break;
                                case PRODUCT_HOME_BASIC:
                                    edition = "Home Basic";
                                    break;
                                case PRODUCT_HOME_BASIC_N:
                                    edition = "Home Basic N";
                                    break;
                                case PRODUCT_HOME_BASIC_E:
                                    edition = "Home Basic E";
                                    break;
                                case PRODUCT_HOME_PREMIUM:
                                    edition = "Home Premium";
                                    break;
                                case PRODUCT_HOME_PREMIUM_N:
                                    edition = "Home Premium N";
                                    break;
                                case PRODUCT_HOME_PREMIUM_E:
                                    edition = "Home Premium E";
                                    break;
                                case PRODUCT_HOME_PREMIUM_SERVER:
                                    edition = "Home Premium Server";
                                    break;
                                case PRODUCT_HYPERV:
                                    edition = "Microsoft Hyper-V Server";
                                    break;
                                case PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT:
                                    edition = "Windows Essential Business Management Server";
                                    break;
                                case PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING:
                                    edition = "Windows Essential Business Messaging Server";
                                    break;
                                case PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY:
                                    edition = "Windows Essential Business Security Server";
                                    break;
                                case PRODUCT_PROFESSIONAL:
                                    edition = "Professional";
                                    break;
                                case PRODUCT_PROFESSIONAL_N:
                                    edition = "Professional N";
                                    break;
                                case PRODUCT_PROFESSIONAL_E:
                                    edition = "Professional E";
                                    break;
                                case PRODUCT_SB_SOLUTION_SERVER:
                                    edition = "SB Solution Server";
                                    break;
                                case PRODUCT_SB_SOLUTION_SERVER_EM:
                                    edition = "SB Solution Server EM";
                                    break;
                                case PRODUCT_SERVER_FOR_SB_SOLUTIONS:
                                    edition = "Server for SB Solutions";
                                    break;
                                case PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM:
                                    edition = "Server for SB Solutions EM";
                                    break;
                                case PRODUCT_SERVER_FOR_SMALLBUSINESS:
                                    edition = "Windows Essential Server Solutions";
                                    break;
                                case PRODUCT_SERVER_FOR_SMALLBUSINESS_V:
                                    edition = "Windows Essential Server Solutions without Hyper-V";
                                    break;
                                case PRODUCT_SERVER_FOUNDATION:
                                    edition = "Server Foundation";
                                    break;
                                case PRODUCT_SMALLBUSINESS_SERVER:
                                    edition = "Windows Small Business Server";
                                    break;
                                case PRODUCT_SMALLBUSINESS_SERVER_PREMIUM:
                                    edition = "Windows Small Business Server Premium";
                                    break;
                                case PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE:
                                    edition = "Windows Small Business Server Premium (core installation)";
                                    break;
                                case PRODUCT_SOLUTION_EMBEDDEDSERVER:
                                    edition = "Solution Embedded Server";
                                    break;
                                case PRODUCT_SOLUTION_EMBEDDEDSERVER_CORE:
                                    edition = "Solution Embedded Server (core installation)";
                                    break;
                                case PRODUCT_STANDARD_SERVER:
                                    edition = "Standard Server";
                                    break;
                                case PRODUCT_STANDARD_SERVER_CORE:
                                    edition = "Standard Server (core installation)";
                                    break;
                                case PRODUCT_STANDARD_SERVER_SOLUTIONS:
                                    edition = "Standard Server Solutions";
                                    break;
                                case PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE:
                                    edition = "Standard Server Solutions (core installation)";
                                    break;
                                case PRODUCT_STANDARD_SERVER_CORE_V:
                                    edition = "Standard Server without Hyper-V (core installation)";
                                    break;
                                case PRODUCT_STANDARD_SERVER_V:
                                    edition = "Standard Server without Hyper-V";
                                    break;
                                case PRODUCT_STARTER:
                                    edition = "Starter";
                                    break;
                                case PRODUCT_STARTER_N:
                                    edition = "Starter N";
                                    break;
                                case PRODUCT_STARTER_E:
                                    edition = "Starter E";
                                    break;
                                case PRODUCT_STORAGE_ENTERPRISE_SERVER:
                                    edition = "Enterprise Storage Server";
                                    break;
                                case PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE:
                                    edition = "Enterprise Storage Server (core installation)";
                                    break;
                                case PRODUCT_STORAGE_EXPRESS_SERVER:
                                    edition = "Express Storage Server";
                                    break;
                                case PRODUCT_STORAGE_EXPRESS_SERVER_CORE:
                                    edition = "Express Storage Server (core installation)";
                                    break;
                                case PRODUCT_STORAGE_STANDARD_SERVER:
                                    edition = "Standard Storage Server";
                                    break;
                                case PRODUCT_STORAGE_STANDARD_SERVER_CORE:
                                    edition = "Standard Storage Server (core installation)";
                                    break;
                                case PRODUCT_STORAGE_WORKGROUP_SERVER:
                                    edition = "Workgroup Storage Server";
                                    break;
                                case PRODUCT_STORAGE_WORKGROUP_SERVER_CORE:
                                    edition = "Workgroup Storage Server (core installation)";
                                    break;
                                case PRODUCT_UNDEFINED:
                                    edition = "Unknown product";
                                    break;
                                case PRODUCT_ULTIMATE:
                                    edition = "Ultimate";
                                    break;
                                case PRODUCT_ULTIMATE_N:
                                    edition = "Ultimate N";
                                    break;
                                case PRODUCT_ULTIMATE_E:
                                    edition = "Ultimate E";
                                    break;
                                case PRODUCT_WEB_SERVER:
                                    edition = "Web Server";
                                    break;
                                case PRODUCT_WEB_SERVER_CORE:
                                    edition = "Web Server (core installation)";
                                    break;
                            }
                        }
                    }
                    #endregion VERSION 6
                }

                s_Edition = edition;
                return edition;
            }
        }
        #endregion EDITION

        #region NAME
        /// <summary>
        /// The name
        /// </summary>
        static private string s_Name;
        /// <summary>
        /// Gets the name of the operating system running on this computer.
        /// </summary>
        /// <value>
        /// The name of the operating system running on this computer.
        /// </value>
        static public string Name
        {
            get
            {
                if (s_Name != null)
                    return s_Name;  //***** RETURN *****//

                string name = "unknown";

                OperatingSystem osVersion = Environment.OSVersion;
                OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

                if (GetVersionEx(ref osVersionInfo))
                {
                    int majorVersion = osVersion.Version.Major;
                    int minorVersion = osVersion.Version.Minor;

                    switch (osVersion.Platform)
                    {
                        case PlatformID.Win32S:
                            name = "Windows 3.1";
                            break;
                        case PlatformID.WinCE:
                            name = "Windows CE";
                            break;
                        case PlatformID.Win32Windows:
                            {
                                if (majorVersion == 4)
                                {
                                    string csdVersion = osVersionInfo.szCSDVersion;
                                    switch (minorVersion)
                                    {
                                        case 0:
                                            if (csdVersion == "B" || csdVersion == "C")
                                                name = "Windows 95 OSR2";
                                            else
                                                name = "Windows 95";
                                            break;
                                        case 10:
                                            if (csdVersion == "A")
                                                name = "Windows 98 Second Edition";
                                            else
                                                name = "Windows 98";
                                            break;
                                        case 90:
                                            name = "Windows Me";
                                            break;
                                    }
                                }
                                break;
                            }
                        case PlatformID.Win32NT:
                            {
                                byte productType = osVersionInfo.wProductType;

                                switch (majorVersion)
                                {
                                    case 3:
                                        name = "Windows NT 3.51";
                                        break;
                                    case 4:
                                        switch (productType)
                                        {
                                            case 1:
                                                name = "Windows NT 4.0";
                                                break;
                                            case 3:
                                                name = "Windows NT 4.0 Server";
                                                break;
                                        }
                                        break;
                                    case 5:
                                        switch (minorVersion)
                                        {
                                            case 0:
                                                name = "Windows 2000";
                                                break;
                                            case 1:
                                                name = "Windows XP";
                                                break;
                                            case 2:
                                                name = "Windows Server 2003";
                                                break;
                                        }
                                        break;
                                    case 6:
                                        switch (minorVersion)
                                        {
                                            case 0:
                                                switch (productType)
                                                {
                                                    case 1:
                                                        name = "Windows Vista";
                                                        break;
                                                    case 3:
                                                        name = "Windows Server 2008";
                                                        break;
                                                }
                                                break;

                                            case 1:
                                                switch (productType)
                                                {
                                                    case 1:
                                                        name = "Windows 7";
                                                        break;
                                                    case 3:
                                                        name = "Windows Server 2008 R2";
                                                        break;
                                                }
                                                break;
                                            case 2:
                                                switch (productType)
                                                {
                                                    case 1:
                                                        name = "Windows 8";
                                                        break;
                                                    case 3:
                                                        name = "Windows Server 2012";
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                }
                                break;
                            }
                    }
                }

                s_Name = name;
                return name;
            }
        }
        #endregion NAME

        #region PINVOKE

        #region GET
        #region PRODUCT INFO
        /// <summary>
        /// Gets the product info.
        /// </summary>
        /// <param name="osMajorVersion">The os major version.</param>
        /// <param name="osMinorVersion">The os minor version.</param>
        /// <param name="spMajorVersion">The sp major version.</param>
        /// <param name="spMinorVersion">The sp minor version.</param>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        internal static extern bool GetProductInfo(
            int osMajorVersion,
            int osMinorVersion,
            int spMajorVersion,
            int spMinorVersion,
            out int edition);
        #endregion PRODUCT INFO

        #region VERSION
        /// <summary>
        /// Gets the version ex.
        /// </summary>
        /// <param name="osVersionInfo">The os version info.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);
        #endregion VERSION

        #region SYSTEMMETRICS
        /// <summary>
        /// Gets the system metrics.
        /// </summary>
        /// <param name="nIndex">Index of the n.</param>
        /// <returns>the system metrics</returns>
        [DllImport("user32")]
        public static extern int GetSystemMetrics(int nIndex);
        #endregion SYSTEMMETRICS

        #region SYSTEMINFO
        /// <summary>
        /// Gets the system info.
        /// </summary>
        /// <param name="lpSystemInfo">The lp system info.</param>
        [DllImport("kernel32.dll")]
        public static extern void GetSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SYSTEM_INFO lpSystemInfo);

        /// <summary>
        /// Gets the native system info.
        /// </summary>
        /// <param name="lpSystemInfo">The lp system info.</param>
        [DllImport("kernel32.dll")]
        public static extern void GetNativeSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SYSTEM_INFO lpSystemInfo);
        #endregion SYSTEMINFO

        #endregion GET

        #region OSVERSIONINFOEX
        /// <summary>
        /// OS VERSION INFO EX
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct OSVERSIONINFOEX
        {
            /// <summary>
            /// The dw OS version info size
            /// </summary>
            public int dwOSVersionInfoSize;
            /// <summary>
            /// The dw major version
            /// </summary>
            public int dwMajorVersion;
            /// <summary>
            /// The dw minor version
            /// </summary>
            public int dwMinorVersion;
            /// <summary>
            /// The dw build number
            /// </summary>
            public int dwBuildNumber;
            /// <summary>
            /// The dw platform id
            /// </summary>
            public int dwPlatformId;
            /// <summary>
            /// The sz CSD version
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            /// <summary>
            /// The service pack major
            /// </summary>
            public short wServicePackMajor;
            /// <summary>
            /// The service pack minor
            /// </summary>
            public short wServicePackMinor;
            /// <summary>
            /// The suite mask
            /// </summary>
            public short wSuiteMask;
            /// <summary>
            /// The product type
            /// </summary>
            public byte wProductType;
            /// <summary>
            /// The reserved
            /// </summary>
            public byte wReserved;
        }
        #endregion OSVERSIONINFOEX

        #region SYSTEM_INFO
        /// <summary>
        /// SYSTEM INFO
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            /// <summary>
            /// The processor info
            /// </summary>
            internal _PROCESSOR_INFO_UNION uProcessorInfo;
            /// <summary>
            /// The dw page size
            /// </summary>
            public uint dwPageSize;
            /// <summary>
            /// The lp minimum application address
            /// </summary>
            public IntPtr lpMinimumApplicationAddress;
            /// <summary>
            /// The lp maximum application address
            /// </summary>
            public IntPtr lpMaximumApplicationAddress;
            /// <summary>
            /// The dw active processor mask
            /// </summary>
            public IntPtr dwActiveProcessorMask;
            /// <summary>
            /// The dw number of processors
            /// </summary>
            public uint dwNumberOfProcessors;
            /// <summary>
            /// The dw processor type
            /// </summary>
            public uint dwProcessorType;
            /// <summary>
            /// The dw allocation granularity
            /// </summary>
            public uint dwAllocationGranularity;
            /// <summary>
            /// The dw processor level
            /// </summary>
            public ushort dwProcessorLevel;
            /// <summary>
            /// The dw processor revision
            /// </summary>
            public ushort dwProcessorRevision;
        }
        #endregion SYSTEM_INFO

        #region _PROCESSOR_INFO_UNION
        /// <summary>
        ///  PROCESSOR INFO UNION
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct _PROCESSOR_INFO_UNION
        {
            /// <summary>
            /// The oem id
            /// </summary>
            [FieldOffset(0)]
            internal uint dwOemId;
            /// <summary>
            /// The processor architecture
            /// </summary>
            [FieldOffset(0)]
            internal ushort wProcessorArchitecture;
            /// <summary>
            /// The reserved
            /// </summary>
            [FieldOffset(2)]
            internal ushort wReserved;
        }
        #endregion _PROCESSOR_INFO_UNION

        #region 64 BIT OS DETECTION
        /// <summary>
        /// Loads the library.
        /// </summary>
        /// <param name="libraryName">Name of the library.</param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public extern static IntPtr LoadLibrary(string libraryName);

        /// <summary>
        /// Gets the proc address.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public extern static IntPtr GetProcAddress(IntPtr hwnd, string procedureName);
        #endregion 64 BIT OS DETECTION

        #region PRODUCT
        /// <summary>
        /// The PRODUCT UNDEFINED
        /// </summary>
        private const int PRODUCT_UNDEFINED = 0x00000000;
        /// <summary>
        /// The PRODUCT ULTIMATE
        /// </summary>
        private const int PRODUCT_ULTIMATE = 0x00000001;
        /// <summary>
        /// The Product HOM e_ BASIC
        /// </summary>
        private const int PRODUCT_HOME_BASIC = 0x00000002;
        /// <summary>
        /// The Product HOM e_ PREMIUM
        /// </summary>
        private const int PRODUCT_HOME_PREMIUM = 0x00000003;
        /// <summary>
        /// The Product ENTERPRISE
        /// </summary>
        private const int PRODUCT_ENTERPRISE = 0x00000004;
        /// <summary>
        /// The Product HOM e_ BASI c_ N
        /// </summary>
        private const int PRODUCT_HOME_BASIC_N = 0x00000005;
        /// <summary>
        /// The Product BUSINESS
        /// </summary>
        private const int PRODUCT_BUSINESS = 0x00000006;
        /// <summary>
        /// The Product Standard SERVER
        /// </summary>
        private const int PRODUCT_STANDARD_SERVER = 0x00000007;
        /// <summary>
        /// The Product Datacenter SERVER
        /// </summary>
        private const int PRODUCT_DATACENTER_SERVER = 0x00000008;
        /// <summary>
        /// The PRODUCT SMALLBUSINES SERVER
        /// </summary>
        private const int PRODUCT_SMALLBUSINESS_SERVER = 0x00000009;
        /// <summary>
        /// The Product ENTERPRIS e_ SERVER
        /// </summary>
        private const int PRODUCT_ENTERPRISE_SERVER = 0x0000000A;
        /// <summary>
        /// The Product STARTER
        /// </summary>
        private const int PRODUCT_STARTER = 0x0000000B;
        /// <summary>
        /// The Product Datacenter Server CORE
        /// </summary>
        private const int PRODUCT_DATACENTER_SERVER_CORE = 0x0000000C;
        /// <summary>
        /// The Product Standard Server CORE
        /// </summary>
        private const int PRODUCT_STANDARD_SERVER_CORE = 0x0000000D;
        /// <summary>
        /// The Product ENTERPRIS e_ Server CORE
        /// </summary>
        private const int PRODUCT_ENTERPRISE_SERVER_CORE = 0x0000000E;
        /// <summary>
        /// The Product ENTERPRIS e_ Server I a64
        /// </summary>
        private const int PRODUCT_ENTERPRISE_SERVER_IA64 = 0x0000000F;
        /// <summary>
        /// The Product BUSINES s_ N
        /// </summary>
        private const int PRODUCT_BUSINESS_N = 0x00000010;
        /// <summary>
        /// The Product Web SERVER
        /// </summary>
        private const int PRODUCT_WEB_SERVER = 0x00000011;
        /// <summary>
        /// The Product CLUSTE r_ SERVER
        /// </summary>
        private const int PRODUCT_CLUSTER_SERVER = 0x00000012;
        /// <summary>
        /// The Product HOM e_ SERVER
        /// </summary>
        private const int PRODUCT_HOME_SERVER = 0x00000013;
        /// <summary>
        /// The Product Storage EXPRES s_ SERVER
        /// </summary>
        private const int PRODUCT_STORAGE_EXPRESS_SERVER = 0x00000014;
        /// <summary>
        /// The Product Storage Standard SERVER
        /// </summary>
        private const int PRODUCT_STORAGE_STANDARD_SERVER = 0x00000015;
        /// <summary>
        /// The Product Storage WORKGROU p_ SERVER
        /// </summary>
        private const int PRODUCT_STORAGE_WORKGROUP_SERVER = 0x00000016;
        /// <summary>
        /// The Product Storage ENTERPRIS e_ SERVER
        /// </summary>
        private const int PRODUCT_STORAGE_ENTERPRISE_SERVER = 0x00000017;
        /// <summary>
        /// The Product Server FO r_ SMALLBUSINESS
        /// </summary>
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS = 0x00000018;
        /// <summary>
        /// The Product SMALLBUSINES s_ Server PREMIUM
        /// </summary>
        private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = 0x00000019;
        /// <summary>
        /// The Product HOM e_ PREMIU m_ N
        /// </summary>
        private const int PRODUCT_HOME_PREMIUM_N = 0x0000001A;
        /// <summary>
        /// The Product ENTERPRIS e_ N
        /// </summary>
        private const int PRODUCT_ENTERPRISE_N = 0x0000001B;
        /// <summary>
        /// The Product ULTIMAT e_ N
        /// </summary>
        private const int PRODUCT_ULTIMATE_N = 0x0000001C;
        /// <summary>
        /// The Product Web Server CORE
        /// </summary>
        private const int PRODUCT_WEB_SERVER_CORE = 0x0000001D;
        /// <summary>
        /// The Product MEDIUMBUSINES s_ Server MANAGEMENT
        /// </summary>
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 0x0000001E;
        /// <summary>
        /// The Product MEDIUMBUSINES s_ Server SECURITY
        /// </summary>
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 0x0000001F;
        /// <summary>
        /// The Product MEDIUMBUSINES s_ Server MESSAGING
        /// </summary>
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 0x00000020;
        /// <summary>
        /// The Product Server FOUNDATION
        /// </summary>
        private const int PRODUCT_SERVER_FOUNDATION = 0x00000021;
        /// <summary>
        /// The Product HOM e_ PREMIU m_ SERVER
        /// </summary>
        private const int PRODUCT_HOME_PREMIUM_SERVER = 0x00000022;
        /// <summary>
        /// The Product Server FO r_ SMALLBUSINES s_ V
        /// </summary>
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 0x00000023;
        /// <summary>
        /// The Product Standard Server V
        /// </summary>
        private const int PRODUCT_STANDARD_SERVER_V = 0x00000024;
        /// <summary>
        /// The Product Datacenter Server V
        /// </summary>
        private const int PRODUCT_DATACENTER_SERVER_V = 0x00000025;
        /// <summary>
        /// The Product ENTERPRIS e_ Server V
        /// </summary>
        private const int PRODUCT_ENTERPRISE_SERVER_V = 0x00000026;
        /// <summary>
        /// The Product Datacenter Server COR e_ V
        /// </summary>
        private const int PRODUCT_DATACENTER_SERVER_CORE_V = 0x00000027;
        /// <summary>
        /// The Product Standard Server COR e_ V
        /// </summary>
        private const int PRODUCT_STANDARD_SERVER_CORE_V = 0x00000028;
        /// <summary>
        /// The Product ENTERPRIS e_ Server COR e_ V
        /// </summary>
        private const int PRODUCT_ENTERPRISE_SERVER_CORE_V = 0x00000029;
        /// <summary>
        /// The Product HYPERV
        /// </summary>
        private const int PRODUCT_HYPERV = 0x0000002A;
        /// <summary>
        /// The Product Storage EXPRES s_ Server CORE
        /// </summary>
        private const int PRODUCT_STORAGE_EXPRESS_SERVER_CORE = 0x0000002B;
        /// <summary>
        /// The Product Storage Standard Server CORE
        /// </summary>
        private const int PRODUCT_STORAGE_STANDARD_SERVER_CORE = 0x0000002C;
        /// <summary>
        /// The Product Storage WORKGROU p_ Server CORE
        /// </summary>
        private const int PRODUCT_STORAGE_WORKGROUP_SERVER_CORE = 0x0000002D;
        /// <summary>
        /// The Product Storage ENTERPRIS e_ Server CORE
        /// </summary>
        private const int PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE = 0x0000002E;
        /// <summary>
        /// The Product STARTE r_ N
        /// </summary>
        private const int PRODUCT_STARTER_N = 0x0000002F;
        /// <summary>
        /// The Product PROFESSIONAL
        /// </summary>
        private const int PRODUCT_PROFESSIONAL = 0x00000030;
        /// <summary>
        /// The Product PROFESSIONA l_ N
        /// </summary>
        private const int PRODUCT_PROFESSIONAL_N = 0x00000031;
        /// <summary>
        /// The Product S b_ SOLUTIO n_ SERVER
        /// </summary>
        private const int PRODUCT_SB_SOLUTION_SERVER = 0x00000032;
        /// <summary>
        /// The Product Server FO r_ S b_ SOLUTIONS
        /// </summary>
        private const int PRODUCT_SERVER_FOR_SB_SOLUTIONS = 0x00000033;
        /// <summary>
        /// The Product Standard Server SOLUTIONS
        /// </summary>
        private const int PRODUCT_STANDARD_SERVER_SOLUTIONS = 0x00000034;
        /// <summary>
        /// The Product Standard Server SOLUTION s_ CORE
        /// </summary>
        private const int PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE = 0x00000035;
        /// <summary>
        /// The Product S b_ SOLUTIO n_ Server EM
        /// </summary>
        private const int PRODUCT_SB_SOLUTION_SERVER_EM = 0x00000036;
        /// <summary>
        /// The Product Server FO r_ S b_ SOLUTION s_ EM
        /// </summary>
        private const int PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM = 0x00000037;
        /// <summary>
        /// The Product SOLUTIO n_ EMBEDDEDSERVER
        /// </summary>
        private const int PRODUCT_SOLUTION_EMBEDDEDSERVER = 0x00000038;
        /// <summary>
        /// The Product SOLUTIO n_ EMBEDDEDServer CORE
        /// </summary>
        private const int PRODUCT_SOLUTION_EMBEDDEDSERVER_CORE = 0x00000039;
        //private const int ???? = 0x0000003A;
        /// <summary>
        /// The Product ESSENTIALBUSINES s_ Server MGMT
        /// </summary>
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT = 0x0000003B;
        /// <summary>
        /// The Product ESSENTIALBUSINES s_ Server ADDL
        /// </summary>
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL = 0x0000003C;
        /// <summary>
        /// The Product ESSENTIALBUSINES s_ Server MGMTSVC
        /// </summary>
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC = 0x0000003D;
        /// <summary>
        /// The Product ESSENTIALBUSINES s_ Server ADDLSVC
        /// </summary>
        private const int PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC = 0x0000003E;
        /// <summary>
        /// The Product SMALLBUSINES s_ Server PREMIU m_ CORE
        /// </summary>
        private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE = 0x0000003F;
        /// <summary>
        /// The Product CLUSTE r_ Server V
        /// </summary>
        private const int PRODUCT_CLUSTER_SERVER_V = 0x00000040;
        /// <summary>
        /// The Product EMBEDDED
        /// </summary>
        private const int PRODUCT_EMBEDDED = 0x00000041;
        /// <summary>
        /// The Product STARTE r_ E
        /// </summary>
        private const int PRODUCT_STARTER_E = 0x00000042;
        /// <summary>
        /// The Product HOM e_ BASI c_ E
        /// </summary>
        private const int PRODUCT_HOME_BASIC_E = 0x00000043;
        /// <summary>
        /// The Product HOM e_ PREMIU m_ E
        /// </summary>
        private const int PRODUCT_HOME_PREMIUM_E = 0x00000044;
        /// <summary>
        /// The Product PROFESSIONA l_ E
        /// </summary>
        private const int PRODUCT_PROFESSIONAL_E = 0x00000045;
        /// <summary>
        /// The Product ENTERPRIS e_ E
        /// </summary>
        private const int PRODUCT_ENTERPRISE_E = 0x00000046;
        /// <summary>
        /// The Product ULTIMAT e_ E
        /// </summary>
        private const int PRODUCT_ULTIMATE_E = 0x00000047;
        //private const int PRODUCT_UNLICENSED = 0xABCDABCD;
        #endregion PRODUCT

        #region VERSIONS
        /// <summary>
        /// The VE r_ N t_ WORKSTATION
        /// </summary>
        private const int VER_NT_WORKSTATION = 1;
        /// <summary>
        /// The VE r_ N t_ DOMAI n_ CONTROLLER
        /// </summary>
        private const int VER_NT_DOMAIN_CONTROLLER = 2;
        /// <summary>
        /// The VE r_ N t_ SERVER
        /// </summary>
        private const int VER_NT_SERVER = 3;
        /// <summary>
        /// The VE r_ SUIT e_ SMALLBUSINESS
        /// </summary>
        private const int VER_SUITE_SMALLBUSINESS = 1;
        /// <summary>
        /// The VE r_ SUIT e_ ENTERPRISE
        /// </summary>
        private const int VER_SUITE_ENTERPRISE = 2;
        /// <summary>
        /// The VE r_ SUIT e_ TERMINAL
        /// </summary>
        private const int VER_SUITE_TERMINAL = 16;
        /// <summary>
        /// The VE r_ SUIT e_ DATACENTER
        /// </summary>
        private const int VER_SUITE_DATACENTER = 128;
        /// <summary>
        /// The VE r_ SUIT e_ SINGLEUSERTS
        /// </summary>
        private const int VER_SUITE_SINGLEUSERTS = 256;
        /// <summary>
        /// The VE r_ SUIT e_ PERSONAL
        /// </summary>
        private const int VER_SUITE_PERSONAL = 512;
        /// <summary>
        /// The VE r_ SUIT e_ BLADE
        /// </summary>
        private const int VER_SUITE_BLADE = 1024;
        #endregion VERSIONS

        #endregion PINVOKE

        #region SERVICE PACK
        /// <summary>
        /// Gets the service pack information of the operating system running on this computer.
        /// </summary>
        /// <value>
        /// The service pack.
        /// </value>
        static public string ServicePack
        {
            get
            {
                string servicePack = String.Empty;
                OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();

                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

                if (GetVersionEx(ref osVersionInfo))
                {
                    servicePack = osVersionInfo.szCSDVersion;
                }

                return servicePack;
            }
        }
        #endregion SERVICE PACK

        #region VERSION
        #region BUILD
        /// <summary>
        /// Gets the build version number of the operating system running on this computer.
        /// </summary>
        /// <value>
        /// The build version.
        /// </value>
        static public int BuildVersion
        {
            get
            {
                return Environment.OSVersion.Version.Build;
            }
        }
        #endregion BUILD

        #region FULL
        #region STRING
        /// <summary>
        /// Gets the full version string of the operating system running on this computer.
        /// </summary>
        /// <value>
        /// The version string.
        /// </value>
        static public string VersionString
        {
            get
            {
                return Environment.OSVersion.Version.ToString();
            }
        }
        #endregion STRING

        #region VERSION
        /// <summary>
        /// Gets the full version of the operating system running on this computer.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        static public Version Version
        {
            get
            {
                return Environment.OSVersion.Version;
            }
        }
        #endregion VERSION
        #endregion FULL

        #region MAJOR
        /// <summary>
        /// Gets the major version number of the operating system running on this computer.
        /// </summary>
        /// <value>
        /// The major version.
        /// </value>
        static public int MajorVersion
        {
            get
            {
                return Environment.OSVersion.Version.Major;
            }
        }
        #endregion MAJOR

        #region MINOR
        /// <summary>
        /// Gets the minor version number of the operating system running on this computer.
        /// </summary>
        /// <value>
        /// The minor version.
        /// </value>
        static public int MinorVersion
        {
            get
            {
                return Environment.OSVersion.Version.Minor;
            }
        }
        #endregion MINOR

        #region REVISION
        /// <summary>
        /// Gets the revision version number of the operating system running on this computer.
        /// </summary>
        /// <value>
        /// The revision version.
        /// </value>
        static public int RevisionVersion
        {
            get
            {
                return Environment.OSVersion.Version.Revision;
            }
        }
        #endregion REVISION
        #endregion VERSION

        #region 64 BIT OS DETECTION
        /// <summary>
        /// Gets the is wow64 process delegate.
        /// </summary>
        /// <returns></returns>
        private static IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
        {
            IntPtr handle = LoadLibrary("kernel32");

            if (handle != IntPtr.Zero)
            {
                IntPtr fnPtr = GetProcAddress(handle, "IsWow64Process");

                if (fnPtr != IntPtr.Zero)
                {
                    return (IsWow64ProcessDelegate)Marshal.GetDelegateForFunctionPointer((IntPtr)fnPtr, typeof(IsWow64ProcessDelegate));
                }
            }

            return null;
        }

        /// <summary>
        /// Is32s the bit process on64 bit processor.
        /// </summary>
        /// <returns></returns>
        private static bool Is32BitProcessOn64BitProcessor()
        {
            IsWow64ProcessDelegate fnDelegate = GetIsWow64ProcessDelegate();

            if (fnDelegate == null)
            {
                return false;
            }

            bool isWow64;
            bool retVal = fnDelegate.Invoke(Process.GetCurrentProcess().Handle, out isWow64);

            if (retVal == false)
            {
                return false;
            }

            return isWow64;
        }
        #endregion 64 BIT OS DETECTION
    }
}
