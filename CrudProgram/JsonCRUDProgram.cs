using CrudProgram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrudProgram
{

  // Use the PatientDetails class from CrudProgram namespace
        //internal class Exercises
        
            // Remove the local PatientDetails class to avoid conflicts.
            // Use the global PatientDetails class from CrudProgram.

            public class JsonCRUDprogram     // <--- internal class  JsonCRUDprogram

    {
                private List<PatientDetails> patients = new List<PatientDetails>();
                public string filePath = Path.Combine(/*AppDomain.CurrentDomain.BaseDirectory,*/ "Data", "data.json");

                public JsonCRUDprogram()
                {
                    string dir = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    patients = LoadData();
                }

                public void ChooseOption()
                {
                    int choose = 0;
                    while (choose <= 6)
                    {
                        Console.WriteLine("\nChoose The Options");
                        Console.WriteLine("1. Add Patient Details");
                        Console.WriteLine("2. Update Details");
                        Console.WriteLine("3. Remove Details");
                        Console.WriteLine("4. Print Details");
                        Console.WriteLine("5. Search Details");
                        Console.WriteLine("6. Exit");
                        Console.WriteLine("Enter The Option Number (1 to 6):");

                        if (!int.TryParse(Console.ReadLine(), out choose))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            continue;
                        }

                      if (choose == 1)
                      {
                        AddPatientDetail(); 
                      }
                      else if (choose == 2)
                      {
                        UpdateDetails();
                      }
                      else if (choose == 3)
                      {
                       DeleteDetails();
                      }
                      else if (choose == 4)
                      {
                        PrintDetails();
                      }
                      else if (choose == 5)
                      {
                        SearchDetails();
                      }
                      else if (choose == 6)
                      {
                        Console.WriteLine("Exit From The Program ");
                        break;
                      }
                      else
                      {
                        Console.WriteLine("You Are Enter Invalid Option Number,Try Again ! ");
                      }
                    }     
                }

                public void AddPatientDetail()
                {
                    Console.WriteLine("\nEnter The Patient Details");

                    PatientDetails patient = new PatientDetails();

                    Console.Write("Name: ");
                    patient.Name = Console.ReadLine();

                    Console.Write("Age: ");
                    patient.Age = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Mobile Number: ");
                    long mobile = Convert.ToInt64(Console.ReadLine());

                    Console.Write("Email: ");
                    string email = Console.ReadLine();

                    if (Duplicate(mobile, email))
                    {
                        Console.WriteLine("This mobile number or email already exists! Patient not added.\n");
                        return;
                    }

                    patient.MobileNumber = mobile;
                    patient.Email = email;

                    patients.Add(patient);
                    SaveData(patients);

                    Console.WriteLine("Patient added successfully.");
                    PrintDetails();
                }

                public void UpdateDetails()
                {
                    Console.Write("\nEnter the Mobile Number of the patient to update: ");
                    long mobile = Convert.ToInt64(Console.ReadLine());

                    var patient = patients.FirstOrDefault(p => p.MobileNumber == mobile);
                    if (patient == null)
                    {
                        Console.WriteLine("Patient not found.");
                        return;
                    }

                    Console.WriteLine("Update Options:");
                    Console.WriteLine("1. Update Name");
                    Console.WriteLine("2. Update Age");
                    Console.Write("Enter the option number: ");
                    int update = Convert.ToInt32(Console.ReadLine());

                    if (update == 1)
                    {
                        Console.Write("Enter new Name: ");
                        patient.Name = Console.ReadLine();
                    }
                    else if (update == 2)
                    {
                        Console.Write("Enter new Age: ");
                        patient.Age = Convert.ToInt32(Console.ReadLine());
                    }
                    else
                    {
                        Console.WriteLine("Invalid option.");
                        return;
                    }

                    SaveData(patients);
                    Console.WriteLine("Patient updated successfully.");
                }

                public void DeleteDetails()
                {
                    Console.Write("\nEnter the Mobile Number of the patient to delete: ");
                    long mobile = Convert.ToInt64(Console.ReadLine());

                    var patient = patients.FirstOrDefault(p => p.MobileNumber == mobile);
                    if (patient == null)
                    {
                        Console.WriteLine("Patient not found.");
                        return;
                    }

                    patients.Remove(patient);
                    SaveData(patients);

                    Console.WriteLine("Patient deleted successfully.");
                }

                public void PrintDetails()
                {
                    if (patients.Count == 0)
                    {
                        Console.WriteLine("\nNo patient details to display.");
                        return;
                    }

                    Console.WriteLine("\nPatient List:");
                    foreach (var p in patients)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine($"Name: {p.Name}");
                        Console.WriteLine($"Age: {p.Age}");
                        Console.WriteLine($"Mobile: {p.MobileNumber}");
                        Console.WriteLine($"Email: {p.Email}");
                    }
                }

                public void SearchDetails()
                {
                    Console.Write("\nEnter the Mobile Number to search: ");
                    long mobile = Convert.ToInt64(Console.ReadLine());

                    var patient = patients.FirstOrDefault(p => p.MobileNumber == mobile);
                    if (patient == null)
                    {
                        Console.WriteLine("No patient found with the given mobile number.");
                        return;
                    }

                    Console.WriteLine("-----------------------------");
                    Console.WriteLine($"Name: {patient.Name}");
                    Console.WriteLine($"Age: {patient.Age}");
                    Console.WriteLine($"Mobile: {patient.MobileNumber}");
                    Console.WriteLine($"Email: {patient.Email}");
                }

                public bool Duplicate(long mobile, string email)
                {
                    return patients.Any(p => p.MobileNumber == mobile || p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                }

                public List<PatientDetails> LoadData()
                {
                    if (!File.Exists(filePath))
                    {
                        return new List<PatientDetails>();
                    }

                    string jsonData = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<PatientDetails>>(jsonData) ?? new List<PatientDetails>();
                }

                public void SaveData(List<PatientDetails> patientList)
                {
                    string jsonData = JsonSerializer.Serialize(patientList, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filePath, jsonData);
                }
            }
        
}
