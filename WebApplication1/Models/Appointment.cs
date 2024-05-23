namespace WebApplication1.Models;

using System;
using System.Collections.Generic;

public class Appointment
{
    private Dictionary<string, List<Doctor>> _doctors = new();

    public void AddDoctor(Doctor doctor)
    {
        if (!_doctors.ContainsKey(doctor.GetSpecialization()))
        {
            _doctors[doctor.GetSpecialization()] = new List<Doctor>();
        }
        _doctors[doctor.GetSpecialization()].Add(doctor);
    }

    public DateTime FindNearestAvailableDateTime(string specialization)
    {
        DateTime currentTime = DateTime.Now;
        int currentDayOfWeek = (int)currentTime.DayOfWeek == 0 ? 7 : (int)currentTime.DayOfWeek;
        int currentHour = currentTime.Hour;
        int currentMinute = currentTime.Minute;

        bool found = false;
        foreach (Doctor doctor in _doctors[specialization])
        {
            foreach (var schedule in doctor.GetSchedule())
            {
                int targetDayOfWeek = schedule.Key;
                int targetHour = schedule.Value;

                if (targetDayOfWeek > currentDayOfWeek ||
                    (targetDayOfWeek == currentDayOfWeek && targetHour > currentHour) ||
                    (targetDayOfWeek == currentDayOfWeek && targetHour == currentHour && schedule.Value > currentMinute))
                {
                    int daysToAdd = targetDayOfWeek - currentDayOfWeek;
                    if (daysToAdd < 0)
                    {
                        daysToAdd += 7;
                    }

                    DateTime nextDate = currentTime.AddDays(daysToAdd);
                    nextDate = nextDate.Date + TimeSpan.FromHours(targetHour);

                    found = true;
                    return nextDate;
                }
            }
        }

        if (!found && _doctors.ContainsKey(specialization) && _doctors[specialization].Count > 0)
        {
            int daysToAdd = 7 - currentDayOfWeek + _doctors[specialization][0].GetSchedule()[0].Key;
            DateTime nextDate = currentTime.AddDays(daysToAdd);
            nextDate = nextDate.Date + TimeSpan.FromHours(_doctors[specialization][0].GetSchedule()[0].Value);

            return nextDate;
        }

        return DateTime.MinValue;
    }

    public List<KeyValuePair<string, Data>> GetAppointments(MedicalRecord patient)
    {
        return patient.Appointments;
    }

    public bool AddAppointment(MedicalRecord patient, string doctorName, Data dateTime)
    {
        foreach (var appointment in patient.Appointments)
        {
            if (appointment.Value == dateTime)
            {
                Console.WriteLine("Appointment time is already taken. Please choose another time.");
                return false;
            }
        }

        patient.Appointments.Add(new KeyValuePair<string, Data>(doctorName, dateTime));
        return true;
    }

    public void DisplayDoctors(string specialization)
    {
        if (_doctors.ContainsKey(specialization))
        {
            foreach (var doctor in _doctors[specialization])
            {
                doctor.DisplayInfo();
                Console.WriteLine();
            }
        }
    }

    public void DisplaySpecializations(Appointment appointment)
    {
        HashSet<string> specializations = new HashSet<string>();
        foreach (var pair in appointment._doctors)
        {
            foreach (Doctor doctor in pair.Value)
            {
                specializations.Add(doctor.GetSpecialization());
            }
        }
        Console.WriteLine("Available specializations:");
        foreach (string specialization in specializations)
        {
            Console.WriteLine("- " + specialization);
        }
    }
}
