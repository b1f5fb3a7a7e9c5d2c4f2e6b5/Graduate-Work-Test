using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace GraduateWork_Test
{
    public class PerfomanceInfoData
    {
        // data in pages
        public long CommitTotalPages;       //
        public long CommitLimitPages;       //
        public long CommitPeakPages;        //

        // data in bytes
        public long PhysicalTotalMb;        // Максимальное значение физической памяти
        public long PhysicalAvailableMb;    // Текущее значение физической памяти
        public decimal PhysicalPercentFree; // избыточные данные
        public decimal PhysicalOccupied;    // избыточные данные

        public long SystemCacheMb;          //
        public long KernelTotalMb;          // Объем физической памяти, %
        public long KernelPagedMb;          //
        public long KernelNonPagedMb;       //
        public long PageSizeMb;             //

        // counters
        public int HandlesCount;            //
        public int ProcessCount;            //
        public int ThreadCount;             //

        public void PerfomanceInfoDataXml()
        {
            var xdoc = new XDocument(new XElement("elements",
                new XElement("elemen",
                    new XAttribute("name", "Physical"),
                    new XElement("PhysicalTotalMb", PhysicalTotalMb),
                    new XElement("PhysicalAvailableMb", PhysicalAvailableMb),
                    new XElement("PhysicalPercentFree", PhysicalPercentFree),
                    new XElement("PhysicalOccupied", PhysicalOccupied))));
            xdoc.Save(@"..\..\PerfomanceInfoData.xml");
        }
    }

    public static class PsApiWrapper
    {
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPerformanceInfo([Out] out PsApiPerformanceInformation performanceInformation, [In] int size);

        [StructLayout(LayoutKind.Sequential)]
        private struct PsApiPerformanceInformation
        {
            public int Size;
            public IntPtr CommitTotal;
            public IntPtr CommitLimit;
            public IntPtr CommitPeak;
            public IntPtr PhysicalTotal;
            public IntPtr PhysicalAvailable;
            public IntPtr SystemCache;
            public IntPtr KernelTotal;
            public IntPtr KernelPaged;
            public IntPtr KernelNonPaged;
            public IntPtr PageSize;
            public int HandlesCount;
            public int ProcessCount;
            public int ThreadCount;
        }

        public static PerfomanceInfoData GetPerformanceInfo()
        {
            var data = new PerfomanceInfoData();
            var perfInfo = new PsApiPerformanceInformation();

            if (!GetPerformanceInfo(out perfInfo, Marshal.SizeOf(perfInfo))) return data;

            // data in pages
            data.CommitTotalPages = perfInfo.CommitTotal.ToInt64();
            data.CommitLimitPages = perfInfo.CommitLimit.ToInt64();
            data.CommitPeakPages = perfInfo.CommitPeak.ToInt64();

            // data in bytes
            var pageSize = perfInfo.PageSize.ToInt64();

            data.PhysicalTotalMb = perfInfo.PhysicalTotal.ToInt64() * pageSize / 1048576;
            data.PhysicalAvailableMb = perfInfo.PhysicalAvailable.ToInt64() * pageSize / 1048576;
            data.PhysicalPercentFree = (decimal) data.PhysicalAvailableMb / data.PhysicalTotalMb * 100;
            data.PhysicalOccupied = 100 - data.PhysicalPercentFree;

            data.SystemCacheMb = perfInfo.SystemCache.ToInt64() * pageSize / 1048576;
            data.KernelTotalMb = perfInfo.KernelTotal.ToInt64() * pageSize / 1048576;
            data.KernelPagedMb = perfInfo.KernelPaged.ToInt64() * pageSize / 1048576;
            data.KernelNonPagedMb = perfInfo.KernelNonPaged.ToInt64() * pageSize / 1048576;
            data.PageSizeMb = pageSize / 1048576;

            // counters
            data.HandlesCount = perfInfo.HandlesCount;
            data.ProcessCount = perfInfo.ProcessCount;
            data.ThreadCount = perfInfo.ThreadCount;

            return data;
        }
    }
}
