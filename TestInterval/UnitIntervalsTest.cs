using System;
using System.Collections.Generic;
using System.Linq;
using intervals.Models;
using Xunit;

namespace TestInterval
{
    public class UnitIntervalsTest
    {
        [Fact]
        public void IntervalsBefore_fEmployeeTags_t()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = false,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = true,
                    TagId = "t1"
                }
            };
            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Single(intervals);
            Assert.Equal(3, intervals.First().From.Hour);
            Assert.Equal(22, intervals.First().To.Hour);
        }
        
        [Fact]
        public void IntervalsBefore_fEmployeeTags_t_f()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = false,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = true,
                    TagId = "t1"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 4, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                }
            };
            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Single(intervals);
            Assert.Equal(3, intervals.First().From.Hour);
            Assert.Equal(4, intervals.First().To.Hour);
        }

        [Fact]
        public void IntervalsBefore_fEmployeeTags_f_t()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = false,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 4, 0, 0),
                    IsActual = true,
                    TagId = "t2"
                }
            };
            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Single(intervals);
            Assert.Equal(4, intervals.First().From.Hour);
            Assert.Equal(22, intervals.First().To.Hour);
            Assert.Equal("t2", intervals.First().TagId);
        }
        
        [Fact]
        public void IntervalsBefore_nullEmployeeTags_f_t()
        {
            // Arrange
            
            EmployeeTag beforeEmployeeTag = null;
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 4, 0, 0),
                    IsActual = true,
                    TagId = "t2"
                }
            };
            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Single(intervals);
            Assert.Equal(4, intervals.First().From.Hour);
            Assert.Equal(22, intervals.First().To.Hour);
            Assert.Equal("t2", intervals.First().TagId);
        }
        
        [Fact]
        public void IntervalsBefore_fEmployeeTags_f()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = false,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                }
            };

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Empty(intervals);
        }
        
        [Fact]
        public void IntervalsBefore_nullEmployeeTags_f()
        {
            // Arrange
            
            EmployeeTag beforeEmployeeTag = null;
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                }
            };

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Empty(intervals);
        }
        
        [Fact]
        public void IntervalsBefore_nullEmployeeTags_f_f()
        {
            // Arrange
            
            EmployeeTag beforeEmployeeTag = null;
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 4, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                },
            };

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Empty(intervals);
        }
        
        
        [Fact]
        public void IntervalsBefore_fEmployeeTags_null()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = false,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            List<EmployeeTag> employeeTags = null;

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Empty(intervals);

        }
                
        [Fact]
        public void IntervalsBefore_fEmployeeTags_empty()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = false,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            List<EmployeeTag> employeeTags = new List<EmployeeTag>();

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Empty(intervals);
        }
        
        
        [Fact]
        public void IntervalsBefore_fEmployeeTags_f_f()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = false,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 4, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                }
            };

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Empty(intervals);
        }
        
        [Fact]
        public void IntervalsBefore_tEmployeeTags_f()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = true,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                }
            };

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Single(intervals);
            Assert.Equal(2, intervals.First().From.Hour);
            Assert.Equal(3, intervals.First().To.Hour);
            Assert.Equal("t1", intervals.First().TagId);
        }
        
        [Fact]
        public void IntervalsBefore_tEmployeeTags_t_t_f()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = true,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = true,
                    TagId = "t1"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 4, 0, 0),
                    IsActual = true,
                    TagId = "t1"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 5, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                }
            };

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Single(intervals);
            Assert.Equal(2, intervals.First().From.Hour);
            Assert.Equal(5, intervals.First().To.Hour);
            Assert.Equal("t1", intervals.First().TagId);
        }
        
        [Fact]
        public void IntervalsBefore_tT1EmployeeTags_tT2_tT3_f()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = true,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = true,
                    TagId = "t2"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 4, 0, 0),
                    IsActual = true,
                    TagId = "t3"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 5, 0, 0),
                    IsActual = false,
                    TagId = "t3"
                }
            };

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Equal(3, intervals.Count);
            Assert.Equal(2, intervals.First().From.Hour);
            Assert.Equal(3, intervals.First().To.Hour);
            Assert.Equal("t1", intervals.First().TagId);
        }
        
        [Fact]
        public void IntervalsBefore_tT1EmployeeTags_fT1_tT2_fT2_tT1()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = true,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = false,
                    TagId = "t1"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 4, 0, 0),
                    IsActual = true,
                    TagId = "t2"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 5, 0, 0),
                    IsActual = false,
                    TagId = "t2"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 6, 0, 0),
                    IsActual = true,
                    TagId = "t1"
                }
            };

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Equal(3, intervals.Count);
            Assert.Equal(2, intervals.First().From.Hour);
            Assert.Equal(3, intervals.First().To.Hour);
            Assert.Equal("t1", intervals.First().TagId);
            Assert.Equal(6, intervals.Last().From.Hour);
            Assert.Equal(22, intervals.Last().To.Hour);
            Assert.Equal("t1", intervals.Last().TagId);
        }
        
        [Fact]
        public void IntervalsBefore_tEmployeeTags_f_f()
        {
            // Arrange
            
            var beforeEmployeeTag = new EmployeeTag()
            {
                TimeStamp = new DateTime(2024, 1, 20, 1, 0, 0),
                IsActual = true,
                TagId = "t1"
            };
            
            var from = new DateTime(2024, 1, 20, 2, 0, 0);
            
            var employeeTags = new List<EmployeeTag>()
            {
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 3, 0, 0),
                    IsActual = false,
                    TagId = "t2"
                },
                new EmployeeTag()
                {
                    TimeStamp = new DateTime(2024, 1, 20, 4, 0, 0),
                    IsActual = false,
                    TagId = "t2"
                }
            };

            var to = new DateTime(2024, 1, 20, 22, 0, 0);
            
            // Act
            var intervals = EmployeeActivityInterval.GetIntervals(employeeTags, beforeEmployeeTag, from, to);

            // Assert
            Assert.Single(intervals);
            Assert.Equal(2, intervals.First().From.Hour);
            Assert.Equal(3, intervals.First().To.Hour);
            Assert.Equal("t1", intervals.First().TagId);
        }
        
    }
}