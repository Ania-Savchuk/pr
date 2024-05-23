namespace WebApplication1
{
    struct DateTime
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        public bool IsEqual(DateTime other)
        {
            return Year == other.Year && Month == other.Month && Day == other.Day &&
                   Hour == other.Hour && Minute == other.Minute;
        }
    }

    class MedicalRecord
    {
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<KeyValuePair<string, DateTime>> Appointments { get; set; } = new List<KeyValuePair<string, DateTime>>();

        public string GetBirthdate()
        {
            return FormatDate(DateOfBirth);
        }
        private string FormatDate(DateTime date)
        {
            return $"{date.Day}.{date.Month}.{date.Year}";
        }
        public string GetDayOfWeek(int day)
        {
            return Enum.GetName(typeof(DayOfWeek), (day - 1) % 7);
        }
    }

    class MedCard<TKey, TValue>
    {
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Next { get; set; }

            public Node(string key, MedicalRecord value)
            {
                Key = key;
                Value = value;
                Next = null;
            }
        }

        private const int Size = 1000000;
        private Node[] table;

        public MedCard()
        {
            table = new Node[Size];
        }

        private int Hash(TKey key)
        {
            return key.GetHashCode() % Size;
        }

        public void Insert(string key, MedicalRecord value)
        {
            int index = Hash(key);
            Node newNode = new Node(key, value);
            if (table[index] == null)
            {
                table[index] = newNode;
            }
            else
            {
                Node current = table[index];
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public bool Find(TKey key, out TValue value)
        {
            int index = Hash(key);
            Node current = table[index];
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    value = current.Value;
                    return true;
                }
                current = current.Next;
            }
            value = default(TValue);
            return false;
        }

        public void Update(TKey key, TValue value)
        {
            int index = Hash(key);
            Node current = table[index];
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    current.Value = value;
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine("Key not found in hash table");
        }

        public void Remove(TKey key)
        {
            int index = Hash(key);
            Node current = table[index];
            Node prev = null;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (prev != null)
                    {
                        prev.Next = current.Next;
                    }
                    else
                    {
                        table[index] = current.Next;
                    }
                    return;
                }
                prev = current;
                current = current.Next;
            }
        }

        public void MakeMedCard(out MedicalRecord patient)
        {
            patient = new MedicalRecord();
            Console.Write("Enter the patient's full name: ");
            patient.FullName = Console.ReadLine();
            Console.Write("Enter the patient's date of birth (dd mm yyyy): ");
            var dobInput = Console.ReadLine()?.Split(' ');
            patient.DateOfBirth = new DateTime
            {
                Day = int.Parse(dobInput[0]),
                Month = int.Parse(dobInput[1]),
                Year = int.Parse(dobInput[2])
            };
            string patientId = GenerateRandomId();
            Console.WriteLine($"Your patient's personal code: {patientId}");
            Insert(patientId, patient);
            Console.WriteLine($"Patient card added successfully! Patient ID: {patientId}");
        }

        public List<TKey> GetAllKeys()
        {
            List<TKey> keys = new List<TKey>();
            foreach (var node in table)
            {
                Node current = node;
                while (current != null)
                {
                    keys.Add(current.Key);
                    current = current.Next;
                }
            }
            return keys;
        }

        public static string GenerateRandomId()
        {
            Random rand = new Random();
            return string.Concat(Enumerable.Range(0, 8).Select(_ => rand.Next(0, 10).ToString()));
        }

        private static string FormatDate(DateTime date)
        {
            return $"{date.Day}.{date.Month}.{date.Year} {date.Hour}:{date.Minute:D2}";
        }
    }

    abstract class Doctor
    {
        public string Name { get; }
        public string Specialization { get; }
        private List<KeyValuePair<int, int>> Schedule { get; set; } = new List<KeyValuePair<int, int>>();

        protected Doctor(string name, string specialization)
        {
            Name = name;
            Specialization = specialization;
        }
        public void AddSchedule(KeyValuePair<int, int> schedule)
        {
            Schedule.Add(schedule);
        }
        public abstract void DisplayInfo();
        public abstract List<KeyValuePair<int, int>> GetSchedule();
        public abstract void BookAppointment(DateTime time);
    }

    class Pediatrician : Doctor
    {
        private readonly List<KeyValuePair<int, int>> schedule = new List<KeyValuePair<int, int>>();

        public Pediatrician(string name, string specialization) : base(name, specialization) { }

        public void AddSchedule(KeyValuePair<int, int> time)
        {
            schedule.Add(time);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine("Schedule:");
            foreach (var scheduleItem in schedule)
            {
                Console.WriteLine($"{GetDayOfWeek(scheduleItem.Key)} at {scheduleItem.Value}:00");
            }
        }

        public override void BookAppointment(DateTime time)
        {
            Console.WriteLine($"Appointment booked with {Name} at {FormatDate(time)}");
        }

        public override List<KeyValuePair<int, int>> GetSchedule()
        {
            return schedule;
        }

        private static string GetDayOfWeek(int day)
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

        private static string FormatDate(DateTime date)
        {
            return $"{date.Day}.{date.Month}.{date.Year} {date.Hour}:{date.Minute:D2}";
        }
    }

    class Surgeon : Doctor
    {
        private readonly List<KeyValuePair<int, int>> schedule = new List<KeyValuePair<int, int>>();

        public Surgeon(string name, string specialization) : base(name, specialization) { }

        public void AddSchedule(KeyValuePair<int, int> time)
        {
            schedule.Add(time);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine("Schedule:");
            foreach (var scheduleItem in schedule)
            {
                Console.WriteLine($"{GetDayOfWeek(scheduleItem.Key)} at {scheduleItem.Value}:00");
            }
        }

        public override void BookAppointment(DateTime time)
        {
            Console.WriteLine($"Appointment booked with {Name} at {FormatDate(time)}");
        }

        public override List<KeyValuePair<int, int>> GetSchedule()
        {
            return schedule;
        }

        private static string GetDayOfWeek(int day)
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

        private static string FormatDate(DateTime date)
        {
            return $"{date.Day}.{date.Month}.{date.Year} {date.Hour}:{date.Minute:D2}";
        }
    }

    class Ophthalmologist : Doctor
    {
        private readonly List<KeyValuePair<int, int>> schedule = new List<KeyValuePair<int, int>>();

        public Ophthalmologist(string name, string specialization) : base(name, specialization) { }

        public void AddSchedule(KeyValuePair<int, int> time)
        {
            schedule.Add(time);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine("Schedule:");
            foreach (var scheduleItem in schedule)
            {
                Console.WriteLine($"{GetDayOfWeek(scheduleItem.Key)} at {scheduleItem.Value}:00");
            }
        }

        public override void BookAppointment(DateTime time)
        {
            Console.WriteLine($"Appointment booked with {Name} at {FormatDate(time)}");
        }

        public override List<KeyValuePair<int, int>> GetSchedule()
        {
            return schedule;
        }

        private static string GetDayOfWeek(int day)
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

        private static string FormatDate(DateTime date)
        {
            return $"{date.Day}.{date.Month}.{date.Year} {date.Hour}:{date.Minute:D2}";
        }
    }

    class Psychiatrist : Doctor
    {
        private readonly List<KeyValuePair<int, int>> schedule = new List<KeyValuePair<int, int>>();

        public Psychiatrist(string name, string specialization) : base(name, specialization) { }

        public void AddSchedule(KeyValuePair<int, int> time)
        {
            schedule.Add(time);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine("Schedule:");
            foreach (var scheduleItem in schedule)
            {
                Console.WriteLine($"{GetDayOfWeek(scheduleItem.Key)} at {scheduleItem.Value}:00");
            }
        }

        public override void BookAppointment(DateTime time)
        {
            Console.WriteLine($"Appointment booked with {Name} at {FormatDate(time)}");
        }

        public override List<KeyValuePair<int, int>> GetSchedule()
        {
            return schedule;
        }

        private static string GetDayOfWeek(int day)
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

        private static string FormatDate(DateTime date)
        {
            return $"{date.Day}.{date.Month}.{date.Year} {date.Hour}:{date.Minute:D2}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MedCard<string, MedicalRecord> medCard = new MedCard<string, MedicalRecord>();
            List<Doctor> doctors = new List<Doctor>
            {
                new Pediatrician("John Doe", "Pediatrician"),
                new Surgeon("Jane Smith", "Surgeon"),
                new Ophthalmologist("Jim Brown", "Ophthalmologist"),
                new Psychiatrist("Joan White", "Psychiatrist")
            };

            doctors[0].AddSchedule(new KeyValuePair<int, int>(1, 9));
            doctors[0].AddSchedule(new KeyValuePair<int, int>(3, 11));
            doctors[0].AddSchedule(new KeyValuePair<int, int>(5, 10));
            doctors[1].AddSchedule(new KeyValuePair<int, int>(2, 14));
            doctors[1].AddSchedule(new KeyValuePair<int, int>(4, 16));
            doctors[2].AddSchedule(new KeyValuePair<int, int>(1, 13));
            doctors[2].AddSchedule(new KeyValuePair<int, int>(3, 15));
            doctors[3].AddSchedule(new KeyValuePair<int, int>(5, 9));
            doctors[3].AddSchedule(new KeyValuePair<int, int>(7, 11));

            Console.WriteLine("Enter the patient's full name: ");
            string fullName = Console.ReadLine();
            Console.WriteLine("Enter the patient's date of birth (dd mm yyyy): ");
            var dobInput = Console.ReadLine().Split(' ');
            DateTime dob = new DateTime
            {
                Day = int.Parse(dobInput[0]),
                Month = int.Parse(dobInput[1]),
                Year = int.Parse(dobInput[2])
            };

            MedicalRecord patient = new MedicalRecord
            {
                FullName = fullName,
                DateOfBirth = dob
            };

            string patientID = MedCard<string, MedicalRecord>.GenerateRandomId();
            medCard.Insert(patientID, patient);

            Console.WriteLine("Available doctors:");
            for (int i = 0; i < doctors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {doctors[i].Name} ({doctors[i].Specialization})");
            }

            Console.WriteLine("Select a doctor by number: ");
            int doctorIndex = int.Parse(Console.ReadLine()) - 1;

            Doctor selectedDoctor = doctors[doctorIndex];

            Console.WriteLine("Doctor's schedule:");
            var schedule = selectedDoctor.GetSchedule();
            for (int i = 0; i < schedule.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectedDoctor.GetDayOfWeek(schedule[i].Key)} at {schedule[i].Value}:00");
            }

            Console.WriteLine("Select a time slot by number: ");
            int timeSlotIndex = int.Parse(Console.ReadLine()) - 1;

            DateTime appointmentTime = new DateTime
            {
                Day = schedule[timeSlotIndex].Key,
                Hour = schedule[timeSlotIndex].Value,
                Minute = 0,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year
            };

            patient.Appointments.Add(new KeyValuePair<string, DateTime>(selectedDoctor.Name, appointmentTime));
            selectedDoctor.BookAppointment(appointmentTime);

            Console.WriteLine("Appointment booked successfully!");

            Console.WriteLine("All patients:");
            var allKeys = medCard.GetAllKeys();
            foreach (var key in allKeys)
            {
                if (medCard.Find(key, out MedicalRecord record))
                {
                    Console.WriteLine($"Patient ID: {key}, Name: {record.FullName}, Birthdate: {record.GetBirthdate()}");
                }
            }
        }
    }
}

