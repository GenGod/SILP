using System;
using System.Collections.Generic;

//Interface for accessing data source
namespace SILP
{
    interface IGettingData
    {
        List<DateTime> GetPRISMData(int dayOfTheWeek = -1); //If dayOfTheWeek == -1, method returns data of all days
        List<DateTime> GetTimeReportsData(int dayOfTheWeek = -1); //If dayOfTheWeek == -1, method returns data of all days
    }
}