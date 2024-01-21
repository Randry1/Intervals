using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace intervals.Models
{
    public class EmployeeActivityInterval
    {
        public DateTimeOffset From;
        public DateTimeOffset To;
        public string TagId;

        public override string ToString()
        {
            return $"{From.ToString("HH")} | {To.ToString("HH")} | {TagId}";
        }  
        
        public static List<EmployeeActivityInterval> GetIntervals(IEnumerable<EmployeeTag> employeeTags,
            EmployeeTag beforeEmployeeTag, DateTime from, DateTime to)
        {
            var outIntervals = new List<EmployeeActivityInterval>();
            
            var interval = new EmployeeActivityInterval();

            if ((employeeTags == null || employeeTags.Count() == 0) 
                && beforeEmployeeTag != null && beforeEmployeeTag.IsActual == true)
            {
                interval.From = from;
                interval.To = to;
                interval.TagId = beforeEmployeeTag.TagId;
                
                outIntervals.Add(interval);

                return outIntervals;
            }

            if (employeeTags == null && (beforeEmployeeTag == null || !beforeEmployeeTag.IsActual))
            {
                return outIntervals;
            }
            
            var employeeTagsArray = employeeTags.ToImmutableArray();
            
            int i = 0;
            EmployeeTag prev = null;
            while (i < employeeTagsArray.Length)
            {
                var current = employeeTagsArray[i];
                
                EmployeeTag next;
                try
                {
                    next = employeeTagsArray[i + 1];
                }
                catch
                {
                    next = null;;
                }

                #region EmployeeTag.Count ==  1 || 0
                
                if (employeeTagsArray.Count() == 1)
                {
                    if (beforeEmployeeTag != null && beforeEmployeeTag.IsActual)
                    { // 10
                        interval.From = from;
                        interval.TagId = beforeEmployeeTag.TagId;

                        if (current.IsActual == false)
                        {
                            interval.To = current.TimeStamp;
                            outIntervals.Add(interval);
                            interval = new EmployeeActivityInterval();
                        }
                        else // current.IsActual == true
                        {
                            if (beforeEmployeeTag.TagId == current.TagId)
                            {
                                interval.To = to;
                                outIntervals.Add(interval);
                                interval = new EmployeeActivityInterval();
                            }
                            else // beforeEmployeeTag.TagId != current.TagId
                            {
                                interval.To = current.TimeStamp;
                                outIntervals.Add(interval);
                                interval = new EmployeeActivityInterval();

                                interval.From = current.TimeStamp;
                                interval.To = to;
                                interval.TagId = current.TagId;
                                outIntervals.Add(interval);
                            }
                        }
                        
                    }
                    else
                    {
                        if (current.IsActual == true)
                        {
                            interval.From = current.TimeStamp;
                            interval.To = to;
                            interval.TagId = current.TagId;
                            outIntervals.Add(interval);
                        }
                    }

                    i += 1;
                    continue;
                }
                #endregion

                #region EmployeeTags.Count > 0

                if (employeeTagsArray.Count() > 0) 
                {
                    if (i == 0 
                        && beforeEmployeeTag != null
                        && beforeEmployeeTag.IsActual
                        && string.IsNullOrEmpty(interval.TagId))
                    {
                        interval.From = from;
                        interval.TagId = beforeEmployeeTag.TagId;
                    }

                    if (!string.IsNullOrEmpty(interval.TagId))
                    {
                        if (current.IsActual)
                        {
                            if (interval.TagId != current.TagId)
                            {
                                interval.To = current.TimeStamp;
                                outIntervals.Add(interval);
                                interval = new EmployeeActivityInterval();

                                interval.From = current.TimeStamp;
                                interval.TagId = current.TagId;
                            }
                        }
                        else // current.IsActual == false
                        {
                            interval.To = current.TimeStamp;
                            outIntervals.Add(interval);
                            interval = new EmployeeActivityInterval();
                        }
                    }
                    else // interval.TagId == null
                    {
                        if (current.IsActual)
                        {
                            interval.From = current.TimeStamp;
                            interval.TagId = current.TagId;
                        }
                    }
                }

                #endregion
                
                // if (prev == null && current.IsActual == false 
                //                  && beforeEmployeeTag != null 
                //                  && beforeEmployeeTag.IsActual == true
                //                  && !string.IsNullOrEmpty(interval.TagId))
                // {
                //     interval.To = current.TimeStamp;
                //     outIntervals.Add(interval);
                //     interval = new EmployeeActivityInterval();
                //     
                //     prev = current;
                //     i += 1;
                //     continue;
                //     
                // }
                //
                // if (string.IsNullOrEmpty(interval.TagId) 
                //     && next == null && current.IsActual == true)
                // {
                //     interval.From = current.TimeStamp;
                //     interval.To = to;
                //     interval.TagId = current.TagId;
                //     outIntervals.Add(interval);
                //     continue;
                //     
                // }
                
                if (next == null 
                         && !string.IsNullOrEmpty(interval.TagId))
                {
                    interval.To = to;
                    outIntervals.Add(interval);
                }
                
                Console.WriteLine($"      \t {current}");

                prev = current;
                i += 1;
            }
            return outIntervals;
        }
        
    }
}