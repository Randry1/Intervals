using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using intervals.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace intervals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Intervals");

            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1 ,20, 2,1, 0),
                    IsActual = true,
                    TagId = "t1"
                },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 3,1, 0),
                //     IsActual = false,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 4,1, 0),
                //     IsActual = true,
                //     TagId = "t2"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 5,1, 0),
                //     IsActual = true,
                //     TagId = "t2"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 6,1, 0),
                //     IsActual = true,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 7,1, 0),
                //     IsActual = false,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 8,1, 0),
                //     IsActual = true,
                //     TagId = "t3"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 9,1, 0),
                //     IsActual = true,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 10,1, 0),
                //     IsActual = true,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 11,1, 0),
                //     IsActual = true,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 12,1, 0),
                //     IsActual = true,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 13,1, 0),
                //     IsActual = true,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 14,1, 0),
                //     IsActual = true,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 15,1, 0),
                //     IsActual = true,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 16,1, 0),
                //     IsActual = true,
                //     TagId = "t1"
                // },
                // new EmployeeTag()
                // {
                //     TimeStamp = new DateTime(2024, 1 ,20, 17,1, 0),
                //     IsActual = false,
                //     TagId = "t1"
                // },
            };
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1 ,20, 1,1, 0),
                IsActual = false,
                TagId = "t1"
            };

            var from = new DateTime(2024, 1 ,20, 1,1, 0);
            var to = new DateTime(2024, 1 ,20, 20,1, 0);

            var intervals = GetIntervals(employeeTags, beforeEmployeeTag, from, to);

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
            
            var employeeTagsArray = employeeTags.ToImmutableArray();
            
            Console.WriteLine($"before \t {beforeEmployeeTag.ToString()}");

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
