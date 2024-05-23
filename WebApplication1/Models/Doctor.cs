namespace WebApplication1.Models;

using System;
using System.Collections.Generic;

public abstract class Doctor(string name, string specialization)
{
    protected string Name = name;
    protected string Specialization = specialization;

    public string GetName()
    {
        return Name;
    }

    public string GetSpecialization()
    {
        return Specialization;
    }
    
    public string GetDayOfWeek(int day)
    {
        return day switch
        {
            1 => "Monday",
            2 => "Tuesday",
            3 => "Wednesday",
            4 => "Thursday",
            5 => "Friday",
            6 => "Saturday",
            7 => "Sunday",
            _ => "Unknown"
        };
    }

    public abstract void DisplayInfo();
    public abstract List<KeyValuePair<int, int>> GetSchedule();
    public abstract void BookAppointment(Data time);
}
