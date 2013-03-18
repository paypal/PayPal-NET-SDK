using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayPal.Util
{
    /// <summary>
    /// Query Parameters
    /// </summary>
    public class QueryParameters
    {
        private static readonly String COUNT = "count";

        private static readonly String STARTID = "start_id";

        private static readonly String STARTINDEX = "start_index";

        private static readonly String STARTTIME = "start_time";

        private static readonly String ENDTIME = "end_time";

        private static readonly String PAYEEID = "payee_id";

        private static readonly String SORTBY = "sort_by";

        private static readonly String SORTORDER = "sort_order";

        private Dictionary<String, String> containerMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParameters"/> class.
        /// </summary>
        public QueryParameters()
        {
            containerMap = new Dictionary<String, String>();
        }

        /**
         * @return the containerMap
         */
        public Dictionary<String, String> GetMap()
        {
            return containerMap;
        }

        /**
         * Set the count
         * 
         * @param count
         *            Number of items to return.
         */
        public void SetCount(String count)
        {
            containerMap.Add(COUNT, count);
        }

        /**
         * Set the startId
         * 
         * @param startid
         *            Resource ID that indicates the starting resource to return.
         */
        public void SetStartId(String startId)
        {
            containerMap.Add(STARTID, startId);
        }

        /**
         * Set the start index
         * 
         * @param startIndex
         *            Start index of the resources to be returned. Typically used to
         *            jump to a specific position in the resource history based on
         *            its order.
         */
        public void SetStartIndex(String startIndex)
        {
            containerMap.Add(STARTINDEX, startIndex);
        }

        /**
         * Set the starttime
         * 
         * @param starttime
         *            Resource creation time that indicates the start of a range of
         *            results.
         */
        public void SetStartTime(String startTime)
        {
            containerMap.Add(STARTTIME, startTime);
        }

        /**
         * Set the endtime
         * 
         * @param endTime
         *            Resource creation time that indicates the end of a range of
         *            results.
         */
        public void SetEndTime(String endTime)
        {
            containerMap.Add(ENDTIME, endTime);
        }

        /**
         * Set the payee id
         * 
         * @param payeeId
         *            PayeeId
         */
        public void SetPayeeId(String payeeId)
        {
            containerMap.Add(PAYEEID, payeeId);
        }

        /// <summary>
        /// Sets the sort by based on create_time or update_time.
        /// </summary>
        /// <param name="sortBy">The sort by.</param>
        public void SetSortBy(String sortBy)
        {
            containerMap.Add(SORTBY, sortBy);
        }

        /**
         * Set the sort order
         * 
         * @param sortOrder
         *            Sort based on order of results. Options include asc for
         *            ascending order or dec for descending order.
         */
        public void SetSortOrder(String sortOrder)
        {
            containerMap.Add(SORTORDER, sortOrder);
        }
    }    
}