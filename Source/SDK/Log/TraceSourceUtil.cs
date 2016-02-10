using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PayPal.Log
{
    /// <summary>
    /// Trace Source utility for the given Type having listener or the closest "parent" TraceRoute that has listeners
    /// </summary>
    internal static class TraceSourceUtil
    {
        private static object syncLock = new object();
        private static Dictionary<string, string> traceSourceWithListeners = new Dictionary<string, string>();

        /// <summary>
        /// Gets the TraceSource for the given Type having SourceLevels.All
        /// Returns null if there are no listeners configured for given Type or the closest "parent" TraceRoute that has listeners
        /// </summary>
        /// <param name="givenType"></param>
        /// <returns></returns>
        public static TraceSource GetTraceSource(Type givenType)
        {
            return GetTraceSource(givenType, SourceLevels.All);
        }

        /// <summary>
        /// Gets the TraceSource for the given Type and SourceLevels
        /// Returns null if there are no listeners configured for given Type or the closest "parent" TraceRoute that has listeners
        /// </summary>
        /// <param name="givenType"></param>
        /// <param name="sourceLevels"></param>
        /// <returns></returns>
        public static TraceSource GetTraceSource(Type givenType, SourceLevels sourceLevels)
        {
            TraceSource sourceTrace = GetTraceSourceWithListeners(givenType.FullName, sourceLevels);
            return sourceTrace;
        }

        /// <summary>
        /// Gets the closest "parent" TraceRoute that has listeners, or null otherwise
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sourceLevels"></param>
        /// <returns></returns>
        private static TraceSource GetTraceSourceWithListeners(string name, SourceLevels sourceLevels)
        {
            lock (syncLock)
            {
                TraceSource sourceTrace = null;
                string givenName;
                if (!traceSourceWithListeners.TryGetValue(name, out givenName))
                {
                    sourceTrace = GetTraceSourceWithListenersSyncLock(name, sourceLevels);
                    givenName = sourceTrace == null ? null : sourceTrace.Name;
                    traceSourceWithListeners[name] = givenName;
                }
                else if (givenName != null)
                {
                    sourceTrace = new TraceSource(givenName, sourceLevels);
                }
                return sourceTrace;
            }
        }

        /// <summary>
        /// Gets the closest "parent" TraceRoute that has listeners, or null otherwise
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sourceLevels"></param>
        /// <returns></returns>
        private static TraceSource GetTraceSourceWithListenersSyncLock(string name, SourceLevels sourceLevels)
        {
            string[] splitters = name.Split(new char[] { '.' }, StringSplitOptions.None);
            List<string> nameSplitList = new List<string>();
            StringBuilder builder = new StringBuilder();

            foreach (string split in splitters)
            {
                if (builder.Length > 0)
                {
                    builder.Append(".");
                }

                builder.Append(split);

                string partialName = builder.ToString();
                nameSplitList.Add(partialName);
            }

            nameSplitList.Reverse();

            foreach (string nameSplit in nameSplitList)
            {
                TraceSource sourceTrace;
                sourceTrace = new TraceSource(nameSplit, sourceLevels);

                if (sourceTrace.Listeners == null || sourceTrace.Listeners.Count == 0)
                {
                    sourceTrace.Close();
                    continue;
                }

                if (sourceTrace.Listeners.Count > 1)
                {
                    return sourceTrace;
                }
                TraceListener listenerTrace = sourceTrace.Listeners[0];

                if (!(listenerTrace is DefaultTraceListener))
                {
                    return sourceTrace;
                }

                if (!string.Equals(listenerTrace.Name, "Default", StringComparison.Ordinal))
                {
                    return sourceTrace;
                }
                sourceTrace.Close();
            }

            return null;
        }
    }
}
