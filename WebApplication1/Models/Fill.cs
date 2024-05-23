namespace WebApplication1.Models;

public  static class Fill
{
  public static void fill(Appointment appointment, MedCard<string, MedicalRecord> medicalRecords)
    {
        Pediatrician ped1 = new Pediatrician("Drs. Melnyk", "Pediatrics");
        ped1.AddSchedule(new KeyValuePair<int, int>(1, 9));   // Monday = 1, ..., Sunday = 7; second parameter is the hour
        ped1.AddSchedule(new KeyValuePair<int, int>(3, 10));
        ped1.AddSchedule(new KeyValuePair<int, int>(5, 11));

        Pediatrician ped2 = new Pediatrician("Dr. Kovalenko", "Pediatrics");
        ped2.AddSchedule(new KeyValuePair<int, int>(2, 14));
        ped2.AddSchedule(new KeyValuePair<int, int>(4, 12));
        ped2.AddSchedule(new KeyValuePair<int, int>(6, 13));

        Surgeon sur1 = new Surgeon("Drs. Bondarenko", "Surgery");
        sur1.AddSchedule(new KeyValuePair<int, int>(1, 10));
        sur1.AddSchedule(new KeyValuePair<int, int>(3, 14));
        sur1.AddSchedule(new KeyValuePair<int, int>(5, 8));

        Surgeon sur2 = new Surgeon("Dr. Shevchenko", "Surgery");
        sur2.AddSchedule(new KeyValuePair<int, int>(2, 10));
        sur2.AddSchedule(new KeyValuePair<int, int>(4, 15));
        sur2.AddSchedule(new KeyValuePair<int, int>(6, 11));

        Ophthalmologist oph1 = new Ophthalmologist("Drs. Boyko", "Ophthalmology");
        oph1.AddSchedule(new KeyValuePair<int, int>(1, 14));
        oph1.AddSchedule(new KeyValuePair<int, int>(3, 12));
        oph1.AddSchedule(new KeyValuePair<int, int>(5, 11));

        Ophthalmologist oph2 = new Ophthalmologist("Dr. Kovalchuk", "Ophthalmology");
        oph2.AddSchedule(new KeyValuePair<int, int>(2, 9));
        oph2.AddSchedule(new KeyValuePair<int, int>(4, 9));
        oph2.AddSchedule(new KeyValuePair<int, int>(5, 17));

        Psychiatrist psy1 = new Psychiatrist("Drs. Koval", "Psychiatry");
        psy1.AddSchedule(new KeyValuePair<int, int>(1, 11));
        psy1.AddSchedule(new KeyValuePair<int, int>(3, 11));
        psy1.AddSchedule(new KeyValuePair<int, int>(5, 13));

        Psychiatrist psy2 = new Psychiatrist("Dr. Tkachenko", "Psychiatry");
        psy2.AddSchedule(new KeyValuePair<int, int>(2, 11));
        psy2.AddSchedule(new KeyValuePair<int, int>(4, 12));
        psy2.AddSchedule(new KeyValuePair<int, int>(6, 11));

        EnTdoctor ent1 = new EnTdoctor("Drs. Kravchenko", "Otolaryngology");
        ent1.AddSchedule(new KeyValuePair<int, int>(2, 14));
        ent1.AddSchedule(new KeyValuePair<int, int>(3, 12));

        EnTdoctor ent2 = new EnTdoctor("Dr. Oliinyk", "Otolaryngology");
        ent2.AddSchedule(new KeyValuePair<int, int>(1, 10));
        ent2.AddSchedule(new KeyValuePair<int, int>(4, 8));

        MedicalRecord patient1 = new MedicalRecord();
        string id1 = DataGenerator.GenerateRandomId();
        patient1.FullName = "Ivan Ivanenko";
        patient1.DateOfBirth = new Data(2000, 1, 1);
        medicalRecords.Insert(id1,  patient1);

        MedicalRecord patient2 = new MedicalRecord();
        string id2 = DataGenerator.GenerateRandomId();
        patient2.FullName = "Petro Petrenko";
        patient2.DateOfBirth = new Data(1980, 2, 2);
        medicalRecords.Insert(id2, patient2);

        MedicalRecord patient3 = new MedicalRecord();
        string id3 = DataGenerator.GenerateRandomId();
        patient3.FullName = "Olena Oleksandrenko";
        patient3.DateOfBirth = new Data(1990, 3, 3);
        medicalRecords.Insert(id3, patient3);

        appointment.AddDoctor(ped1);
        appointment.AddDoctor(ped2);
        appointment.AddDoctor(sur1);
        appointment.AddDoctor(sur2);
        appointment.AddDoctor(oph1);
        appointment.AddDoctor(oph2);
        appointment.AddDoctor(psy1);
        appointment.AddDoctor(psy2);
        appointment.AddDoctor(ent1);
        appointment.AddDoctor(ent2);
    }
}