using Application.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TaskmanagerV2.Domain.Enums;

namespace Shared.Utilities
{
    public static class Util
    {
        private static readonly IConfiguration _configuration;

        // Inject IConfiguration through a static constructor
        static Util()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public static DateTime GetLastDayOfCurrentWeek()
        {
            var currentDay = DateTime.UtcNow.DayOfWeek.ToString();
            var curentDate = DateTime.UtcNow;
            switch (currentDay)
            {
                case "Sunday":
                    curentDate = DateTime.UtcNow.AddDays(6);
                    break;
                case "Monday":
                    curentDate = DateTime.UtcNow.AddDays(5);
                    break;
                case "Tuesday":
                    curentDate = DateTime.UtcNow.AddDays(4);
                    break;
                case "Wednesday":
                    curentDate = DateTime.UtcNow.AddDays(3);
                    break;
                case "Thursday":
                    curentDate = DateTime.UtcNow.AddDays(2);
                    break;
                case "Friday":
                    curentDate = DateTime.UtcNow.AddDays(1);
                    break;
                case "Saturday":
                    curentDate = DateTime.UtcNow;
                    break;
                default:
                    curentDate = DateTime.UtcNow;
                    break;
            }
            return curentDate;
        }

        //  THIS METHOD HELPS TO CHECK IF LIST OF USERTASK OBJECTS CONTAINS A USERTASK OBJECT 
        public static bool IsListContainTask(List<UserTask> userTasks, UserTask task)
        {
            foreach (var item in userTasks)
            {
                if (item.Id == task.Id)
                    return true;
            }
            return false;
        }

        public static bool IsNotificationTypeValid(CreateNotificationRequest notificationType)
        {
            if(notificationType.ToString() == NotificationType.Status_update.ToString() || notificationType.ToString() == NotificationType.Due_date.ToString())
                return true;
            return false;
        }
        public static bool IsInputLetterOnly(string inputString)
        {
            var array = inputString.Split(' ');
            string firstElem = array.First();
            string removedSpace = string.Join(" ", array.Skip(1));
            return removedSpace.All(c => char.IsLetter(c));
        }

        public static bool EmailIsValid(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static string StringHasher(string input)
        {
            return ComputeSha256Hash(input);
        }

        private static string ComputeSha256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static string GetClaimValue(this IEnumerable<Claim> claims, string claimType) => new List<Claim>(claims).Find(c => c.Type == claimType)?.Value;

        public static TokenUserData GetSessionUser(this HttpContext context)
        {
            var data = new TokenUserData();

            string str1;
            if (context == null)
            {
                data = null;
            }
            else
            {
                ClaimsPrincipal user = context.User;
                if (user == null)
                {
                    str1 = null;
                }
                else
                {
                    IEnumerable<Claim> claims = user.Claims;

                    data.UserId = claims != null ? claims.GetClaimValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") : null;
                    data.Email = claims != null ? claims.GetClaimValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress") : null;
                    data.Name = claims != null ? claims.GetClaimValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name") : null;
                }
            }

            return data;
        }


        public static bool IsDateDue(DateTime startTime)
        {
            DateTime endTime = DateTime.UtcNow;

            int thresholdHours = _configuration.GetValue<int>("DueDateMaxHour", 48); // Read the DueDateMaxHour from appSettings.json or use a default value of 48

            int totalHours = startTime.Subtract(endTime).Hours;

            return totalHours < thresholdHours;
        }
    }
}