using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SILP.Model
{
    public class Employee
    {
        [BsonId]
        public string Id { get; set; }
        public List<DateTime> InOfficeTimes { get; set; } = new List<DateTime>();
        public List<double> TasksTimes { get; set; } = new List<double>();
        public string UserName { get; set; } = string.Empty;
    }
}
