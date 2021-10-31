﻿using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAdminSolution.Models;

namespace TheAdminSolution.Services
{
    public class DatabaseServices
    {
        #region firebase constants
        private static string auth = "ySbSRzvoXD2Z26pGfePP2PsUe3BWEYDFdbjfoOCm"; //  app secret

        FirebaseClient firebase = new FirebaseClient("https://employee-and-student-solution-default-rtdb.firebaseio.com/",
            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(auth) });
        #endregion


        public async Task<List<string>> GetUserEmail()
        {
            List<string> Emails = new List<string>();
            var allUsers = (await firebase
             .Child("EmployeeSolution")
            .Child("Users")
              .OnceAsync<UserDetails>()).Select(item => new UserDetails
              {
                  EmailAddress = item.Object.EmailAddress,
                  EmailUserID = item.Object.EmailUserID,
                  FullName = item.Object.FullName,
                  PhoneNumber = item.Object.PhoneNumber,
                  Surname = item.Object.Surname
              }).ToList();

            foreach(UserDetails userDetails in allUsers)
            {
                Emails.Add(userDetails.EmailAddress);
            }
            return Emails;

        }

        public async Task<List<EmployeeTask>> GetTasks()
        {
            return (await firebase
             .Child("EmployeeSolution")
            .Child("EmployeeTasks")
              .OnceAsync<EmployeeTask>()).Select(item => new EmployeeTask
              {
                  Email = item.Object.Email,
                  Title = item.Object.Title,
                  Details = item.Object.Details,
                  Status = item.Object.Status,
                  TaskId = item.Object.TaskId
              }).ToList();
        }

        public async Task<bool> AddTask(EmployeeTask employeeTask)
        {
            FirebaseObject<EmployeeTask> response = await firebase
              .Child("EmployeeSolution")
              .Child("EmployeeTasks")
              .PostAsync<EmployeeTask>(new EmployeeTask() { Email = employeeTask.Email, Details = employeeTask.Details, Title = employeeTask.Title, Status = employeeTask.Status, TaskId = employeeTask.TaskId });

            if (!string.IsNullOrEmpty(response.Key) && response.Object != null)
            {
                return true;
            }
            return false;
        }

        public async Task DeleteTask(string taskID)
        {
            var toDeleteTask = (await firebase
             .Child("EmployeeSolution")
             .Child("EmployeeTasks")
             .OnceAsync<EmployeeTask>()).Where(a => a.Object.TaskId == taskID).FirstOrDefault();
           await firebase.Child("EmployeeSolution").Child("EmployeeTasks").Child(toDeleteTask.Key).DeleteAsync();
        }

    }
}
